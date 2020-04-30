using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest.BaseCore.BusinessLogic.IService;
using Nest.BaseCore.Common.BaseModel;
using Nest.BaseCore.Domain.RequestModel;
using Nest.BaseCore.Domain.ResponseModel;

namespace Nest.BaseCore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuService"></param>
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        [HttpPost]
        [Route("GetList")]
        public ApiResultModel<List<MenuResponseModel>> GetList(QueryMenuRequestModel requestModel)
        {
            return _menuService.GetList(requestModel);
        }
    }
}