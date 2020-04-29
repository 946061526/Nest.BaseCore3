using Microsoft.AspNetCore.Mvc;
using Nest.BaseCore.Aop;
using Nest.BaseCore.BusinessLogic.IService;
using Nest.BaseCore.Common.BaseModel;
using Nest.BaseCore.Domain.RequestModel;
using Nest.BaseCore.Domain.ResponseModel;
using Nest.BaseCore.Log;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nest.BaseCore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        public IExceptionlessLogger _Log { get; }
        private readonly IRoleService _roleService;
        public RoleController(IExceptionlessLogger log, IRoleService roleService)
        {
            _Log = log;
            _roleService = roleService;
        }


        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="requestModel">参数</param>
        [HttpPost]
        [Route("GetRoleList")]
        public ApiResultModel<List<RoleResponseModel>> GetRoleList()
        {
            var result = new ApiResultModel<List<RoleResponseModel>>();
            result.Data = _roleService.GetRoleList();
            return result;
        }
    }
}