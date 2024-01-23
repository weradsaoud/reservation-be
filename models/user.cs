namespace resevation_be.models;

public class User
{
    public long UserId { get; set; }
    public string? Uid { get; set; }
    public string? Email { get; set; }
    public string? Name { get; set; }

    public IEnumerable<Reservation>? Reservations { get; set; }
}
