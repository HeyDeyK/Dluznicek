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
    class Category : ATable
    {
        public string Name { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]      // One to many relationship with Valuation
        public List<Polozka> Polozky { get; set; }
    }
}
