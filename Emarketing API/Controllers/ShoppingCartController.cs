using Emarketing_AP.Models;
using Emarketing_API.DataAccess.Repository.IRepository;
using Emarketing_API.Models.DTO;
using Emarketing_API.Modles.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Emarketing_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        

        public ShoppingCartController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

       


        [HttpPost]
        public IActionResult Details(ShoppingCart shoppingCart) 
        {
            try
            {

                var claimsIdentity = (ClaimsIdentity)User.Identity;

                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

                if (userId == null )
                {
                    return BadRequest("Invalid user ID");
                }

                shoppingCart.ApplicationUser_Id = userId;

                Stocks StockCount = _unitOfWork._repositoryStock.Find(u => u.Product_Id == shoppingCart.Product_Id);

                if(StockCount.Quantity<shoppingCart.Count || StockCount.Quantity ==null)
                {
                    return BadRequest("Item Cont In Stock less than u need");
                }

                var CartFromDB = _unitOfWork._repositoryShoppingCart.Find(u => u.ApplicationUser_Id == userId && u.Product_Id == shoppingCart.Product_Id);
               
                if(CartFromDB !=null)
                {
                    CartFromDB.Count += shoppingCart.Count;

                    StockCount.Quantity -= shoppingCart.Count;

                    _unitOfWork._repositoryShoppingCart.Update(CartFromDB);

                    _unitOfWork._repositoryStock.Update(StockCount);

                    _unitOfWork.Save();

                    return Ok(shoppingCart);
                }
                else
                {
                    _unitOfWork._repositoryShoppingCart.Add(shoppingCart);

                    StockCount.Quantity -= shoppingCart.Count;

                    _unitOfWork._repositoryStock.Update(StockCount);

                    _unitOfWork.Save();

                    return Ok(shoppingCart);
                }

                
            }
            catch(Exception ex)
            {
                return StatusCode(500,$"An Error Occurred While Adding in shoppingCart{ex}");
            }

        }


    }
}
