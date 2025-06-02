using KampüsSepeti.Application.Interfaces.Repositories;
using KampüsSepeti.Domain.Common;
using KampüsSepeti.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : class, new()
    {
        private readonly AppDbContext appDbContext;

        public WriteRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task AddAsync(T entity)
        {
            await appDbContext.AddAsync(entity);
        }

        public async Task AddRangeAsync(IList<T> entities)
        {
            await appDbContext.AddRangeAsync(entities);
        }

        public async Task HardDeleteAsync(T entity)
        {
            await Task.Run(() => appDbContext.Remove(entity));
        }

        public async Task HardDeleteRangeAsync(IList<T> entity)
        {
            await Task.Run(() => appDbContext.RemoveRange(entity));
        }

        public async Task<T> UpdateAsync(T entity)
        {
            await Task.Run(() => appDbContext.Update(entity));
            return entity;
        }
    }
}
