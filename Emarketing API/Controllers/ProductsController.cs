﻿using AutoMapper;
using Emarketing_API.Models;
using Emarketing_API.Models.DTO;
using Emarketing_API.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Emarketing_API.DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;

namespace Emarketing_API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;

        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpPost]

        public IActionResult Add(ProductWithCategoryWithBrandVM ProductVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (ProductVM.Products == null)
                {
                    return BadRequest("Product Data Is Missing.");
                }

                _unitOfWork.repositoryProduct.Add(ProductVM.Products);

                _unitOfWork.Save();

                Stocks newStock = new Stocks
                {
                    Quantity = 0,
                    Product_Id = ProductVM.Products.Id
                };

                _unitOfWork._repositoryStock.Add(newStock);
                _unitOfWork.Save();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding the product...... \n {ex}");
            }

        }
        
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Products> Allbroducts = _unitOfWork.repositoryProduct.GetAll(IncludeProperties: new[] { "brand", "categories" }).ToList();
              
                var broductVm = mapper.Map<IEnumerable<ShowProductWithCategoryAndBrandsVM>>(Allbroducts);

                return Ok(broductVm);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while Fetching the product...... \n {ex}");
            }

        }


        [HttpGet("{Id:int}", Name = "FindProductById")]
        public IActionResult Find(int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return BadRequest("Invalid Product ID");
                }

                Products product = _unitOfWork.repositoryProduct.Find(u => u.Id == Id, IncludeProperties: new[] { "brand", "categories" });
               
                if (product == null)
                {
                    return NotFound();
                }

                var productVM = mapper.Map<ShowProductWithCategoryAndBrandsVM>(product);

                return Ok(productVM);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while Fetching product by Id the ...... \n {ex}");
            }
        }

        [HttpPut]


        public IActionResult Edit(int Id, ProductWithCategoryWithBrandVM ProductVM)
        {
            try
            {
                if (Id <= 0)
                {
                    return BadRequest("Invalid Product Id");
                }

                Products Product = _unitOfWork.repositoryProduct.Find(u => u.Id == Id, IncludeProperties: new[] { "brand", "categories" });
              
                if (Product == null)
                {
                    return NotFound();
                }
                if (ProductVM == null)
                {
                    return NotFound();
                }
                Product.Name = ProductVM.Products.Name;

                Product.price = ProductVM.Products.price;

                Product.Model_yesr = ProductVM.Products.Model_yesr;

                Product.brand_Id = ProductVM.Products.brand_Id;

                Product.Category_Id = ProductVM.Products.Category_Id;

                _unitOfWork.repositoryProduct.Update(Product);

                _unitOfWork.Save();

                return Ok(Product);

            }
            catch (Exception ex)
            {
                return StatusCode(500,$"An Erorr Occurred while Updating the Product..... \n{ex}");
            }
        }

        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return BadRequest("Invalid Category ID");
                }

                Products DeleteProduct = _unitOfWork.repositoryProduct.Find(u => u.Id == Id);

                if (DeleteProduct == null)
                {
                    return NotFound("Category Not Found ....");
                }

                var StockData = _unitOfWork._repositoryStock.Find(U => U.Product_Id == Id);

                if (StockData.Quantity > 0)
                {
                    return BadRequest("Cannot delete product with existing stock.");
                }
                _unitOfWork.repositoryProduct.Delete(DeleteProduct);

                _unitOfWork.Save();

                return StatusCode(StatusCodes.Status204NoContent, "Product deleted successfully.");


            }
            catch (Exception ex)
            {
                return StatusCode(500,$"An Erorr Occurred while Updating the Product....\n{ex}");
            }

        }

        [HttpGet("/{Id:int}", Name = "Upser")]

        public IActionResult Upser(int? Id)
        {
            try
            {
                ProductWithCategoryWithBrandVM ProductVM = new ProductWithCategoryWithBrandVM
                {
                    Brands = _unitOfWork._repositroyBrands.GetAll().Select(option => new SelectListItem
                    {
                        Text = option.Name,
                        Value = option.Id.ToString()
                    }),
                    Categories = _unitOfWork._repositoryCategories.GetAll().Select(option => new SelectListItem
                    {
                        Text = option.Name,
                        Value = option.Id.ToString()
                    })

                };
                if (Id == 0 || Id == null)
                {
                    return Ok(ProductVM);
                }
                ProductVM.Products = _unitOfWork.repositoryProduct.Find(u => u.Id == Id);
                return Ok(ProductVM.Products);
            }
            catch (Exception ex)
            {
                return StatusCode(500,$"An Erorr Occurred while used Upser....\n{ex}  ");
            }
        }

    }
}
