namespace WP.User.Domain
{
    [UseDependencyInjection]
    public class UserCommandHandler : CommandHandler,
           IRequestHandler<UserLoginCommand, bool>,
           IRequestHandler<CreateUserCommand, bool> 
    {
        private readonly IMediatorHandler Bus;
        private readonly ISqlSugarRepository<SysUser> userRepository;

        public UserCommandHandler(IMediatorHandler bus, ISqlSugarRepository<SysUser> userRepository) : base(bus)
        {
            this.Bus = bus;
            this.userRepository = userRepository;
        }


        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="message"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Handle(UserLoginCommand message, CancellationToken cancellationToken)
        {
            // 命令验证
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }
            var objUser=await userRepository.FirstOrDefaultAsync(x => x.Account == message.Account);
            if (objUser == null)
            {
                await Bus.RaiseEvent(new DomainNotification("", "用户名或密码错误"));
                return await Task.FromResult(false);
            }
            if (InfraHelper.Hash.GetHashedString(HashType.MD5, message.Password, objUser.Salt) != objUser.Password)
            {
                await Bus.RaiseEvent(new DomainNotification("", "用户名或密码错误"));
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }


        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            //await Bus.RaiseEvent(new DomainNotification("", "该Name已经被使用！"));
            //return await Task.FromResult(false);
            if (!request.IsValid())
            {
                return await Task.FromResult(false);
            }
            var objUser = request.Adapt<SysUser>();
            objUser.Account = objUser.Account.ToLower();
            objUser.Salt = InfraHelper.Security.GenerateRandomCode(5);
            objUser.Id= IdGenerater.GetNextId();
            objUser.Password = InfraHelper.Hash.GetHashedString(HashType.MD5, objUser.Password, objUser.Salt);
            await userRepository.InsertAsync(objUser);
            return await Task.FromResult(true);
        }
    }
}
