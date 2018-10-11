using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Models
{
    public class User
    {
        public User()
        {
            this.Roles = new List<UserRole>();
            this.Deliveries = new List<Delivery>();
        }

        public int Id { get; set; }
        
        public string Password { get; set; }
      
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public Gender Gender { get; set; }

        public string Email { get; set; }

        public string Telephone { get; set; }

        public string Mobile { get; set; }

        public string Address { get; set; }

        public string PostCode { get; set; }

        public string Company { get; set; }

        public UserStatus Status { get; set; }

        public virtual ICollection<UserRole> Roles { get; set; }

        public virtual ICollection<Delivery> Deliveries { get; set; }
    }
}
