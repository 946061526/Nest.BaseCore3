using System.Linq;

namespace Nest.BaseCore.Common
{
    public interface IOrderByBuilder<TEntity> where TEntity : class
    {
        IOrderedQueryable<TEntity> OrderBy(IQueryable<TEntity> query);
        IOrderedQueryable<TEntity> ThenBy(IOrderedQueryable<TEntity> query);
    }
}
