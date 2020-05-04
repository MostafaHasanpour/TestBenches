using System;
using System.Collections.Generic;

namespace TestBenches.Controllers
{
    public class MockPerson
    {
        public List<Person> People { get; set; }
        public MockPerson(int len = 1)
        {
            People = new List<Person>();
            for (int i = 0; i < len; i++)
            {
                var addresses = new List<Address>();
                for (int j = 1; j <= (DateTime.Now.Ticks % 3) + 1; j++)
                {
                    addresses.Add(new Address() { Id = j, Value = $"Address{i + 1},{j}", PersonId = i + 1 });
                }

                People.Add(new Person()
                {
                    Id = i + 1,
                    Name = $"Name{i}",
                    Family = $"Family{i}",
                    BirthDate = DateTime.Now.AddDays(-1 * (DateTime.Now.Ticks)%(80*366)),
                    CityId = (i % 4) + 1,
                    City = new City { Id = (i % 4) + 1, Name = $"City{(i % 4) + 1}" },
                    Addresses = addresses
                });

            }
        }
    }
}
