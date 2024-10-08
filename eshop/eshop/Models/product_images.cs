using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eshop.Models;

public partial class product_images
{
    public int id { get; set; }

    public int? product_id { get; set; }

    public string? image_url { get; set; }

    public virtual products? product { get; set; }
}
