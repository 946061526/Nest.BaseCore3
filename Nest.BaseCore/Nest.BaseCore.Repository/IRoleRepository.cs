using Nest.BaseCore.Domain;
using Nest.BaseCore.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.Repository
{
    public interface IRoleRepository : IBaseRepository<Role>
    {

    }

    public class TRoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public TRoleRepository(MainContext db) : base(db) { }
    }
}
