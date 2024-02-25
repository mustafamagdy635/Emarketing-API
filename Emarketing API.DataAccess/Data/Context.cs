using Emarketing_API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarketing_API.DataAccess.Data
{
    public class Context :IdentityDbContext<ApplicationUser>
    {
        public Context(DbContextOptions option):base(option) { }

        DbSet<Categories>categories { get; set; } 
        DbSet<Brands> brands { get; set; }
        DbSet<Products> products { get; set; }  
        DbSet<Stocks> stocks { get; set; }  
        DbSet<ApplicationUser> applicationUsers { get; set; }
        DbSet<ShoppingCart> shoppingCarts { get; set; }
        DbSet<OrderDetail> orderDetails { get; set; }
        DbSet<OrderHeader> orderHeaders { get; set; }
    }
}
