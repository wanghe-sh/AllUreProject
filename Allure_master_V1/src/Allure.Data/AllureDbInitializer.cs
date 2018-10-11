using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Data
{
    class AllureDbInitializer : DropCreateDatabaseIfModelChanges<AllureContext>
    {
        protected override void Seed(AllureContext context)
        {
            #region Language

            context.Set<Language>().Add(new Language { Code = "zh-CN", Enabled = true, IsDefault = true });
            context.Set<Language>().Add(new Language { Code = "en-US", Enabled = true, IsDefault = false });

            #endregion

            #region User

            var user = new User
            {
                FirstName = "first",
                LastName = "last",
                Email = "test@test.com",
                Password = "password",
                Status = UserStatus.Normal,
                Company = "company",
                Roles = new[] { new UserRole { Role = Role.Customer } },
                Deliveries = new[] { new Delivery { Address = "test address", PostCode = "postcode" } }
            };

            context.Set<User>().Add(user);
            context.SaveChanges();

            #endregion

            #region Category

            var furnishingCategory = new Category
            {
                Id = 1,
                Localized = new List<LocalizedCategory>
                {
                    new LocalizedCategory { CategoryId = 1, LanguageCode = "zh-CN", Name = "家居", Description = "家居的描述" },
                    new LocalizedCategory { CategoryId = 1, LanguageCode = "en-US", Name = "FURNISHINGS", Description = "FURNISHINGS Description" },
                }
            };

            var elecCategory = new Category
            {
                Id = 2,
                Localized = new List<LocalizedCategory>
                {
                    new LocalizedCategory { CategoryId = 2, LanguageCode = "zh-CN", Name = "电器", Description = "电器的描述" },
                    new LocalizedCategory { CategoryId = 2, LanguageCode = "zh-CN", Name = "ELECTRICS", Description = "ELECTRICS Description" }
                }
            };

            context.Set<Category>().Add(furnishingCategory);
            context.SaveChanges();
            context.Set<Category>().Add(elecCategory);
            context.SaveChanges();

            #endregion

            #region Subcategories

            var furnishingSubCategories = new SubCategory[]
            {                        
                new SubCategory
                {
                    ParentId = furnishingCategory.Id,
                    Localized = new List<LocalizedSubCategory>
                    {
                        new LocalizedSubCategory { SubCategoryId = 11, LanguageCode = "zh-CN", Name = "写字台", Description = "写字台的描述" },
                        new LocalizedSubCategory { SubCategoryId = 11, LanguageCode = "en-US", Name = "Desks", Description = "Desks Description" }
                    }
                },
                new SubCategory
                {
                    ParentId = furnishingCategory.Id,
                    Localized = new List<LocalizedSubCategory>
                    {
                        new LocalizedSubCategory { SubCategoryId = 12, LanguageCode = "zh-CN", Name = "椅子", Description = "椅子的描述" },
                        new LocalizedSubCategory { SubCategoryId = 12, LanguageCode = "en-US", Name = "Seating", Description = "Seating Description" }
                    }
                },
                new SubCategory
                {
                    ParentId = furnishingCategory.Id,
                    Localized = new List<LocalizedSubCategory>
                    {
                        new LocalizedSubCategory { SubCategoryId = 13, LanguageCode = "zh-CN", Name = "桌子", Description = "桌子的描述" },
                        new LocalizedSubCategory { SubCategoryId = 13, LanguageCode = "en-US", Name = "Tables", Description = "Tables Description" }
                    }
                },
                new SubCategory
                {
                    ParentId = furnishingCategory.Id,
                    Localized = new List<LocalizedSubCategory>
                    {
                        new LocalizedSubCategory { SubCategoryId = 14, LanguageCode = "zh-CN", Name = "灯具", Description = "灯具的描述" },
                        new LocalizedSubCategory { SubCategoryId = 14, LanguageCode = "en-US", Name = "Lighting", Description = "Lighting Description" }
                    }
                }
            };

            foreach (var sub in furnishingSubCategories)
            {
                context.Set<SubCategory>().Add(sub);
                context.SaveChanges();
            }

            var elecSubCategories = new SubCategory[]
            {
                new SubCategory
                {
                    ParentId = elecCategory.Id,
                    Localized = new List<LocalizedSubCategory>
                    {
                        new LocalizedSubCategory { SubCategoryId = 21, LanguageCode = "zh-CN", Name = "电视机", Description = "电视机的描述" },
                        new LocalizedSubCategory { SubCategoryId = 21, LanguageCode = "en-US", Name = "TVs" , Description = "TVs description"}
                    }
                },
                new SubCategory
                {
                    ParentId = elecCategory.Id,
                    Localized = new List<LocalizedSubCategory>
                    {
                        new LocalizedSubCategory { SubCategoryId = 22, LanguageCode = "zh-CN", Name = "空调", Description = "空调的描述" }
                    }
                }
            };

            foreach (var sub in elecSubCategories)
            {
                context.Set<SubCategory>().Add(sub);
                context.SaveChanges();
            }

            #endregion

            #region Brand

            for (int i = 0; i < 10; i++)
            {
                var brand = new Brand { Id = i, Name = "测试品牌 " + i.ToString() };
                context.Set<Brand>().Add(brand);
            }
            context.SaveChanges();

            #endregion

            #region Locale

            for (int i = 0; i < 10; i++)
            {
                var locale = new Locale
                {
                    Id = i,
                    Localized = new[]
                    {
                        new LocalizedLocale { LanguageCode = "zh-CN", Name = "测试地区 " + i.ToString() },
                        new LocalizedLocale { LanguageCode = "en-US", Name = "Test Locale "+ i.ToString() },
                    }
                };
                context.Set<Locale>().Add(locale);
                context.SaveChanges();
            }

            #endregion

            #region Product

            foreach (var category in new[] { furnishingCategory, elecCategory })
            {
                foreach (var sub in category.Children)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        var random = new Random();
                        var product = new Product
                        {
                            Id = i,
                            Name = sub.Localized.First().Name + i.ToString(),
                            BrandId = random.Next(1, 10),
                            LocaleId = random.Next(1, 10),
                            Number = "Product No." + sub.Id.ToString() + i.ToString(),
                            Price = random.Next(10, 1000),
                            SubCategoryId = sub.Id,
                            CreateDate = DateTime.Now,
                            Localized = new List<LocalizedProduct>
                            {
                                new LocalizedProduct { Id = i, LanguageCode = "zh-CN", Description = sub.Localized.First().Name + i.ToString() + "的描述" },
                                new LocalizedProduct { Id = i, LanguageCode = "en-US", Description = sub.Localized.First().Name + i.ToString() + "Description" },
                            },
                            Images = new List<ProductImage>
                            {
                                new ProductImage { ProductId = i, ImageUrl = "/static/imgs/detail-big-1.jpg" },
                                new ProductImage { ProductId = i, ImageUrl = "/static/imgs/detail-big-1.jpg" },
                                new ProductImage { ProductId = i, ImageUrl = "/static/imgs/detail-big-1.jpg" }
                            }
                        };

                        context.Set<Product>().Add(product);
                        context.SaveChanges();
                    }
                }
            }

            #endregion
        }
    }
}
