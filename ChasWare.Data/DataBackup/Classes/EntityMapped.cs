// -----------------------------------------------------------------------
// file=EntityMapped.cs 
// Copyright (c) 2017 Chas.Brock@Gmail.Com All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChasWare.Demo.Data.Classes
{
    /// <summary>
    ///     used to provide basic access to entity and then onto address etc
    /// </summary>
    public abstract class EntityMapped
    {
        #region Properties

        /// <summary>
        ///     access general details susch as
        /// </summary>
        public virtual Entity Entity { get; set; }

        #endregion

        #region Entity Framework Mapping

        [Key, ForeignKey(nameof(Entity))]
        public int EntityId { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ModifiedDate { get; set; }

        #endregion
    }
}