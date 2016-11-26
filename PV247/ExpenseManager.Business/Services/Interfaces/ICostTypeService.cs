﻿using System;
using System.Collections.Generic;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Filters;

namespace ExpenseManager.Business.Services.Interfaces
{
    
    /// <summary>
    /// Service handles cost type entity operations
    /// </summary>
    public interface ICostTypeService : IService
    {
        /// <summary>
        /// Creaates new cost type
        /// </summary>
        /// <param name="costType">Object to be added to database</param>
        Guid CreateCostType(CostType costType);

        /// <summary>
        /// Updates existing cost type
        /// </summary>
        /// <param name="costType">Modified existing cost type</param>
        void UpdateCostType(CostType costType);

        /// <summary>
        /// Deletes cost type specified by id
        /// </summary>
        /// <param name="costTypeId">Unique cost type id</param>
        void DeleteCostType(Guid costTypeId);

        /// <summary>
        /// Get cost type specified by unique id
        /// </summary>
        /// <param name="costTypeId">Unique cost type id</param>
        /// <returns></returns>
        CostType GetCostType(Guid costTypeId);

        /// <summary>
        /// List cost types specified by filter
        /// </summary>
        /// <param name="filter">Filters cost types</param>
        /// <returns>List of cost typer</returns>
        List<CostType> ListCostTypes(CostTypeFilter filter);
    }
}
