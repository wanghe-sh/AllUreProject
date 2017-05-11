using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core
{
    public class Config
    {
        public static class User
        {
            public const string PasswordRegExp = @"(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,20}$";
        }

        public static class MaxLength
        {
            public static class Language
            {
                public const int Code = 20;
                public const int Description = 100;
            }

            public static class Brand
            {
                public const int Name = 20;
            }

            public static class CheckAddress
            {
                public const int Address = 100;
            }

            public static class Logistic
            {
                public const int Name = 100;
            }

            public static class Warehouse
            {
                public const int Name = 100;
            }

            public static class User
            {
                public const int LastName = 20;
                public const int FirstName = 20;
                public const int Email = 50;
                public const int Telephone = 20;
                public const int Mobile = 20;
                public const int Address = 100;
                public const int PostCode = 20;
                public const int Company = 50;

                public static class Delivery
                {
                    public const int Address = 100;
                    public const int PostCode = 20;
                    public const int Receiver = 20;
                    public const int Phone = 20;
                }
            }

            public static class Product
            {
                public const int Name = 20;
                public const int Number = 20;
            }

            public static class Order
            {
                public const int CheckAddress = 100;
                public const int CheckContact = 20;
                public const int LogisticOrderNumber = 20;
                public const int OrderNO = 11;
            }

            public static class Image
            {
                public const int Url = 100;
                public const int ThumbnailUrl = 100;
            }

            public static class StorageOperation
            {
                public const int PurchaseOrderNumber = 20;
            }
        }
    }
}
