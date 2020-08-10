using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest.BaseCore.BusinessLogic.Es;
using Nest.BaseCore.Common.BaseModel;
using Nest.BaseCore.Domain.EsModel;

namespace Nest.BaseCore.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ElasticSearchController : ControllerBase
    {
        private readonly IDocService _docService;
        public ElasticSearchController(IDocService docService)
        {
            _docService = docService;
        }

        [HttpGet]
        public ApiResultModel<bool> Insert()
        {
            var result = new ApiResultModel<bool>();
            result.Data = _docService.Insert();
            result.Code = ApiResultCode.Success;
            return result;
        }

        [HttpGet]
        public ApiResultModel<bool> InsertMany()
        {
            var result = new ApiResultModel<bool>();
            result.Data = _docService.InsertMany();
            result.Code = ApiResultCode.Success;
            return result;
        }

        [HttpGet]
        public ApiResultModel<List<DocInfoModel>> Get()
        {
            var result = new ApiResultModel<List<DocInfoModel>>();
            result.Data = _docService.Get();
            result.Code = ApiResultCode.Success;
            return result;

        }

        [HttpGet]
        public ApiResultModel<List<DocInfoModel>> GetAll()
        {
            var result = new ApiResultModel<List<DocInfoModel>>();
            result.Data = _docService.GetAll();
            result.Code = ApiResultCode.Success;
            return result;
        }

        [HttpGet]
        public ApiResultModel<bool> DeleteByQuery()
        {
            var result = new ApiResultModel<bool>();
            result.Data = _docService.DeleteByQuery();
            result.Code = ApiResultCode.Success;
            return result;

        }
    }
}
