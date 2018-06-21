// --------------------------------------------------------------------------------------------------------------------
// <copyright file=Address.cs company="chas.brock@outlook.com">
//      copyright charlie brock 2018 
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ChasWare.Common.Utils.Transformation;

namespace ChasWare.Data.Classes
{
    [Transformer.Transform]
    public class Address
    {
        #region Entity Framework Mapping

        [Required, Key]
        public int AddressId { get; set; }

        [Required, ForeignKey("AddressId"), Transformer.Transform(Ignore = true)]
        public virtual EntityAddress EntityAddress { get; set; }

        [Required, StringLength(60)]
        public string AddressLine1 { get; set; }

        [StringLength(60)]
        public string AddressLine2 { get; set; }

        [Required, StringLength(30)]
        public string City { get; set; }

        [Required, ForeignKey(nameof(StateProvince))]
        public int StateProvinceId { get; set; }

        [Transformer.Transform(Conflate = true)]
        public virtual StateProvince StateProvince { get; set; }

        [Required, StringLength(12)]
        public string PostalCode { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ModifiedDate { get; set; }

        #endregion
    }
}