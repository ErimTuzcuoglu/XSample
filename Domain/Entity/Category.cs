using System.Collections;
using System.Collections.Generic;
using XSample.Common;

namespace Domain.Entity
{
    public class Category: IBaseEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<SubCategory> SubCategories  { get; set; }
    }
}