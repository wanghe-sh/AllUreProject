using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core.Models;
using Allure.Common.Validations;

namespace Allure.Core.Criteria.Users
{
    public class UpdateUser
    {
        [Required]
        public Gender? Gender { get; set; }

        [Required]
        [MaxLength(Config.MaxLength.User.LastName)]
        public string LastName { get; set; }

        [MaxLength(Config.MaxLength.User.FirstName)]
        public string FirstName { get; set; }

        [MaxLength(Config.MaxLength.User.Telephone)]
        public string Telephone { get; set; }

        [MaxLength(Config.MaxLength.User.Mobile)]
        public string Mobile { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(Config.MaxLength.User.Company)]
        public string Company { get; set; }

        [Required]
        public UserStatus? Status { get; set; }

        [Required]
        [MinLength(1)]
        public Role[] Roles { get; set; }
                
        [ValidateChildren]
        public UpdateUserDelivery[] Deliveries { get; set; }
    }

    public class UpdateUserDelivery
    {
        public int? Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(Config.MaxLength.User.Delivery.Address)]
        public string Address { get; set; }

        [MaxLength(Config.MaxLength.User.Delivery.PostCode)]
        public string PostCode { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(Config.MaxLength.User.Delivery.Receiver)]
        public string Receiver { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(Config.MaxLength.User.Delivery.Phone)]
        public string Phone { get; set; }
    }
}
