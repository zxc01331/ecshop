﻿using eshop.Models;

namespace eshop.Dtos.Users
{
    public class usersDto
    {
        public int id { get; set; }

        public string? username { get; set; }

        public string? email { get; set; }

        public string? password { get; set; }

        public DateTime created_at { get; set; }

        public DateTime? updated_at { get; set; }

        public virtual ICollection<cart_items> cart_items { get; set; } = new List<cart_items>();
    }
}
