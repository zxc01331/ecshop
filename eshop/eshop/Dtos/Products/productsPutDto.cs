using eshop.Dtos.brand;
using eshop.Dtos.Product_images;
using eshop.Models;

namespace eshop.Dtos.Products
{
    public class productsPutDto
    {
        public int id { get; set; }
        public string? product_name { get; set; }

        public string? description { get; set; }

        public int? price { get; set; }
        public int? stock { get; set; }
        public int? category_id { get; set; }
        //public string? category_name { get; set; }

        public int? brand_id { get; set; }

        //string? brand_name { get; set; }

        //public virtual brands? brand { get; set; }

        //public virtual categories? category { get; set; }

        public virtual ICollection<product_imagesPutDto> product_images { get; set; } = new List<product_imagesPutDto>();

    }
}
