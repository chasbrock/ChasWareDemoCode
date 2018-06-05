// -----------------------------------------------------------------------
// file=Address.cs 
// Copyright (c) 2017 Chas.Brock@Gmail.Com All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChasWare.Demo.Data.Classes
{
    public class Address
    {
        #region Properties

        [Required, ForeignKey("AddressId")]
        public virtual EntityAddress EntityAddress { get; set; }

        public virtual StateProvince StateProvince { get; set; }

        #endregion

        #region Entity Framework Mapping

        [Required, Key]
        public int AddressId { get; set; }

        [Required, StringLength(60)]
        public string AddressLine1 { get; set; }

        [StringLength(60)]
        public string AddressLine2 { get; set; }

        [Required, StringLength(30)]
        public string City { get; set; }

        [Required, ForeignKey(nameof(StateProvince))]
        public int StateProvinceId { get; set; }

        [Required, StringLength(12)]
        public string PostalCode { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ModifiedDate { get; set; }

        #endregion
    }
}