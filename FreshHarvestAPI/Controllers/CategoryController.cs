using FreshHarvestAPI.Data;
using FreshHarvestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreshHarvestAPI.Controllers
{

    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }


        //get all categories
        
        [HttpGet]

        public async Task<ActionResult<IEnumerable<CategoryModel>>> GetCategories()
        {
            var category = await _context.Category.Include(c => c.Products).ToListAsync();
            return Ok(category);
            

        }


        //Get category by Id

        [HttpGet("{id}")]

        public async Task<ActionResult<CategoryModel>> GetCategory(int id)
        {
            var category = await _context.Category.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return category;
        }


        //Create new Category

        [HttpPost]
        public async Task<ActionResult<CategoryModel>> PostCategory([FromBody] CategoryModel category)
        {
            _context.Category.Add(category);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCategories", new { id = category.Id }, category);

        }


        //Edit the category

        [HttpPut("{id}")]
        public async Task<ActionResult> EditCategory(int id, [FromBody]CategoryModel category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }


        //delete a category

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Category.Remove(category);
            await _context.SaveChangesAsync();
            return NoContent();
        }


    }
}
