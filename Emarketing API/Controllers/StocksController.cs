using AutoMapper;
using Emarketing_AP.Models;
using Emarketing_API.Models.DTO;
using Emarketing_API.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.EntityFrameworkCore.Query;

namespace Emarketing_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;

        public StocksController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this.mapper = mapper;
        }


        [HttpPost]
        public IActionResult Add(StockWithProductVM stockWithProductVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                if(stockWithProductVM == null)
                {
                    return BadRequest("Stock Date Is Missing");
                }
                var existingStock = _unitOfWork._repositoryStock.Find(u => u.Product_Id == stockWithProductVM.Id,IncludeProperties: new[] { "products" });
                existingStock.Quantity += stockWithProductVM.Quantity; 

                _unitOfWork._repositoryStock.Update(existingStock);

                _unitOfWork.Save();
                
                return Ok();

            }
            catch (Exception Ex)
            {
                return StatusCode(500,"Ann Error Occurred While Adding Stock");
            }
        }

        [HttpGet(Name ="GetAll")]
        public IActionResult Get()
        {
            try
            {
                List<Stocks> stocks = _unitOfWork._repositoryStock.GetAll(IncludeProperties: new []{ "products" }).ToList();
                if (stocks.Count == 0)
                {
                    return BadRequest("Stock empty");
                }

                var result = mapper.Map<IEnumerable<StockWithProductVM>>(stocks);
                return Ok(result);
            }
            catch (Exception Ex)
            {
                return StatusCode(500, "Ann Error Occurred While Get Stock");
            }
        }

        [HttpGet("{Id:int}", Name = "GetStockById")]
        public IActionResult FindById(int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return BadRequest("Invalid Stock ID");
                }
                if (Id == null)
                {
                    return BadRequest("Invalid Stock ID");
                }

                var stock = _unitOfWork._repositoryStock.Find(u => u.Product_Id ==Id, IncludeProperties: new[] { "products" }); 
                if (stock == null)
                {
                    return BadRequest("No Stock data Found....");
                }
                StockWithProductVM stockWithProductVM = mapper.Map<StockWithProductVM>(stock);

                return Ok(stockWithProductVM);
            }
            catch (Exception Ex)
            {
                return StatusCode(500, "An Error Occurred While Get Data to Edit Stock");
            }

        }
        
        [HttpPut]
        public IActionResult Edit( StockWithProductVM stockWithProductVM)
        {
            try
            {
                if (stockWithProductVM.Id <= 0|| stockWithProductVM.Id == null)
                {
                    return BadRequest("Invalid Stock Id");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Data missing while editing");
                }

                Stocks stock = _unitOfWork._repositoryStock.Find(u => u.Product_Id == stockWithProductVM.Id, IncludeProperties: new[]{ "products" });
                
                if (stock == null)
                {
                    return BadRequest("No Stock data Found....");
                }

                stock.Quantity=stockWithProductVM.Quantity;

                _unitOfWork._repositoryStock.Update(stock);

                _unitOfWork.Save();

                return Ok();
            }
            catch (Exception Ex)
            {
                return StatusCode(500, "An Error Occurred While  Editing Stock"); 
            }

        }

        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            try
            {
                if (Id <= 0 || Id == null)
                {
                    return BadRequest("Invalid Stock Id");
                }
               

                Stocks stock = _unitOfWork._repositoryStock.Find(u => u.Id == Id);

                if (stock == null)
                {
                    return BadRequest("No Stock data Found....");
                }

                stock.Quantity = 0;

                _unitOfWork._repositoryStock.Update(stock);

                _unitOfWork.Save();

                return Ok();

            }
            catch (Exception Ex)
            {
                return StatusCode(500, "An Error Occurred While  deleting Stock");
            }

        }
    
    }
}
