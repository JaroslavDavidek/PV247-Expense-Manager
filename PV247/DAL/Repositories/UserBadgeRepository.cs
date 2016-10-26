﻿using DAL.Entities;
using DAL.Infrastructure;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.Repositories
{
    public class UserBadgeRepository : EntityFrameworkRepository<UserBadge, int>
    {
        public UserBadgeRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}