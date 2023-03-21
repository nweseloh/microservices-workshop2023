using Musicals.Models;

namespace Musicals.Repositories;

public interface IShowsRepository
{
    List<Show> GetAll();
    Show? Get(int id);
}