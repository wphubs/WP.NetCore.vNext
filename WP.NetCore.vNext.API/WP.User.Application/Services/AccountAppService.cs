using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.User.Application.Services;

[UseDependencyInjection]
public class AccountAppService : IAccountAppService
{
    private readonly IMediatorHandler Bus;

    public AccountAppService(IMediatorHandler bus)
    {
        this.Bus = bus;
    }

    public async Task UserAccountAsync(UserLoginDto userLoginDto)
    {

        //await Bus.RaiseEvent(new DomainNotification("", "该Name已经被使用！"));

        await Bus.SendCommand(new UserLoginCommand(userLoginDto.Account, userLoginDto.Password));


    }




}
