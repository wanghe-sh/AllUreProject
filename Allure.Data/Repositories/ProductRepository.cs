using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Common;
using Allure.Common.Unity;
using Allure.Core.Criteria;
using Allure.Core.Criteria.Products;
using Allure.Core.Exceptions;
using Allure.Core.Models;
using Allure.Core.Repositories;
using Allure.Common.Validations;
using AutoMapper;

namespace Allure.Data.Repositories
{
    [Autowire(typeof(IProductRepository), Lifetime.PerResolve)]
    class ProductRepository : IProductRepository
    {
        private readonly IDbContext _dbContext;

        public ProductRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> Add(AddProduct add)
        {
            var product = Mapper.Map<Product>(add);
            product.CreateTime = DateTime.Now;

            if (add.ImageIds?.Length > 0)
            {
                product.Images = await _dbContext
                    .Set<Image>()
                    .Where(i => add.ImageIds.Contains(i.Id))
                    .ToListAsync()
                    .ConfigureAwait(false);
            }

            _dbContext.Set<Product>().Add(product);
            return product;
        }

        public async Task Delete(int id)
        {
            var product = await this.Get(id).ConfigureAwait(false);
            if (product == null)
            {
                throw new NotFoundException<Product>($"{nameof(Product.Id)}={id.ToString()}");
            }

            product.Deleted = true;
        }

        public async Task<Product> Get(int id)
        {
            return await _dbContext
                .Set<Product>()
                .SingleOrDefaultAsync(p => p.Id == id && !p.Deleted)
                .ConfigureAwait(false);
        }

        public async Task<Product> Get(string number)
        {
            return await _dbContext
                .Set<Product>()
                .SingleOrDefaultAsync(p => p.Number == number && !p.Deleted)
                .ConfigureAwait(false);
        }

        public async Task<SearchResult<Product>> Search(SearchProduct search)
        {
            var query = _dbContext
                .Set<Product>()
                .Where(p => !p.Deleted);

            if ((search?.BrandId).HasValue)
            {
                query = query.Where(p => p.BrandId == search.BrandId.Value);
            }

            if ((search?.ParentCategoryId).HasValue)
            {
                query = query.Where(p => p.Category.ParentId == search.ParentCategoryId.Value);
            }

            if ((search?.CategoryId).HasValue)
            {
                query = query.Where(p => p.CategoryId == search.CategoryId.Value);
            }

            if (!(search?.Number).IsNullOrEmpty())
            {
                query = query.Where(p => p.Number.Contains(search.Number));
            }

            if (!(search?.Locale).IsNullOrEmpty())
            {
                query = query.Where(p => p.Locale.Localizations.Any(l => l.Name.Contains(search.Locale)));
            }

            if (!(search?.Name).IsNullOrEmpty())
            {
                query = query.Where(p => p.Name.Contains(search.Name));
            }

            if ((search?.MaxPrice).HasValue)
            {
                query = query.Where(p => p.Price <= search.MaxPrice.Value);
            }

            if ((search?.MinPrice).HasValue)
            {
                query = query.Where(p => p.Price >= search.MinPrice.Value);
            }

            var total = await query.CountAsync().ConfigureAwait(false);

            var orderBy = nameof(Product.DisplayOrder);

            if ((search?.OrderBy).HasValue)
            {
                orderBy = $"{search.OrderBy} {(search.Descending ? Sort.Descending : Sort.Ascending)}, {orderBy}";
            }

            var pageSize = (search?.PageSize).GetValueOrDefault(10);
            var pageNumber = (search?.PageNumber).GetValueOrDefault(1) - 1;

            var items = await query
                .OrderBy(orderBy)
                .Skip<Product>(pageNumber * pageSize)
                .Take<Product>(pageSize)
                .ToListAsync()
                .ConfigureAwait(false);

            return new SearchResult<Product> { Total = total, Items = items };
        }

        public async Task Update(int id, UpdateProduct update)
        {
            var product = await this.Get(id).ConfigureAwait(false);
            if (product == null)
            {
                throw new NotFoundException<Product>($"{nameof(Product.Id)}={id.ToString()}");
            }

            Mapper.Map(update, product);

            var imageIds = update.ImageIds ?? new int[0];
            var images = new Dictionary<int, Image>();

            if (imageIds.Length > 0)
            {
                images = await _dbContext
                    .Set<Image>()
                    .Where(i => imageIds.Contains(i.Id))
                    .ToDictionaryAsync(i => i.Id)
                    .ConfigureAwait(false);
            }

            var added = imageIds.Except(product.Images.Select(i => i.Id));

            foreach (var image in product.Images.ToArray())
            {
                if (!images.ContainsKey(image.Id))
                {
                    _dbContext.Entry(image).State = EntityState.Deleted;
                }
            }

            foreach (var imgId in added)
            {
                product.Images.Add(images[imgId]);
            }
        }
    }
}
