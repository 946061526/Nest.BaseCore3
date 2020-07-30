using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCT.Net.DDD.RepositoryDapper.Repository
{
    /// <summary>
    /// 日志仓储 实现
    /// </summary>
    public class LogInfoRepository //: BaseRepository<Log_Info, string>, ILogInfoRepository
    {
        //private IOptions<DapperDbOption> _options = null;
        //public LogInfoRepository(IOptions<DapperDbOption> options)
        //{
        //    // _dbOption = options.Get("DapperDbOption");
        //    _options = options;
        //    if (_options == null)
        //    {
        //        throw new ArgumentNullException(nameof(DapperDbOption));
        //    }
        //    _dbConnection = ConnectionFactory.CreateConnection(_options.Value.DbType, _options.Value.ConnectionString);
        //}

     //   public int Add(Log_Info model)
     //   {
     //       // return _dbConnection.Insert(model).Value;

     //       var sql = @"INSERT INTO [dbo].[Log_Info]
     //      ([Id]
     //      ,[SourceType]
     //      ,[Module]
     //      ,[ServerName]
     //      ,[Level]
     //      ,[FunctionName]
     //      ,[USER_AD]
     //      ,[InParam]
     //      ,[Short_Description]
     //      ,[Execute_Time]
     //      ,[Log_CreateTime])
     //VALUES
     //      (@Id
     //      ,@SourceType
     //      ,@Module
     //      ,@ServerName
     //      ,@Level
     //      ,@FunctionName
     //      ,@USER_AD
     //      ,@InParam
     //      ,@Short_Description
     //      ,@Execute_Time
     //      ,@Log_CreateTime)";

     //       var i = _dbConnection.Execute(sql, new
     //       {
     //           Id = model.Id,
     //           SourceType = model.SourceType,
     //           model.Module,
     //           model.ServerName,
     //           model.Level,
     //           model.FunctionName,
     //           model.User_AD,
     //           model.InParam,
     //           model.Short_Description,
     //           model.Execute_Time,
     //           model.Log_CreateTime
     //       });
     //       return i;
     //   }

     //   public async Task<int> AddAsync(Log_Info model)
     //   {
     //       // return await _dbConnection.InsertAsync(model);

     //       var sql = @"INSERT INTO [dbo].[Log_Info]
     //      ([Id]
     //      ,[SourceType]
     //      ,[Module]
     //      ,[ServerName]
     //      ,[Level]
     //      ,[FunctionName]
     //      ,[USER_AD]
     //      ,[InParam]
     //      ,[Short_Description]
     //      ,[Execute_Time]
     //      ,[Log_CreateTime])
     //VALUES
     //      (@Id
     //      ,@SourceType
     //      ,@Module
     //      ,@ServerName
     //      ,@Level
     //      ,@FunctionName
     //      ,@USER_AD
     //      ,@InParam
     //      ,@Short_Description
     //      ,@Execute_Time
     //      ,@Log_CreateTime)";

     //       return await _dbConnection.ExecuteAsync(sql, new
     //       {
     //           Id = model.Id,
     //           SourceType = model.SourceType,
     //           model.Module,
     //           model.ServerName,
     //           model.Level,
     //           model.FunctionName,
     //           model.User_AD,
     //           model.InParam,
     //           model.Short_Description,
     //           model.Execute_Time,
     //           model.Log_CreateTime
     //       });
     //   }

     //   public new int Delete(Log_Info model)
     //   {
     //       return _dbConnection.Delete(model);
     //   }

     //   public int DeleteById(string Id)
     //   {
     //       var sql = "delete [dbo].[Log_Info] where Id=@id";
     //       return _dbConnection.Execute(sql, new { Id = Id });
     //   }

     //   public async Task<int> DeleteByIdAsync(string Id)
     //   {
     //       var sql = "delete [dbo].[Log_Info] where Id=@id";
     //       return await _dbConnection.ExecuteAsync(sql, new { Id = Id });
     //   }

     //   public int Edit(Log_Info model)
     //   {
     //       return _dbConnection.Update(model);
     //   }

     //   public async Task<int> EditAsync(Log_Info model)
     //   {
     //       return await _dbConnection.UpdateAsync(model);
     //   }

     //   public Log_Info Query(string Id)
     //   {
     //       var sql = "select * from Log_Info where id=@id";
     //       return _dbConnection.QueryFirstOrDefault<Log_Info>(sql, new { id = Id });
     //   }

     //   public async Task<Log_Info> QueryAsync(string Id)
     //   {
     //       var sql = "select * from Log_Info where id=@id";
     //       return await _dbConnection.QueryFirstOrDefaultAsync<Log_Info>(sql, new { id = Id });
     //   }

     //   public List<Log_Info> QueryList()
     //   {
     //       var sql = "select top 10 * from Log_Info";
     //       return _dbConnection.Query<Log_Info>(sql).ToList();
     //   }

     //   public async Task<List<Log_Info>> QueryListAsync()
     //   {
     //       var sql = "select top 10 * from Log_Info";
     //       var result = await _dbConnection.QueryAsync<Log_Info>(sql);
     //       return result.ToList();
     //   }

     //   public List<Log_Info> QueryListPaged(int pageIndex, int pageSize)
     //   {
     //       var sql = "";
     //       var result = _dbConnection.GetListPaged<Log_Info>(pageIndex, pageSize, "", " Log_CreateTime desc ");
     //       return result.ToList();
     //   }

     //   /// <summary>
     //   /// 事务新增
     //   /// </summary>
     //   /// <param name="model">实体对象</param>
     //   /// <returns></returns>
     //   public int DoByTrans(Log_Info model)
     //   {
     //       int result = 0;
     //       string sql = @"";
     //       using (var tran = _dbConnection.BeginTransaction())
     //       {
     //           try
     //           {
     //               result = _dbConnection.Update(model, tran);

     //               //其他业务逻辑

     //               tran.Commit();
     //           }
     //           catch (Exception ex)
     //           {
     //               tran.Rollback();
     //               throw ex;
     //           }

     //       }

     //       return result;
     //   }


     //   #region 存储过程
     //   /// <summary>
     //   /// 新增
     //   /// </summary>
     //   /// <param name="model"></param>
     //   /// <returns></returns>
     //   public int AddByProc(Log_Info model)
     //   {
     //       var procName = "p_AddLog_Test";
     //       var result = base.ExecProc(procName, new
     //       {
     //           Id = model.Id,
     //           SourceType = model.SourceType,
     //           model.Module,
     //           model.ServerName,
     //           model.Level,
     //           model.FunctionName,
     //           model.User_AD,
     //           model.InParam,
     //           model.Short_Description,
     //           model.Execute_Time,
     //           model.Log_CreateTime
     //       });
     //       return result;
     //   }
     //   /// <summary>
     //   /// 查询单条
     //   /// </summary>
     //   /// <param name="Id"></param>
     //   /// <returns></returns>
     //   public Log_Info QueryByProc(string Id)
     //   {
     //       var procName = "p_GetLog_Test";
     //       var result = base.GetByProc(procName, new
     //       {
     //           Id = Id
     //       });
     //       return result;
     //   }
     //   /// <summary>
     //   /// 查询集合
     //   /// </summary>
     //   /// <param name="pageIndex"></param>
     //   /// <param name="pageSize"></param>
     //   /// <returns></returns>
     //   public List<Log_Info> QueryListByProc(int pageIndex, int pageSize)
     //   {
     //       var procName = "p_GetListLog_Test";
     //       var result = base.GetListPagedByProc(procName, new
     //       {
     //           pageIndex,
     //           pageSize
     //       });
     //       return result.ToList();
     //   }

     //   #region 异步

     //   /// <summary>
     //   /// 新增
     //   /// </summary>
     //   /// <param name="model"></param>
     //   /// <returns></returns>
     //   public async Task<int> AddByProcAsync(Log_Info model)
     //   {
     //       var procName = "p_AddLog_Test";
     //       var result = await base.ExecProcAsync(procName, new
     //       {
     //           Id = model.Id,
     //           SourceType = model.SourceType,
     //           model.Module,
     //           model.ServerName,
     //           model.Level,
     //           model.FunctionName,
     //           model.User_AD,
     //           model.InParam,
     //           model.Short_Description,
     //           model.Execute_Time,
     //           model.Log_CreateTime
     //       });
     //       return result;
     //   }
     //   /// <summary>
     //   /// 查询单条
     //   /// </summary>
     //   /// <param name="Id"></param>
     //   /// <returns></returns>
     //   public async Task<Log_Info> QueryByProcAsync(string Id)
     //   {
     //       var procName = "p_GetLog_Test";
     //       var result = await base.GetByProcAsync(procName, new
     //       {
     //           Id = Id
     //       });
     //       return result;
     //   }
     //   /// <summary>
     //   /// 查询集合
     //   /// </summary>
     //   /// <param name="pageIndex"></param>
     //   /// <param name="pageSize"></param>
     //   /// <returns></returns>
     //   public async Task<List<Log_Info>> QueryListByProcAsync(int pageIndex, int pageSize)
     //   {
     //       var procName = "p_GetListLog_Test";
     //       var result = await base.GetListPagedByProcAsync(procName, new
     //       {
     //           pageIndex,
     //           pageSize
     //       });
     //       return result.ToList();
     //   }
     //   #endregion

     //   #endregion

    }
}
