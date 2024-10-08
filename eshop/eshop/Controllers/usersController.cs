using AutoMapper;
using eshop.Dtos.Users;
using eshop.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usersController : ControllerBase
    {
        private readonly eshopContext _eshopContext;
        private readonly IMapper _mapper;

        public usersController(eshopContext eshopContext, IMapper mapper)
        {
            _eshopContext = eshopContext;
            _mapper = mapper;
        }




        // GET: api/<users>
        [HttpGet]
        public IEnumerable<usersDto> Get(int? id,string? username,DateTime? created_at)
        {
            var result = from a in _eshopContext.users select new usersDto
            {
                id=a.id,
                username=a.username,
                password=a.password,
                email=a.email,
                created_at = a.created_at,
                updated_at = a.updated_at,
            };
            

            if(id.HasValue)
            {
                result = result.Where(a => a.id == id.Value);
            }
            if(!string.IsNullOrWhiteSpace(username))
            {
                result = result.Where(a => a.username == username);
            }
            if(created_at !=null)
            {
                result = result.Where(a=>a.created_at.Date == ((DateTime)created_at).Date);
            }
            return result;
        }

        // GET api/<test>/5
        [HttpGet("{id}")]
        public usersDto Get(int id)
        {
            var result= (from a in _eshopContext.users 
                         where a.id == id
                         select new usersDto
                         { 
                            id = a.id,
                            username=a.username,
                            password=a.password,
                            email=a.email,
                            created_at = a.created_at,
                            updated_at = a.updated_at,

                         }).SingleOrDefault();

            return result;
        }

        // POST api/<test>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<test>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<test>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
