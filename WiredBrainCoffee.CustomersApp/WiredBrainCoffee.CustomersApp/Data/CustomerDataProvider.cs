using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alexandria.LibraryApp.Model;

namespace Alexandria.LibraryApp.Data
{
    public interface IPatronDataProvider
    {
        Task<IEnumerable<Patron>?> GetAllAsync();
    }

    public class PatronDataProvider : IPatronDataProvider
    {
        public async Task<IEnumerable<Patron>?> GetAllAsync()
        {
            await Task.Delay(100); // fake server delay

            return new List<Patron>
            {
                new Patron { Id = 1, FirstName = "Julia", LastName = "Developer", IsDeveloper = true },
                new Patron { Id = 2, FirstName = "Alex", LastName = "Rider"},
                new Patron { Id = 3, FirstName = "Thomas Claudius", LastName = "Huber", IsDeveloper = true},
                new Patron { Id = 4, FirstName = "Anna", LastName = "Rockstar"},
                new Patron { Id = 5, FirstName = "Sara", LastName = "Metroid"},
                new Patron { Id = 6, FirstName = "Ben", LastName = "Ronaldo"}
            };
        }
    }
}
