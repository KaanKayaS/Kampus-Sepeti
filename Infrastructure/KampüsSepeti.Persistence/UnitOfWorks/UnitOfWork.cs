using KampüsSepeti.Application.Interfaces.Repositories;
using KampüsSepeti.Application.Interfaces.UnitOfWorks;
using KampüsSepeti.Persistence.Context;
using KampüsSepeti.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Persistence.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext appDbContext;

        public UnitOfWork(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async ValueTask DisposeAsync()
        {
            await appDbContext.DisposeAsync(); // kaynakaları serbest bırakır 
        }

        public int Save()
        {
            return appDbContext.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
           return await appDbContext.SaveChangesAsync();
        }

        IReadRepository<T> IUnitOfWork.GetReadRepository<T>()
        {
           return new ReadRepository<T>(appDbContext);
        }

        IWriteRepository<T> IUnitOfWork.GetWriteRepository<T>()
        {
            return new WriteRepository<T>(appDbContext);
        }
    }
}
