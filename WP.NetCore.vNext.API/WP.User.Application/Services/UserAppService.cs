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
        public async Task<ResponseResult<long>> CreateUserAsync(UserCreateDto input)
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
        /// 修改用户信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResponseResult> UpdateUserAsync(long id, UserUpdateDto input)
        {
            if (!await userRepository.AnyAsync(x => x.Id == id))
            {
                return Problem(HttpStatusCode.BadRequest, "用户信息不存在");
            }
            //if (await userRepository.AnyAsync(x => x.Account == input.Account))
            //{
            //    return Problem(HttpStatusCode.BadRequest, "账号已经存在");
            //}
            var objRole = input.Adapt<SysUser>();
            objRole.Id = id;
            await userRepository.UpdateAsync(objRole, it => new { it.Name, it.Avatar, it.Roles,it.Sex,it.Account });
            return DefaultResult();
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
            return DefaultResult();

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
        public async Task<SqlSugarPagedList<UserDto>> GetUserListAsync(UserSearchPagedDto input)
        {
           var userList=await userRepository.AsQueryable().Includes(role=>role.Roles).ToPagedListAsync(input.PageIndex, input.PageSize);
           var userDto = userList.Adapt<SqlSugarPagedList<UserDto>>();
           return userDto;
        }
    }
}
