using Microsoft.AspNetCore.Mvc;
using Nest.BaseCore.Aop;
using Nest.BaseCore.BusinessLogic.IService;
using Nest.BaseCore.Common.BaseModel;
using Nest.BaseCore.Domain.Entity;
using Nest.BaseCore.Domain.RequestModel;
using Nest.BaseCore.Domain.ResponseModel;
using Nest.BaseCore.Log;
using System.Collections.Generic;

namespace Nest.BaseCore.Api.Controllers
{
    /// <summary>
    /// 用户服务
    /// </summary>
    [Route("api/[controller]")]
    //[Token]
    [ValidateSignature]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IExceptionlessLogger _Log { get; }
        private readonly IUserService _userService;
        public UserController(IExceptionlessLogger log, IUserService userService)
        {
            _Log = log;
            _userService = userService;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="requestModel">参数</param>
        [HttpPost]
        [Route("Login")]
        public ApiResultModel<LoginResponseModel> Login([FromBody]LoginRequestModel requestModel)
        {
            //throw new System.Exception("test");

            var result = _userService.Login(requestModel);
            return result;
        }

        /// <summary>
        /// 查询用户
        /// </summary>
        [HttpPost]
        [Route("GetUserList")]
        public ApiResultModel<List<LoginResponseModel>> GetUserList()
        {
            var result = new ApiResultModel<List<LoginResponseModel>>();
            result.Data = _userService.GetUserList();
            result.Code = ApiResultCode.Success;
            return result;
        }

        [HttpPost]
        [Route("Add")]
        public void Add()
        {
            var i = _userService.Add(new UserInfo());
        }

        [HttpPost]
        [Route("Update")]
        public void Update()
        {
            _userService.Update();
        }

        [HttpPost]
        [Route("Query")]
        public void Query()
        {
            _userService.Query();
        }

        [HttpPost]
        [Route("Delete")]
        public void Delete()
        {
            _userService.Delete();
        }
    }
}