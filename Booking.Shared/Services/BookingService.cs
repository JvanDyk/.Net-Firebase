namespace Booking.Shared.Contracts;

public class BookingService : IBookingService
{
    private readonly IRepository<BookingEntity> _bookingRepo;

    public BookingService(IRepository<BookingEntity> bookingRepo)
    {
        _bookingRepo = bookingRepo;
    }

    public async Task<string> CreateBookingAsync(AddBookingRequest request)
    {
        var specifiedStart = DateTime.SpecifyKind(request.Start, DateTimeKind.Utc);
        var specifiedEnd = DateTime.SpecifyKind(request.End, DateTimeKind.Utc);

        var entity = new BookingEntity()
        {
            Title = request.Title,
            Start = specifiedStart,
            End = specifiedEnd,
            Cell = request.Cell,
            Email = request.Email,
            Description = request.Description,
            IssuedEmail = request.IssuedEmail,
            AccountId = request.AccountId
        };
        return await _bookingRepo.AddAsync(entity);
    }

    public async Task<IEnumerable<BookingEntity>> ReadAllBookingsAsync(string accountId)
    {
        return await ((IBookingRepository)_bookingRepo).ReadAllBookingsAsync(accountId);
    }

    public async Task<BookingEntity> ReadBookingAsync(string id)
    {
        return await _bookingRepo.GetAsync(id);
    }

    public async Task DeleteBookingAsync(DeleteBookingRequest request)
    {
        await _bookingRepo.DeleteAsync(request.Id);
    }

    public async Task UpdateBookingAsync(PatchBookingRequest request)
    {
        var specifiedStart = DateTime.SpecifyKind(request.Start, DateTimeKind.Utc);
        var specifiedEnd = DateTime.SpecifyKind(request.End, DateTimeKind.Utc);

        var entity = new BookingEntity()
        {
            Id = request.Id,
            Title = request.Title,
            Start = specifiedStart,
            End = specifiedEnd,
            Cell = request.Cell,
            Email = request.Email,
            Description = request.Description,
            IssuedEmail = request.IssuedEmail,
            AccountId = request.AccountId
        };
        await _bookingRepo.UpdateAsync(entity);
    }
}
