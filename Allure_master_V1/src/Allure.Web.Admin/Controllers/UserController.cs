using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Allure.Common;
using Allure.Data;
using RefactorThis.GraphDiff;
using System.Data.Entity;
using Allure.Admin.Models;

namespace Allure.Admin.Controllers
{
    public class UserController : AdminControllerBase
    {
        [HttpPost]
        public SearchUserOutput Search(SearchUserInput input)
        {
            using (var dbContext = new AllureContext())
            {
                IQueryable<User> query = dbContext
                    .Set<User>()
                    .Include(u => u.Roles)
                    .Include(u => u.Deliveries);

                if (!input.Email.IsNullOrEmpty())
                {
                    query = query.Where(u => u.Email == input.Email);
                }

                if (input.Gender.HasValue)
                {
                    query = query.Where(u => u.Gender == input.Gender.Value);
                }

                if (!input.Telephone.IsNullOrEmpty())
                {
                    query = query.Where(u => u.Telephone == input.Telephone);
                }

                if (!input.Mobile.IsNullOrEmpty())
                {
                    query = query.Where(u => u.Mobile == input.Mobile);
                }

                if (!input.LastName.IsNullOrEmpty())
                {
                    query = query.Where(u => u.LastName == input.LastName);
                }

                if (!input.Company.IsNullOrEmpty())
                {
                    query = query.Where(u => u.Company.Contains(input.Company));
                }

                if (input.Status.HasValue)
                {
                    query = query.Where(u => u.Status == input.Status);
                }

                if (input.Roles != null && input.Roles.Length > 0)
                {
                    query = query.Where(u => input.Roles.All(r => u.Roles.Any(ur => ur.Role == r)));
                }

                var result = new SearchUserOutput();
                result.Count = query.Count();

                var pageSize = input.PageSize.GetValueOrDefault(10);
                var pageNumber = input.PageNumber.GetValueOrDefault(1) - 1;

                result.Users = query
                    .OrderBy(u => u.Id)
                    .Skip(pageNumber * pageSize)
                    .Take(pageSize)
                    .ToList()
                    .Select(u => new UserOutput(u))
                    .ToArray();

                return result;
            }
        }

        [HttpGet]
        public UserOutput Id(int id)
        {
            using (var dbContext = new AllureContext())
            {
                var user = dbContext
                    .Set<User>()
                    .Include(u => u.Roles)
                    .Include(u => u.Deliveries)
                    .SingleOrDefault(u => u.Id == id);

                if (user == null)
                {
                    throw new HttpException(404, string.Format("user {0} doesn't exist", id.ToString()));
                }

                return new UserOutput(user);
            }
        }

        [HttpPost]
        public UserOutput Create(CreateUserInput input)
        {
            var user = new User
            {
                Email = input.Email,
                Gender = input.Gender,
                FirstName = input.FirstName,
                LastName = input.LastName,
                Telephone = input.Telephone,
                Mobile = input.Mobile,
                Company = input.Company,
                Roles = input.Roles.Select(r => new UserRole { Role = r }).ToList(),
                Deliveries = input.Deliveries.Select(d => new Delivery { Address = d.Address, PostCode = d.PostCode, Receiver = d.Receiver, Phone = d.Phone }).ToList(),
                Status = UserStatus.Unactivated
            };

            using (var dbContext = new AllureContext())
            {
                dbContext.Set<User>().Add(user);
                dbContext.SaveChanges();
            }

            return Id(user.Id);
        }

        [HttpPost]
        public UserOutput Update(UpdateUserInput input)
        {
            var user = new User
            {
                Id = input.UserId,
                Email = input.Email,
                Gender = input.Gender,
                FirstName = input.FirstName,
                LastName = input.LastName,
                Telephone = input.Telephone,
                Mobile = input.Mobile,
                Company = input.Company,
                Status = input.Status,
                Roles = input.Roles.Select(r => new UserRole { UserId = input.UserId, Role = r }).ToList(),
                Deliveries = input.Deliveries
                    .Select(d => new Delivery
                    {
                        Id = d.Id,
                        UserId = input.UserId,
                        Address = d.Address,
                        Phone = d.Phone,
                        Receiver = d.Receiver,
                        PostCode = d.PostCode
                    }).ToList(),
            };

            using (var dbContext = new AllureContext())
            {
                dbContext.UpdateGraph(user, u => u.OwnedCollection(uu => uu.Roles).OwnedCollection(uu => uu.Deliveries));
                dbContext.SaveChanges();
            }

            return Id(user.Id);
        }

        [HttpPost]
        public void Delete(int id)
        {
            using (var dbContext = new AllureContext())
            {
                var user = dbContext.Set<User>().SingleOrDefault(u => u.Id == id);

                if (user == null)
                {
                    throw new HttpException(404, string.Format("user {0} doesn't exist", id.ToString()));
                }

                dbContext.Set<UserRole>().RemoveRange(user.Roles);
                dbContext.Set<Delivery>().RemoveRange(user.Deliveries);
                dbContext.Entry(user).State = EntityState.Deleted;
                dbContext.SaveChanges();
            }
        }
    }
}
