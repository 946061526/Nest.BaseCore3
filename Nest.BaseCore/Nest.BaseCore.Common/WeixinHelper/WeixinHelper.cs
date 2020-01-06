using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using Senparc.Weixin.MP.Containers;

namespace Nest.BaseCore.Common
{
    /// <summary>
    /// 微信辅助类
    /// </summary>
    public class WeixinHelper
    {
        public WeixinHelper() { }

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="getNewToken">是否获取新的token</param>
        /// <returns></returns>
        public static string GetAccessToken(string appId, bool getNewToken = true)
        {
            return AccessTokenContainer.GetAccessToken(appId, getNewToken);
        }

        /// <summary>
        /// 发送模板消息
        /// </summary>
        /// <param name="appId">appId</param>
        /// <param name="openId">openId</param>
        /// <param name="TemplateMessage">模板消息接口</param>
        /// <param name="miniProgram">跳小程序所需数据</param>
        /// <param name="timeOut">超时ms</param>
        /// <returns></returns>
        public static SendTemplateMessageResult SendTemplateMessage(string appId, string openId, WeixinTemplateMessage TemplateMessage, TempleteModel_MiniProgram miniProgram = null, int timeOut = 3000)
        {
            return TemplateApi.SendTemplateMessage(appId, openId, TemplateMessage, miniProgram, timeOut);
        }
    }
}
