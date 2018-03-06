﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Extensions;
using SQLiteNetExtensions.Attributes;
using Dluznicek.Abstract;

namespace Dluznicek
{
    public class TodoItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Item_price { get; set; }
        public DateTime? Datum { get; set; }
        public string Kategorie {get;set;}
        
        public TodoItem()
        {

        }
    }
}
