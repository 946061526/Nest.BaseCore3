using Nest.BaseCore.BusinessLogic.IService;
using Nest.BaseCore.Common;
using Nest.BaseCore.Common.BaseModel;
using Nest.BaseCore.Common.Enums;
using Nest.BaseCore.Common.Extension;
using Nest.BaseCore.Domain.Entity;
using Nest.BaseCore.Domain.RequestModel;
using Nest.BaseCore.Domain.ResponseModel;
using Nest.BaseCore.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Nest.BaseCore.BusinessLogic.Service
{
    public class MenuService : IMenuService
    {
        //private readonly MainContext _db;
        private readonly IMenuRepository _menuRepository;

        public MenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="requestModel">参数</param>
        /// <returns></returns>
        public ApiResultModel<int> Add(AddMenuRequestModel requestModel)
        {
            var result = new ApiResultModel<int>();

            if (requestModel.MenuName.IsNullOrEmpty())
            {
                result.Message = "菜单名称不能为空";
                return result;
            }
            if (requestModel.ParentId.IsNullOrEmpty())
            {
                result.Message = "上级不能为空";
                return result;
            }
            //if (requestModel.Path.IsNullOrEmpty())
            //{
            //    result.Message = "菜单路径不能为空";
            //    return result;
            //}
            if (requestModel.Type == MenuTypeEnum.Module || requestModel.Type == MenuTypeEnum.Function)
            {
                var menu = _menuRepository.FirstOrDefault(x => x.name == requestModel.MenuName && x.type == (int)requestModel.Type);
                if (menu != null)
                {
                    result.Message = "此菜单名已存在";
                    return result;
                }
            }
            var item = new Menu()
            {
                id = GuidTool.GetGuid(),
                name = requestModel.MenuName,
                parentId = requestModel.ParentId,
                type = (int)requestModel.Type,
                sort = requestModel.Sort,
                path = requestModel.Path ?? "",
                icon = requestModel.Icon ?? "",
            };
            _menuRepository.Add(item);
            //_menuRepository.Entry(item).State = EntityState.Added;
            _menuRepository.SaveChanges();

            result.Code = ApiResultCode.Success;
            return result;
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="requestModel">参数</param>
        /// <returns></returns>
        public ApiResultModel<int> Delete(BaseIdModel requestModel)
        {
            var result = new ApiResultModel<int>();

            if (requestModel.Id.IsNullOrEmpty())
            {
                result.Message = "菜单ID不能为空";
                return result;
            }
            var menus = _menuRepository.Find(x => x.id == requestModel.Id || x.parentId == requestModel.Id).ToList();
            if (!menus.Any(x => x.id == requestModel.Id))
            {
                result.Message = "菜单不存在";
                return result;
            }
            else if (menus.Any(x => x.parentId == requestModel.Id))
            {
                result.Message = "该菜单存在子级，不能删除";
                return result;
            }
            var menu = menus.FirstOrDefault(x => x.id == requestModel.Id);
            //_db.Entry(menu).State = EntityState.Deleted;
            //_db.SaveChanges();
            _menuRepository.Delete(menu);
            _menuRepository.SaveChanges();

            result.Code = ApiResultCode.Success;
            return result;
        }

        public ApiResultModel<int> Edit(EditMenuRequestModel requestModel)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 查询菜单列表
        /// </summary>
        /// <returns></returns>
        public ApiResultModel<List<MenuResponseModel>> GetList(QueryMenuRequestModel requestModel)
        {
            var result = new ApiResultModel<List<MenuResponseModel>>();

            #region 查询条件

            var filter = PredicateBuilder.True<Menu>();
            filter = filter.And(xx => true);
            if (!requestModel.MenuName.IsNullOrEmpty())
            {
                filter = filter.And(xx => xx.name.Contains(requestModel.MenuName));
            }
            if (!requestModel.ParentId.IsNullOrEmpty())
            {
                filter = filter.And(xx => xx.parentId == requestModel.ParentId);
            }
            if (requestModel.Type.HasValue && requestModel.Type.Value != MenuTypeEnum.All)
            {
                filter = filter.And(xx => xx.type == (int)requestModel.Type.Value);
            }
            #endregion

            var list = (from m in _menuRepository.Find(filter)
                        orderby m.type, m.sort
                        select new MenuResponseModel
                        {
                            MenuId = m.id,
                            Name = m.name,
                            ParentId = m.parentId,
                            Path = m.path,
                            Icon = m.icon,
                            Sort = m.sort,
                            Type = (MenuTypeEnum)m.type
                        }).ToList();

            result.Data = list.Any() ? list : new List<MenuResponseModel>();
            result.Code = ApiResultCode.Success;
            return result;
        }

        public ApiResultModel<MenuResponseModel> GetOne(BaseIdModel idModel)
        {
            throw new System.NotImplementedException();
        }
    }
}
