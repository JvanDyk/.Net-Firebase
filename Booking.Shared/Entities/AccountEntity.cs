using Booking.Shared.Entities.Interfaces;

namespace Booking.Shared.Entities;

[FirestoreData]
public class AccountEntity : Entity
{
    [FirestoreProperty]
    public string Email { get; set; }
    [FirestoreProperty]
    public string BookingUrl { get; set; }

    // Retrieve from Firebase authentication service,
    // you can use UserId for every entity.
    // Firebase can apply condition rules who may access what resource.
    //[FirestoreProperty]
    //public string UserId { get; set; }
    [FirestoreProperty]
    public string BusinessUrl { get; set; }
    [FirestoreProperty]
    public string Cell { get; set; }
    [FirestoreDocumentUpdateTimestamp]
    public DateTime CreatedOn { get; set; }
    [FirestoreDocumentUpdateTimestamp]
    public DateTime UpdatedOn { get; set; }
    [FirestoreProperty] 
    public string DisplayName { get; set; }
    [FirestoreProperty] 
    public string Fullname { get; set; }
    [FirestoreProperty]
    public string Description { get; set; }
}
