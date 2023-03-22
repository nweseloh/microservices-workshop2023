using Musicals.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace Musicals.Models;

public class Reservation
{
    public int ShowId { get; set; }

    public int Tickets { get; set; }
    
    [SwaggerSchema(ReadOnly = true)]
    public int Id { get; set; }

    [SwaggerSchema(ReadOnly = true)]
    public DateTime ValidUntil { get; set; }

    [SwaggerSchema(ReadOnly = true)]
    public ReservationStatus Status { get; set; }
}