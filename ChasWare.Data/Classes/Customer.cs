﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file=Customer.cs company="chas.brock@outlook.com">
//      copyright charlie brock 2018 
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ChasWare.Common.Utils.Transformation;

namespace ChasWare.Data.Classes
{
    [Transformer.Transform]
    public class Customer
    {
        #region Entity Framework Mapping

        [Key]
        public int CustomerId { get; set; }

        [ForeignKey(nameof(SalesTerritory))]
        public int? SalesTerritoryId { get; set; }

        public virtual SalesTerritory SalesTerritory { get; set; }

        [ForeignKey(nameof(Store))]
        public int? StoreId { get; set; }

        public virtual Store Store { get; set; }

        [ForeignKey(nameof(Person))]
        public int? PersonId { get; set; }

        public virtual Person Person { get; set; }

        [StringLength(50)]
        public string AccountNumber { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ModifiedDate { get; set; }

        #endregion
    }
}