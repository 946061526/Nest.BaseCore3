using Microsoft.EntityFrameworkCore;
using Nest.BaseCore.Common;
using Nest.BaseCore.Common.Extension;
using Nest.BaseCore.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace Nest.BaseCore.Repository
{
    public abstract class BaseRepository<T> where T : class
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        private MainContext _db;

        public BaseRepository(MainContext db)
        {
            _db = db;
        }

        #region 新增

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体对象</param>
        public virtual void Add(T entity)
        {
            _db.Set<T>().Add(entity);
        }
        /// <summary>
        /// 添加集合
        /// </summary>
        /// <param name="entity">实体对象集合</param>
        public virtual void Add(IEnumerable<T> entities)
        {
            foreach (var obj in entities)
            {
                _db.Set<T>().Attach(obj);
                _db.Entry(obj).State = EntityState.Added;
            }
        }
        #endregion

        #region 删除

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键id</param>
        public virtual void Delete(object id)
        {
            var obj = _db.Set<T>().Find(id);
            if (obj != null)
            {
                _db.Set<T>().Remove(obj);
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">实体对象</param>
        public virtual void Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="where">条件(lambda表达式)</param>
        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            var dataList = _db.Set<T>().Where(where).AsEnumerable();
            _db.Set<T>().RemoveRange(dataList);
        }
        /// <summary>
        /// 删除集合
        /// </summary>
        /// <param name="entity">实体对象集合</param>
        public void Delete(IEnumerable<T> entities)
        {
            //foreach (var obj in entities)
            //{
            //    //db.Set<T>().Attach(obj);
            //    _db.Entry(obj).State = EntityState.Deleted;
            //}

            _db.Set<T>().RemoveRange(entities);
        }
        #endregion

        #region 更新

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体对象</param>
        public virtual void Update(T entity)
        {
            _db.Set<T>().Attach(entity);
            _db.Entry<T>(entity).State = EntityState.Modified;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="fields">要更新的字段</param>
        public virtual void Update(T entity, params string[] fields)
        {
            if (_db.Entry(entity).State == EntityState.Detached)
            {
                _db.Set<T>().Attach(entity);
            }
            if (fields != null && fields.Length > 0)
            {
                foreach (var field in fields)
                {
                    if (!string.IsNullOrEmpty(field))
                    {
                        _db.Entry(entity).Property(field).IsModified = true;
                    }
                }
            }
            else
            {
                _db.Entry(entity).State = EntityState.Modified;
            }
        }
        /// <summary>
        /// 更新集合
        /// </summary>
        /// <param name="entity">实体对象集合</param>
        public virtual void Update(IEnumerable<T> entities)
        {
            foreach (var obj in entities)
            {
                _db.Set<T>().Attach(obj);
                _db.Entry(obj).State = EntityState.Modified;
            }
        }
        /// <summary>
        /// 更新集合
        /// </summary>
        /// <param name="entity">实体对象集合</param>
        /// <param name="fields">要更新的字段</param>
        public virtual void Update(IEnumerable<T> entities, params string[] fields)
        {
            foreach (var obj in entities)
            {
                Update(obj, fields);
            }
        }
        #endregion

        #region 查询

        /// <summary>
        /// 根据ID获取一个对象
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns>对象</returns>
        public virtual T FindById(object id)
        {
            return _db.Set<T>().Find(id);
        }
        /// <summary>
        /// 根据条件获取一个对象
        /// </summary>
        /// <param name="where">条件(lambda表达式)</param>
        /// <returns>对象</returns>
        public virtual T FirstOrDefault(Expression<Func<T, bool>> where = null)
        {
            if (where == null)
            {
                return _db.Set<T>().AsNoTracking().FirstOrDefault();
            }
            return _db.Set<T>().AsNoTracking().FirstOrDefault(where);
        }
        /// <summary>
        /// 根据条件获取一个对象
        /// </summary>
        /// <param name="where">条件(lambda表达式)</param>
        /// <param name="orderby">排序</param>
        /// <returns></returns>
        public T FirstOrDefault(Expression<Func<T, bool>> where = null, params IOrderByBuilder<T>[] orderby)
        {
            var query = _db.Set<T>().AsNoTracking();
            if (where != null)
            {
                query = query.Where(where);
            }
            if (orderby != null)
            {
                query = query.OrderBy(orderby);
            }
            return query.FirstOrDefault();
        }
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns>所有数据</returns>
        public virtual System.Linq.IQueryable<T> Find()
        {
            return _db.Set<T>().AsNoTracking();
        }
        /// <summary>
        /// 根据条件获取数据
        /// </summary>
        /// <param name="where">条件(lambda表达式)</param>
        /// <returns>数据</returns>
        public virtual System.Linq.IQueryable<T> Find(Expression<Func<T, bool>> where)
        {
            return _db.Set<T>().AsNoTracking().Where(where);
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize">页数大小</param>
        /// <param name="where">查询条件</param>
        /// <param name="orderby">排序方式：new OrderByBuilder TEntity string (a => a.UserName[,true])，true=倒序，默认false正序</param>
        /// <returns></returns>
        public IQueryable<T> Find(out int totalCount, int pageIndex = 1, int pageSize = 10, Expression<Func<T, bool>> where = null, params IOrderByBuilder<T>[] orderby)
        {
            if (pageIndex < 1) pageIndex = 1;

            var query = _db.Set<T>().AsNoTracking();
            if (where != null)
            {
                query = query.Where(where);
            }
            if (orderby != null)
            {
                query = query.OrderBy(orderby);
            }

            totalCount = query.Count();

            return query.Skip(pageSize * (pageIndex - 1)).Take(pageSize);
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize">页数大小</param>
        /// <param name="query">linq表达式</param>
        /// <returns></returns>
        public IQueryable<SEntity> Find<SEntity>(IQueryable<SEntity> query, out int totalCount, int pageIndex = 1, int pageSize = 10)
        {
            if (pageIndex < 1) pageIndex = 1;

            if (query == null)
            {
                totalCount = 0;
                return null;
            }

            totalCount = query.Count();

            return query.Skip(pageSize * (pageIndex - 1)).Take(pageSize);
        }
        /// <summary>
        /// 是否有指定条件的元素
        /// </summary>
        /// <param name="where">条件(lambda表达式)</param>
        /// <returns></returns>
        public virtual bool Any(Expression<Func<T, bool>> where = null)
        {
            if (where == null)
            {
                return _db.Set<T>().AsEnumerable().Any();
            }
            return _db.Set<T>().Where(where).AsEnumerable().Any();
        }
        /// <summary>
        /// 根据条件获取记录数
        /// </summary>
        /// <param name="where">条件(lambda表达式)</param>
        /// <returns></returns>
        public virtual int Count(Expression<Func<T, bool>> where = null)
        {
            if (where == null)
            {
                return _db.Set<T>().Count();
            }
            return _db.Set<T>().Count(where);
        }
        #endregion

        #region 执行sql语句

        ///// <summary>
        ///// 执行SQL返回受影响行数
        ///// </summary>
        ///// <param name="SqlCommandText">SQL语句</param>
        ///// <param name="parameters">参数</param>
        ///// <returns>受影响行数</returns>
        //public int ExecuteBySql(string SqlCommandText, params object[] parameters)
        //{
        //    return ExecuteNonQuery(SqlCommandText, CommandType.Text, parameters);
        //}
        ///// <summary>
        ///// 执行存储过程返回受影响行数
        ///// </summary>
        ///// <param name="ProcName">存储过程名称</param>
        ///// <param name="parameters">参数</param>
        ///// <returns>受影响行数</returns>
        //public int ExecuteByProc(string ProcName, params object[] parameters)
        //{
        //    return ExecuteNonQuery(ProcName, CommandType.StoredProcedure, parameters);
        //}
        ///// <summary>
        ///// 执行sql查询
        ///// </summary>
        ///// <typeparam name="TEntity"></typeparam>
        ///// <param name="sql">sql语句</param>
        ///// <param name="parameters">参数</param>
        ///// <returns>结果集</returns>
        //public IEnumerable<TEntity> FindBySql<TEntity>(string sql, params object[] parameters) where TEntity : new()
        //{
        //    //return ExecuteQuery<TEntity>(sql, CommandType.Text, parameters);
        //    return SqlQuery<TEntity>(sql, CommandType.Text, parameters);
        //}
        ///// <summary>
        ///// 执行存储过程查询
        ///// </summary>
        ///// <typeparam name="TEntity"></typeparam>
        ///// <param name="procName">存储过程名称</param>
        ///// <param name="parameters">参数</param>
        ///// <returns>结果集</returns>
        //public IEnumerable<TEntity> FindByProc<TEntity>(string procName, params object[] parameters) where TEntity : new()
        //{
        //    //return ExecuteQuery<TEntity>(procName, CommandType.StoredProcedure, parameters);
        //    return SqlQuery<TEntity>(procName, CommandType.StoredProcedure, parameters);
        //}
        ///// <summary>
        ///// 批量新增
        ///// </summary>
        ///// <typeparam name="TEntity">泛型集合的类型</typeparam>
        ///// <param name="tableName">表名</param>
        ///// <param name="list">数据集合</param>
        //public void BulkInsert<TEntity>(IList<TEntity> list, string tableName = "")
        //{
        //    if (list == null || !list.Any())
        //        return;
        //    if (string.IsNullOrEmpty(tableName))
        //    {
        //        Type t = typeof(TEntity);
        //        tableName = t.Name;
        //    }
        //    var connection = _db.Database.GetDbConnection().ConnectionString;
        //    using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default))
        //    {
        //        bulkCopy.BatchSize = list.Count;
        //        bulkCopy.DestinationTableName = tableName;

        //        var table = new DataTable();
        //        var props = TypeDescriptor.GetProperties(typeof(TEntity))
        //            .Cast<PropertyDescriptor>()
        //            .Where(propertyInfo => propertyInfo.PropertyType.Namespace.Equals("System"))
        //            .ToArray();

        //        foreach (var propertyInfo in props)
        //        {
        //            bulkCopy.ColumnMappings.Add(propertyInfo.Name, propertyInfo.Name);
        //            table.Columns.Add(propertyInfo.Name, Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType);
        //        }

        //        var values = new object[props.Length];
        //        foreach (var item in list)
        //        {
        //            for (var i = 0; i < values.Length; i++)
        //            {
        //                values[i] = props[i].GetValue(item);
        //            }

        //            table.Rows.Add(values);
        //        }

        //        bulkCopy.WriteToServer(table);
        //    }
        //}

        /////// <summary>
        /////// 使用SqlBulkCopy批量拷贝数据（一般使用在大量数据写入，例如：一次写入1000条）
        /////// </summary>
        /////// <param name="entitys"></param>
        ////public void BulkInsertCopyAll(IEnumerable<TEntity> entitys)
        ////{
        ////    if (entitys == null || entitys.Count() == 0)
        ////    {
        ////        return;
        ////    }
        ////    entitys = entitys.ToArray();

        ////    string cs = UnitOfWork.context.Database.Connection.ConnectionString;
        ////    using (var conn = new SqlConnection(cs))
        ////    {

        ////        Type t = typeof(TEntity);

        ////        using (var bulkCopy = new SqlBulkCopy(conn)
        ////        {
        ////            DestinationTableName = t.Name
        ////        })
        ////        {

        ////            var properties = t.GetProperties().Where(EventTypeFilter).ToArray();
        ////            var table = new DataTable();

        ////            foreach (var property in properties)
        ////            {
        ////                Type propertyType = property.PropertyType;
        ////                if (propertyType.IsGenericType &&
        ////                    propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
        ////                {
        ////                    propertyType = Nullable.GetUnderlyingType(propertyType);
        ////                }
        ////                table.Columns.Add(new DataColumn(property.Name, propertyType));
        ////            }
        ////            foreach (var entity in entitys)
        ////            {
        ////                table.Rows.Add(properties.Select(
        ////                  property => GetPropertyValue(
        ////                  property.GetValue(entity, null))).ToArray());
        ////            }

        ////            conn.Open();
        ////            bulkCopy.WriteToServer(table);
        ////        }
        ////    }
        ////}
        ////private bool EventTypeFilter(System.Reflection.PropertyInfo p)
        ////{
        ////    var keyAttribute = Attribute.GetCustomAttribute(p,
        ////        typeof(KeyAttribute)) as KeyAttribute;
        ////    if (keyAttribute == null) return true;

        ////    var generatedAttribute = Attribute.GetCustomAttribute(p,
        ////      typeof(DatabaseGeneratedAttribute)) as DatabaseGeneratedAttribute;
        ////    if (generatedAttribute == null) return true;
        ////    if (generatedAttribute.DatabaseGeneratedOption != DatabaseGeneratedOption.Identity) return true;


        ////    if (!(Attribute.GetCustomAttribute(p,
        ////        typeof(AssociationAttribute)) is AssociationAttribute attribute)) return true;

        ////    if (attribute.IsForeignKey == false) return true;

        ////    return false;
        ////}

        ////private object GetPropertyValue(object o)
        ////{
        ////    if (o == null)
        ////        return DBNull.Value;
        ////    return o;
        ////}


        //private int ExecuteNonQuery(string sql, CommandType cmdType = CommandType.Text, params object[] parameters)
        //{
        //    DbConnection connection = _db.Database.GetDbConnection();
        //    DbCommand cmd = connection.CreateCommand();
        //    int result = 0;
        //    _db.Database.OpenConnection();
        //    cmd.CommandText = sql;
        //    cmd.CommandType = cmdType;
        //    if (parameters != null)
        //    {
        //        cmd.Parameters.AddRange(parameters);
        //    }
        //    result = cmd.ExecuteNonQuery();
        //    _db.Database.CloseConnection();
        //    return result;
        //}
        //private IEnumerable<TEntity> ExecuteQuery<TEntity>(string sql, CommandType cmdType = CommandType.Text, params object[] parameters) where TEntity : new()
        //{
        //    DbConnection connection = _db.Database.GetDbConnection();
        //    DbCommand cmd = connection.CreateCommand();
        //    _db.Database.OpenConnection();
        //    cmd.CommandText = sql;
        //    cmd.CommandType = cmdType;
        //    if (parameters != null)
        //    {
        //        cmd.Parameters.AddRange(parameters);
        //    }
        //    DataTable dt = new DataTable();
        //    using (DbDataReader reader = cmd.ExecuteReader())
        //    {
        //        dt.Load(reader);
        //    }
        //    _db.Database.CloseConnection();
        //    return dt.ToEnumerable<TEntity>();
        //}
        //private IEnumerable<TEntity> SqlQuery<TEntity>(string sql, CommandType cmdType = CommandType.Text, params object[] parameters) where TEntity : new()
        //{
        //    var connection = _db.Database.GetDbConnection();
        //    using (var cmd = connection.CreateCommand())
        //    {
        //        _db.Database.OpenConnection();
        //        cmd.CommandText = sql;
        //        cmd.CommandType = cmdType;
        //        if (parameters != null)
        //        {
        //            cmd.Parameters.AddRange(parameters);
        //        }
        //        var dr = cmd.ExecuteReader();
        //        var columnSchema = dr.GetColumnSchema();
        //        var data = new List<TEntity>();
        //        while (dr.Read())
        //        {
        //            TEntity item = new TEntity();
        //            Type t = item.GetType();
        //            foreach (var kv in columnSchema)
        //            {
        //                var propertyInfo = t.GetProperty(kv.ColumnName);
        //                if (kv.ColumnOrdinal.HasValue && propertyInfo != null)
        //                {
        //                    //注意需要转换数据库中的DBNull类型
        //                    var value = dr.IsDBNull(kv.ColumnOrdinal.Value) ? null : dr.GetValue(kv.ColumnOrdinal.Value);
        //                    propertyInfo.SetValue(item, value);
        //                }
        //            }
        //            data.Add(item);
        //        }
        //        dr.Dispose();
        //        return data;
        //    }
        //}
        #endregion

        /// <summary>
        /// 保存上下文
        /// </summary>
        /// <returns></returns>
        public virtual int SaveChanges()
        {
            try
            {
                return _db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                //处理成不跟踪
                foreach (var entry in ex.Entries)
                {
                    entry.State = EntityState.Detached;
                }

                throw ex;
            }
        }
    }
}
