using BookingAppointment.Domain.Entities;
using BookingAppointment.Infrastructure.Data.Repository;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingAppointment.Domain.Repositories;
using BookingAppointment.Domain.Unitofwork;

namespace BookingAppointment.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppointmentContext _context;
        private readonly Dictionary<string, object> _repositories;
        private IDbContextTransaction _transaction;
        private bool _disposed;

        public UnitOfWork(AppointmentContext context)
        {
            _context = context;
            _repositories = new Dictionary<string, object>();
        }

        public IRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<,>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity), typeof(TKey)), _context);
                _repositories.Add(type, repositoryInstance);
            }

            return (IRepository<TEntity, TKey>)_repositories[type];
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            finally
            {
                await _transaction.DisposeAsync();
            }
        }

        public async Task RollbackTransactionAsync()
        {
            try
            {
                await _transaction.RollbackAsync();
            }
            finally
            {
                await _transaction.DisposeAsync();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
    }
    
}
