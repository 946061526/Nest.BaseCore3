using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.Database.UnitOfWork
{
    /// <summary>
    /// 表示EF的工作单元接口，因为DbContext是EF的对象
    /// </summary>
    public interface IEFUnitOfWork : IUnitOfWorkRepositoryContext
    {
        DbContext context { get; }
    }
}
