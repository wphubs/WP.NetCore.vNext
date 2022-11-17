using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Shared.Application.Contracts;
using WP.User.Application.Contracts.Dtos.Role;

namespace WP.User.Application.Services
{
    [UseDependencyInjection]
    public class RoleAppService : AbstractAppService, IRoleAppService
    {
        private readonly ISqlSugarRepository<SysRole> roleRepository;

        public RoleAppService(ISqlSugarRepository<SysRole> roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ResponseResult<long>> CreateRoleAsync(RoleCreateDto input)
        {
            if (await roleRepository.AnyAsync(x => x.Name == input.Name))
            {
                return Problem(HttpStatusCode.BadRequest, "角色名称已存在");
            }
            var objRole = input.Adapt<SysRole>();
            objRole.Id = IdGenerater.GetNextId();
            await roleRepository.InsertAsync(objRole);
            return objRole.Id;
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResponseResult> DeleteRoleAsync(long id)
        {
            if (!await roleRepository.AnyAsync(x => x.Id == id))
            {
                return Problem(HttpStatusCode.BadRequest, "角色信息不存在");
            }

            await roleRepository.SoltDeleteAsync(x => x.Id == id);
            return DefaultResult();
        }

        /// <summary>
        /// 修改角色信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResponseResult<long>> UpdateRoleAsync(long id, RoleUpdateDto input)
        {
            if (!await roleRepository.AnyAsync(x => x.Id == id))
            {
                return Problem(HttpStatusCode.BadRequest, "角色信息不存在");
            }

            var objRole = input.Adapt<SysRole>();
            objRole.Id = id;
            await roleRepository.UpdateAsync(objRole);
            return DefaultResult();
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SqlSugarPagedList<RoleDto>> GetRoleListAsync(RoleSearchPagedDto input)
        {
            var roleList = await roleRepository.AsQueryable().ToPagedListAsync(input.PageIndex, input.PageSize);
            var roleDto = roleList.Adapt<SqlSugarPagedList<RoleDto>>();
            return roleDto;
        }


    }
}
