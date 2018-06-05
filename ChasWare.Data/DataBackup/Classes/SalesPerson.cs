// -----------------------------------------------------------------------
// file=SalesPerson.cs 
// Copyright (c) 2017 Chas.Brock@Gmail.Com All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChasWare.Demo.Data.Classes
{
    public class SalesPerson
    {
        #region Properties

        public virtual Person Person { get; set; }
        public virtual SalesTerritory SalesTerritory { get; set; }

        #endregion

        #region Entity Framework Mapping

        [Key, ForeignKey(nameof(Person))]
        public int EntityId { get; set; }

        [ForeignKey(nameof(SalesTerritory))]
        public int? SalesTerritoryId { get; set; }

        public decimal? SalesQuota { get; set; }

        public decimal Bonus { get; set; }

        public decimal CommissionPct { get; set; }

        public decimal SalesYTD { get; set; }

        public decimal SalesLastYear { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ModifiedDate { get; set; }

        #endregion
    }
}