using WP.User.Domain.Events;
using WP.User.Domain.Interfaces;

namespace WP.User.Domain
{
    [UseDependencyInjection]
    public class UserCommandHandler : CommandHandler,
           IRequestHandler<UserLoginCommand, bool>,
          IRequestHandler<RemoveUserCommand, bool>,
           IRequestHandler<CreateUserCommand, bool> 
    {
        private readonly IMediatorHandler Bus;
        private readonly ISqlSugarRepository<SysUser> userBaseRepository;
        private readonly IUserRepository userRepository;

        public UserCommandHandler(IMediatorHandler bus, ISqlSugarRepository<SysUser> userBaseRepository, IUserRepository userRepository) : base(bus)
        {
            this.Bus = bus;
            this.userBaseRepository = userBaseRepository;
            this.userRepository = userRepository;
        }



        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return await Task.FromResult(false);
            }
            if (await userBaseRepository.FirstOrDefaultAsync(x => x.Id == request.Id) == null)
            {
                NotifyValidationErrors("用户信息不存在");
                return await Task.FromResult(false);
            }
            await userRepository.RemoveUserAsync(request.Id);

            return await Task.FromResult(true);
        }


        #region 用户登录

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            // 命令验证
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return await Task.FromResult(false);
            }
            var objUser=await userBaseRepository.FirstOrDefaultAsync(x => x.Account == request.Account);
            if (objUser == null)
            {
                NotifyValidationErrors("用户名或密码错误");
                return await Task.FromResult(false);
            }
            if (InfraHelper.Hash.GetHashedString(HashType.MD5, request.Password, objUser.Salt) != objUser.Password)
            {
                NotifyValidationErrors("用户名或密码错误");
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }

        #endregion

        #region 创建用户

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return await Task.FromResult(false);
            }
            if (await userBaseRepository.FirstOrDefaultAsync(x => x.Account == request.Account)!= null)
            {
                NotifyValidationErrors("用户名已存在");
                return await Task.FromResult(false);
            }
            var objUser = request.Adapt<SysUser>();
            objUser.Account = objUser.Account.ToLower();
            objUser.Salt = InfraHelper.Security.GenerateRandomCode(5);
            objUser.Id= IdGenerater.GetNextId();
            objUser.Password = InfraHelper.Hash.GetHashedString(HashType.MD5, objUser.Password, objUser.Salt);
            await userBaseRepository.InsertAsync(objUser);
            NotifyDomainEvent(new CreatedStudentEvent(objUser.Id, objUser.Account, objUser.Name));
            return await Task.FromResult(true);
        }

        #endregion
    }
}
