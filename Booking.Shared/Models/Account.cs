namespace Booking.Shared.Models;

public class Account
{
    public string Id { get; set; } = null;

    public string Email { get; set; }

    public string BookingUrl { get; set; }

    // Check AccountEntity
    //public string UserId { get; set; }

    public string BusinessUrl { get; set; }

    public string Cell { get; set; }

    public string DisplayName { get; set; }

    public string Fullname { get; set; }

    public string Description { get; set; }
}
