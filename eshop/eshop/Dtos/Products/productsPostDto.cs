using eshop.Dtos.Product_images;
using eshop.Models;

namespace eshop.Dtos.Products;

public class productsPostDto
{

    public string? product_name { get; set; }

    public string? description { get; set; }

    public int? price { get; set; }

    public int? stock { get; set; }

    public int? category_id { get; set; }
    public string? category_name { get; set; }
    public int? brand_id { get; set; }

    public string? brand_name { get; set; }

    public virtual ICollection<product_imagesPostDto> product_images { get; set; } = new List<product_imagesPostDto>();

}
