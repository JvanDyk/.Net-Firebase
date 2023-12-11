using Google.Cloud.Firestore;

namespace Booking.Shared.Firestore.Interfaces
{
    public interface IFirestoreContext
    {
        public CollectionReference Collection { get; }
    }
}
