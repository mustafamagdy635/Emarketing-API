using Emarketing_API.Models.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarketing_API.Models.DTO
{
    public class ProductWithCategoryWithBrandVM
    {
        public Products Products { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> Categories { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Brands { get; set; }
    }
}
