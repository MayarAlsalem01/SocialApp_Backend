using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SocialApp.data;
using SocialApp.Domain.Entities;
using SocialApp.Dtos.PaginationDtos;
using SocialApp.Extensions;
using SocialApp.IRepository;
using System.Linq.Expressions;

namespace SocialApp.Repositories
{
    public class GenericRepository<TEntity>:IGenericRepository<TEntity> where TEntity : BaseEntity 
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> _dbset;
        private readonly IMediator _mediator;
        public GenericRepository(ApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _dbset = _context.Set<TEntity>();
            _mediator = mediator;
        }

        public void Delete(TEntity entity)
        {
            
            _dbset.Remove(entity);
            _dbset.Any(x=>x.Id==entity.Id);
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            var entity =await _dbset.FirstOrDefaultAsync(e => e.Id == id);
            return entity;
        }

        public async Task InserAsync(TEntity entity)
        {
           
            await _dbset.AddAsync(entity);

        }
        public  IIncludableQueryable<TEntity, TProperty> Include< TProperty>( Expression<Func<TEntity, TProperty>> navigationPropertyPath)
        {
            var x = _dbset.Include(navigationPropertyPath);
            return x;
        }
        public async Task SaveChangesAsync()
        {

            await _mediator.DispatchDomainEventAsync(_context);
            await _context.SaveChangesAsync();
        }

        public void Update(TEntity entity)
        {
           _dbset.Update(entity);
            
        }

        public  IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
           
            return  _dbset.Where(predicate).AsNoTracking().ToList();
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbset.Any(predicate);
        }

        public IEnumerable<TEntity> GetPagination(IEnumerable<TEntity> source,int page, int pageSize)
        {
           
            var data= source.Skip((page * pageSize) - pageSize).Take(pageSize);
            return data;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            var entities= await _dbset.ToListAsync();
            return entities ==null ? Enumerable.Empty<TEntity>() : entities;
        }

      
    }
}
