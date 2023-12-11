namespace Booking.Shared.Models.DataTransferObjects;

public class GetBookingsRequest
{
}

public class AddBookingRequest
{
    public string Title { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string Cell { get; set; }
    public string Email { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public string IssuedEmail { get; set; }
    public string AccountId { get; set; }
}

public class PatchBookingRequest
{
    public string Id { get; set; }
    public string Title { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string Cell { get; set; }
    public string Email { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public string IssuedEmail { get; set; }
    public string AccountId { get; set; }
}

public class DeleteBookingRequest
{
    public string Id { get; set; }
    public string AccountId { get; set; }
}
