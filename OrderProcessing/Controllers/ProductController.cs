using OrderProcessing.DatabaseContexts;
using OrderProcessing.Items;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace OrderProcessing.Controllers
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

        //TODO - need to have it so ID shows when get requests are made, but not when post requests are made
        // may have to not use the FromBody attribute, or fanagle JsonIgnore

        //TODO - create a data access layer to holde the query building. keep all db logic in that layer

        //TODO - write some damn tests for TDD

        // GET api/product
        [HttpGet()]
        [Route("GetProducts")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var query = _context.Products.AsQueryable();

            var result = await query.ToListAsync();
            return Ok(await query.ToListAsync());

        }

        // GET api/product/{id}
        [HttpGet]
        [Route("GetProduct")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct(Guid? id = null, string? name = null )
        {
            var query = _context.Products.AsQueryable();

            if (id != Guid.Empty
                && id != null)
            {
                query = query.Where(m => m.Id.Equals(id));  // Filter by Id
            }
            else if( !string.IsNullOrEmpty(name))
            {
                query = query.Where(m => m.Name.Contains(name));  // Filter by Name
            }
            else
            {
                //make error builder
                return BadRequest($"Missing Parameter(s): {nameof(id)}, {nameof(name)}");
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
            //TODO - in the future make it so each property on Product is nullable, and only update if a value is provided
            
            //TODO - make it so the user cannot update the id, only query by it

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
