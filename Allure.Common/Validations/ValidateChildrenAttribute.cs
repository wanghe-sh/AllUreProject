using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Common.Validations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ValidateChildrenAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var result = new List<ValidationResult>();
                var collection = value as IEnumerable;

                if (collection == null)
                {
                    if (!Validator.TryValidateObject(value, new ValidationContext(value), result, true))
                    {
                        return result.First(r => r != ValidationResult.Success);
                    }
                }
                else
                {
                    foreach (var item in collection)
                    {
                        if (!Validator.TryValidateObject(item, new ValidationContext(item), result, true))
                        {
                            return result.First(r => r != ValidationResult.Success);
                        }
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}
