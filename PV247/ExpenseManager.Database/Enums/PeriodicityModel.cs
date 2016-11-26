﻿namespace ExpenseManager.Database.Enums
{
    /// <summary>
    /// Represents periodicity of costs
    /// </summary>
    public enum PeriodicityModel 
    {
        /// <summary>
        /// Cost is NOT periodic
        /// </summary>
        None,

        /// <summary>
        /// Day period
        /// </summary>
        Day,

        /// <summary>
        /// Week period
        /// </summary>
        Week,

        /// <summary>
        /// Month period
        /// </summary>
        Month
    }
}
