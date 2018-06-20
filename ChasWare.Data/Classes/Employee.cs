// --------------------------------------------------------------------------------------------------------------------
// <copyright file=Employee.cs company="chas.brock@outlook.com">
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
    public class Employee
    {
        #region public properties

        public virtual Person Person { get; set; }

        #endregion

        #region public methods

        /// <summary>
        ///     static method to create free standing object,
        ///     and populate any child objects
        /// </summary>
        /// <returns>
        ///     new object
        /// </returns>
        public static Employee Create()
        {
            return new Employee
                {
                    Person = Person.Create()
                };
        }

        #endregion

        #region Entity Framework Mapping

        [Key, ForeignKey(nameof(Person))]
        public int PersonId { get; set; }

        [StringLength(256)]
        public string LoginId { get; set; }

        [StringLength(50)]
        public string JobTitle { get; set; }

        public bool CurrentFlag { get; set; }

        public DateTime HireDate { get; set; }

        public DateTime BirthDate { get; set; }

        [Column(TypeName = "char"), StringLength(1)]
        public string Gender { get; set; }

        [Column(TypeName = "char"), StringLength(1)]
        public string MaritalStatus { get; set; }

        [StringLength(15)]
        public string NationalIDNumber { get; set; }

        public bool SalariedFlag { get; set; }

        public short SickLeaveHours { get; set; }

        public short VacationHours { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ModifiedDate { get; set; }

        #endregion
    }
}