// --------------------------------------------------------------------------------------------------------------------
// <copyright file=SalesTerritory.cs company="chas.brock@outlook.com">
//      copyright charlie brock 2018 
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ChasWare.Common.Utils.Transformation;

namespace ChasWare.Data.Classes
{
    [Transformer.Transform(ContextName = "SalesTerritories")]
    public class SalesTerritory
    {
        #region Constants and fields 

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