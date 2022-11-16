using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WP.User.Application.Contracts.Dtos.Role;

namespace WP.User.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ApiController
    {
        private readonly IRoleAppService roleAppService;

        public RoleController(IRoleAppService roleAppService)
        {
            this.roleAppService = roleAppService;
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] long id) => Result(await roleAppService.DeleteRoleAsync(id));


        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<long>> Post([FromBody] RoleCreateDto input) => CreatedResult(await roleAppService.CreateRoleAsync(input));



        /// <summary>
        /// 修改角色信息
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<long>> Put([FromRoute] long id,[FromBody] RoleUpdateDto input) => Result(await roleAppService.UpdateRoleAsync(id,input));


        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<SqlSugarPagedList<RoleDto>>> Get([FromQuery] RoleSearchPagedDto input) => await roleAppService.GetRoleListAsync(input);


    }
}
