
namespace Booking.Api.Controllers;

[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class AccountsController : Controller
{
    private readonly IAccountService _accountService;
    public AccountsController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateAccount([FromBody] Account account)
    {
        if (!string.IsNullOrEmpty(account.Email))
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            var emailIsMatch = regex.IsMatch(account.Email);
            if (!emailIsMatch)
            {
                return BadRequest("Invalid email");
            }

        }
        var accountId = await _accountService.CreateAccountAsync(account);
        return Ok(new { id = accountId });
    }

    [HttpGet("Read/{id}")]
    public async Task<IActionResult> GetAccount([FromRoute] string id)
    {
        if (String.IsNullOrEmpty(id))
        {
            return BadRequest("ID not provided");
        }

        var account = await _accountService.ReadAccountAsync(id);
        if (account != null)
        {
            return Ok(account);
        }
        return NotFound("Account not found");
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAccounts()
    {
        var account = await _accountService.ReadAccountsAsync();
        if (account != null)
        {
            return Ok(account);
        }
        return NotFound("Account not found");
    }

    [HttpPatch("Update")]
    public async Task<IActionResult> PatchBooking([FromBody] Account request)
    {
        if (string.IsNullOrEmpty(request.Id))
        {
            return BadRequest();
        }
        await _accountService.UpdateAccountAsync(request);
        return Ok();
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteAccount([FromRoute] string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("Invalid accountId");
        }

        await _accountService.DeleteAccountAsync(id);
        return Ok();
    }
}
