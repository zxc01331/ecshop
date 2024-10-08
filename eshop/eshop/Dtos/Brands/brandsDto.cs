using eshop.Dtos.Products;
using eshop.Models;

namespace eshop.Dtos.brand
{
    public class brandsDto
    {
        public int id { get; set; }

        public string? brand_name { get; set; }

        public virtual ICollection<productsDto> products { get; set; } = new List<productsDto>();
    }
}
