namespace WP.User.Application.Services
{
    [UseDependencyInjection]
    public class UserAppService : IUserAppService
    {
        private readonly IMediatorHandler Bus;

        public UserAppService(IMediatorHandler bus)
        {
            this.Bus = bus;
        }


        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="userCreateOrUpdate"></param>
        /// <returns></returns>
        public async Task CreateUserAsync(UserCreateOrUpdateDto userCreateOrUpdate)
        {
           await Bus.SendCommand(new CreateUserCommand(userCreateOrUpdate.account, userCreateOrUpdate.password));
        }
    }
}
