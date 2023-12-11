using Booking.Shared.Entities.Interfaces;

namespace Booking.Shared.Entities;

[FirestoreData]
public class BookingEntity : Entity, IAccount
{
    [FirestoreProperty]
    public string IssuedEmail { get; set; }
    [FirestoreProperty]
    public string Title { get; set; }
    [FirestoreProperty]
    public string Cell { get; set; }
    [FirestoreProperty]
    public string Email { get; set; }
    [FirestoreProperty]
    public string Description { get; set; }
    [FirestoreProperty]
    public DateTime Start { get; set; }
    [FirestoreProperty]
    public DateTime End { get; set; }
    [FirestoreProperty]
    public string AccountId { get; set; }
}
