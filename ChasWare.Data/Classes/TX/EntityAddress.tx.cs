// WARNING: this code is auto generated and should not be modified.
// hint:    if you need to modify it, let it build into a non-project directory
//          then use a text comparison to sync any changes.

using ChasWare.Data.Classes.DTO;

namespace ChasWare.Data.Classes.TX
{
    public static class EntityAddressTX
    {
        #region public methods

        public static void ReadFromDTO(EntityAddressDTO source, EntityAddress target)
        {
            target.Address = source.Address;
            target.AddressType = source.AddressType;
            target.Entity = source.Entity;
            target.AddressId = source.AddressId;
            target.EntityId = source.EntityId;
            target.AddressTypeId = source.AddressTypeId;
            target.ModifiedDate = source.ModifiedDate;
        }

        public static EntityAddressDTO WriteToDTO(EntityAddress source)
        {
            EntityAddressDTO created = new EntityAddressDTO();
            created.Address = source.Address;
            created.AddressType = source.AddressType;
            created.Entity = source.Entity;
            created.AddressId = source.AddressId;
            created.EntityId = source.EntityId;
            created.AddressTypeId = source.AddressTypeId;
            created.ModifiedDate = source.ModifiedDate;
            return created;
        }

        #endregion
    }
}