// --------------------------------------------------------------------------------------------------------------------
// <copyright file=EntityContact.cs company="chas.brock@outlook.com">
//      copyright charlie brock 2018 
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChasWare.Data.Classes
{
    /// <summary>
    ///     maps between entity  contact details
    ///     allows aentity to have more than one address
    /// </summary>
    public class EntityContact
    {
        #region Constants and fields 

        private const string UniqueIndex = "UK_EntityContact";

        #endregion

        #region public properties

        public virtual ContactDetail ContactDetail { get; set; }

        public virtual ContactType ContactType { get; set; }

        [ForeignKey("EntityId")]
        public virtual Entity Entity { get; set; }

        #endregion

        #region Entity Framework Mapping

        [Required, Key, ForeignKey(nameof(ContactDetail)), Index(UniqueIndex, 0, IsUnique = true)]
        public int ContactId { get; set; }

        [Required, ForeignKey(nameof(Entity)), Index(UniqueIndex, 1, IsUnique = true)]
        public int EntityId { get; set; }

        [Required, ForeignKey(nameof(ContactType)), Index(UniqueIndex, 2, IsUnique = true)]
        public int ContactTypeId { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ModifiedDate { get; set; }

        #endregion
    }
}