// --------------------------------------------------------------------------------------------------------------------
// <copyright file=EntityMapped.cs company="chas.brock@outlook.com">
//      copyright charlie brock 2018 
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ChasWare.Common.Utils.Transformation;

namespace ChasWare.Data.Classes
{
    /// <summary>
    ///     used to provide basic access to entity and then onto address etc
    /// </summary>
    [Transformer.Transform]
    public abstract class EntityMapped
    {
        #region public properties

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