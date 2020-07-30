using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Nest.BaseCore.Dapper
{
    /// <summary>
    /// 仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IBaseRepository<T, TKey> : IDisposable where T : class
    {
        #region 同步
        /// <summary>
        /// 通过主键获取实体对象
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        T Get(TKey id);
        /// <summary>
        /// 条件获取第一个数据
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        T Get(string conditions, object parameters = null);
        /// <summary>
        /// 获取所有的数据
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetList();
        /// <summary>
        /// 执行具有条件的查询，并将结果映射到强类型列表
        /// </summary>
        /// <param name="whereConditions">条件</param>
        /// <returns></returns>
        IEnumerable<T> GetList(object whereConditions);
        /// <summary>
        /// 带参数的查询满足条件的数据
        /// </summary>
        /// <param name="conditions">条件</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        IEnumerable<T> GetList(string conditions, object parameters = null);
        /// <summary>
        /// 使用where子句执行查询，并将结果映射到具有Paging的强类型List
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示数据</param>
        /// <param name="conditions">查询条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        IEnumerable<T> GetListPaged(int pageIndex, int pageSize, string conditions, string orderby, object parameters = null);
        /// <summary>
        /// 插入一条记录并返回主键值(自增类型返回主键值，否则返回null)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int? Insert(T entity);
        /// <summary>
        /// 更新一条数据并返回影响的行数
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>影响的行数</returns>
        int Update(T entity);
        /// <summary>
        /// 根据实体主键删除一条数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>影响的行数</returns>
        int Delete(TKey id);
        /// <summary>
        /// 根据实体删除一条数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>返回影响的行数</returns>
        int Delete(T entity);
        /// <summary>
        /// 条件删除多条记录
        /// </summary>
        /// <param name="whereConditions">条件</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns>影响的行数</returns>
        int DeleteList(object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null);
        /// <summary>
        /// 使用where子句删除多个记录
        /// </summary>
        /// <param name="conditions">wher子句</param>
        /// <param name="parameters">参数</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns>影响的行数</returns>
        int DeleteList(string conditions, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null);
        /// <summary>
        /// 满足条件的记录数量
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int RecordCount(string conditions = "", object parameters = null);

        #region 存储过程

        /// <summary>
        /// 增删改（执行存储过程）
        /// </summary>
        /// <param name="procName">过程名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="commandTimeout">超时</param>
        /// <returns></returns>
        int ExecProc(string procName, object parameters = null, int? commandTimeout = null);
        /// <summary>
        /// 查询单条（执行存储过程）
        /// </summary>
        /// <param name="procName">过程名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="commandTimeout">超时</param>
        /// <returns></returns>
        T GetByProc(string procName, object parameters = null, int? commandTimeout = null);
        /// <summary>
        /// 查询集合（执行存储过程）
        /// </summary>
        /// <param name="procName">过程名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="commandTimeout">超时</param>
        /// <returns></returns>
        IEnumerable<T> GetListByProc(string procName, object parameters = null, int? commandTimeout = null);
        /// <summary>
        /// 查询集合分页（执行存储过程）
        /// </summary>
        /// <param name="procName">过程名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="commandTimeout">超时</param>
        /// <returns></returns>
        IEnumerable<T> GetListPagedByProc(string procName, object parameters = null, int? commandTimeout = null);
        #endregion

        #endregion


        #region 异步
        /// <summary>
        /// 通过主键获取实体对象
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        Task<T> GetAsync(TKey id);
        /// <summary>
        /// 条件获取第一个
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<T> GetAsync(string conditions, object parameters = null);
        /// <summary>
        /// 获取所有的数据
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetListAsync();
        /// <summary>
        /// 执行具有条件的查询，并将结果映射到强类型列表
        /// </summary>
        /// <param name="whereConditions">条件</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetListAsync(object whereConditions);
        /// <summary>
        /// 带参数的查询满足条件的数据
        /// </summary>
        /// <param name="conditions">条件</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetListAsync(string conditions, object parameters = null);
        /// <summary>
        /// 使用where子句执行查询，并将结果映射到具有Paging的强类型List
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示数据</param>
        /// <param name="conditions">查询条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetListPagedAsync(int pageIndex, int pageSize, string conditions, string orderby, object parameters = null);
        /// <summary>
        /// 插入一条记录并返回主键值
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int?> InsertAsync(T entity);
        /// <summary>
        /// 更新一条数据并返回影响的行数
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>影响的行数</returns>
        Task<int> UpdateAsync(T entity);
        /// <summary>
        /// 根据实体主键删除一条数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>影响的行数</returns>
        Task<int> DeleteAsync(TKey id);
        /// <summary>
        /// 根据实体删除一条数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>返回影响的行数</returns>
        Task<int> DeleteAsync(T entity);
        /// <summary>
        /// 条件删除多条记录
        /// </summary>
        /// <param name="whereConditions">条件</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns>影响的行数</returns>
        Task<int> DeleteListAsync(object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null);
        /// <summary>
        /// 使用where子句删除多个记录
        /// </summary>
        /// <param name="conditions">wher子句</param>
        /// <param name="parameters">参数</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns>影响的行数</returns>
        Task<int> DeleteListAsync(string conditions, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null);
        /// <summary>
        /// 满足条件的记录数量
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<int> RecordCountAsync(string conditions = "", object parameters = null);

        #region 存储过程

        /// <summary>
        /// 增删改（执行存储过程）
        /// </summary>
        /// <param name="procName">过程名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="commandTimeout">超时</param>
        /// <returns></returns>
        Task<int> ExecProcAsync(string procName, object parameters = null, int? commandTimeout = null);
        /// <summary>
        /// 查询单条（执行存储过程）
        /// </summary>
        /// <param name="procName">过程名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="commandTimeout">超时</param>
        /// <returns></returns>
        Task<T> GetByProcAsync(string procName, object parameters = null, int? commandTimeout = null);
        /// <summary>
        /// 查询集合（执行存储过程）
        /// </summary>
        /// <param name="procName">过程名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="commandTimeout">超时</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetListByProcAsync(string procName, object parameters = null, int? commandTimeout = null);
        /// <summary>
        /// 查询集合分页（执行存储过程）
        /// </summary>
        /// <param name="procName">过程名称</param>
        /// <param name="parameters">参数</param>
        /// <param name="commandTimeout">超时</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetListPagedByProcAsync(string procName, object parameters = null, int? commandTimeout = null);

        #endregion

        #endregion
    }
}
