// WARNING: this code is auto generated and should not be modified.
// hint:    if you need to modify it, let it build into a non-project directory
//          then use a text comparison to sync any changes.

using ChasWare.Data.Classes.DTO;

namespace ChasWare.Data.Classes.TX
{
    public static class ContactTypeTX
    {
        #region public methods

        public static void ReadFromDTO(ContactTypeDTO source, ContactType target)
        {
            target.ContactTypeId = source.ContactTypeId;
            target.Type = source.Type;
            target.ModifiedDate = source.ModifiedDate;
        }

        public static ContactTypeDTO WriteToDTO(ContactType source)
        {
            ContactTypeDTO created = new ContactTypeDTO();
            created.ContactTypeId = source.ContactTypeId;
            created.Type = source.Type;
            created.ModifiedDate = source.ModifiedDate;
            return created;
        }

        #endregion
    }
}