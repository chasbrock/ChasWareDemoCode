// WARNING: this code is auto generated and should not be modified.
// hint:    if you need to modify it, let it build into a non-project directory
//          then use a text comparison to sync any changes.

using ChasWare.Data.Classes.DTO;

namespace ChasWare.Data.Classes.TX
{
    public static class ContactDetailTX
    {
        #region public methods

        public static void ReadFromDTO(ContactDetailDTO source, ContactDetail target)
        {
            target.ContactId = source.ContactId;
            target.Details = source.Details;
            target.ModifiedDate = source.ModifiedDate;
        }

        public static ContactDetailDTO WriteToDTO(ContactDetail source)
        {
            ContactDetailDTO created = new ContactDetailDTO();
            created.ContactId = source.ContactId;
            created.Details = source.Details;
            created.ModifiedDate = source.ModifiedDate;
            return created;
        }

        #endregion
    }
}