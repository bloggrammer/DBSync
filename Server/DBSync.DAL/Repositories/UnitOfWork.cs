using DBSync.DAL.Configurations;
using DBSync.Models;
using NHibernate;
using System;

namespace DBSync.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(SessionFactory factory)
        {
            _session = factory.OpenSession();
            UserRepository = new UserRepository(_session);
        }      
        public bool CommitToDatabase()
        {
            using var transaction = _session.BeginTransaction();
            try
            {
                if (transaction.IsActive)
                    transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Rollback(transaction);
                return false;
            }
        }

        private void Rollback(ITransaction transaction)
        {
            try
            {
                if (transaction.IsActive)
                    transaction.Rollback();
            }
            finally
            {
                _session.Dispose();
            }
        }
        public IUserRepository UserRepository { get; }

        private readonly ISession _session;
    }
}
