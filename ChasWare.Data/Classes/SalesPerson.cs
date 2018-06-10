// --------------------------------------------------------------------------------------------------------------------
// <copyright file=SalesPerson.cs company="chas.brock@outlook.com">
//      copyright charlie brock 2018 
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using ChasWare.Common.Utils.Transform;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChasWare.Data.Classes
{
    [ExportToTs]
    public class SalesPerson
    {
        #region public properties

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