using FreshHarvestAPI.Data;
using FreshHarvestAPI.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreshHarvestAPI.Controllers
{

    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }



        //get all products

        [HttpGet]

        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProducts()
        {                                                                                 
            var products = await _context.Products.Include(p => p.Category).ToListAsync();
            return Ok(products);
        }




        //get product by ID


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);//looks for the product with the specific id in the database
            if (product == null)//if found returns the product else return 404
            {
                return NotFound();
            }
            return product;
        }




        //create new product
        [HttpPost]

        public async Task<ActionResult<ProductModel>> PostProduct([FromBody] ProductModel product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetProduct", new {id = product.Id}, product);
        }


        //update a product

        [HttpPut("{id}")]

        public async Task<IActionResult> PutProduct(int id, [FromBody] ProductModel product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            var existingProduct = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);

            

            if (existingProduct == null)
            {
                return NotFound("Product not found.");
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;


            var existingCategory = await _context.Category.FindAsync(product.CategoryId);
            if (existingCategory == null)
            {
                return NotFound("Category not found.");
            }

            existingProduct.CategoryId = product.CategoryId;
            existingProduct.Category = existingCategory; 

            try
            {
                await _context.SaveChangesAsync();
                return NoContent(); 
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("The product was modified or deleted by another process.");
            }
        }


        //delete a product

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteProduct(int id)//calls this when api/product/id is deleted
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }




    }
}
