using Musicals.Exceptions;
using Musicals.Models;

namespace Musicals.Repositories;

public class ShowsRepository : IRepository<Show>
{
    private readonly List<Show> _shows = new()
    {
        new() { Id = 1, Name = "Cats", AvailableTickets = 50, Price = 40 },
        new() { Id = 2, Name = "Starlight Express", AvailableTickets = 100, Price = 99 },
        new() { Id = 3, Name = "Hamilton", AvailableTickets = 3, Price = 75 },
    };
    public IEnumerable<Show> GetAll()
    {
        return _shows;
    }
    
    public Show Get(int id)
    {
        return _shows.SingleOrDefault(x => x.Id == id) ?? throw new EntityNotFoundException();
    }

    public void Add(Show entity)
    {
        _shows.Add(entity);
    }
}