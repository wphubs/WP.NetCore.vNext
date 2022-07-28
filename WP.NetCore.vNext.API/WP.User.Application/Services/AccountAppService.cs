namespace WP.User.Application.Services;

[UseDependencyInjection]
public class AccountAppService : IAccountAppService
{
    private readonly IMediatorHandler Bus;
    private readonly IUserRepository userRepository;

    public AccountAppService(IMediatorHandler bus, IUserRepository userRepository)
    {
        this.Bus = bus;
        this.userRepository = userRepository;
    }

    public async Task<bool> UserAccountAsync(UserLoginDto userLoginDto)
    {
        return await Bus.SendCommand(new UserLoginCommand(userLoginDto.Account, userLoginDto.Password));
    }

  
}
