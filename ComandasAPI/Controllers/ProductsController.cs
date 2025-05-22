using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ComandasAPI.Data;
using ComandasAPI.DTO;
using ComandasAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComandasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _context.Products.Include(p => p.Options).ThenInclude(po => po.Values).ToListAsync();

            var productsDTO = _mapper.Map<List<ProductDTO>>(products);
            return Ok(productsDTO);


        }

        // Obtener un producto por ID con sus opciones y valores
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products
                .Include(p => p.Options)
                    .ThenInclude(po => po.Values)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return NotFound();
            var productDTO = _mapper.Map<ProductDTO>(product);


            return Ok(productDTO);
        }

        // Crear un producto
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(ProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<ProductDTO>(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, resultDto);
        }
        // Actualizar un producto
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);

            if (id != product.Id) return BadRequest();

            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        // Eliminar un producto
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}