using FreshHarvestAPI.Data;
using FreshHarvestAPI.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreshHarvestAPI.Controllers
{

    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]// this api handles requests at api/Product
    [ApiController]//makes it an api controller
    public class ProductController : ControllerBase
    {

        private readonly ApplicationDbContext _context;//allows interaction with the database

        public ProductController(ApplicationDbContext context)
        {
            _context = context;//assigns database connection when the controller starts
        }



        //get all products

        [HttpGet]

        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProducts()
        {                                                                                 //ToListAsync is used with EF to fetch data from a database asynchronously.
            var products = await _context.Products.Include(p => p.Category).ToListAsync();//gets all products including their category details
            return Ok(products);//returns list of products as response
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

        public async Task<ActionResult<ProductModel>> PostProduct([FromBody] ProductModel product)//gets product details from the request body
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetProduct", new {id = product.Id}, product);//responds with the newly created product
        }


        //update a product

        [HttpPut("{id}")]

        public async Task<IActionResult> PutProduct(int id, [FromBody] ProductModel product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }


        //delete a product

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteProduct(int id)
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
