// --------------------------------------------------------------------------------------------------------------------
// <copyright file=Entity.cs company="chas.brock@outlook.com">
//      copyright charlie brock 2018 
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ChasWare.Common.Utils.Transformation;
using ChasWare.Data.DummyData;

namespace ChasWare.Data.Classes
{
    /// <summary>
    ///     maps entities such as persons and stores to common
    ///     data such as addresses and contacts
    /// </summary>
    [Transformer.Transform(ContextName = "Entities")]
    public class Entity
    {
        #region public properties

        [Required, ForeignKey("EntityId")]
        public virtual ICollection<EntityAddress> Addresses { get; set; }

        [Required, ForeignKey("EntityId")]
        public virtual ICollection<EntityContact> ContactDetails { get; set; }

        #endregion

        #region public methods

        /// <summary>
        ///     static method to create free standing object,
        ///     and populate any child objects
        /// </summary>
        /// <returns>
        ///     new object
        /// </returns>
        public static Entity Create()
        {
            /*          return  new Entity
                                    {
                                        Addresses = new List<EntityAddress>(),
                                        ContactDetails = new List<EntityContact>()
                                    };
              */
            return DummyDataFactory.GetEntity(5479);
        }

        #endregion

        #region Entity Framework Mapping

        [Required, Key]
        public int EntityId { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ModifiedDate { get; set; }

        #endregion
    }
}