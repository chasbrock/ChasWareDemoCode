// --------------------------------------------------------------------------------------------------------------------
// <copyright file=Role.cs company="chas.brock@outlook.com">
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
    public class Role
    {
        #region Constants and fields 

        private const string UniqueKey = "UK_" + nameof(Role);

        #endregion

        #region Entity Framework Mapping

        [Key]
        public int RoleId { get; set; }

        [StringLength(60), Index(UniqueKey, 0, IsUnique = true)]
        public string Name { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ModifiedDate { get; set; }

        #endregion
    }
}