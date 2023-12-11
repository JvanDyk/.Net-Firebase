using Booking.Shared.Models.DataTransferObjects;

namespace Booking.Shared.Services.Interfaces;

public interface IBookingService
{
    public Task<string> CreateBookingAsync(AddBookingRequest request);
    public Task<IEnumerable<BookingEntity>> ReadAllBookingsAsync(string accountId);
    public Task<BookingEntity> ReadBookingAsync(string id);
    public Task UpdateBookingAsync(PatchBookingRequest request);
    public Task DeleteBookingAsync(DeleteBookingRequest request);
}
