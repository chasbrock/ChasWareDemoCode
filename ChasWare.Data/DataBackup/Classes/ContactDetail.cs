// -----------------------------------------------------------------------
// file=ContactDetail.cs 
// Copyright (c) 2017 Chas.Brock@Gmail.Com All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChasWare.Demo.Data.Classes
{
    public class ContactDetail
    {
        #region Properties

        [ForeignKey("ContactId")]
        public virtual EntityContact EntityContact { get; set; }

        #endregion

        #region Entity Framework Mapping

        [Required, Key]
        public int ContactId { get; set; }

        [Required, StringLength(60)]
        public string Details { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ModifiedDate { get; set; }

        #endregion
    }
}