using Nest.BaseCore.Domain;
using Nest.BaseCore.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.Repository
{
    /// <summary>
    /// 用户仓储
    /// </summary>
    public interface IUserRepository : IBaseRepository<UserInfo>
    {

    }

    public class TUserRepository : BaseRepository<UserInfo>, IUserRepository
    {
        public TUserRepository(MainContext db) : base(db) { }
    }
}
