using Booking.Shared.Models.DataTransferObjects;

namespace Booking.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class BookingsController : Controller
{
    private readonly IBookingService _bookingService;
    public BookingsController(IBookingService bookingService) {
        _bookingService = bookingService;
    }

    [HttpPost("Add")]
    public async Task<IActionResult> AddBooking([FromBody] AddBookingRequest request)
    {
        if(string.IsNullOrEmpty(request.AccountId)) 
        {
            return BadRequest();
        }
        var id = await _bookingService.CreateBookingAsync(request);
        return Ok(new {BookingId = id});
    }

    [HttpGet("{accountId}")]
    public async Task<IActionResult> GetBookings([FromRoute] string accountId)
    {
        var bookingCollection = await _bookingService.ReadAllBookingsAsync(accountId);
        return Ok(bookingCollection);
    }

    [HttpGet("Get/{id}")]
    public async Task<IActionResult> GetBooking([FromRoute] string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest();
        }
        var booking = await _bookingService.ReadBookingAsync(id);
        if (booking == null)
        {
            return NotFound();
        }
        return Ok(booking);
    }

    [HttpPatch("Patch")]
    public async Task<IActionResult> PatchBooking([FromBody] PatchBookingRequest request)
    {
        if (string.IsNullOrEmpty(request.AccountId) || string.IsNullOrEmpty(request.Id))
        {
            return BadRequest();
        }
        await _bookingService.UpdateBookingAsync(request);
        return Ok();
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteBookings([FromQuery] DeleteBookingRequest request)
    {
        if (string.IsNullOrEmpty(request.AccountId) || string.IsNullOrEmpty(request.Id))
        {
            return BadRequest();
        }
        await _bookingService.DeleteBookingAsync(request);
        return Ok();
    }
}
