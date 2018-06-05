// --------------------------------------------------------------------------------------------------------------------
// <copyright file=Customer.cs company="chas.brock@outlook.com">
//      copyright charlie brock 2018 
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChasWare.Data.Classes
{
    public class Customer
    {
        #region public properties

        public virtual Person Person { get; set; }
        public virtual SalesTerritory SalesTerritory { get; set; }
        public virtual Store Store { get; set; }

        #endregion

        #region Entity Framework Mapping

        [Key]
        public int CustomerId { get; set; }

        [ForeignKey(nameof(SalesTerritory))]
        public int? SalesTerritoryId { get; set; }

        [ForeignKey(nameof(Store))]
        public int? StoreId { get; set; }

        [ForeignKey(nameof(Person))]
        public int? PersonId { get; set; }

        [StringLength(50)]
        public string AccountNumber { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ModifiedDate { get; set; }

        #endregion
    }
}