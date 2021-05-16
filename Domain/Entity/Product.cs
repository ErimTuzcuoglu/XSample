using System;
using XSample.Common;

namespace Domain.Entity
{
    public class Product: IBaseEntity
    {
        public long Id { get; set; }
        public int SubCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string ImagePath { get; set; }
        public string Origin { get; set; }
        public long Stock { get; set; }
        public float Price { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string LastModifiedBy { get; set; }
        public bool IsDeleted { get; set; }

        public SubCategory SubCategory { get; set; }
    }
}