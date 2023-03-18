using AutoMapper;
using AutoMapper.QueryableExtensions;
using Certificados.Domain.Core.DTO;
using Certificados.Domain.Core.Entities;
using Certificados.Domain.Core.Interfaces;
using Certificados.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Certificados.Infra.Core.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly IMapper _mapper;
        protected readonly ApplicationContext _context;
        private readonly DbSet<T> dbSet = null;

        public Repository(ApplicationContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

            dbSet = _context.Set<T>();
            
        }


        protected  IQueryable<T> Queryable(bool stateless = false)
        {
            var result = stateless ? dbSet.AsNoTracking() : dbSet.AsQueryable();

            return result;
        }

        public async Task<List<T>> Get()
        {
            var result = Queryable();
                
            return await result.ToListAsync();
        }

        public async Task<List<T>> Get(Expression<Func<T, bool>> predicate)
        {
            var result =  Queryable();
            
            return await result.Where(predicate) .ToListAsync();
        }

        public async Task<List<TDTO>> Get<TDTO>()
        {
            var result =  Queryable();
            return await result.ProjectTo<TDTO>(_mapper.ConfigurationProvider) .ToListAsync();
        }

        public async Task<List<TDTO>> Get<TDTO>(Expression<Func<T, bool>> predicate)
        {
            var result =  Queryable();
            return await result.Where(predicate)
                                .ProjectTo<TDTO>(_mapper.ConfigurationProvider)
                                .ToListAsync();
        }

        private async Task<PagedListResponse<TDTO>> GetPrivatePaged<TDTO>(Expression<Func<T, bool>> predicate, int page = 1, int pageSize = 10, bool countTotal = false)
        {
            var queryable =  Queryable();

            if (predicate != null)
                queryable = queryable.Where(predicate);

            var result = await queryable.ProjectTo<TDTO>(_mapper.ConfigurationProvider) 
                                        .ToPagedListAsync(page, pageSize, countTotal);

            return result;
        }

        public async Task<PagedListResponse<TDTO>> GetPaged<TDTO>(int page = 1, int pageSize = 10, bool countTotal = false)
        {
            var result = await  GetPrivatePaged<TDTO>(null, page, pageSize, countTotal);
            return result;
        }

        public async Task<PagedListResponse<TDTO>> GetPaged<TDTO>(Expression<Func<T, bool>> predicate, int page = 1, int pageSize = 10, bool countTotal = false)
        {
            var result = await GetPrivatePaged<TDTO>(predicate, page, pageSize, countTotal);
            return result;
        }     

        public async Task<T> GetById(int id)
        {
            var result =  Queryable();
                
            return await result.Where(x => x.Id == id).FirstOrDefaultAsync();;
        }

        public async Task<TDTO> GetById<TDTO>(int id)
        {
            var result =  Queryable();
                
            return await result.Where(x => x.Id == id)
                                .ProjectTo<TDTO>(_mapper.ConfigurationProvider) 
                                .FirstOrDefaultAsync();
        }

        public async Task<T> Insert(T entity)
        {
            //_context.Entry(entity).State = EntityState.Added;
            await dbSet.AddAsync(entity);
          

            return entity;
        }

        public async Task<List<T>> InsertRange(List<T> entity)
        {
            await dbSet.AddRangeAsync(entity);

            return entity;
        }

        public async Task<T> Update(T entity)
        {
            dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            return entity;
        }

        public async Task<List<T>> Update(List<T> entities)
        {
            dbSet.AttachRange(entities);
            _context.UpdateRange(entities);
            return entities;
        }

        public async Task<T> Disable(T entity)
        {
            dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            return entity;
        }

        public async Task Delete(object id)
        {
            T existing = dbSet.Find(id);
            dbSet.Remove(existing);

            return;
        }

        public async Task DeleteRange(int[] ids)
        {
            var existing = dbSet.Where(x => ids.Contains(x.Id));
            dbSet.RemoveRange(existing);

            return;
        }

        public async Task<T> Clone(T entity)
        {

            var clone = await Task.Run(() => (T) _context.Entry(entity).CurrentValues.ToObject());
           _context.Entry(entity).State = EntityState.Detached;
            return clone;
        }

        public async  Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
