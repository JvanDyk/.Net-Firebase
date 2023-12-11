namespace Booking.Shared.Repositories.Interfaces;

public interface IBookingRepository
{
    Task<IEnumerable<BookingEntity>> ReadAllBookingsAsync(string accountId);
}
