using Musicals.Models;

namespace Musicals.Repositories;

public interface IReservationRepository
{
    void Save(Reservation reservation);
    Reservation Get(int id);
}