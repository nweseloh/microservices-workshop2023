using Microsoft.AspNetCore.Mvc;
using Musicals.Exceptions;
using Musicals.Models;
using Musicals.UseCases;
using Swashbuckle.AspNetCore.Annotations;

namespace Musicals.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationUseCase _reservationUseCase;

        public ReservationsController(
            IReservationUseCase reservationUseCase
            )
        {
            _reservationUseCase = reservationUseCase;
        }

        [HttpPost(Name="CreateReservation")]
        [SwaggerOperation(OperationId = nameof(Create))]
        [SwaggerResponse(404, Description = "Show not found")]
        [SwaggerResponse(400, Description = "Tickets not available")]
        [SwaggerResponse(201, Description = "Reservation created")]
        public ActionResult<Reservation> Create(Reservation reservation)
        {
            try
            {
                _reservationUseCase.CreateReservation(reservation);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            catch (TicketsNotAvailableException)
            {
                return BadRequest();
            }

            return Created(nameof(Get), reservation);
        }

        [HttpGet]
        [SwaggerOperation(OperationId = nameof(GetAll))]
        public IEnumerable<Reservation> GetAll()
        {
            return _reservationUseCase.GetAll();
        }

        [HttpGet("{id}")]
        [SwaggerOperation(OperationId = nameof(Get))]
        public ActionResult<Reservation> Get(int id)
        {
            try
            {
                return _reservationUseCase.Get(id);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }
    }
}