﻿using System.Linq;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Infrastructure.Query;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.DataAccess.Queries
{
    /// <summary>
    /// Implementation of Query for cost types.
    /// </summary>
    internal class ListCostTypesQuery : ExpenseManagerQuery<CostTypeModel>
    {
        /// <summary>
        /// Create query.
        /// </summary>
        /// <param name="provider">unitOfWork provider</param>
        internal ListCostTypesQuery(IUnitOfWorkProvider provider) : base(provider)
        {
        }

        /// <summary>
        /// Return IQueryable.
        /// </summary>
        /// <returns>IQueryable</returns>
        protected override IQueryable<CostTypeModel> GetQueryable()
        {
            return ApplyFilters(Context.CostTypes);
        }
    }
}
