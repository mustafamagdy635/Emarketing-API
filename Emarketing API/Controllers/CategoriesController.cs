using Emarketing_API.DataAccess.Repository.IRepository;
using Emarketing_API.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Emarketing_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Categories> categories = _unitOfWork._repositoryCategories.GetAll().ToList();

                if (categories.Count == 0)
                {
                    return BadRequest("No Categories Found ....");
                }
                
                    return Ok(categories);
                
            }catch(Exception ex) 
            {
                return StatusCode(500, "An Error Occurred whil Get Categories");
            }
        }

        [HttpPost]
        public IActionResult Add(Categories categories )
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                if (categories == null)
                {
                    return BadRequest("Category Data is Missing .....");
                }

                _unitOfWork._repositoryCategories.Add(categories);
                
                _unitOfWork.Save();

                string url = Url.Link("GetCategoryById", new { Id = categories.Id });

                return Created(url, categories);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex}");

                return StatusCode(500,$"Ann Error Occurred While Adding Category \n {ex}");
            }

        }

        [HttpGet("{Id:int}",Name ="GetCategoryById")]
        public IActionResult FindById(int Id)
        {
            try
            {
                if(Id <= 0)
                {
                    return BadRequest("Invalid Category ID");
                }
                Categories category = _unitOfWork._repositoryCategories.Find(u => u.Id == Id);

                if (category == null)
                {
                    return BadRequest("no Category Found .....");
                }
                    return Ok(category);
                
                   
            }
            catch (Exception ex)
            {
                return StatusCode(500,$"An Error Occurred While Find Category \n {ex}");
            }
        }

        [HttpGet("Name:alpha",Name ="GetCategoryByName")]
        public IActionResult FindByName(string Name)
        {
            try
            {
                if(Name== null)
                {
                    return BadRequest("Invalid Name");
                }

                Categories category = _unitOfWork._repositoryCategories.Find(u => u.Name == Name);

                if (category == null)
                {
                    return BadRequest();
                }

                return Ok(category);
            }
            catch ( Exception ex)
            {
                return StatusCode(500, $"An Error Occurred while Find Using name \n {ex}");
            }
        }

        [HttpPut("{Id:int}",Name ="EditCategory")]
        public IActionResult Eidt(int Id,Categories categories)
        {
            try
            {
                if (Id <= 0)
                {
                    return BadRequest("Invalid category ID");
                }

                Categories GetCategories = _unitOfWork._repositoryCategories.Find(u => u.Id == Id);

                if (GetCategories == null)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest();                   
                }

                GetCategories.Name = categories.Name;

                _unitOfWork._repositoryCategories.Update(GetCategories);

                _unitOfWork.Save();

                return StatusCode(StatusCodes.Status204NoContent, "Data Saved");


            }
            catch (Exception ex)
            {
                return StatusCode(500,$"An Error Occurred while Editing..... \n {ex}");

            }
        }

        [HttpDelete("{Id:int}")]
        public IActionResult Delete(int Id)
        {
            try
            {
                if(Id <= 0)
                {
                    return BadRequest("Invalid Category Id");
                }

                Categories category = _unitOfWork._repositoryCategories.Find(u => u.Id == Id);

                if (category == null)
                {
                    return NotFound();
                }

                _unitOfWork._repositoryCategories.Delete(category);
                
                _unitOfWork.Save();
                
                return StatusCode(StatusCodes.Status204NoContent, "Data Deleted");

            }
            catch (Exception ex)
            {
                return StatusCode(500,$"An Error Occurred while Deleting..... \n {ex}");

            }
        }
    }
}
