using AutoMapper;
using eshop.Dtos;
using eshop.Dtos.Product_images;
using eshop.Dtos.Products;
using eshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class productsController : ControllerBase
    {
        private readonly eshopContext _eshopContext;
        private readonly IMapper _mapper;

        public productsController(eshopContext eshopContext, IMapper mapper)
        {
            _eshopContext = eshopContext;
            _mapper = mapper;
        }

        // GET: api/<productsController>
        [HttpGet]
        public  ActionResult<productsDto> Get(int? id,string? product_name,string? description, int? minPrice, int? maxPrice)
        {
            var result = from a in _eshopContext.products
                         .Include(p=>p.product_images)
                         select new productsDto
            {
                id=a.id,
                product_name=a.product_name,
                description=a.description,
                price=a.price,
                product_images = a.product_images.Select(img => new product_imagesDto
                {
                    image_url = img.image_url,
                    product_id = img.product_id
                }).ToList()

            };

            if (id.HasValue)
            {
                result = result.Where(a => a.id == id.Value);
            }
            if (!string.IsNullOrWhiteSpace(product_name))
            {
                result = result.Where(a => a.product_name.Contains(product_name));
            }
            if(!string.IsNullOrWhiteSpace(description))
            {
                result = result.Where(a=>a.description.Contains(description));
            }
            // 篩選價格大於等於 minPrice
            if (minPrice.HasValue)
            {
                result = result.Where(a => a.price >= minPrice.Value);
            }

            // 篩選價格小於等於 maxPrice
            if (maxPrice.HasValue)
            {
                result = result.Where(a => a.price <= maxPrice.Value);
            }

            if (!result.Any())
            {
                return NotFound("找不到符合的產品");
            }
            return Ok(result);
        }
        
        [HttpGet("AM")]
        public async Task<ActionResult<List<productsDto>>> GetAMAsync(int? id, string? product_name, string? description, int? minPrice, int? maxPrice)
        {
            var result =_eshopContext.products
                         .Include(p => p.category)
                         .Include(p => p.brand)
                         .Include(p => p.product_images)
                         .AsQueryable();

            if (id.HasValue)
            {
                result = result.Where(a => a.id == id.Value);
            }
            if (!string.IsNullOrWhiteSpace(product_name))
            {
                result = result.Where(a => a.product_name.Contains(product_name));
            }
            if (!string.IsNullOrWhiteSpace(description))
            {
                result = result.Where(a => a.description.Contains(description));
            }
            // 篩選價格大於等於 minPrice
            if (minPrice.HasValue)
            {
                result = result.Where(a => a.price >= minPrice.Value);
            }

            // 篩選價格小於等於 maxPrice
            if (maxPrice.HasValue)
            {
                result = result.Where(a => a.price <= maxPrice.Value);
            }


            if (!result.Any())
            {
                return NotFound("找不到符合的產品");
            }
            var productList = await result.ToListAsync();
            var map = _mapper.Map<List<productsDto>>(productList);
            //var map = _mapper.Map<List<productsDto>>(result.ToList());

            return Ok(map);
        }

        // GET api/<productsController>/5
        [HttpGet("{id}")]
        public ActionResult<productsDto> GetById(int id)
        {
            var product = _eshopContext.products
                .Include(p => p.product_images)
                .SingleOrDefault(p => p.id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<productsDto>(product));
        }

        // POST api/<productsController>
        [HttpPost]
        public string Post([FromBody] products value)
        {
            value.created_at = DateTime.Now;
            value.updated_at = DateTime.Now;

            _eshopContext.products.Add(value);
            _eshopContext.SaveChanges();


            return "新增完成";
        }
        [HttpPost("AM")]
        public async Task<IActionResult> PostAM([FromBody] productsPostDto value)
        {
            var product = _mapper.Map<products>(value);

            product.created_at = DateTime.Now;
            product.updated_at = DateTime.Now;

            _eshopContext.products.Add(product);

            await _eshopContext.SaveChangesAsync();

            await _eshopContext.Entry(product).Reference(p => p.brand).LoadAsync(); // 載入品牌資料
            await _eshopContext.Entry(product).Reference(p => p.category).LoadAsync(); // 載入分類資料
            await _eshopContext.Entry(product).Collection(p => p.product_images).LoadAsync(); // 載入產品圖片資料

            return Ok(_mapper.Map<productsDto>(product));

        }

        // PUT api/<productsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] productsPutDto value)
        {
            //_eshopContext.products.Update(value);
            //_eshopContext.SaveChanges();
            var update = _eshopContext.products.Find(id);
            
            update.updated_at = DateTime.Now;

        }
        [HttpPut("AM/{id}")]
        public IActionResult PutAM(int id, [FromBody] productsPutDto value)
        {
            if (id != value.id)
            {
                return BadRequest();
            }
            var update = _eshopContext.products.Find(id);

            if(update != null)
            {
                _mapper.Map(value,update);
                update.updated_at = DateTime.Now;

                _eshopContext.SaveChanges();
            }else
            {
                return NotFound();
            }

            return NoContent();

        }

        // DELETE api/<productsController>/5
        //刪除指定產品
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var delete = await (from a in _eshopContext.products
                         where a.id==id select a)
                         .Include(p => p.product_images).SingleOrDefaultAsync();
            
            if (delete != null)
            {
                // 手動刪除關聯的 product_images 資料
                _eshopContext.product_images.RemoveRange(delete.product_images);
                _eshopContext.products.Remove(delete);
                await _eshopContext.SaveChangesAsync();
            }
            else
            {
                return NotFound("找不到指定刪除的資料");
            }

            return NoContent();
        }
        //刪除多筆指定資料
        [HttpDelete("list/{ids}")]
        public async Task<IActionResult> Delete(string ids)
        {
            var deleteList = ids.Split(',').Select(int.Parse).ToList();

            var delete = await (from a in _eshopContext.products
                                where deleteList.Contains(a.id)
                          select a)
                         .Include(p => p.product_images).ToListAsync();

            if (delete != null && delete.Count > 0)
            {
                // 手動刪除關聯的 product_images 資料
                foreach(var product in delete)
                {
                    if(product.product_images !=null)
                    {
                        _eshopContext.product_images.RemoveRange(product.product_images);
                    }
                }
                _eshopContext.products.RemoveRange(delete);
                await _eshopContext.SaveChangesAsync();
            }
            else
            {
                return NotFound("找不到指定刪除的資料");
            }

            return NoContent();
        }

        //刪除指定圖片
        [HttpDelete("img/{id}")]
        public IActionResult Delete2(int id)
        {
            var delete = (from a in _eshopContext.products
                          where a.id == id
                          select a)
                         .Include(p => p.product_images).SingleOrDefault();

            if (delete != null)
            {
                // 手動刪除關聯的 product_images 資料
                _eshopContext.product_images.RemoveRange(delete.product_images);
                //_eshopContext.products.Remove(delete);
                _eshopContext.SaveChanges();
            }
            else
            {
                return NotFound("找不到指定刪除的資料");
            }

            return NoContent();
        }


    }
}
