﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.TomaInv.RechazarItem
{
    public class Ficha
    {
        public string IdToma { get; set; }
        public List<Item> Items { get; set; }
    }
}