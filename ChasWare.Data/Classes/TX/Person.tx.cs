// WARNING: this code is auto generated and should not be modified.
// hint:    if you need to modify it, let it build into a non-project directory
//          then use a text comparison to sync any changes.

using ChasWare.Data.Classes.DTO;

namespace ChasWare.Data.Classes.TX
{
    public static class PersonTX
    {
        #region public methods

        public static void ReadFromDTO(PersonDTO source, Person target)
        {
            target.RoleId = source.RoleId;
            target.PersonType = source.PersonType;
            target.Title = source.Title;
            target.FirstName = source.FirstName;
            target.MiddleName = source.MiddleName;
            target.LastName = source.LastName;
            target.Suffix = source.Suffix;
            target.Person.EntityId = source.EntityId;
            target.Person.ModifiedDate = source.ModifiedDate;
        }

        public static PersonDTO WriteToDTO(Person source)
        {
            PersonDTO created = new PersonDTO();
            created.RoleId = source.RoleId;
            created.PersonType = source.PersonType;
            created.Title = source.Title;
            created.FirstName = source.FirstName;
            created.MiddleName = source.MiddleName;
            created.LastName = source.LastName;
            created.Suffix = source.Suffix;
            created.EntityId = source.Person.EntityId;
            created.ModifiedDate = source.Person.ModifiedDate;
            return created;
        }

        #endregion
    }
}