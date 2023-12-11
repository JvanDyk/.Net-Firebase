using Booking.Shared.Entities;
using Booking.Shared.Firestore;
using Google.Cloud.Firestore;

namespace Booking.Shared.Repositories;

public class BookingRepository : FirestoreContext<BookingEntity>, IBookingRepository
{
    public BookingRepository(FirestoreDb db) : base("bookings", db)
    {
    }

    public async Task<IEnumerable<BookingEntity>> ReadAllBookingsAsync(string accountId)
    {
        Query query = Collection.WhereEqualTo("AccountId", accountId);
        QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

        var collectionOfBookings = new List<BookingEntity>();
        if (querySnapshot.Count != 0)
        {
            foreach (var doc in querySnapshot.Documents)
            {
                BookingEntity booking = doc.ConvertTo<BookingEntity>();
                booking.Id = doc.Id;
                collectionOfBookings.Add(booking);
            }
        }

        return collectionOfBookings;
    }
}
