using Nest.BaseCore.Common.BaseModel;
using Nest.BaseCore.Domain.RequestModel;
using Nest.BaseCore.Domain.ResponseModel;
using System.Collections.Generic;

namespace Nest.BaseCore.BusinessLogic.IService
{
    public interface IRoleService : IBaseService<BaseIdModel, AddRoleRequestModel, EditRoleRequestModel, QueryRoleRequestModel, QueryRoleResponseModel>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="requestModel">参数</param>
        List<RoleResponseModel> GetRoleList();
    }
}
