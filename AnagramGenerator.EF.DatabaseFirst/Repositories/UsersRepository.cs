using AnagramGenerator.EF.DatabaseFirst.Entities;
using Contracts.DTO;
using Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnagramGenerator.EF.DatabaseFirst.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly WordsDBContext _wordsDBContext;

        public UsersRepository(WordsDBContext wordsDBContext)
        {
            _wordsDBContext = wordsDBContext;
        }

        public void AddUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException("argument user is null");

            _wordsDBContext.Users.Add(new UserEntity
            {
                Id = user.Id,
                Ip = user.Ip
            });

            _wordsDBContext.SaveChanges();
        }

        public void AddUsers(params User[] users)
        {
            if (users == null || users.Length == 0)
                throw new ArgumentNullException("Argument user is null or empty");

            _wordsDBContext.Users.AddRange(users.Select(u => new UserEntity
            {
                Id = u.Id,
                Ip = u.Ip
            }));

            _wordsDBContext.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            var userEntity = _wordsDBContext.Users.FirstOrDefault(u => u.Id == id);

            if (userEntity == null)
                throw new ArgumentException($"user by id of {id} not found");

            _wordsDBContext.Users.Remove(userEntity);

            _wordsDBContext.SaveChanges();
        }

        public User GetUser(int id)
        {
            var userEntity = _wordsDBContext.Users.FirstOrDefault(u => u.Id == id);

            if (userEntity == null)
                throw new ArgumentException($"user by id of {id} not found");

            return new User
            {
                Id = userEntity.Id,
                Ip = userEntity.Ip
            };
        }

        public IList<User> GetUsers()
        {
            var wordEntities = _wordsDBContext.Users;
            return wordEntities
                .Select(u => new User
                {
                    Id = u.Id,
                    Ip = u.Ip
                }).ToList();
        }
    }
}
