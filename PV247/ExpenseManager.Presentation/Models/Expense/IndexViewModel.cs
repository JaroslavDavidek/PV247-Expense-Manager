﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.Presentation.Models.Expense
{
    /// <summary>
    /// Presentation layer representation of non-permanent CostInfoModel object
    /// </summary>
    public class IndexViewModel : ViewModelId
    {
        /// <summary>
        /// State whether set money is income or outcome.
        /// </summary>
        public bool IsIncome { get; set; }

        /// <summary>
        /// How much money has changed.
        /// </summary>
        [Required]
        public decimal Money { get; set; }

        /// <summary>
        /// More concrete description of the cost
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Date when the cost info was created.
        /// </summary>
        [Required]
        public DateTime Created { get; set; }

        /// <summary>
        /// Type of the cost.
        /// </summary>
        public string TypeName { get; set; }
    }
}
