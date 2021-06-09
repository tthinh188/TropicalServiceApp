using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TropicalServerApp.Models
{
    public class TropicalUser
    {
        public int userID { get; set; }
        [Required]
        public string LoginID { get; set; }
        [Required]
        public string password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int roleID { get; set; }
        public int UserRouteNumber { get; set; }
        public bool Active { get; set; }
        public string Email { get; set; }
    }
}