using Booking.Shared.Models;

namespace Booking.Shared.Mapper;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<Account, AccountEntity>().ReverseMap();
    }
}
