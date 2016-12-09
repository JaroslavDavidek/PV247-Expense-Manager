﻿using System;
using System.Linq.Expressions;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.DataTransferObjects.Filters.CostInfos
{
    /// <summary>
    /// Filtery by money to
    /// </summary>
    internal class CostInfosByMoneyTo : FilterValueBase<CostInfoModel, decimal>
    {
        public override Expression<Func<CostInfoModel, bool>> GetWhereCondition(decimal value)
            => costInfo => costInfo.Money <= value;
    }
}
