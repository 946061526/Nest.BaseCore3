using Dapper;
using Nest.BaseCore.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Nest.BaseCore.Dapper
{
    /// <summary>
    /// 仓储基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class BaseRepository<T, TKey> : IBaseRepository<T, TKey> where T : class
    {
        /// <summary>
        /// 数据库连接
        /// </summary>
        protected IDbConnection _dbConnection;

        #region 创建数据库连接
        public BaseRepository(DatabaseType databaseType)
        {
            var connSection = GetDbConnSection(databaseType);

            var ConnectionString = AppSettingsHelper.Configuration.GetSection("DapperDbOpion").GetSection(connSection).Value;
            if (string.IsNullOrEmpty(ConnectionString))
                throw new Exception("数据库连接字符串配置不能为空");

            _dbConnection = ConnectionFactory.CreateConnection(databaseType, ConnectionString);
        }

        /// <summary>
        /// 获取数据库连接节点名称
        /// </summary>
        /// <param name="databaseType"></param>
        /// <returns></returns>
        private string GetDbConnSection(DatabaseType databaseType)
        {
            var connSection = "SqlServerConn";
            switch (databaseType)
            {
                case DatabaseType.MySQL:
                    connSection = "MySqlConn";
                    break;
                case DatabaseType.Oracle:
                    connSection = "OracleConn";
                    break;
                default:
                    break;
            }
            return connSection;
        }
        #endregion


        #region 同步
        /// <summary>
        /// 查询单条（通过ID）
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public T Get(TKey id) => _dbConnection.Get<T>(id);
        /// <summary>
        /// 查询单条
        /// </summary>
        /// <param name="conditions">条件</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public T Get(string conditions, object parameters = null) => _dbConnection.QueryFirstOrDefault<T>(conditions, parameters);
        /// <summary>
        /// 查询集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetList() => _dbConnection.GetList<T>();
        /// <summary>
        /// 查询集合
        /// </summary>
        /// <param name="whereConditions">条件</param>
        /// <returns></returns>
        public IEnumerable<T> GetList(object whereConditions) => _dbConnection.GetList<T>(whereConditions);
        /// <summary>
        /// 查询集合
        /// </summary>
        /// <param name="conditions">条件</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public IEnumerable<T> GetList(string conditions, object parameters = null) => _dbConnection.GetList<T>(conditions, parameters);
        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="conditions">条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public IEnumerable<T> GetListPaged(int pageIndex, int pageSize, string conditions, string orderby, object parameters = null)
        {
            return _dbConnection.GetListPaged<T>(pageIndex, pageSize, conditions, orderby, parameters);
        }
        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public int? Insert(T entity) => _dbConnection.Insert(entity);
        /// <summary>
        /// 编辑实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public int Update(T entity) => _dbConnection.Update(entity);
        /// <summary>
        /// 删除实体（通过id）
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public int Delete(TKey id) => _dbConnection.Delete<T>(id);
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public int Delete(T entity) => _dbConnection.Delete(entity);
        /// <summary>
        /// 删除集合
        /// </summary>
        /// <param name="whereConditions">条件</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns></returns>
        public int DeleteList(object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return _dbConnection.DeleteList<T>(whereConditions, transaction, commandTimeout);
        }
        /// <summary>
        /// 删除集合
        /// </summary>
        /// <param name="conditions">条件</param>
        /// <param name="parameters">参数</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns></returns>
        public int DeleteList(string conditions, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return _dbConnection.DeleteList<T>(conditions, parameters, transaction, commandTimeout);
        }
        /// <summary>
        /// 读取记录数
        /// </summary>
        /// <param name="conditions">条件</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public int RecordCount(string conditions = "", object parameters = null)
        {
            return _dbConnection.RecordCount<T>(conditions, parameters);
        }

        #region 存储过程

        /// <summary>
        /// 增删改（执行存储过程）
        /// </summary>
        /// <param name="procName">过程名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="commandTimeout">超时</param>
        /// <returns></returns>
        public int ExecProc(string procName, object parameters = null, int? commandTimeout = null)
        {
            return _dbConnection.Execute(procName, parameters, null, commandTimeout, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 查询单条（执行存储过程）
        /// </summary>
        /// <param name="procName">过程名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="commandTimeout">超时</param>
        /// <returns></returns>
        public T GetByProc(string procName, object parameters = null, int? commandTimeout = null)
        {
            return _dbConnection.QueryFirstOrDefault<T>(procName, parameters, null, commandTimeout, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 查询集合（执行存储过程）
        /// </summary>
        /// <param name="procName">过程名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="commandTimeout">超时</param>
        /// <returns></returns>
        public IEnumerable<T> GetListByProc(string procName, object parameters = null, int? commandTimeout = null)
        {
            return _dbConnection.Query<T>(procName, parameters, null, true, commandTimeout, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 查询集合分页（执行存储过程）
        /// </summary>
        /// <param name="procName">过程名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="commandTimeout">超时</param>
        /// <returns></returns>
        public IEnumerable<T> GetListPagedByProc(string procName, object parameters = null, int? commandTimeout = null)
        {
            return _dbConnection.Query<T>(procName, parameters, null, true, commandTimeout, CommandType.StoredProcedure);
        }
        #endregion

        #endregion


        #region 异步

        /// <summary>
        /// 异步查询单条（通过ID）
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public Task<T> GetAsync(TKey id)
        {
            return _dbConnection.GetAsync<T>(id);
        }
        /// <summary>
        /// 异步查询单条
        /// </summary>
        /// <param name="conditions">条件</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public Task<T> GetAsync(string conditions, object parameters = null) => _dbConnection.QueryFirstOrDefaultAsync<T>(conditions, parameters);

        /// <summary>
        /// 异步查询集合
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<T>> GetListAsync()
        {
            return _dbConnection.GetListAsync<T>();
        }
        /// <summary>
        /// 异步查询集合
        /// </summary>
        /// <param name="whereConditions">条件</param>
        /// <returns></returns>
        public Task<IEnumerable<T>> GetListAsync(object whereConditions)
        {
            return _dbConnection.GetListAsync<T>(whereConditions);
        }
        /// <summary>
        /// 异步查询集合
        /// </summary>
        /// <param name="conditions">条件</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public Task<IEnumerable<T>> GetListAsync(string conditions, object parameters = null)
        {
            return _dbConnection.GetListAsync<T>(conditions, parameters);
        }
        /// <summary>
        /// 异步查询分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="conditions">条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public Task<IEnumerable<T>> GetListPagedAsync(int pageIndex, int pageSize, string conditions, string orderby, object parameters = null)
        {
            return _dbConnection.GetListPagedAsync<T>(pageIndex, pageSize, conditions, orderby, parameters);
        }
        /// <summary>
        /// 异步新增
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public Task<int?> InsertAsync(T entity)
        {
            return _dbConnection.InsertAsync(entity);
        }
        /// <summary>
        /// 异步编辑实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public Task<int> UpdateAsync(T entity)
        {
            return _dbConnection.UpdateAsync(entity);
        }
        /// <summary>
        /// 异步删除（通过ID）
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public Task<int> DeleteAsync(TKey id)
        {
            return _dbConnection.DeleteAsync<T>(id);
        }
        /// <summary>
        /// 异步删除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public Task<int> DeleteAsync(T entity)
        {
            return _dbConnection.DeleteAsync<T>(entity);
        }

        /// <summary>
        /// 异步删除集合
        /// </summary>
        /// <param name="whereConditions">条件</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns></returns>
        public Task<int> DeleteListAsync(object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return _dbConnection.DeleteListAsync<T>(whereConditions, transaction, commandTimeout);
        }
        /// <summary>
        /// 异步删除集合
        /// </summary>
        /// <param name="conditions">条件</param>
        /// <param name="parameters">参数</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns></returns>
        public Task<int> DeleteListAsync(string conditions, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return DeleteListAsync(conditions, parameters, transaction, commandTimeout);
        }
        /// <summary>
        /// 异步读取记录数
        /// </summary>
        /// <param name="conditions">条件</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public Task<int> RecordCountAsync(string conditions = "", object parameters = null)
        {
            return _dbConnection.RecordCountAsync<T>(conditions, parameters);
        }

        #region 存储过程

        /// <summary>
        /// 增删改（执行存储过程）
        /// </summary>
        /// <param name="procName">过程名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="commandTimeout">超时</param>
        /// <returns></returns>
        public Task<int> ExecProcAsync(string procName, object parameters = null, int? commandTimeout = null)
        {
            return _dbConnection.ExecuteAsync(procName, parameters, null, commandTimeout, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 查询单条（执行存储过程）
        /// </summary>
        /// <param name="procName">过程名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="commandTimeout">超时</param>
        /// <returns></returns>
        public Task<T> GetByProcAsync(string procName, object parameters = null, int? commandTimeout = null)
        {
            return _dbConnection.QueryFirstOrDefaultAsync<T>(procName, parameters, null, commandTimeout, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 查询集合（执行存储过程）
        /// </summary>
        /// <param name="procName">过程名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="commandTimeout">超时</param>
        /// <returns></returns>
        public Task<IEnumerable<T>> GetListByProcAsync(string procName, object parameters = null, int? commandTimeout = null)
        {
            return _dbConnection.QueryAsync<T>(procName, parameters, null, commandTimeout, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 查询集合分页（执行存储过程）
        /// </summary>
        /// <param name="procName">过程名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="commandTimeout">超时</param>
        /// <returns></returns>
        public Task<IEnumerable<T>> GetListPagedByProcAsync(string procName, object parameters = null, int? commandTimeout = null)
        {
            return _dbConnection.QueryAsync<T>(procName, parameters, null, commandTimeout, CommandType.StoredProcedure);
        }
        #endregion

        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        /// <summary>
        /// 连接释放
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                    _dbConnection?.Dispose();
                }

                disposedValue = true;
            }
        }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
