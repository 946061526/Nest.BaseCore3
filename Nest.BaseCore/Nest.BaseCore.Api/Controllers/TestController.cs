using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using Nest.BaseCore.Aop;
using Nest.BaseCore.BusinessLogic.IService;
using Nest.BaseCore.Common;
using Nest.BaseCore.Common.BaseModel;
using Nest.BaseCore.Common.Extension;
using Nest.BaseCore.Domain.MqModel;
using Nest.BaseCore.Log;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nest.BaseCore.Api.Controllers
{
    /// <summary>
    /// 用于测试
    /// </summary>
    [InnerService]
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        //public IExceptionlessLogger _Log { get; }
        // private readonly IUserService _userService;
        private readonly ICapPublisher _publisher;

        private readonly INLogService _nLogService;

        //public TestController(IExceptionlessLogger log, ICapPublisher publisher, INLogService nLogService)
        //{
        //    _Log = log;
        //    //  _userService = userService;
        //    _publisher = publisher;
        //    _nLogService = nLogService;
        //}

        //public TestController(ICapPublisher publisher, INLogService nLogService)
        //{
        //    _publisher = publisher;
        //    _nLogService = nLogService;
        //}

        private readonly ICapService _capService;
        public TestController(ICapService capService)
        {
            _capService = capService;
        }

        /// <summary>
        /// 测试Log
        /// </summary>
        [HttpPost]
        [Route("TestLog")]
        public ApiResultModel<string> TestLog()
        {
            //Net4Logger.Debug("debug", "阿萨德法师法方为人阿萨德法师法方为人阿萨德法师法方为人", new Exception("debug"));
            //Net4Logger.Error("error", "asfsafdsfasfdsf阿萨德法师法方为人阿萨德法师法方为人阿萨德法师法方为人", new Exception("error"));
            //Net4Logger.Info("info", "1q324154354325654阿萨德法师法方为人阿萨德法师法方为人阿萨德法师法方为人", new Exception("info"));


            //nLog

            _nLogService.DebugToFile("test ", "测试一下DebugToFile", "sty", "sname", "modu", "func", "uad", "inp", "sd");
            _nLogService.InfoToFile("test ", "测试一下InfoToFile", "sty", "sname", "modu", "func", "uad", "inp", "sd");
            _nLogService.ErrorToFile(new Exception("异常信息ErrorToFile"), "test ", "测试一下ErrorToFile", "sty", "sname", "modu", "func", "uad", "inp", "sd");
            _nLogService.FatalToFile(new Exception("异常信息FatalToFile"), "test ", "测试一下FatalToFile", "sty", "sname", "modu", "func", "uad", "inp", "sd");

            //_nLogService.DebugToDB("test ", "测试一下DebugToDB", "sty", "sname", "modu", "func", "uad", "inp", "sd");
            //_nLogService.InfoToDB("test ", "测试一下InfoToDB","sty","sname","modu","func","uad","inp","sd");
            //_nLogService.ErrorToDB(new Exception("异常信息ErrorToDB"), "test ", "测试一下ErrorToDB", "sty", "sname", "modu", "func", "uad", "inp", "sd");
            //_nLogService.FatalToDB(new Exception("异常信息FatalToDB"), "test ", "测试一下FatalToDB", "sty", "sname", "modu", "func", "uad", "inp", "sd");

            //_nLogService.DebugToDbAndFile("test ", "测试一下DebugToDbAndFile", "sty", "sname", "modu", "func", "uad", "inp", "sd");
            //_nLogService.InfoToDbAndFile("test ", "测试一下InfoToDbAndFile", "sty", "sname", "modu", "func", "uad", "inp", "sd");
            //_nLogService.ErrorToDbAndFile(new Exception("异常信息ErrorToDbAndFile"), "test ", "测试一下ErrorToDbAndFile", "sty", "sname", "modu", "func", "uad", "inp", "sd");
            //_nLogService.FatalToDbAndFile(new Exception("异常信息FatalToDbAndFile"), "test ", "测试一下FatalToDbAndFile", "sty", "sname", "modu", "func", "uad", "inp", "sd");

            return new ApiResultModel<string>();
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetTimestamp")]
        public ApiResultModel<string> GetTimestamp()
        {
            return new ApiResultModel<string>() { Code = ApiResultCode.Success, Data = Utils.GetTimestamp() };
        }



        #region  生成签名

        ///// <summary>
        ///// 获取签名1（简单参数）
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //[Route("GetSign1")]
        //public ApiResultModel<TestSignResponseModel> GetSign1([FromBody]TestSignRequestModel1 requestModel)
        //{
        //    var secret = "";
        //    var ticket = HttpContext.Request.Headers["Ticket"].ToString();
        //    if (!string.IsNullOrEmpty(ticket) && ticket != "Ticket")
        //    {
        //        var redisKey = RedisCommon.GetTicketKey(ticket);
        //        var redisData = RedisClient.Get<AppTicketModel>(RedisDatabase.DB_AuthorityService, redisKey);
        //        if (redisData != null)
        //            secret = redisData.AppSecret;
        //    }
        //    else
        //    {
        //        secret = AppSettingsHelper.Configuration["ApiConfig:SignDefaultKey"];
        //    }
        //    HttpContext.Request.Body.Position = 0;
        //    Stream stream = HttpContext.Request.Body;
        //    byte[] buffer = new byte[HttpContext.Request.ContentLength.Value];
        //    stream.Read(buffer, 0, buffer.Length);

        //    var bodyStr = Encoding.UTF8.GetString(buffer);
        //    Dictionary<string, object> dictionary = JsonHelper.DeserializeObject<Dictionary<string, object>>(bodyStr);
        //    dictionary.Remove("sign");
        //    var keys = dictionary.Keys.ToList();
        //    foreach (var key in keys)
        //    {
        //        //参数为集合类型
        //        var value = dictionary[key];
        //        if (value != null && value.GetType().Namespace == "Newtonsoft.Json.Linq")
        //        {
        //            dictionary[key] = JsonHelper.SerializeObject(value);
        //        }
        //    }
        //    var sign = AuthenticationHelper.GetSign(dictionary, secret, out string parms);

        //    return new ApiResultModel<TestSignResponseModel>() { Code = ApiResultCode.Success, Data = new TestSignResponseModel() { Params = parms, Sign = sign } };
        //}

        ///// <summary>
        ///// 获取签名2（参数包含对象）
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //[Route("GetSign2")]
        //public ApiResultModel<TestSignResponseModel> GetSign2([FromBody]TestSignRequestModel2 requestModel)
        //{
        //    var secret = "";
        //    var ticket = HttpContext.Request.Headers["Ticket"].ToString();
        //    if (!string.IsNullOrEmpty(ticket) && ticket != "Ticket")
        //    {
        //        var redisKey = RedisCommon.GetTicketKey(ticket);
        //        var redisData = RedisClient.Get<AppTicketModel>(RedisDatabase.DB_AuthorityService, redisKey);
        //        if (redisData != null)
        //            secret = redisData.AppSecret;
        //    }
        //    else
        //    {
        //        secret = AppSettingsHelper.Configuration["ApiConfig:SignDefaultKey"];
        //    }
        //    HttpContext.Request.Body.Position = 0;
        //    Stream stream = HttpContext.Request.Body;
        //    byte[] buffer = new byte[HttpContext.Request.ContentLength.Value];
        //    stream.Read(buffer, 0, buffer.Length);

        //    var bodyStr = Encoding.UTF8.GetString(buffer);
        //    Dictionary<string, object> dictionary = JsonHelper.DeserializeObject<Dictionary<string, object>>(bodyStr);
        //    dictionary.Remove("sign");
        //    var keys = dictionary.Keys.ToList();
        //    foreach (var key in keys)
        //    {
        //        //参数为集合类型
        //        var value = dictionary[key];
        //        if (value != null && value.GetType().Namespace == "Newtonsoft.Json.Linq")
        //        {
        //            dictionary[key] = JsonHelper.SerializeObject(value);
        //        }
        //    }
        //    var sign = AuthenticationHelper.GetSign(dictionary, secret, out string parms);

        //    return new ApiResultModel<TestSignResponseModel>() { Code = ApiResultCode.Success, Data = new TestSignResponseModel() { Params = parms, Sign = sign } };
        //}

        ///// <summary>
        ///// 获取签名3（参数包含集合）
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //[Route("GetSign3")]
        //public ApiResultModel<TestSignResponseModel> GetSign3([FromBody]TestSignRequestModel3 requestModel)
        //{
        //    var secret = "";
        //    var ticket = HttpContext.Request.Headers["Ticket"].ToString();
        //    if (!string.IsNullOrEmpty(ticket) && ticket != "Ticket")
        //    {
        //        var redisKey = RedisCommon.GetTicketKey(ticket);
        //        var redisData = RedisClient.Get<AppTicketModel>(RedisDatabase.DB_AuthorityService, redisKey);
        //        if (redisData != null)
        //            secret = redisData.AppSecret;
        //    }
        //    else
        //    {
        //        secret = AppSettingsHelper.Configuration["ApiConfig:SignDefaultKey"];
        //    }
        //    HttpContext.Request.Body.Position = 0;
        //    Stream stream = HttpContext.Request.Body;
        //    byte[] buffer = new byte[HttpContext.Request.ContentLength.Value];
        //    stream.Read(buffer, 0, buffer.Length);

        //    var bodyStr = Encoding.UTF8.GetString(buffer);
        //    Dictionary<string, object> dictionary = JsonHelper.DeserializeObject<Dictionary<string, object>>(bodyStr);
        //    dictionary.Remove("sign");
        //    var keys = dictionary.Keys.ToList();
        //    foreach (var key in keys)
        //    {
        //        //参数为集合类型
        //        var value = dictionary[key];
        //        if (value != null && value.GetType().Namespace == "Newtonsoft.Json.Linq")
        //        {
        //            dictionary[key] = JsonHelper.SerializeObject(value);
        //        }
        //    }
        //    var sign = AuthenticationHelper.GetSign(dictionary, secret, out string parms);

        //    return new ApiResultModel<TestSignResponseModel>() { Code = ApiResultCode.Success, Data = new TestSignResponseModel() { Params = parms, Sign = sign } };
        //}

        #endregion


        #region 验证签名

        ///// <summary>
        ///// 验证签名1（简单参数）
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //[Route("CheckSign1")]
        //public ApiResultModel<bool> CheckSign1([FromBody]TestSignRequestModel1 requestModel)
        //{
        //    var result = new ApiResultModel<bool>();
        //    var secret = "";
        //    var ticket = HttpContext.Request.Headers["Ticket"].ToString();
        //    if (!string.IsNullOrEmpty(ticket) && ticket != "Ticket")
        //    {
        //        var redisKey = RedisCommon.GetTicketKey(ticket);
        //        var redisData = RedisClient.Get<AppTicketModel>(RedisDatabase.DB_AuthorityService, redisKey);
        //        if (redisData != null)
        //            secret = redisData.AppSecret;
        //    }
        //    else
        //    {
        //        secret = AppSettingsHelper.Configuration["ApiConfig:SignDefaultKey"];
        //    }
        //    HttpContext.Request.Body.Position = 0;
        //    Stream stream = HttpContext.Request.Body;
        //    byte[] buffer = new byte[HttpContext.Request.ContentLength.Value];
        //    stream.Read(buffer, 0, buffer.Length);

        //    var bodyStr = Encoding.UTF8.GetString(buffer);
        //    Dictionary<string, object> dictionary = JsonHelper.DeserializeObject<Dictionary<string, object>>(bodyStr);
        //    var pSign = dictionary["sign"].ToString();
        //    dictionary.Remove("sign");
        //    var keys = dictionary.Keys.ToList();
        //    foreach (var key in keys)
        //    {
        //        //参数为集合类型
        //        var value = dictionary[key];
        //        if (value != null && value.GetType().Namespace == "Newtonsoft.Json.Linq")
        //        {
        //            dictionary[key] = JsonHelper.SerializeObject(value);
        //        }
        //    }
        //    var sign = AuthenticationHelper.GetSign(dictionary, secret, out string parms);
        //    if (pSign == sign)
        //    {
        //        result.Code = ApiResultCode.Success;
        //        result.Data = true;
        //    }
        //    return result;
        //}

        ///// <summary>
        ///// 验证签名2（参数包含对象）
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //[Route("CheckSign2")]
        //public ApiResultModel<bool> CheckSign2([FromBody]TestSignRequestModel2 requestModel)
        //{
        //    var result = new ApiResultModel<bool>();
        //    var secret = "";
        //    var ticket = HttpContext.Request.Headers["Ticket"].ToString();
        //    if (!string.IsNullOrEmpty(ticket) && ticket != "Ticket")
        //    {
        //        var redisKey = RedisCommon.GetTicketKey(ticket);
        //        var redisData = RedisClient.Get<AppTicketModel>(RedisDatabase.DB_AuthorityService, redisKey);
        //        if (redisData != null)
        //            secret = redisData.AppSecret;
        //    }
        //    else
        //    {
        //        secret = AppSettingsHelper.Configuration["ApiConfig:SignDefaultKey"];
        //    }
        //    HttpContext.Request.Body.Position = 0;
        //    Stream stream = HttpContext.Request.Body;
        //    byte[] buffer = new byte[HttpContext.Request.ContentLength.Value];
        //    stream.Read(buffer, 0, buffer.Length);

        //    var bodyStr = Encoding.UTF8.GetString(buffer);
        //    Dictionary<string, object> dictionary = JsonHelper.DeserializeObject<Dictionary<string, object>>(bodyStr);
        //    var pSign = dictionary["sign"].ToString();
        //    dictionary.Remove("sign");
        //    var keys = dictionary.Keys.ToList();
        //    foreach (var key in keys)
        //    {
        //        //参数为集合类型
        //        var value = dictionary[key];
        //        if (value != null && value.GetType().Namespace == "Newtonsoft.Json.Linq")
        //        {
        //            dictionary[key] = JsonHelper.SerializeObject(value);
        //        }
        //    }
        //    var sign = AuthenticationHelper.GetSign(dictionary, secret, out string parms);
        //    if (pSign == sign)
        //    {
        //        result.Code = ApiResultCode.Success;
        //        result.Data = true;
        //    }
        //    return result;
        //}

        ///// <summary>
        ///// 验证签名3（参数包含集合）
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //[Route("CheckSign3")]
        //public ApiResultModel<bool> CheckSign3([FromBody]TestSignRequestModel3 requestModel)
        //{
        //    var result = new ApiResultModel<bool>();
        //    var secret = "";
        //    var ticket = HttpContext.Request.Headers["Ticket"].ToString();
        //    if (!string.IsNullOrEmpty(ticket) && ticket != "Ticket")
        //    {
        //        var redisKey = RedisCommon.GetTicketKey(ticket);
        //        var redisData = RedisClient.Get<AppTicketModel>(RedisDatabase.DB_AuthorityService, redisKey);
        //        if (redisData != null)
        //            secret = redisData.AppSecret;
        //    }
        //    else
        //    {
        //        secret = AppSettingsHelper.Configuration["ApiConfig:SignDefaultKey"];
        //    }
        //    HttpContext.Request.Body.Position = 0;
        //    Stream stream = HttpContext.Request.Body;
        //    byte[] buffer = new byte[HttpContext.Request.ContentLength.Value];
        //    stream.Read(buffer, 0, buffer.Length);

        //    var bodyStr = Encoding.UTF8.GetString(buffer);
        //    Dictionary<string, object> dictionary = JsonHelper.DeserializeObject<Dictionary<string, object>>(bodyStr);
        //    var pSign = dictionary["sign"].ToString();
        //    dictionary.Remove("sign");
        //    var keys = dictionary.Keys.ToList();
        //    foreach (var key in keys)
        //    {
        //        //参数为集合类型
        //        var value = dictionary[key];
        //        if (value != null && value.GetType().Namespace == "Newtonsoft.Json.Linq")
        //        {
        //            dictionary[key] = JsonHelper.SerializeObject(value);
        //        }
        //    }
        //    var sign = AuthenticationHelper.GetSign(dictionary, secret, out string parms);
        //    if (pSign == sign)
        //    {
        //        result.Code = ApiResultCode.Success;
        //        result.Data = true;
        //    }
        //    return result;
        //}

        #endregion


        /// <summary>
        /// 测试签名 key=6424cb5a2d99e2128d234f7cf3527c7d
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [InnerService]
        public Dictionary<string, object> TestSign([FromBody] Dictionary<string, object> paramModel, string appKey = "6424cb5a2d99e2128d234f7cf3527c7d", bool isGetTimestamp = true, bool isShowParamStr = false)
        {
            Dictionary<string, object> dirPre = paramModel;//待签名字典
            if (isGetTimestamp)
            {
                if (dirPre.ContainsKey("timestamp"))
                {
                    dirPre.Remove("timestamp");
                }
                dirPre.Add("timestamp", DateTime.Now.ToTimeSpan().ToString());
            }
            if (!dirPre.ContainsKey("nonce") || dirPre["nonce"].IsNull() || dirPre["nonce"].ToString() == "string")
            {
                dirPre.Remove("nonce");
                dirPre.Add("nonce", Guid.NewGuid().GetHashCode().ToString("x"));
            }
            if (dirPre.ContainsKey("sign"))
            {
                dirPre.Remove("sign");
            }
            dirPre.Add("sign", AuthenticationHelper.GetSign(dirPre, appKey, out string param).ToUpper());

            Dictionary<string, object> newDirPre = new Dictionary<string, object>();
            foreach (var dic in dirPre)
            {
                newDirPre.Add(dic.Key, dic.Value);
            }

            if (isShowParamStr)
            {
                newDirPre = new Dictionary<string, object>
                {
                    { "Res", dirPre },
                    { "ParamStr", param }
                };
            }

            return newDirPre;
        }

        #region CAP消息（RabbitMQ）

        /// <summary>
        /// 测试CAP发送消息
        /// </summary>
        [HttpPost]
        [Route("TestCap_SendMsg")]
        public async Task TestCap_SendMsg()
        {
            IMqModel model = new TestMqModel()
            {
                Id = Guid.NewGuid().ToString(),
                Title = "测试mq",
                Time = DateTime.Now
            };
            await _capService.SendAsync("mqKey.test.log", model);
        }

        #endregion

    }

    #region 签名实体

    /// <summary>
    /// 签名测试请求参数1
    /// </summary>
    public class TestSignRequestModel1 : BaseRequestModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    /// <summary>
    /// 签名测试请求参数2
    /// </summary>
    public class TestSignRequestModel2 : TestSignRequestModel1
    {
        public TestSignNode Nodes { get; set; } = new TestSignNode();
    }

    /// <summary>
    /// 签名测试请求参数3
    /// </summary>
    public class TestSignRequestModel3 : TestSignRequestModel1
    {
        public List<TestSignNode> Nodes { get; set; } = new List<TestSignNode>();
    }

    public class TestSignNode
    {
        /// <summary>
        /// 参数1
        /// </summary>
        public string P1 { get; set; }
        /// <summary>
        /// 参数2
        /// </summary>
        public string P2 { get; set; }
    }

    public class TestSignResponseModel
    {
        public string Params { get; set; }
        public string Sign { get; set; }
    }

    #endregion

}