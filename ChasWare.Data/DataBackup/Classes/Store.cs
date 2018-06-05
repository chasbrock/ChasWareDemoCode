// -----------------------------------------------------------------------
// file=Store.cs 
// Copyright (c) 2017 Chas.Brock@Gmail.Com All rights reserved.
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChasWare.Demo.Data.Classes
{
    public class Store : EntityMapped
    {
        #region Properties

        public virtual SalesPerson SalesPerson { get; set; }

        #endregion

        #region Entity Framework Mapping

        [ForeignKey(nameof(Classes.SalesPerson))]
        public int? SalesPersonId { get; set; }

        [StringLength(256)]
        public string StoreName { get; set; }

        #endregion
    }
}