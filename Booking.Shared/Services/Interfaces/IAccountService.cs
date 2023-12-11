using Booking.Shared.Models;

namespace Booking.Shared.Services.Interfaces;

public interface IAccountService
{
    Task<string> CreateAccountAsync(Account account);
    Task<Account> ReadAccountAsync(string id);
    Task<IEnumerable<Account>> ReadAccountsAsync();
    Task UpdateAccountAsync(Account account);
    Task DeleteAccountAsync(string id);
   
}
