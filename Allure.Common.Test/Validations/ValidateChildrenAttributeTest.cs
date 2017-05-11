using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Common.Validations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Allure.Common.Test.Validations
{
    [TestClass]
    public class ValidateChildrenAttributeTest
    {
        class Person
        {
            [ValidateChildren]
            public Address Address { get; set; }
        }

        class Address
        {
            [ValidateChildren]
            public City City { get; set; }
        }

        class City
        {
            [Required]
            public string Name { get; set; }
        }

        [TestMethod]
        public void NullValueIsValid()
        {            
            var attr = new ValidateChildrenAttribute();
            var person = new Person();
            var result = attr.GetValidationResult(person, new ValidationContext(person));
            Assert.AreSame(ValidationResult.Success, result);
            
            person = new Person { Address = new Address() };
            result = attr.GetValidationResult(person, new ValidationContext(person));
            Assert.AreSame(ValidationResult.Success, result);
        }

        [TestMethod]
        public void ValidateChildren()
        {
            var person = new Person
            {
                Address = new Address
                {
                    City = new City
                    {
                        Name = "New York"
                    }
                }
            };
            var attr = new ValidateChildrenAttribute();
            var result = attr.GetValidationResult(person, new ValidationContext(person));
            Assert.AreSame(ValidationResult.Success, result);

            person.Address.City.Name = null;
            result = attr.GetValidationResult(person, new ValidationContext(person));
            Assert.AreNotSame(ValidationResult.Success, result);
            Assert.IsFalse(string.IsNullOrEmpty(result.ErrorMessage));
        }
    }
}
