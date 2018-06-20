// --------------------------------------------------------------------------------------------------------------------
// <copyright file=Person.cs company="chas.brock@outlook.com">
//      copyright charlie brock 2018 
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ChasWare.Common.Utils.Helpers;
using ChasWare.Common.Utils.Transformation;

namespace ChasWare.Data.Classes
{
    [Transformer.Transform]
    public class Person : EntityMapped
    {
        #region public properties

        /// <summary>
        ///     expand all name parts into on string
        /// </summary>
        public string FullName => StrFuncs.PackOutStrings(EntityId.ToString(), Title, FirstName, MiddleName, LastName, Suffix);

        /// <summary>
        ///     what role does this person perform
        /// </summary>
        public virtual Role Role { get; set; }

        #endregion

        #region public methods

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