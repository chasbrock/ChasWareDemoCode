// -----------------------------------------------------------------------
// file=Role.cs 
// Copyright (c) 2017 Chas.Brock@Gmail.Com All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChasWare.Demo.Data.Classes
{
    public class Role
    {
        #region Constants

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