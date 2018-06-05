// -----------------------------------------------------------------------
// file=Customer.cs 
// Copyright (c) 2017 Chas.Brock@Gmail.Com All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChasWare.Demo.Data.Classes
{
    public class Customer
    {
        #region Properties

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