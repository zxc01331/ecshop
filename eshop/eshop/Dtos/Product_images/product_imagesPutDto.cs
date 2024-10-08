namespace eshop.Dtos.Product_images
{
    public class product_imagesPutDto
    {
        public int id { get; set; }
        public int? product_id { get; set; }
        public string? image_url { get; set; }
    }
}
