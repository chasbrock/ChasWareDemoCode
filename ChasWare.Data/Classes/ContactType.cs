// --------------------------------------------------------------------------------------------------------------------
// <copyright file=ContactType.cs company="chas.brock@outlook.com">
//      copyright charlie brock 2018 
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChasWare.Data.Classes
{
    public class ContactType
    {
        #region Constants and fields 

        private const string UniqueKey = "UK_" + nameof(ContactType);

        #endregion

        #region Entity Framework Mapping

        [Key]
        public int ContactTypeId { get; set; }

        [StringLength(60), Index(UniqueKey, 0, IsUnique = true)]
        public string Type { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ModifiedDate { get; set; }

        #endregion
    }
}