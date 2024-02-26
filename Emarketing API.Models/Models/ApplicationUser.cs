using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarketing_API.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string firstName { get; set; }
        public string lastName { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string Zip_Code { get; set; }
        [NotMapped]
        public string Role { get; set; }


    }
}
