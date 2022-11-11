using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using WP.Shared.Application.Contracts;

namespace WP.User.Application.Services
{
    [UseDependencyInjection]
    public class UserAppService : AbstractAppService, IUserAppService
    {
        private readonly ISqlSugarRepository<SysUser> userRepository;

        public UserAppService(ISqlSugarRepository<SysUser> userRepository)
        {
            this.userRepository = userRepository;
        }


        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ResponseResult<long>> CreateUserAsync(UserCreateAndUpdateDto input)
        {
            if (await userRepository.AnyAsync(x => x.Account == input.Account))
            {
                return Problem(HttpStatusCode.BadRequest, "账号已经存在");
            }
            var objUser = input.Adapt<SysUser>();
            objUser.Account = objUser.Account.ToLower();
            objUser.Salt = InfraHelper.Security.GenerateRandomCode(5);
            objUser.Id = IdGenerater.GetNextId();
            objUser.Password = InfraHelper.Hash.GetHashedString(HashType.MD5, objUser.Password, objUser.Salt);
            await userRepository.InsertReturnIdentityAsync(objUser);
            return objUser.Id;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResponseResult> DeleteUserAsync(long id)
        {
            if (!await userRepository.AnyAsync(x => x.Id == id))
            {
                return Problem(HttpStatusCode.BadRequest, "用户信息不存在");
            }
            await userRepository.SoltDeleteAsync(x=>x.Id==id);
            return ResponseResult();

        }


        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserInfoDto> GetUserInfoAsync(long userId)
        {
            var objUser = await userRepository.FirstOrDefaultAsync(x => x.Id == userId);
            var userInfo = objUser.Adapt<UserInfoDto>();
            return userInfo;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<SqlSugarPagedList<SysUser>> GetUserListAsync(UserSearchPagedDto input)
        {
           var list=await userRepository.AsQueryable().ToPagedListAsync(input.PageIndex, input.PageSize);
           return list;
        }
    }
}
