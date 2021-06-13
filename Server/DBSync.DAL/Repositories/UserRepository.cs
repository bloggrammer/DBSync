using DBSync.Models;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DBSync.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(ISession session) => _session = session;       
        public void Add(User user) => _session.SaveOrUpdate(user);

        public IList<User> FetchAll() => _session.Query<User>()
                                                 .ToList();

        public User FindById(Guid id) => _session.Query<User>()
                                                  .Where(x => x.Id == id)
                                                  .FirstOrDefault();

        public void Remove(Guid id) => _session.Query<User>()
                                                  .Where(x => x.Id == id)
                                                  .Delete();

        public void Update(User user) => _session.Update(user);
        protected ISession _session;
    }
}
