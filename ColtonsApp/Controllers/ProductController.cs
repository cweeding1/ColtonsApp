using ColtonsApp.DatabaseContexts;
using ColtonsApp.ProducDependencies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ColtonsApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly AppDbContext _context;

        public ProductController(ILogger<ProductController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET api/product
        [HttpGet()]
        [Route("GetProduct")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            var query = _context.Products.AsQueryable();

            var result = await query.ToListAsync();
            return Ok(await query.ToListAsync());

        }

        // GET api/product/{id}
        [HttpGet]
        [Route("GetProduct/{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct( Guid id )
        {
            var query = _context.Products.AsQueryable();

            if (id != Guid.Empty)
            {
                query = query.Where(m => m.Id.Equals(id));  // Filter by Id
            }
            else
            {
                return BadRequest($"Missing Parameter: {nameof(id)}");
            }

            return Ok(await query.ToListAsync());
        }

        // POST api/product
        [HttpPost()]
        [Route("AddProduct")]
        public async Task<ActionResult> AddProduct([FromBody] Product product)
        {
            product.Id = Guid.NewGuid();

            _context.Products.Add(product);
            
            return Ok(await _context.SaveChangesAsync());
        }

        // DELETE api/product
        [HttpDelete]
        [Route("DeleteProduct/{id}")]
        public async Task<ActionResult> DeleteProduct(Guid id)
        {            
            var query = _context.Products.AsQueryable();

            query = query.Where (m => m.Id.Equals(id));
            
            await query.ExecuteDeleteAsync();
            return NoContent();
        }

        // PUT api/product
        [HttpPut]
        [Route("UpdateProduct/{id}")]
        public async Task<ActionResult> UpdateProduct(Guid id, [FromBody] Product updatedProduct)
        {
            // in the future make it so each property on Product is nullable, and only update if a value is provided

            if (updatedProduct == null)
            {
                return BadRequest("Product data is required.");
            }

            var existingProduct = await _context.Products.FindAsync(id);

            if (existingProduct == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            existingProduct.Name = updatedProduct.Name;
            existingProduct.Description = updatedProduct.Description;

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
