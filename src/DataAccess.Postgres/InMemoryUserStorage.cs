using Core.Abstractions;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Postgres
{
    internal class InMemoryUserStorage : IUserStorage
    {
        private readonly Dictionary<string, User> _userStorage = new Dictionary<string, User>();

        public Task AddUser(User user)
        {
            _userStorage[user.Name] = user;

            return Task.CompletedTask;
        }

        public Task<User?> FindUser(string name)
        {
            User? user = null;
            if (_userStorage.ContainsKey(name))
            {
                user = _userStorage[name];
            }

            return Task.FromResult(user);
        }
    }
}
