﻿using BistroWeb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BistroWeb.Application.ViewModels
{
    public class MenuItemViewModel
    {
        public IList<Item> Items { get; set; }
    }
}