using Microsoft.Extensions.Options;
using Musicals.Controllers;
using Musicals.Enums;
using Musicals.Exceptions;
using Musicals.Models;
using Musicals.Repositories;

namespace Musicals.UseCases;

public class ReservationUseCase : IReservationUseCase
{
    private readonly IRepository<Show> _showRepository;
    private readonly IRepository<Reservation> _reservationRepository;
    private readonly ReservationOptions _options;

    public ReservationUseCase(
        IRepository<Show> showRepository,
        IRepository<Reservation> reservationRepository,
        IOptions<ReservationOptions> options)
    {
        _showRepository = showRepository;
        _reservationRepository = reservationRepository;
        _options = options.Value;
    }
    public void CreateReservation(Reservation reservation)
    {
        Show show = _showRepository.Get(reservation.ShowId);

        if (show.AvailableTickets - reservation.Tickets < 0)
        {
            throw new TicketsNotAvailableException();
        }

        reservation.Status = ReservationStatus.Initial;

        reservation.ValidUntil
            = DateTime.UtcNow.Add(TimeSpan.FromMinutes(_options.DurationMinutes));

        _reservationRepository.Add(reservation);
    }

    public Reservation Get(int id)
    {
        return _reservationRepository.Get(id);
    }

    public IEnumerable<Reservation> GetAll()
    {
        return _reservationRepository.GetAll();
    }
}