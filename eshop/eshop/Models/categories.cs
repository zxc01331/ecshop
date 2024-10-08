using System;
using System.Collections.Generic;

namespace eshop.Models;

public partial class categories
{
    public int id { get; set; }

    public string? category_name { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual ICollection<products> products { get; set; } = new List<products>();
}
