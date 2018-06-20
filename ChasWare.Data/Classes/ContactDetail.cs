// --------------------------------------------------------------------------------------------------------------------
// <copyright file=ContactDetail.cs company="chas.brock@outlook.com">
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
    public class ContactDetail
    {
        #region public properties

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