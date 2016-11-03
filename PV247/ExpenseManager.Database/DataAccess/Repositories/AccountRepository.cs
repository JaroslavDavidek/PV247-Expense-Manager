﻿using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.DataAccess.Repositories
{
    public class AccountRepository : ExpenseManagerRepository<Account, int>
    {
        public AccountRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}