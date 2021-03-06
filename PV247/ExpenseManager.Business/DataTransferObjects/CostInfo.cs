﻿using System;
using ExpenseManager.Business.DataTransferObjects.Enums;

namespace ExpenseManager.Business.DataTransferObjects
{
    /// <summary>
    /// Business layer representation of CostInfoModel object
    /// </summary>
    public class CostInfo : BusinessObject<Guid>
    {
        /// <summary>
        /// State whether set money is income or outcome.
        /// </summary>
        public bool IsIncome { get; set; }
       
        /// <summary>
        /// How much money has changed.
        /// </summary>
        public decimal Money { get; set; }
        
        /// <summary>
        /// More concrete description of the cost
        /// </summary>
        public string Description { get; set; }
       
        /// <summary>
        /// Account id.
        /// </summary>
        public Guid AccountId { get; set; }
       
        /// <summary>
        /// Account whom this cost belongs.
        /// </summary>
        public string AccountName { get; set; }
      
        /// <summary>
        /// Date when the cost info was created.
        /// </summary>
        public DateTime? Created { get; set; }
       
        /// <summary>
        /// Type id.
        /// </summary>
        public Guid TypeId { get; set; }
      
        /// <summary>
        /// Type of the cost.
        /// </summary>
        public string TypeName { get; set; }
      
        /// <summary>
        /// Periodicity of cost
        /// </summary>
        public Periodicity Periodicity { get; set; }
       
        /// <summary>
        /// Mulptiplies periodicity
        /// </summary>
        public int PeriodicMultiplicity { get; set; }
       
        /// <summary>
        /// Makes string representation of object based on its properties
        /// </summary>
        /// <returns>String representation of object</returns>
        public override string ToString()
        {
            return $"IsIncome: {IsIncome}, Money: {Money}, Description: {Description}, AccountId: {AccountId}, AccountName: {AccountName}, Created: {Created}, TypeId: {TypeId}, TypeName: {TypeName}, Periodicity: {Periodicity}, PeriodicMultiplicity: {PeriodicMultiplicity}";
        }
       
        /// <summary>
        /// Determites if two objects are the same one
        /// </summary>
        /// <param name="other">Object to be compared with</param>
        /// <returns>true if objects are same</returns>
        protected bool Equals(CostInfo other)
        {
            return IsIncome == other.IsIncome && Money == other.Money && string.Equals(Description, other.Description) && AccountId == other.AccountId && string.Equals(AccountName, other.AccountName) && Created.Equals(other.Created) && TypeId == other.TypeId && string.Equals(TypeName, other.TypeName) && Periodicity == other.Periodicity && PeriodicMultiplicity == other.PeriodicMultiplicity;
        }
       
        /// <summary>
        /// Determites if two objects are the same one
        /// </summary>
        /// <param name="obj">Object to be compared with</param>
        /// <returns>true if objects are same</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CostInfo)obj);
        }
      
        /// <summary>
        /// Compute hash of this object based on his properties
        /// </summary>
        /// <returns>This object hashcode</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = IsIncome.GetHashCode();
                hashCode = (hashCode*397) ^ Money.GetHashCode();
                hashCode = (hashCode*397) ^ (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ AccountId.GetHashCode();
                hashCode = (hashCode*397) ^ (AccountName != null ? AccountName.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ Created.GetHashCode();
                hashCode = (hashCode*397) ^ TypeId.GetHashCode();
                hashCode = (hashCode*397) ^ (TypeName != null ? TypeName.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (int) Periodicity;
                hashCode = (hashCode*397) ^ PeriodicMultiplicity;
                return hashCode;
            }
        }
    }
}
