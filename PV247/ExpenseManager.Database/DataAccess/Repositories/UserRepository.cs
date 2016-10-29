﻿using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using ExpenseManager.Contract.DTOs;
using AutoMapper;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.DataAccess.Repositories
{
    /// <summary>
    /// Implementation of Repository for User entity.
    /// </summary>
    public class UserRepository : ExpenseManagerRepository<User, UserDTO, int>
    {
        /// <summary>
        /// Create repository.
        /// </summary>
        /// <param name="provider">UoW provider</param>
        /// <param name="mapper">Mapper</param>
        public UserRepository(IUnitOfWorkProvider provider, Mapper mapper) : base(provider, mapper) { }

        /// <summary>
        /// Gets currently signed user according to its email
        /// </summary>
        /// <param name="email">User unique email</param>
        /// <param name="includes">Property to include with obtained user</param>
        /// <returns>UserDTO with user details</returns>
        public UserDTO GetUserByEmail(string email, params Expression<Func<UserDTO, object>>[] includes)
        {
            IQueryable<User> users = Context.Set<User>();
            var processedIncludes = ProcessIncludesList(includes);

            // Include all required properties
            users = processedIncludes.Aggregate(users, (current, include) => current.Include(include));

            var user = users.FirstOrDefault(usr => usr.Email.Equals(email));

            if (user == null)
            {
                Debug.WriteLine($"User with email {email} does not exists in the DB!");
                return null;
            }
            return ExpenseManagerMapper.Map<User, UserDTO>(user);
        }
    }
}