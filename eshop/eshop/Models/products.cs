using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eshop.Models;

public partial class products
{
    public int id { get; set; }

    public string? product_name { get; set; }

    public string? description { get; set; }

    public int? price { get; set; }

    public int? stock { get; set; }

    public int? category_id { get; set; }

    public int? brand_id { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual brands? brand { get; set; }

    public virtual categories? category { get; set; }

    public virtual ICollection<product_images> product_images { get; set; } = new List<product_images>();

    public virtual ICollection<products_cart_items> products_cart_items { get; set; } = new List<products_cart_items>();
}
