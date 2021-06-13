using System;
using System.Collections.Generic;

namespace DBSync.Models
{
    public interface IUserRepository
    {
        void Add(User user);
        void Update(User user);
        void Remove(Guid id);
        User FindById(Guid id);
        IList<User> FetchAll();
    }
}
