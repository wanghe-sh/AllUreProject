using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Allure.Web.Models
{
    public class UserRegister
    {
        public string Email { get; set; }

        [Display(Name = "Password")]
        public string PlainTextPassword { get; set; }

        [Required]
        [MaxLength(10)]
        public string LastName { get; set; }

        [MaxLength(10)]
        public string FirstName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [MaxLength(20)]
        public string Telephone { get; set; }

        [MaxLength(20)]
        public string Mobile { get; set; }

        [MaxLength(50)]
        public string Address { get; set; }

        [MaxLength(10)]
        public string PostCode { get; set; }

        [MaxLength(20)]
        public string Company { get; set; }
    }
}