﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interview.WebApi.Models
{
    public class CreateProductModel
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int Amount { get; set; }
    }
}
