using System;
using System.Collections.Generic;

namespace eshop.Models;

public partial class order_items
{
    public int id { get; set; }

    public int? order_id { get; set; }

    public int? product_id { get; set; }

    public int? quantity { get; set; }

    public int? price { get; set; }
}
