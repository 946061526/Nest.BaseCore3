using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nest.BaseCore.BusinessLogic.IService;
using Nest.BaseCore.Common.BaseModel;
using Nest.BaseCore.Common.Extension;
using Nest.BaseCore.Domain.RequestModel;

namespace Nest.BaseCore.Web.Controllers
{
    /// <summary>
    /// 菜单管理
    /// </summary>
    public class MenuController : Controller
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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            return View();
        }

        public IActionResult Add([FromQuery]string pId, [FromQuery]string pType, [FromQuery]string pName)
        {
            ViewBag.pId = pId;
            ViewBag.pType = pType;
            ViewBag.pName = pName;

            return View();
        }

        public IActionResult Edit([FromQuery]string id)
        {
            ViewBag.Id = id;

            return View();
        }


        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="requestModel">参数</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddAjax(AddMenuRequestModel requestModel)
        {
            var res = _menuService.Add(requestModel);
            return Content(res.ToJsonString());
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="requestModel">参数</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Delete")]
        public ApiResultModel<int> Delete(BaseIdModel requestModel)
        {
            return _menuService.Delete(requestModel);
        }

        /// <summary>
        /// 查询菜单列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetList(QueryMenuRequestModel requestModel)
        {
            var res = _menuService.GetList(requestModel);
            return Content(res.ToJsonString());
        }
    }
}