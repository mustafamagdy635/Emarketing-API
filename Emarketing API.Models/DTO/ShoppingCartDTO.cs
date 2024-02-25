using Emarketing_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarketing_API.Models.DTO
{
    public class ShoppingCartDTO
    {
        public  IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
        public OrderHeader orderHeader { get; set; }

    }
}
