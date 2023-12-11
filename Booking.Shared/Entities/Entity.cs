
using Booking.Shared.Entities.Interfaces;

namespace Booking.Shared.Entities
{
    [FirestoreData]
    public class Entity : IEntity
    {
        [FirestoreProperty]
        public string Id { get; set; }
    }
}
