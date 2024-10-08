using System;
using System.Collections.Generic;

namespace eshop.Models;

public partial class brands
{
    public int id { get; set; }

    public string? brand_name { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual ICollection<products> products { get; set; } = new List<products>();
}
