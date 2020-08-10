using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.Domain.EsModel
{
    /// <summary>
    /// 文档实体模型
    /// </summary>
    [ElasticsearchType(IdProperty = "Id", RelationName = "doc_info")]
    public class DocInfoModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [Keyword(Index = false)]
        public string Id { get; set; } = "";
        /// <summary>
        /// 上级ID
        /// </summary>
        [Keyword(Index = false)]
        public string ParentId { get; set; } = "";
        /// <summary>
        /// 文件名
        /// </summary>
        [Keyword(Index = false)]
        public string FileName { get; set; } = "";
        /// <summary>
        /// 标题
        /// </summary>
        [Text(Index = true, Analyzer = "")]
        public string Title { get; set; } = "";
        /// <summary>
        /// 内容
        /// </summary>
        [Text(Index = true, Analyzer = "")]
        public string Content { get; set; } = "";
        /// <summary>
        /// 版本
        /// </summary>
        [Keyword(Index = false)]
        public string Version { get; set; } = "";
        /// <summary>
        /// 服务器保存的路径
        /// </summary>
        [Keyword(Index = false)]
        public string Path { get; set; } = "";
        /// <summary>
        /// 作者
        /// </summary>
        [Keyword(Index = false)]
        public string UserName { get; set; } = "";
        /// <summary>
        /// 时间
        /// </summary>
        [Keyword(Index = false)]
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
