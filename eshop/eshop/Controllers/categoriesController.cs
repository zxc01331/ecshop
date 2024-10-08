using AutoMapper;
using eshop.Dtos.brand;
using eshop.Dtos.categories;
using eshop.Dtos.Categories;
using eshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class categoriesController : ControllerBase
    {
        private readonly eshopContext _eshopContext;
        private readonly IMapper _mapper;
        public categoriesController(eshopContext eshopContext, IMapper mapper)
        {
            _eshopContext = eshopContext;
            _mapper = mapper;
        }
        // GET: api/<categoriesController>
        [HttpGet]
        public async Task<ActionResult<List<categoriesDto>>> GetAMAsync()
        {
            var result = _eshopContext.categories
                         //.Include(b => b.products)
                         //.ThenInclude(p => p.brand)
                         //.Include(b => b.products)
                         //.ThenInclude(p => p.product_images)
                         .AsQueryable();

            if (!result.Any())
            {
                return NotFound("找不到符合的類別");
            }
            var categoriesList = await result.ToListAsync();
            var map = _mapper.Map<List<categoriesDto>>(categoriesList);

            return Ok(map);
        }

        // POST api/<categoriesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] categoriesPostDto value)
        {
            var Checkcategory = await _eshopContext.categories
                .FirstOrDefaultAsync(b => b.category_name == value.category_name);
            if (Checkcategory != null)
            {
                return NotFound("此類別已存在");
            }

            var category = _mapper.Map<categories>(value);

            category.created_at = DateTime.Now;
            category.updated_at = DateTime.Now;

            _eshopContext.categories.Add(category);

            await _eshopContext.SaveChangesAsync();

            return Ok(_mapper.Map<categoriesDto>(category));
        }

        // PUT api/<categoriesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<categoriesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var delete = await (from a in _eshopContext.categories
                                where a.id == id
                                select a).SingleOrDefaultAsync();

            if (delete != null)
            {
                _eshopContext.categories.Remove(delete);
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
