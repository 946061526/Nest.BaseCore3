using Nest.BaseCore.Common.BaseModel;
using Nest.BaseCore.Domain.RequestModel;
using Nest.BaseCore.Domain.ResponseModel;

namespace Nest.BaseCore.BusinessLogic.IService
{
    public interface IAppTicketService
    {
        /// <summary>
        /// 生成票据
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        ApiResultModel<AddAppTicketResponseModel> GetAppTicket(AddAppTicketRequestModel requestModel);
    }
}
