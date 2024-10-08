using System;
using System.Collections.Generic;

namespace eshop.Models;

public partial class user_profiles
{
    public int id { get; set; }

    public int? user_id { get; set; }

    public int? gender { get; set; }

    public string? address { get; set; }

    public int? phone_number { get; set; }

    public string? avatar_url { get; set; }

    public string? perferences { get; set; }
}
