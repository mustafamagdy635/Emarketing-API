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
    public class CartController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartDTO ShoppingCartDTO { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;

                string userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

                ShoppingCartDTO = new()
                {
                    ShoppingCartList = _unitOfWork._repositoryShoppingCart.GetAll(u => u.ApplicationUser_Id == userId, IncludeProperties: new[] { "Product" }).ToList()
                };
                foreach (var cart in ShoppingCartDTO.ShoppingCartList)
                {

                    cart.Price = GetPriceBasedOnQuantity(cart);
                    ShoppingCartDTO.TotalPrice += (cart.Price * cart.Count);
                }

                return Ok(ShoppingCartDTO.TotalPrice);
            }

            catch (Exception ex)
            {
                return StatusCode(500,$"An error occurred while fitch shopping Cart data \n {ex}");
            }
        }

        private double GetPriceBasedOnQuantity(ShoppingCart shoppingCart)
        {
            return shoppingCart.Product.price;
        }

        [HttpPost("Plus", Name = "Plus")]
        public IActionResult Plus(int CartId)
        {
            try
            {
                if (CartId == null || CartId == 0)
                {
                    return BadRequest("Ivalid Cart ID");
                }

                var CartFromDb = _unitOfWork._repositoryShoppingCart.Find(u => u.Id == CartId);

                if (CartFromDb == null)
                {
                    return BadRequest("Cart Missing Data");
                }
                Stocks stock = _unitOfWork._repositoryStock.Find(u => u.Product_Id == CartFromDb.Product_Id);

                if (stock.Quantity <= 0 || stock.Quantity == null)
                {
                    return BadRequest($"Dont have Any quantiy to{stock.products.Name} ");
                }

                CartFromDb.Count += 1;

                stock.Quantity   -= 1;

                _unitOfWork._repositoryShoppingCart.Update(CartFromDb);

                _unitOfWork._repositoryStock.Update(stock);

                _unitOfWork.Save();

                return Ok(CartFromDb);

            }
            catch (Exception ex)
            {
                return StatusCode(500,$"An error occurred while Adding shopping Cart data \n {ex}");
            }
        }
        [HttpPost("Minus", Name = "Minus")]

        public IActionResult Minus(int CartId)
        {
            try
            {
                if (CartId == null || CartId <= 0)
                {
                    return BadRequest("Ivalid Cart ID");
                }

                var CartFromDb = _unitOfWork._repositoryShoppingCart.Find(u => u.Id == CartId);

                if (CartFromDb == null)
                {
                    return BadRequest("Cart Missing Data");
                }

                Stocks stock = _unitOfWork._repositoryStock.Find(u => u.Product_Id == CartFromDb.Product_Id);

                if (CartFromDb.Count == 1)
                {
                    _unitOfWork._repositoryShoppingCart.Delete(CartFromDb);
                }
                else
                {
                    CartFromDb.Count -= 1;

                    _unitOfWork._repositoryShoppingCart.Update(CartFromDb);
                }

                stock.Quantity += 1;

                _unitOfWork._repositoryStock.Update(stock);

                _unitOfWork.Save();

                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(500,$"An error occurred while Minus shopping Cart data \n {ex}");
            }
        }


        [HttpPost("Remove", Name = "Remove")]

        public IActionResult Remove(int CartId)
        {
            try
            {
                if (CartId == null || CartId <= 0)
                {
                    return BadRequest("Ivalid Cart ID");
                }

                var CartFromDb = _unitOfWork._repositoryShoppingCart.Find(u => u.Id == CartId);

                if (CartFromDb == null)
                {
                    return BadRequest("Cart Missing Data");
                }

                Stocks stock = _unitOfWork._repositoryStock.Find(u => u.Product_Id == CartFromDb.Product_Id);

                stock.Quantity += CartFromDb.Count;

                _unitOfWork._repositoryShoppingCart.Delete(CartFromDb);

                _unitOfWork._repositoryStock.Update(stock);

                _unitOfWork.Save();

                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(500,$"An error occurred while Minus shopping Cart data \n {ex}");
            }
        }

    }
}
