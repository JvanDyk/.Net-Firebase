using Booking.Shared.Models;

namespace Booking.Shared.Contracts;

public class AccountService : IAccountService
{
    private readonly IRepository<AccountEntity> _accountRepo;
    private readonly IMapper _mapper;

    public AccountService(IRepository<AccountEntity> accountRepo, IMapper mapper)
    {
        _accountRepo = accountRepo;
        _mapper = mapper;
    }

    public async Task<string> CreateAccountAsync(Account account)
    {
        AccountEntity entity = _mapper.Map<AccountEntity>(account);
        entity.CreatedOn = DateTime.UtcNow;
        entity.UpdatedOn = DateTime.UtcNow;
        return await _accountRepo.SetAsync(entity);
    }

    public async Task<Account> ReadAccountAsync(string id)
    {
        var accountEntity = await _accountRepo.GetAsync(id);
        Account account = _mapper.Map<Account>(accountEntity);
        return account;
    }

    public async Task<IEnumerable<Account>> ReadAccountsAsync()
    {
        var accounts = await _accountRepo.GetAllAsync();
        return accounts.Select(account => _mapper.Map<Account>(account));
    }

    public async Task UpdateAccountAsync(Account account)
    {
        AccountEntity entity = _mapper.Map<AccountEntity>(account);
        await _accountRepo.UpdateAsync(entity);
    }

    public async Task DeleteAccountAsync(string id)
    {
        await _accountRepo.DeleteAsync(id);
    }
}
