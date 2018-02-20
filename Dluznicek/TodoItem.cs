using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Dluznicek
{
    public class TodoItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        private string item_price { get; set; }
        private string item_name { get; set; }
        private DateTime? datum { get; set; }
        public int Done { get; set; }

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
        public TodoItem()
        {

        }
    }
}
