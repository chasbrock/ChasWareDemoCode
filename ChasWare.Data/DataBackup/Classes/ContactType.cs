// -----------------------------------------------------------------------
// file=ContactType.cs 
// Copyright (c) 2017 Chas.Brock@Gmail.Com All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChasWare.Demo.Data.Classes
{
    public class ContactType
    {
        #region Constants

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