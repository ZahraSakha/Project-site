﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SakhaSite.Models.ViewModels
{
    public class PagingItem
    {
        public int PageNumber { get; set; }
        public string Display { get; set; }
        public bool Active { get; set; }
    }
}
