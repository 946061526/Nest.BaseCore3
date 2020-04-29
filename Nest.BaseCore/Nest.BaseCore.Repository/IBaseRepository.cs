using Nest.BaseCore.Common;
using Nest.BaseCore.Common.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Nest.BaseCore.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        #region 新增

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体对象</param>
        void Add(T entity);
        /// <summary>
        /// 添加集合
        /// </summary>
        /// <param name="entity">实体对象集合</param>
        void Add(IEnumerable<T> entities);
        #endregion

        #region 删除

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键id</param>
        void Delete(object id);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">实体对象</param>
        void Delete(T entity);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="where">条件(lambda表达式)</param>
        void Delete(Expression<Func<T, bool>> where);
        /// <summary>
        /// 删除集合
        /// </summary>
        /// <param name="entity">实体对象集合</param>
        void Delete(IEnumerable<T> entities);
        #endregion

        #region 更新

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体对象</param>
        void Update(T entity);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="fields">要更新的字段</param>
        void Update(T entity, params string[] fields);
        /// <summary>
        /// 更新集合
        /// </summary>
        /// <param name="entity">实体对象集合</param>
        void Update(IEnumerable<T> entities);
        /// <summary>
        /// 更新集合
        /// </summary>
        /// <param name="entity">实体对象集合</param>
        /// <param name="fields">要更新的字段</param>
        void Update(IEnumerable<T> entities, params string[] fields);
        #endregion

        #region 查询

        /// <summary>
        /// 根据ID获取一个对象
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns>对象</returns>
        T FindById(object id);
        /// <summary>
        /// 根据条件获取一个对象
        /// </summary>
        /// <param name="where">条件(lambda表达式)</param>
        /// <returns>对象</returns>
        T FirstOrDefault(Expression<Func<T, bool>> where = null);
        /// <summary>
        /// 根据条件获取一个对象
        /// </summary>
        /// <param name="where">条件(lambda表达式)</param>
        /// <param name="orderby">排序</param>
        /// <returns></returns>
        T FirstOrDefault(Expression<Func<T, bool>> where = null, params IOrderByBuilder<T>[] orderby);
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns>所有数据</returns>
        IQueryable<T> Find();
        /// <summary>
        /// 根据条件获取数据
        /// </summary>
        /// <param name="where">条件(lambda表达式)</param>
        /// <returns>数据</returns>
        IQueryable<T> Find(Expression<Func<T, bool>> where);
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize">页数大小</param>
        /// <param name="where">查询条件</param>
        /// <param name="orderby">排序方式：new OrderByBuilder TEntity string (a => a.UserName[,true])，true=倒序，默认false正序</param>
        /// <returns></returns>
        IQueryable<T> Find(out int totalCount, int pageIndex = 1, int pageSize = 10, Expression<Func<T, bool>> where = null, params IOrderByBuilder<T>[] orderby);
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize">页数大小</param>
        /// <param name="query">linq表达式</param>
        /// <returns></returns>
        IQueryable<SEntity> Find<SEntity>(IQueryable<SEntity> query, out int totalCount, int pageIndex = 1, int pageSize = 10);
        /// <summary>
        /// 是否有指定条件的元素
        /// </summary>
        /// <param name="where">条件(lambda表达式)</param>
        /// <returns></returns>
        bool Any(Expression<Func<T, bool>> where = null);
        /// <summary>
        /// 根据条件获取记录数
        /// </summary>
        /// <param name="where">条件(lambda表达式)</param>
        /// <returns></returns>
        int Count(Expression<Func<T, bool>> where = null);
        #endregion

        #region 执行sql语句

        ///// <summary>
        ///// 执行SQL返回受影响行数
        ///// </summary>
        ///// <param name="SqlCommandText">SQL语句</param>
        ///// <param name="parameters">参数</param>
        ///// <returns>受影响行数</returns>
        //int ExecuteBySql(string SqlCommandText, params object[] parameters);
        ///// <summary>
        ///// 执行存储过程返回受影响行数
        ///// </summary>
        ///// <param name="ProcName">存储过程名称</param>
        ///// <param name="parameters">参数</param>
        ///// <returns>受影响行数</returns>
        //int ExecuteByProc(string ProcName, params object[] parameters);
        ///// <summary>
        ///// 执行sql查询
        ///// </summary>
        ///// <typeparam name="TEntity"></typeparam>
        ///// <param name="sql">sql语句</param>
        ///// <param name="parameters">参数</param>
        ///// <returns>结果集</returns>
        //IEnumerable<TEntity> FindBySql<TEntity>(string sql, params object[] parameters) where TEntity : new();
        ///// <summary>
        ///// 执行存储过程查询
        ///// </summary>
        ///// <typeparam name="TEntity"></typeparam>
        ///// <param name="procName">存储过程名称</param>
        ///// <param name="parameters">参数</param>
        ///// <returns>结果集</returns>
        //IEnumerable<TEntity> FindByProc<TEntity>(string procName, params object[] parameters) where TEntity : new();
        ///// <summary>
        ///// 批量新增
        ///// </summary>
        ///// <typeparam name="TEntity">泛型集合的类型</typeparam>
        ///// <param name="tableName">表名</param>
        ///// <param name="list">数据集合</param>
        //void BulkInsert<TEntity>(IList<TEntity> list, string tableName = "");
        #endregion

        /// <summary>
        /// 保存上下文
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
    }
}
