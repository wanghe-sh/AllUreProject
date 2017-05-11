using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Common;
using Allure.Common.Unity;
using Allure.Core.Criteria;
using Allure.Core.Criteria.Users;
using Allure.Core.Exceptions;
using Allure.Core.Models;
using Allure.Core.Repositories;
using Allure.Common.Validations;
using AutoMapper;

namespace Allure.Data.Repositories
{
    [Autowire(typeof(IUserRepository), Lifetime.PerResolve)]
    class UserRepository : IUserRepository
    {
        private readonly IDbContext _dbContext;

        public UserRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User Add(AddUser add)
        {
            var user = Mapper.Map<User>(add);
            _dbContext.Set<User>().Add(user);
            return user;
        }

        public async Task<User> Get(int id)
        {
            return await _dbContext
                .Set<User>()
                .SingleOrDefaultAsync(u => u.Id == id)
                .ConfigureAwait(false);
        }

        public async Task<SearchResult<User>> Search(SearchUser search)
        {
            IQueryable<User> query = _dbContext.Set<User>();

            if (!(search?.Address).IsNullOrEmpty())
            {
                query = query.Where(u => u.Address.Contains(search.Address));
            }

            if (!(search?.Company).IsNullOrEmpty())
            {
                query = query.Where(u => u.Company.Contains(search.Company));
            }

            if (!(search?.Email).IsNullOrEmpty())
            {
                query = query.Where(u => u.Email.Contains(search.Email));
            }

            if (!(search?.Mobile).IsNullOrEmpty())
            {
                query = query.Where(u => u.Mobile.Contains(search.Mobile));
            }

            if (!(search?.Name).IsNullOrEmpty())
            {
                query = query.Where(u => (u.FirstName + u.LastName + u.FirstName).Contains(search.Name));
            }

            if (!(search?.Telephone).IsNullOrEmpty())
            {
                query = query.Where(u => u.Telephone.Contains(search.Telephone));
            }

            if ((search?.Status).HasValue)
            {
                query = query.Where(u => u.Status == search.Status.Value);
            }

            if (search?.Roles != null)
            {
                foreach (var role in search.Roles)
                {
                    query = query.Where(u => u.Roles.Any(r => r.Role == role));
                }
            }

            var total = await query.CountAsync().ConfigureAwait(false);

            var orderBy = nameof(User.Id);

            if ((search?.OrderBy).HasValue)
            {
                orderBy = $"{search.OrderBy.ToString()} {(search.Descending ? Sort.Descending : Sort.Ascending)}, {orderBy}";
            }

            var pageSize = (search?.PageSize).GetValueOrDefault(10);
            var pageNumber = (search?.PageNumber).GetValueOrDefault(1) - 1;

            var items = await query
                .OrderBy(orderBy)
                .Skip<User>(pageNumber * pageSize)
                .Take<User>(pageSize)
                .ToListAsync()
                .ConfigureAwait(false);

            return new SearchResult<User> { Total = total, Items = items };
        }

        public async Task Update(int id, UpdateUser update)
        {
            var user = await this.Get(id).ConfigureAwait(false);

            if (user == null)
            {
                throw new NotFoundException<User>($"{nameof(User.Id)}={id.ToString()}");
            }

            Mapper.Map(update, user);

            user.Roles.Clear();
            foreach (var role in update.Roles)
            {
                user.Roles.Add(new UserRole { UserId = user.Id, Role = role });
            }

            var updateDeliveries = update.Deliveries.Where(d => d.Id.HasValue).ToDictionary(d => d.Id);

            foreach (var existingDelivery in user.Deliveries.ToArray())
            {
                if (updateDeliveries.ContainsKey(existingDelivery.Id))
                {
                    _dbContext.Entry(existingDelivery).CurrentValues.SetValues(updateDeliveries[existingDelivery.Id]);
                }
                else
                {
                    _dbContext.Entry(existingDelivery).State = EntityState.Deleted;
                }
            }

            foreach (var newDelivery in update.Deliveries.Where(d => !d.Id.HasValue))
            {
                user.Deliveries.Add(Mapper.Map<UserDelivery>(newDelivery));
            }
        }
    }
}
