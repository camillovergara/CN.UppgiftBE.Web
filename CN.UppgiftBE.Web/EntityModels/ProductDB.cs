using System;
using LinqToDB.Mapping;

namespace CN.UppgiftBE.Web.EntityModels
{
    [Table(Name = "Products")]
    public class ProductDB
    {
        [PrimaryKey, Identity]
        [Column(Name = "Id")]
        public string Id { get; set; }

        [Column(Name = "Name")]
        public string Name { get; set; }

        [Column(Name = "Price")]
        public string Price { get; set; }
    }
}