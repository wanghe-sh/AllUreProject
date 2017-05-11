using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core.Criteria.StorageOperations;
using Allure.Core.Models;

namespace Allure.Core.Validations.StorageOperations
{    
    class OrderRequiredDependsOnType : RequiredAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var add = validationContext.ObjectInstance as AddStorageOperation;

            if (add == null)
            {
                throw new ValidationException($"Invalid validation context type {validationContext.ObjectInstance.GetType().Name}");
            }

            if (add.Type == StorageOperationType.Out)
            {
                if (value == null)
                {
                    return new ValidationResult(this.ErrorMessage);
                }
                else
                {
                    var details = value as AddStorageOperationDetail[];
                    if (details != null && details.Any(d => !d.OrderDetailId.HasValue))
                    {
                        return new ValidationResult(this.ErrorMessage);
                    }
                }
            }

            return ValidationResult.Success;
        }

        public override bool RequiresValidationContext
        {
            get
            {
                return true;
            }
        }
    }
}
