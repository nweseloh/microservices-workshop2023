using Musicals.Models;

namespace Musicals.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly List<Reservation> _reservations = new List<Reservation>();

    public void Save(Reservation reservation)
    {
        reservation.Id = Random.Shared.Next();
        _reservations.Add(reservation);
    }

    public Reservation Get(int id)
    {
        return _reservations.Single(x => x.Id == id);
    }
}