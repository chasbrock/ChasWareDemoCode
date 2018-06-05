// -----------------------------------------------------------------------
// file=StateProvince.cs 
// Copyright (c) 2017 Chas.Brock@Gmail.Com All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChasWare.Demo.Data.Classes
{
    public class StateProvince
    {
        #region Public members

        public override string ToString()
        {
            return $"{StateProvinceId} - {CountryRegionCode} {StateProvinceCode} {Name}";
        }

        #endregion

        #region Entity Framework Mapping

        [Key]
        public int StateProvinceId { get; set; }

        [Column(TypeName = "char"), StringLength(3)]
        public string StateProvinceCode { get; set; }

        [Column(TypeName = "char"), StringLength(2)]
        public string CountryRegionCode { get; set; }

        public bool IsOnlyStateProvinceFlag { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public int TerritoryId { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ModifiedDate { get; set; }

        #endregion
    }
}