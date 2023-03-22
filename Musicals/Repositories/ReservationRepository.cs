using Musicals.Exceptions;
using Musicals.Models;

namespace Musicals.Repositories;

public class ReservationRepository : IRepository<Reservation>
{
    private int _incrementId = 1;
    private readonly List<Reservation> _reservations = new();

    public void Add(Reservation reservation)
    {
        reservation.Id = _incrementId++;
        _reservations.Add(reservation);
    }

    public IEnumerable<Reservation> GetAll()
    {
        return _reservations;
    }

    public Reservation Get(int id)
    {
        return _reservations.SingleOrDefault(x => x.Id == id) ?? throw new EntityNotFoundException();
    }
}