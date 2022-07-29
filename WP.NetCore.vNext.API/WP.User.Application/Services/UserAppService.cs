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


        public async Task<List<UserInfoVM>> GetUserListAsync()
        {
           var listUser= await userRepository.GetUserListAsync();
            var userInfo = listUser.Adapt<List<UserInfoVM>>();
            return userInfo;


        }

        public async Task<UserInfoVM> GetUserInfoAsync(string account)
        {
            var objUser = await userRepository.GetUserInfoAsync(account);
            var userInfo = objUser.Adapt<UserInfoVM>();
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
