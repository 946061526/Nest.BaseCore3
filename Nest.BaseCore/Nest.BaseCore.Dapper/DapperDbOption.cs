using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.Dapper
{
    /// <summary>
    /// 数据库连接对象
    /// 【对应appsettings.json的配置节点DapperDbOption】
    /// </summary>
    public class DapperDbOption
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        public string DbType { get; set; }
        /// <summary>
        ///  SqlServer数据库连接字符串
        /// </summary>
        public string SqlServerConn { get; set; }
        /// <summary>
        ///  MySql数据库连接字符串
        /// </summary>
        public string MySqlConn { get; set; }
    }
}
