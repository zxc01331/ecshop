using System;
using System.Collections.Generic;

namespace eshop.Models;

public partial class products_cart_items
{
    public int products_id { get; set; }

    public int cart_items_product_id { get; set; }

    public virtual products products { get; set; } = null!;
}
