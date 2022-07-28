namespace WP.User.Application.Services
{
    [UseDependencyInjection]
    public class UserAppService : IUserAppService
    {
        private readonly IMediatorHandler Bus;
        private readonly IUserRepository userRepository;

        public UserAppService(IMediatorHandler bus, IUserRepository userRepository)
        {
            this.Bus = bus;
            this.userRepository = userRepository;
        }

        public async Task<UserInfoDto> GetUserInfo(string account)
        {
            var objUser = await userRepository.GetAsync(account);
            var userInfo = objUser.Adapt<UserInfoDto>();
            return userInfo;
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

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task RemoveUserAsync(long id)
        {
            await Bus.SendCommand(new RemoveUserCommand(id));
        }

    }
}
