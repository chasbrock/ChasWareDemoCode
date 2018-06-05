// -----------------------------------------------------------------------
// file=Employee.cs 
// Copyright (c) 2017 Chas.Brock@Gmail.Com All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChasWare.Demo.Data.Classes
{
    public class Employee
    {
        #region Properties

        public virtual Person Person { get; set; }

        #endregion

        #region Public members

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