using System;
using System.Collections.Generic;

namespace eshop.Models;

public partial class cart_items
{
    public int id { get; set; }

    public int? user_id { get; set; }

    public int? product_id { get; set; }

    public int? quantity { get; set; }

    public DateTime? added_at { get; set; }

    public virtual users? user { get; set; }
}
