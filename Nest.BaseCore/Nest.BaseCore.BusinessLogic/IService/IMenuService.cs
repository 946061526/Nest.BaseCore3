using Nest.BaseCore.Common.BaseModel;
using Nest.BaseCore.Domain.RequestModel;
using Nest.BaseCore.Domain.ResponseModel;
using System.Collections.Generic;

namespace Nest.BaseCore.BusinessLogic.IService
{
    public interface IMenuService : IBaseService<BaseIdModel, AddMenuRequestModel, EditMenuRequestModel, QueryMenuRequestModel, MenuResponseModel>
    {
        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="requestModel">参数</param>
        /// <returns></returns>
        ApiResultModel<int> Add(AddMenuRequestModel requestModel);

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="requestModel">参数</param>
        /// <returns></returns>
        ApiResultModel<int> Delete(BaseIdModel requestModel);

        /// <summary>
        /// 查询菜单列表
        /// </summary>
        /// <returns></returns>
        ApiResultModel<List<MenuResponseModel>> GetList(QueryMenuRequestModel requestModel);
    }
}
