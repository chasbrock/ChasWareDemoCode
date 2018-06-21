// WARNING: this code is auto generated and should not be modified.
// hint:    if you need to modify it, let it build into a non-project directory
//          then use a text comparison to sync any changes.

using ChasWare.Data.Classes.DTO;

namespace ChasWare.Data.Classes.TX
{
    public static class EntityTX
    {
        #region public methods

        public static void ReadFromDTO(EntityDTO source, Entity target)
        {
            target.Addresses = source.Addresses;
            target.ContactDetails = source.ContactDetails;
            target.EntityId = source.EntityId;
            target.ModifiedDate = source.ModifiedDate;
        }

        public static EntityDTO WriteToDTO(Entity source)
        {
            EntityDTO created = new EntityDTO();
            created.Addresses = source.Select(i => EntityAddressTX.WriteToDTO(i)).ToArray();
            created.ContactDetails = source.Select(i => EntityContactTX.WriteToDTO(i)).ToArray();
            created.EntityId = source.EntityId;
            created.ModifiedDate = source.ModifiedDate;
            return created;
        }

        #endregion
    }
}