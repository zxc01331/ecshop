using AutoMapper;
using eshop.Dtos.brand;
using eshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class brandsController : ControllerBase
    {
        private readonly eshopContext _eshopContext;
        private readonly IMapper _mapper;
        public brandsController(eshopContext eshopContext, IMapper mapper)
        {
            _eshopContext = eshopContext;
            _mapper = mapper;
        }
        // GET: api/<brandsController>
        [HttpGet]
        public async Task<ActionResult<List<brandsDto>>> GetAMAsync()
        {
            var result = _eshopContext.brands
                         //.Include(b => b.products)
                         //.ThenInclude(p => p.category)
                         //.Include(b => b.products)
                         //.ThenInclude(p => p.product_images)
                         .AsQueryable();

            if (!result.Any())
            {
                return NotFound("找不到符合的產品");
            }
            var brandsList = await result.ToListAsync();
            var map = _mapper.Map<List<brandsDto>>(brandsList);
            //var map = _mapper.Map<List<productsDto>>(result.ToList());

            return Ok(map);
        }

        // GET api/<brandsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<brandsDto>> GetById(int id)
        {
            var brand = await _eshopContext.brands
                .Include(b => b.products)
                .ThenInclude(p => p.category)
                .Include(b => b.products)
                .ThenInclude(p => p.product_images)
                .FirstOrDefaultAsync(p => p.id == id);
            if (brand == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<brandsDto>(brand));
        }

        // POST api/<brandsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] brandsPostDto value)
        {
            var Checkbrand = await _eshopContext.brands
                .FirstOrDefaultAsync(b => b.brand_name == value.brand_name);
            if (Checkbrand != null)
            {
                return NotFound("品牌名稱已存在");
            }

            var brand = _mapper.Map<brands>(value);

            brand.created_at = DateTime.Now;
            brand.updated_at = DateTime.Now;

            _eshopContext.brands.Add(brand);

            await _eshopContext.SaveChangesAsync();

            return Ok(_mapper.Map<brandsDto>(brand));
        }

        // PUT api/<brandsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<brandsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var delete = await (from a in _eshopContext.brands
                                where a.id == id
                                select a).SingleOrDefaultAsync();

            if (delete != null)
            {
                _eshopContext.brands.Remove(delete);
                await _eshopContext.SaveChangesAsync();
            }
            else
            {
                return NotFound("找不到指定刪除的資料");
            }

            return NoContent();
        }
    }
}
