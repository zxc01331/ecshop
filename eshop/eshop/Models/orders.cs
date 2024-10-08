using System;
using System.Collections.Generic;

namespace eshop.Models;

public partial class orders
{
    public int id { get; set; }

    public int? user_id { get; set; }

    public int? total_price { get; set; }

    public string? status { get; set; }

    public string? payment_method { get; set; }

    public string? shipping_address { get; set; }

    public DateTime? create_at { get; set; }
}
