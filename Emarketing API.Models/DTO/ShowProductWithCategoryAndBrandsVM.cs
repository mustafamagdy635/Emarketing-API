using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarketing_API.Models.DTO
{
    public class ShowProductWithCategoryAndBrandsVM
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Product Name")]
        public string Name { get; set; }
        public int brand_Id { get; set; }       
        public string ?brandName { get; set; }
        public int Category_Id { get; set; }
        public string ?categoryName { get; set; }
        [Required]
        public DateTime Model_yesr { get; set; }
        [Required]
        public double price { get; set; }
    }
}
