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
        private string item_price { get; set; }
        private string item_name { get; set; }
        private DateTime? datum { get; set; }
        public int Done { get; set; }

        [ManyToOne]      // Many to one relationship with Stock
        public Category Category { get; set; }

        [ForeignKey(typeof(Category))]
        public int CategoryId { get; set; }
        public string Item_price
        {
            get { return item_price; }
            set { item_price = value; }
        }

        public DateTime? Datum
        {
            get { return datum; }
            set { datum = value; }
        }
        public string Item_name
        {
            get { return item_name; }
            set { item_name = value; }
        }
    }
}
