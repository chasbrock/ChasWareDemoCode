// -----------------------------------------------------------------------
// file=SalesTerritory.cs 
// Copyright (c) 2017 Chas.Brock@Gmail.Com All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChasWare.Demo.Data.Classes
{
    public class SalesTerritory
    {
        #region Constants

        private const string UniqueKey = "UK_" + nameof(SalesTerritory);

        #endregion

        #region Entity Framework Mapping

        [Key]
        public int TerritoryId { get; set; }

        [StringLength(50), Index(UniqueKey, 0, IsUnique = true)]
        public string Name { get; set; }

        [Column(TypeName = "char"), StringLength(3)]
        public string CountryRegionCode { get; set; }

        [StringLength(50)]
        public string SalesGroup { get; set; }

        public decimal SalesYTD { get; set; }

        public decimal SalesLastYear { get; set; }

        public decimal CostYTD { get; set; }

        public decimal CostLastYear { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ModifiedDate { get; set; }

        #endregion
    }
}