using System.Collections;
using System.Collections.Generic;
using XSample.Common;

namespace Domain.Entity
{
    public class SubCategory : IBaseEntity<int>
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public Category Category { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}