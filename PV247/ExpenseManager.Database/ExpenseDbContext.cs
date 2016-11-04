﻿using System.Data.Entity;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Database
{
    internal class ExpenseDbContext : DbContext
    {
        public ExpenseDbContext() : base("ExpenseManagerDB") { }

        public ExpenseDbContext(string nameOrConnectionString) : base(nameOrConnectionString) { }

        public DbSet<BadgeModel> Badges { get; set; }
        public DbSet<CostInfoModel> CostInfos { get; set; }
        public DbSet<CostTypeModel> CostTypes { get; set; }
        public DbSet<PlanModel> Plans { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<AccountModel> Accounts { get; set; }
        public DbSet<AccountBadgeModel> AccountBadges { get; set; }
        
    }
}
