using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Emarketing_API.Models
{
    public class Products : Base
    {
        [Required]
        [MaxLength(30)]
        [DisplayName("Product Name")]
        public string Name { get; set; }
        [ForeignKey("brand")]
        public int brand_Id { get; set; }
        [ValidateNever]
        public Brands? brand { get; set; }
        [ForeignKey("categories")]
        public int Category_Id { get; set; }
        [ValidateNever]
        public Categories? categories { get; set; }
        [Required]
        public DateTime Model_yesr { get; set; }
        [Required]
        public double price { get; set; }
        [ValidateNever]
        public List<Stocks> stocks { get; set; }
        [ValidateNever]
        public List<ShoppingCart> shoppingCarts { get; set; }


    }
}
