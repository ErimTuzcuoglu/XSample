using XSample.Common;

namespace Domain.Entity
{
    public class Country: IBaseEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}