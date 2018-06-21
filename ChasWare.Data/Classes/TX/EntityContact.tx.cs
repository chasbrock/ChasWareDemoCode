// WARNING: this code is auto generated and should not be modified.
// hint:    if you need to modify it, let it build into a non-project directory
//          then use a text comparison to sync any changes.

using ChasWare.Data.Classes.DTO;

namespace ChasWare.Data.Classes.TX
{
    public static class EntityContactTX
    {
        #region public methods

        public static void ReadFromDTO(EntityContactDTO source, EntityContact target)
        {
            target.ContactDetail = source.ContactDetail;
            target.ContactType = source.ContactType;
            target.Entity = source.Entity;
            target.ContactId = source.ContactId;
            target.EntityId = source.EntityId;
            target.ContactTypeId = source.ContactTypeId;
            target.ModifiedDate = source.ModifiedDate;
        }

        public static EntityContactDTO WriteToDTO(EntityContact source)
        {
            EntityContactDTO created = new EntityContactDTO();
            created.ContactDetail = source.ContactDetail;
            created.ContactType = source.ContactType;
            created.Entity = source.Entity;
            created.ContactId = source.ContactId;
            created.EntityId = source.EntityId;
            created.ContactTypeId = source.ContactTypeId;
            created.ModifiedDate = source.ModifiedDate;
            return created;
        }

        #endregion
    }
}