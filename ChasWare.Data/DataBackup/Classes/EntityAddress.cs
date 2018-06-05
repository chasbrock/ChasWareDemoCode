// -----------------------------------------------------------------------
// file=EntityAddress.cs 
// Copyright (c) 2017 Chas.Brock@Gmail.Com All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChasWare.Demo.Data.Classes
{
    /// <summary>
    ///     maps between entity /address / addresstype
    ///     allows entity to have more than one address
    /// </summary>
    public class EntityAddress
    {
        #region Constants

        private const string UniqueIndex = "UK_EntityAddress";

        #endregion

        #region Properties

        [Required, ForeignKey("AddressId")]
        public virtual Address Address { get; set; }

        [Required]
        public virtual AddressType AddressType { get; set; }

        [Required, ForeignKey("EntityId")]
        public virtual Entity Entity { get; set; }

        #endregion

        #region Entity Framework Mapping

        [Required, Key, ForeignKey(nameof(Address)), Index(UniqueIndex, 0, IsUnique = true)]
        public int AddressId { get; set; }

        [Required, ForeignKey(nameof(Entity)), Index(UniqueIndex, 1, IsUnique = true)]
        public int EntityId { get; set; }

        [Required, ForeignKey(nameof(AddressType)), Index(UniqueIndex, 2, IsUnique = true)]
        public int AddressTypeId { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ModifiedDate { get; set; }

        #endregion
    }
}