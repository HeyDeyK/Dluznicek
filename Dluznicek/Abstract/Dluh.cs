using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions.Extensions;

namespace Dluznicek.Abstract
{
    class Dluh : ATable
    {
        public string Name { get; set; }
        public string Item_price { get; set; }
        public string Item_sazba { get; set; }
        public DateTime? Datum { get; set; }
        public string Stav { get; set; }

        public string aktdluzi { get; set; }
    }
}
