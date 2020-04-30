using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using Nest.BaseCore.Aop;
using Nest.BaseCore.BusinessLogic.IService;
using Nest.BaseCore.Common.BaseModel;
using Nest.BaseCore.Domain.Entity;
using Nest.BaseCore.Domain.RequestModel;
using Nest.BaseCore.Domain.ResponseModel;
using Nest.BaseCore.Log;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        private readonly ICapPublisher _publisher;
        public IExceptionlessLogger _Log { get; }
        private readonly IUserService _userService;

        public UserController(ICapPublisher publisher, IExceptionlessLogger log, IUserService userService)
        {
            _publisher = publisher;
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

        //[HttpPost]
        //[Route("Publish")]
        //public async Task Publish()
        //{
        //    try
        //    {
        //        await _publisher.PublishAsync<string>("cap.user.queue", "test");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}
        [HttpPost]
        [Route("Publish")]
        public void Publish()
        {
            try
            {
                _publisher.Publish("cap.user.queue", "test");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        [NonAction]
        [CapSubscribe("cap.user.queue", Group = "g1")]
        public void DataSubscribe(string message)
        {
            try
            {
                Console.WriteLine(message);
            }
            catch (Exception ex)
            {
                _Log.Error("", ex.Message, "");
            }

        }
    }
}