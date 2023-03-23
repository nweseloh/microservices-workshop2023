using Musicals.Models;

namespace Musicals.UseCases;

public interface IReservationUseCase
{
    void CreateReservation(Reservation reservation);
    Reservation Get(int id);
    IEnumerable<Reservation> GetAll();
    void ConfirmReservation(int reservationId);
}