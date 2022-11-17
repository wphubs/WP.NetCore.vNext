using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using WP.Infrastructures.SqlSugar.Extensions;
using WP.Shared.Application.Contracts;

namespace WP.User.Application.Services
{
    [UseDependencyInjection]
    public class UserAppService : AbstractAppService, IUserAppService
    {
        private readonly ISqlSugarRepository<SysUser> userRepository;
        private readonly ISqlSugarRepository<SysRole> roleRepository;

        public UserAppService(ISqlSugarRepository<SysUser> userRepository, ISqlSugarRepository<SysRole> roleRepository)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
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
            var roleList = new List<SysRole>();
            input.Roles.ForEach(async item =>
            {
                roleList.Add(await roleRepository.FirstOrDefaultAsync(x => x.Id == item));
            });
            objUser.Roles = roleList;
            await userRepository.Context.InsertNav(objUser).Include(x => x.Roles, new InsertNavOptions()
            {
                ManyToManyNoDeleteMap = true//禁止清空中间表 新功能5.1.3.29-preview01

            }).ExecuteCommandAsync(); ;


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
            if (await userRepository.AnyAsync(x => x.Account == input.Account && x.Id != id))
            {
                return Problem(HttpStatusCode.BadRequest, "账号已经存在");
            }
            var objUser = input.Adapt<SysUser>();
            objUser.Id = id;
            var roleList = new List<SysRole>();
            input.Roles.ForEach(async item =>
            {
                roleList.Add(await roleRepository.FirstOrDefaultAsync(x => x.Id == item));
            });
            objUser.Roles = roleList;
            await userRepository.Context.UpdateNav(objUser, new UpdateNavRootOptions()
            {
                UpdateColumns = EntityExtensions.GetPropertiesUpdateArrary(input).ToArray()
            }).Include(x => x.Roles, new UpdateNavOptions()
            {
                ManyToManyIsUpdateA = true
            }).ExecuteCommandAsync(); ;
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
            await userRepository.SoltDeleteAsync(x => x.Id == id);
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
            var userList = await userRepository.AsQueryable()
                .WhereIF(!string.IsNullOrWhiteSpace(input.Account),x=>x.Account.Contains(input.Account))
                .WhereIF(!string.IsNullOrWhiteSpace(input.Name), x => x.Account.Contains(input.Name))
                .Includes(role => role.Roles).ToPagedListAsync(input.PageIndex, input.PageSize);
            var userDto = userList.Adapt<SqlSugarPagedList<UserDto>>();
            return userDto;
        }
    }
}
