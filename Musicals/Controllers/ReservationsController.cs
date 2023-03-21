using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Musicals.Models;
using Musicals.Repositories;

namespace Musicals.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class ReservationsController : ControllerBase
    {
        private readonly IShowsRepository _showRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly ReservationOptions _options;

        public ReservationsController(
            IShowsRepository showRepository, 
            IReservationRepository reservationRepository,
            IOptions<ReservationOptions> options
            )
        {
            _showRepository = showRepository;
            _reservationRepository = reservationRepository;
            _options = options.Value;
        }
        [HttpPost]
        //[ProducesResponseType(404)]
        public ActionResult<Reservation> Create(Reservation reservation)
        {
            var show = _showRepository.Get(reservation.ShowId);
            if (show == null)
                return base.NotFound();

            if (show.AvailableTickets - reservation.Tickets < 0 )
                return base.BadRequest();

            reservation.ValidUntil
                = DateTime.UtcNow.Add(TimeSpan.FromMinutes(_options.DurationMinutes));

            _reservationRepository.Save(reservation);

            return Ok(reservation);
        }

        [HttpGet("{id}")]
        public Reservation Get(int id)
        {
            return _reservationRepository.Get(id);
        }
    }
}