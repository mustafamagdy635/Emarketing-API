using Emarketing_API.Models.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarketing_AP.Models
{
    public class Stocks :Base
    {
        [Required]
        [ForeignKey("products")]
        public int Product_Id { get; set; }

        [ValidateNever]
        public Products? products { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
