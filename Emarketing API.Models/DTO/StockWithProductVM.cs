using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarketing_API.Models.DTO
{
    public class StockWithProductVM
    {

        public int Id { get; set; }
        [ValidateNever]
        public string  Name { get; set; }
        [Range(1, 100000, ErrorMessage = " Number Must Be Greater Than '0' ")]
        public int Quantity { get; set; }

    }
}
