// --------------------------------------------------------------------------------------------------------------------
// <copyright file=Address.cs company="chas.brock@outlook.com">
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
    public class Address
    {
        #region public properties

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