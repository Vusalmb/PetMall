﻿using PetMall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMall.ViewModels
{
    public class BasketItemVM
    {
        public Shop Shop { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
    }
}
