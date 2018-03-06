using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using SQLiteNetExtensions.Extensions;
using SQLiteNetExtensions.Attributes;



namespace Dluznicek.Abstract
{
    class Polozka : ATable
    {
        public string Name { get; set; }
        public string Item_price { get; set; }
        public DateTime? Datum { get; set; }


        [ForeignKey(typeof(Category))]
        public int CategoryID { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Category Category { get; set; }




    }

}
