using Emarketing_API.DataAccess.Repository.IRepository;
using Emarketing_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Emarketing_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BrandsController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }


        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Brands> brands = _unitOfWork._repositroyBrands.GetAll().ToList();
                if (brands.Count == 0)
                {
                    return NotFound("No Brand Found .....");
                }
                    return Ok(brands);
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{Id:int}", Name = "FindByID")]
        public IActionResult Find(int Id)
        {
            try
            {
                if(Id <= 0)
                {
                    return BadRequest("Invalid Brand ID"); 
                }
                Brands brand = _unitOfWork._repositroyBrands.Find(u => u.Id == Id);
                if (brand == null)
                {
                    return NotFound("Brand not found.");
                }
                return Ok(brand);
            }
            catch (Exception ex)
            {
                return StatusCode(500,$"an Error Occurred while finding the Brand ....\n {ex} ");
            }

                
           
        }

        [HttpPost]
        public IActionResult Add (Brands brand)
        {
            if (!ModelState.IsValid)
            {               
              return BadRequest();
            }
            try
            {
                if (brand == null)
                {
                    return BadRequest("Brand Data Is Missing. ");
                }

                _unitOfWork._repositroyBrands.Add(brand);

                _unitOfWork.Save();

                string url = Url.Link("FindByID", new { brand.Id });

                return Created(url, brand);
            }
            catch (Exception ex)
            {
                return StatusCode(500,$"An Error Occured While Adding Brand \n {ex}");
            }   
         }

        [HttpPut("{Id:int}",Name = "Edit")]
        public IActionResult Edit(int Id ,Brands brand)
        {
            try
            {
                if (Id <= 0)
                {
                    return BadRequest("Invalid brand ID.");
                }
                Brands Getbrand = _unitOfWork._repositroyBrands.Find(u => u.Id == Id);
                if (Getbrand == null)
                {
                    return NotFound("Brand not found...");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest();      
                }
                Getbrand.Name = brand.Name;

                _unitOfWork._repositroyBrands.Update(Getbrand);

                _unitOfWork.Save();

                return StatusCode(StatusCodes.Status204NoContent, "Date Updated");
            }
            catch(Exception ex)
            {
                return StatusCode(500,$"An Error Occurred while Editing Data \n {ex}");
            }

        }
        [HttpDelete("{Id:int}",Name ="Delete")]
        public IActionResult Delete(int Id)
        {
            try
            {
                if(Id <=0 )
                {
                    return BadRequest("Invalid  Brand ID ");
                }
                Brands brand = _unitOfWork._repositroyBrands.Find(u => u.Id == Id);

                if (brand == null)
                {
                    return NotFound("Brand Not Found");
                }

                _unitOfWork._repositroyBrands.Delete(brand);

                _unitOfWork.Save();

                return StatusCode(StatusCodes.Status204NoContent, "Brand Deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500,$"An Errro Occurred while Deleting \n {ex} ");
            }


        }

      

    }
    
}
