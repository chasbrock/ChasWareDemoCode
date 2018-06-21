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
        #region Entity Framework Mapping

        [Key, ForeignKey(nameof(Entity))]
        public int EntityId { get; set; }

        /// <summary>
        ///     access general details susch as
        /// </summary>
        [Transformer.Transform(Conflate = true)]
        public virtual Entity Entity { get; set; }


        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ModifiedDate { get; set; }

        #endregion
    }
}