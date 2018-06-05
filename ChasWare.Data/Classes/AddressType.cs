// --------------------------------------------------------------------------------------------------------------------
// <copyright file=AddressType.cs company="chas.brock@outlook.com">
//      copyright charlie brock 2018 
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChasWare.Data.Classes
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