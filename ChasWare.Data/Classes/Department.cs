// --------------------------------------------------------------------------------------------------------------------
// <copyright file=Department.cs company="chas.brock@outlook.com">
//      copyright charlie brock 2018 
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChasWare.Data.Classes
{
    public class Department
    {
        #region Constants and fields 

        private const string UniqueKey = "UK_" + nameof(Department);

        #endregion

        #region Entity Framework Mapping

        [Key]
        public short DepartmentId { get; set; }

        [StringLength(50), Index(UniqueKey, 0, IsUnique = true)]
        public string Name { get; set; }

        [StringLength(50)]
        public string GroupName { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ModifiedDate { get; set; }

        #endregion
    }
}