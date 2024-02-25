using Emarketing_API.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarketing_API.Models
{
    public class OrderDetail:Base
    {
        [ForeignKey("OrderHeader")]
        [Required]
        public int OrderHeaderId { get; set; }      
        [ValidateNever]
        public OrderHeader OrderHeader { get; set; }
        [ForeignKey("Product")]
        [Required]
        public int ProductId { get; set; }    
        [ValidateNever]
        public Products Product { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }

    }
}
