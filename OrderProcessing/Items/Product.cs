﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OrderProcessing.Items
{
    public class Product
    {
        [Key]
        //[JsonIgnore]
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }
    }
}
