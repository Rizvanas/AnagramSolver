using AnagramGenerator.EF.CodeFirst.Entities;
using Contracts.DTO;
using Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnagramGenerator.EF.CodeFirst.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly WordsDB_CFContext _wordsDB_CFContext;

        public UsersRepository(WordsDB_CFContext wordsDB_CFContext)
        {
            _wordsDB_CFContext = wordsDB_CFContext;
        }

        public void AddUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException("argument user is null");

            _wordsDB_CFContext.Users.Add(new UserEntity
            {
                Id = user.Id,
                Ip = user.Ip
            });

            _wordsDB_CFContext.SaveChanges();
        }

        public void AddUsers(params User[] users)
        {
            if (users == null || users.Length == 0)
                throw new ArgumentNullException("Argument user is null or empty");

            _wordsDB_CFContext.Users.AddRange(users.Select(u => new UserEntity
            {
                Id = u.Id,
                Ip = u.Ip
            }));

            _wordsDB_CFContext.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            var userEntity = _wordsDB_CFContext.Users.FirstOrDefault(u => u.Id == id);

            if (userEntity == null)
                throw new ArgumentException($"user by id of {id} not found");

            _wordsDB_CFContext.Users.Remove(userEntity);

            _wordsDB_CFContext.SaveChanges();
        }

        public User GetUser(int id)
        {
            var userEntity = _wordsDB_CFContext.Users.FirstOrDefault(u => u.Id == id);

            if (userEntity == null)
                throw new ArgumentException($"user by id of {id} not found");

            return new User
            {
                Id = userEntity.Id,
                Ip = userEntity.Ip,
                SearchesLeft = userEntity.SearchesLeft
            };
        }

        public IList<User> GetUsers()
        {
            var wordEntities = _wordsDB_CFContext.Users;
            return wordEntities
                .Select(u => new User
                {
                    Id = u.Id,
                    Ip = u.Ip,
                    SearchesLeft = u.SearchesLeft
                }).ToList();
        }

        public void UpdateUser(User user)
        {
            var userEntity = _wordsDB_CFContext.Users.FirstOrDefault(u => u.Id == user.Id);

            if (userEntity == null)
                throw new ArgumentException($"user by id of {user.Id} not found");

            userEntity.SearchesLeft = user.SearchesLeft;

            _wordsDB_CFContext.SaveChanges();
        }
    }
}
