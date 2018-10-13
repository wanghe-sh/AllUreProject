using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core.Models;

namespace Allure.Data
{
    class AllureDbInitializer : DropCreateDatabaseIfModelChanges<AllureContext>
    {
        protected override void Seed(AllureContext context)
        {
            var random = new Random();

            #region Language

            context.Set<Language>().Add(new Language { Code = "zh-CN", Description = "中文(中华人民共和国)", IsDefault = true, Enabled = true });
            context.Set<Language>().Add(new Language { Code = "en-US", Description = "English (United States)", IsDefault = false, Enabled = true });

            #endregion

            #region Category

            var furnishingCategory = new Category
            {
                Localizations = new List<CategoryLocalization>
                {
                    new CategoryLocalization { LanguageCode = "zh-CN", Name = "家居", Description = "家居的描述" },
                    new CategoryLocalization { LanguageCode = "en-US", Name = "FURNISHINGS", Description = "FURNISHINGS Description" },
                },
                Children = new List<Category>
                {
                    new Category
                    {
                        Localizations = new List<CategoryLocalization>
                        {
                            new CategoryLocalization { LanguageCode = "zh-CN", Name = "写字台", Description = "写字台的描述" },
                            new CategoryLocalization { LanguageCode = "en-US", Name = "Desks", Description = "Desks Description" }
                        }
                    },
                    new Category
                    {
                        Localizations = new List<CategoryLocalization>
                        {
                            new CategoryLocalization { LanguageCode = "zh-CN", Name = "椅子", Description = "椅子的描述" },
                            new CategoryLocalization { LanguageCode = "en-US", Name = "Seating", Description = "Seating Description" }
                        }
                    },
                    new Category
                    {
                        Localizations = new List<CategoryLocalization>
                        {
                            new CategoryLocalization { LanguageCode = "zh-CN", Name = "桌子", Description = "桌子的描述" },
                            new CategoryLocalization { LanguageCode = "en-US", Name = "Tables", Description = "Tables Description" }
                        }
                    },
                    new Category
                    {
                        Localizations = new List<CategoryLocalization>
                        {
                            new CategoryLocalization { LanguageCode = "zh-CN", Name = "灯具", Description = "灯具的描述" },
                            new CategoryLocalization { LanguageCode = "en-US", Name = "Lighting", Description = "Lighting Description" }
                        }
                    }
                }
            };

            //stool
            var stoolCategory = new Category
            {
                Localizations = new List<CategoryLocalization>
                {
                    new CategoryLocalization { LanguageCode = "zh-CN", Name = "凳子", Description = "凳子的描述" },
                    new CategoryLocalization { LanguageCode = "en-US", Name = "STOOL", Description = "STOOL Description" }
                }
            };

            //cabinet
            var cabinetCategory = new Category
            {
                Localizations = new List<CategoryLocalization>
                {
                    new CategoryLocalization { LanguageCode = "zh-CN", Name = "柜子", Description = "柜子的描述" },
                    new CategoryLocalization { LanguageCode = "en-US", Name = "CABINET", Description = "CABINET Description" }
                }
            };

            //case
            var caseCategory = new Category
            {
                Localizations = new List<CategoryLocalization>
                {
                    new CategoryLocalization { LanguageCode = "zh-CN", Name = "案", Description = "案的描述" },
                    new CategoryLocalization { LanguageCode = "en-US", Name = "CASE", Description = "CASE Description" }
                }
            };


            //sTable
            var sTableCategory = new Category
            {
                Localizations = new List<CategoryLocalization>
                {
                    new CategoryLocalization { LanguageCode = "zh-CN", Name = "几", Description = "几的描述" },
                    new CategoryLocalization { LanguageCode = "en-US", Name = "STABLE", Description = "STABLE Description" }
                }
            };

            //Cupboard
            var cupboardCategory = new Category
            {
                Localizations = new List<CategoryLocalization>
                {
                    new CategoryLocalization { LanguageCode = "zh-CN", Name = "橱柜", Description = "橱柜的描述" },
                    new CategoryLocalization { LanguageCode = "en-US", Name = "CUPBOARD", Description = "CUPBOARD Description" }
                }
            };

            var elecCategory = new Category
            {
                Localizations = new List<CategoryLocalization>
                {
                    new CategoryLocalization { LanguageCode = "zh-CN", Name = "电器", Description = "电器的描述" },
                    new CategoryLocalization { LanguageCode = "en-US", Name = "ELECTRICS", Description = "ELECTRICS Description" }
                },
                Children = new List<Category>
                {
                    new Category
                    {
                        Localizations = new List<CategoryLocalization>
                        {
                            new CategoryLocalization { LanguageCode = "zh-CN", Name = "电视机", Description = "电视机的描述" },
                            new CategoryLocalization { LanguageCode = "en-US", Name = "TVs" , Description = "TVs description"}
                        }
                    },
                    new Category
                    {
                        Localizations = new List<CategoryLocalization>
                        {
                            new CategoryLocalization { LanguageCode = "zh-CN", Name = "空调", Description = "空调的描述" }
                        }
                    }
                }
            };

            //refrigerator
            var refrigeratorCategory = new Category
            {
                Localizations = new List<CategoryLocalization>
                {
                    new CategoryLocalization { LanguageCode = "zh-CN", Name = "冰箱", Description = "冰箱的描述" },
                    new CategoryLocalization { LanguageCode = "en-US", Name = "REFRIGERATOR", Description = "REFRIGERATOR Description" }
                }
            };

            //stove
            var stoveCategory = new Category
            {
                Localizations = new List<CategoryLocalization>
                {
                    new CategoryLocalization { LanguageCode = "zh-CN", Name = "烤灶", Description = "烤灶的描述" },
                    new CategoryLocalization { LanguageCode = "en-US", Name = "STOVE", Description = "STOVE Description" }
                }
            };

            //Coffeemachine
            var coffeeMachineCategory = new Category
            {
                Localizations = new List<CategoryLocalization>
                {
                    new CategoryLocalization { LanguageCode = "zh-CN", Name = "咖啡机", Description = "咖啡机的描述" },
                    new CategoryLocalization { LanguageCode = "en-US", Name = "COFFEE MACHINE", Description = "COFFEE MACHINE Description" }
                }
            };



            context.Set<Category>().Add(furnishingCategory);
            context.Set<Category>().Add(stoolCategory);
            context.Set<Category>().Add(cabinetCategory);
            context.Set<Category>().Add(caseCategory);
            context.Set<Category>().Add(sTableCategory);
            context.Set<Category>().Add(cupboardCategory);
            context.Set<Category>().Add(elecCategory);
            context.Set<Category>().Add(refrigeratorCategory);
            context.Set<Category>().Add(stoveCategory);
            context.Set<Category>().Add(coffeeMachineCategory);

            #endregion

            #region Brand

            context.Set<Brand>()
                .AddRange(Enumerable
                    .Range(0, 10)
                    .Select(n => new Brand
                    {
                        Name = $"测试品牌 {n.ToString()}"
                    }));

            #endregion

            #region CheckAddress

            context.Set<CheckAddress>()
                .AddRange(Enumerable
                    .Range(0, 1)
                    .Select(n => new CheckAddress
                    {
                        Address = $"验货地址 {n.ToString()}"
                    }));

            #endregion

            #region Locale

            context.Set<Locale>()
                .AddRange(Enumerable
                    .Range(0, 10)
                    .Select(n => new Locale
                    {
                        Localizations = new List<LocaleLocalization>
                        {
                            new LocaleLocalization { LanguageCode = "zh-CN", Name = "测试地区 " + n.ToString() },
                            new LocaleLocalization { LanguageCode = "en-US", Name = "Test Locale "+ n.ToString() }
                        }
                    }));

            #endregion

            #region Logistic

            var logistics = new List<Logistic>
            {
                new Logistic { Name = "物流A" },
                new Logistic { Name = "物流B" }
            };
            context.Set<Logistic>().AddRange(logistics);

            #endregion

            #region Warehouse

            var warehouses = new List<Warehouse>
            {
                new Warehouse { Name = "仓库1" },
                new Warehouse { Name = "仓库2" }
            };
            context.Set<Warehouse>().AddRange(warehouses);

            #endregion

            #region User

            var users = Enumerable.Range(0, 20).Select(n => new User
            {
                FirstName = $"first{n.ToString()}",
                LastName = $"last{n.ToString()}",
                Email = $"test{n.ToString()}@test.com",
                Password = $"password{n.ToString()}",
                Status = UserStatus.Normal,
                Company = $"company{n.ToString()}",
                Roles = new[] { new UserRole { Role = Role.Customer } },
                Deliveries = Enumerable.Range(0, 3).Select(m => new UserDelivery
                {
                    Address = $"test address{n.ToString()}{m.ToString()}",
                    PostCode = $"postcode{n.ToString()}{m.ToString()}",
                    Phone = $"phone{n.ToString()}{m.ToString()}",
                    Receiver = $"receiver{n.ToString()}{m.ToString()}"
                }).ToArray()
            }).ToList();

            users.Insert(0, new User
            {
                FirstName = "admin",
                LastName = "admin",
                Email = "admin@allure.com",
                Password = "admin",
                Status = UserStatus.Normal,
                Company = "allure",
                Roles = new[] { new UserRole {  Role = Role.SystemAdmin } }
            });

            context.Set<User>().AddRange(users);

            #endregion

            context.SaveChanges();

            #region Product

            var products = new List<Product>();

            foreach (var category in new[] { furnishingCategory, elecCategory })
            {
                foreach (var sub in category.Children)
                {
                    //
                    for (int i = 0; i < 4; i++)
                    {
                        var product = new Product
                        {
                            Name = sub.Localizations.First().Name + i.ToString(),
                            BrandId = random.Next(1, 10),
                            LocaleId = random.Next(1, 10),
                            CategoryId = sub.Id,
                            Number = "PN" + sub.Id.ToString() + i.ToString(),
                            Price = random.Next(10, 1000),
                            CreateTime = DateTime.Now,
                            OriginalPrice = 5000,
                            Width = "100x",
                            Height = "100cm",
                            Length = "100x",
                            EnglishName = "Desk" + i.ToString(),
                            RecommendedL=true,
                            Localizations = new List<ProductLocalization>
                            {
                                new ProductLocalization { LanguageCode = "zh-CN", Description = sub.Localizations.First().Name + i.ToString() + "的描述",Comment = sub.Localizations.First().Name + i.ToString() + "的备注", DeliveryTerm = sub.Localizations.First().Name + i.ToString() + "交货期"},
                                new ProductLocalization { LanguageCode = "en-US", Description = sub.Localizations.First().Name + i.ToString() + "Description" ,Comment = sub.Localizations.First().Name + i.ToString() + "Comment",DeliveryTerm = sub.Localizations.First().Name + i.ToString() + "Delivery Term"},
                            },
                            Images = new List<Image>
                            {
                                new Image { Url = "/public/images/banner01.jpg", ThumbnailUrl = "/public/images/banner01.jpg", Width = 1000, Height = 500 },
                                new Image { Url = "/public/NewHtml/images/pro_img11.jpg", ThumbnailUrl = "/public/NewHtml/images/pro_img11.jpg", Width = 140, Height = 105 },
                                new Image { Url = "/public/NewHtml/images/pro_img12.jpg", ThumbnailUrl = "/public/NewHtml/images/pro_img12.jpg", Width = 140, Height = 105 },
                                new Image { Url = "/public/NewHtml/images/pro_img13.jpg", ThumbnailUrl = "/public/NewHtml/images/pro_img13.jpg", Width = 140, Height = 105 }
                            }
                        };

                        products.Add(product);
                    }

                    //
                    for (int i = 4; i < 10; i++)
                    {
                        var product = new Product
                        {
                            Name = sub.Localizations.First().Name + i.ToString(),
                            BrandId = random.Next(1, 10),
                            LocaleId = random.Next(1, 10),
                            CategoryId = sub.Id,
                            Number = "PN" + sub.Id.ToString() + i.ToString(),
                            Price = random.Next(10, 1000),
                            CreateTime = DateTime.Now,
                            OriginalPrice = 5000,
                            Width = "100x",
                            Height = "100cm",
                            Length = "100x",
                            EnglishName = "Desk" + i.ToString(),
                            RecommendedS=true,
                            Localizations = new List<ProductLocalization>
                            {
                                new ProductLocalization { LanguageCode = "zh-CN", Description = sub.Localizations.First().Name + i.ToString() + "的描述",Comment = sub.Localizations.First().Name + i.ToString() + "的备注", DeliveryTerm = sub.Localizations.First().Name + i.ToString() + "交货期"},
                                new ProductLocalization { LanguageCode = "en-US", Description = sub.Localizations.First().Name + i.ToString() + "Description" ,Comment = sub.Localizations.First().Name + i.ToString() + "Comment",DeliveryTerm = sub.Localizations.First().Name + i.ToString() + "Delivery Term"},
                            },
                            Images = new List<Image>
                            {
                                new Image { Url = "/public/NewHtml/images/design_img01.jpg", ThumbnailUrl = "/public/NewHtml/images/design_img01.jpg", Width = 140, Height = 105 },
                                new Image { Url = "/public/NewHtml/images/design_img02.jpg", ThumbnailUrl = "/public/NewHtml/images/design_img02.jpg", Width = 140, Height = 105 },
                                new Image { Url = "/public/NewHtml/images/design_img03.jpg", ThumbnailUrl = "/public/NewHtml/images/design_img03.jpg", Width = 140, Height = 105 },
                                new Image { Url = "/public/NewHtml/images/design_img04.jpg", ThumbnailUrl = "/public/NewHtml/images/design_img04.jpg", Width = 140, Height = 105 }
                            }
                        };

                        products.Add(product);
                    }
                }
            }

            context.Set<Product>().AddRange(products);

            #endregion

            context.SaveChanges();

            #region Order

            var orders = Enumerable.Range(0, 3)
                .Select(n =>
                {
                    var customer = users[random.Next(users.Count)];
                    var deliveries = customer.Deliveries.ToList();
                    var delivery = deliveries[random.Next(deliveries.Count)];

                    return new Order
                    {
                        OrderNO = DateTime.Today.ToString("yyyyMMdd") + n.ToString().PadLeft(3,'0'),
                        CustomerId = customer.Id,
                        DeliveryId = delivery.Id,
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now,                        
                        Status = OrderStatus.ToBeContact,
                        Details = Enumerable.Range(0, random.Next(1, 2))
                            .Select(m => new OrderDetail
                            {
                                ProductId = products[random.Next(products.Count)].Id,
                                Count = random.Next(1, 5)
                            }).ToList()
                    };
                }).ToList();

            context.Set<Order>().AddRange(orders);

            #endregion

            context.SaveChanges();

            #region Operation Storage

            var storageOperations = products
                .OrderBy(p => Guid.NewGuid())
                .Take(random.Next(1, products.Count))
                .Select((p, n) => new StorageOperation
                {
                    Type = StorageOperationType.In,
                    LogisticId = logistics[random.Next(logistics.Count)].Id,
                    LogisticNumber = $"logistic number {n.ToString()}",
                    WarehouseId = warehouses[random.Next(warehouses.Count)].Id,
                    CreatorId = users[random.Next(users.Count)].Id,
                    CreateTime = DateTime.Now.AddHours(random.Next(-100, -50)),
                    UpdaterId = users[random.Next(users.Count)].Id,
                    UpdateTime = DateTime.Now.AddHours(random.Next(-50, 0)),
                    Comment = $"intialized comment {n.ToString()}",
                    Details = new List<StorageOperationDetail>
                    {
                        new StorageOperationDetail { ProductId = p.Id, OriginalCount = 0, OperationCount = n }
                    }
                });

            context.Set<StorageOperation>().AddRange(storageOperations);

            #endregion

            #region Subscribed Mail

            context.Set<SubscribedMail>()
                .AddRange(Enumerable
                    .Range(0, 10)
                    .Select(n => new SubscribedMail
                    {
                        Mail = $"test{n.ToString()}@test.com",
                        CreateTime = DateTime.Now,
                        IsValid = true
                    }));

            #endregion


        }
    }
}