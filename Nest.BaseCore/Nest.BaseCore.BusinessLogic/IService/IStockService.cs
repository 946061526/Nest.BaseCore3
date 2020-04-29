using Nest.BaseCore.Common.BaseModel;
using Nest.BaseCore.Domain.Entity;

namespace Nest.BaseCore.BusinessLogic.IService
{
    public interface IStockService
    {

        ApiResultModel<int> AddStock(Stock model);
        ApiResultModel<int> AddStockCheck(StockCheck model);
    }
}
