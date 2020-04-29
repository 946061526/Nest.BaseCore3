using Nest.BaseCore.Common.BaseModel;
using System.Collections.Generic;

namespace Nest.BaseCore.BusinessLogic.IService
{
    public interface IBaseService<TIdModel, TAddRequestModel, TEditRequestModel, TQueryRequestModel, TResponseModel>
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="requestModel">参数</param>
        /// <returns></returns>
        ApiResultModel<int> Add(TAddRequestModel requestModel);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="idModel">参数</param>
        /// <returns></returns>
        ApiResultModel<int> Delete(TIdModel idModel);

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="requestModel">参数</param>
        /// <returns></returns>
        ApiResultModel<int> Edit(TEditRequestModel requestModel);

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="idModel">参数</param>
        ApiResultModel<TResponseModel> GetOne(TIdModel idModel);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="requestModel">参数</param>
        ApiResultModel<List<TResponseModel>> GetList(TQueryRequestModel requestModel);

        ///// <summary>
        ///// 查询(分页)
        ///// </summary>
        ///// <param name="requestModel">参数</param>
        //ApiResultModel<List<TResponseModel>> GetListPage(TQueryPageRequestModel requestModel);
    }
}
