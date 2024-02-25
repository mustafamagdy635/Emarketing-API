using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Emarketing_API.Models
{
    public class Brands : Base
    {
        [Required]
        [MaxLength(30)]
        [DisplayName("Brand Name")]
        public string Name { get; set; }
        [ValidateNever]
        List<Products> Products { get; set; }
        
    }
}
