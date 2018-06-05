// -----------------------------------------------------------------------
// file=Person.cs 
// Copyright (c) 2017 Chas.Brock@Gmail.Com All rights reserved.
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ChasWare.Utils.Helpers;

namespace ChasWare.Demo.Data.Classes
{
    public class Person : EntityMapped
    {
        #region Properties

        /// <summary>
        ///     expand all name parts into on string
        /// </summary>
        public string FullName => StrFuncs.PackOutStrings(EntityId.ToString(), Title, FirstName, MiddleName, LastName, Suffix);

        /// <summary>
        ///     what role does this person perform
        /// </summary>
        public virtual Role Role { get; set; }

        #endregion

        #region Public members

        /// <summary>
        ///     static method to create free standing object,
        ///     and populate any child objects
        /// </summary>
        /// <returns>
        ///     new object
        /// </returns>
        public static Person Create()
        {
            return new Person
                   {
                       Role = new Role(),
                       Entity = Entity.Create()
                   };
        }

        #endregion

        #region Entity Framework Mapping

        [ForeignKey(nameof(Role))]
        public int? RoleId { get; set; }

        [Column(TypeName = "char"), StringLength(2)]
        public string PersonType { get; set; }

        [StringLength(8)]
        public string Title { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(10)]
        public string Suffix { get; set; }

        #endregion
    }
}