using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBenches.Controllers
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public DateTime? BirthDate { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public ICollection<Address> Addresses { get; set; }
    }
}
