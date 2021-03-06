﻿using System;

namespace ExpenseManager.Database.Enums
{
    /// <summary>
    /// Represents periodicity of costs
    /// </summary>
    [Flags]
    public enum PeriodicityModel 
    {
        /// <summary>
        /// Cost is NOT periodic
        /// </summary>
        None = 1,

        /// <summary>
        /// Day period
        /// </summary>
        Day = 2,

        /// <summary>
        /// Week period
        /// </summary>
        Week = 4,

        /// <summary>
        /// Month period
        /// </summary>
        Month = 8
    }
}
