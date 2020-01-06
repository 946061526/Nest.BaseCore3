using Senparc.Weixin.Entities.TemplateMessage;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using System.Collections.Generic;

namespace Nest.BaseCore.Common
{
    /// <summary>
    /// 微信模板
    /// </summary>
    public class WeixinTemplateMessage : TemplateMessageBase
    {
        /// <summary>
        /// 模板标题
        /// </summary>
        public TemplateDataItem first { get; set; }

        /// <summary>
        /// 模板数据
        /// </summary>
        public TemplateDataItem keyword1 { get; set; }

        /// <summary>
        /// 模板数据
        /// </summary>
        public TemplateDataItem keyword2 { get; set; }

        /// <summary>
        /// 模板数据
        /// </summary>
        public TemplateDataItem keyword3 { get; set; }

        /// <summary>
        /// 模板数据
        /// </summary>
        public TemplateDataItem keyword4 { get; set; }

        /// <summary>
        /// 模板数据
        /// </summary>
        public TemplateDataItem keyword5 { get; set; }

        /// <summary>
        /// 标尾
        /// </summary>
        public TemplateDataItem remark { get; set; }

        /// <summary>
        /// 微信模板构造
        /// </summary>
        /// <param name="_first">标题</param>
        /// <param name="data">模板数据</param>
        /// <param name="_remark">标尾</param>
        /// <param name="url">点击推送消息需要跳转的URL</param>
        /// <param name="templateId">模板编号</param>
        public WeixinTemplateMessage(string _first, List<string> data, string _remark, string url = null, string templateId = "")
            : base(templateId, url, "系统异常告警通知")
        {
            first = new TemplateDataItem(_first);

            var i = 1;
            foreach (var item in data)
            {
                if (i == 1)
                {
                    keyword1 = new TemplateDataItem(item);
                }
                else if (i == 2)
                {
                    keyword2 = new TemplateDataItem(item);
                }
                else if (i == 3)
                {
                    keyword3 = new TemplateDataItem(item);
                }
                else if (i == 4)
                {
                    keyword4 = new TemplateDataItem(item);
                }
                else if (i == 5)
                {
                    keyword5 = new TemplateDataItem(item);
                }
                i++;
            }

            remark = new TemplateDataItem(_remark);
        }
    }
}

