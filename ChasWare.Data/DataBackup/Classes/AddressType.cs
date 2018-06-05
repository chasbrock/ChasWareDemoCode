// -----------------------------------------------------------------------
// file=AddressType.cs 
// Copyright (c) 2017 Chas.Brock@Gmail.Com All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChasWare.Demo.Data.Classes
{
    public class AddressType
    {
        #region Entity Framework Mapping

        [Key]
        public int AddressTypeId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ModifiedDate { get; set; }

        #endregion
    }
}