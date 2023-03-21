using Musicals.Models;

namespace Musicals.Repositories;

public class ShowsRepository : IShowsRepository
{
    private readonly List<Show> _shows = new()
    {
        new() { Id = 1, Name = "Cats", AvailableTickets = 50, Price = 40 },
        new() { Id = 2, Name = "Starlight Express", AvailableTickets = 100, Price = 99 },
        new() { Id = 3, Name = "Hamilton", AvailableTickets = 3, Price = 75 },
    };
    public List<Show> GetAll()
    {
        return _shows;
    }

    public Show? Get(int id)
    {
        return _shows.SingleOrDefault(x => x.Id == id);
    }
}