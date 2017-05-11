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
    public class AddUser
    {
        [Required(AllowEmptyStrings = false)]
        [MaxLength(Config.MaxLength.User.Email)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public Gender? Gender { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(Config.MaxLength.User.LastName)]
        public string LastName { get; set; }

        [MaxLength(Config.MaxLength.User.FirstName)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [RegularExpression(Config.User.PasswordRegExp)]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(Config.MaxLength.User.Company)]
        public string Company { get; set; }

        [Required]
        [MinLength(1)]
        public Role[] Roles { get; set; }
    }
}
