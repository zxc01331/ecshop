using eshop.Dtos.Products;
using eshop.Models;

namespace eshop.Dtos.categories
{
    public class categoriesDto
    {
        public int id { get; set; }

        public string? category_name { get; set; }

        public virtual ICollection<productsDto> products { get; set; } = new List<productsDto>();
    }
}
