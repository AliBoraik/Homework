﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class Item
    {
        [Key] public string Operation { get; set; }

        [DisplayName("Item Value")] public double Value { get; set; }
    }
}