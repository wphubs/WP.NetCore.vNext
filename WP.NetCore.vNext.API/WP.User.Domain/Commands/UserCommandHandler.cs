namespace WP.User.Domain
{
    [UseDependencyInjection]
    public class UserCommandHandler : CommandHandler,
           IRequestHandler<UserLoginCommand, AppResult>,
           IRequestHandler<CreateUserCommand, AppResult> 
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
        public async Task<AppResult> Handle(UserLoginCommand message, CancellationToken cancellationToken)
        {

            // 命令验证
            if (!message.IsValid())
            {
                //NotifyValidationErrors(message);
                return Problem(HttpStatusCode.BadRequest, message.ValidationResult.Errors[0].ErrorMessage);
            }



            return await Task.FromResult(Problem(HttpStatusCode.BadRequest));
        }


        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<AppResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return Problem(HttpStatusCode.BadRequest, request.ValidationResult.Errors[0].ErrorMessage);
            }
            var objUser = request.Adapt<SysUser>();
            objUser.Account = objUser.Account.ToLower();
            objUser.Salt = InfraHelper.Security.GenerateRandomCode(5);
            objUser.Id= IdGenerater.GetNextId();
            objUser.Password = InfraHelper.Hash.GetHashedString(HashType.MD5, objUser.Password, objUser.Salt);
            await userRepository.InsertAsync(objUser);
            return DefaultResult();
        }
    }
}
