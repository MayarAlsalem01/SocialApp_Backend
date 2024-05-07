using MediatR;
using Microsoft.EntityFrameworkCore.Query;
using SocialApp.Domain.Entities;
using SocialApp.Dtos.PaginationDtos;
using System.Linq.Expressions;

namespace SocialApp.IRepository
{
    public interface  IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task InserAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        bool Any(Expression<Func<TEntity, bool>> predicate);
        Task SaveChangesAsync();
        Task<TEntity>GetByIdAsync(Guid id);
         IIncludableQueryable<TEntity, TProperty> Include<TProperty>(Expression<Func<TEntity, TProperty>> navigationPropertyPath);
        IEnumerable<TEntity> GetPagination(IEnumerable<TEntity>source,int page, int pageSize);
        Task<IEnumerable<TEntity>> GetAll();
     
    }
}
