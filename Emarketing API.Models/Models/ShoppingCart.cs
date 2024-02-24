using Emarketing_API.Models.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarketing_API.Modles.Models
{
    public class ShoppingCart : Base
    {
        [ForeignKey("Product")]
        [Required(ErrorMessage = "Product is required.")]
        public int Product_Id { get; set; }

        [ValidateNever]
        public Products Product { get; set; }

        public int Count { get; set; }

        [ForeignKey("ApplicationUser")]
        //[Required(ErrorMessage = "ApplicationUser_Id is required.")]
        public string ? ApplicationUser_Id { get; set; }

        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        [NotMapped]
        public double Price { get; set; }
    }
}
