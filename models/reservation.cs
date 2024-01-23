namespace resevation_be.models;
public class Reservation
{
    public long ReservationId { get; set; }
    public string? Name { get; set; }
    public DateTime ReservationDate { get; set; }

    public long UserId { get; set; }
    public User? User { get; set; }
}