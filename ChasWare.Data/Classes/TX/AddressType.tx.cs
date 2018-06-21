// WARNING: this code is auto generated and should not be modified.
// hint:    if you need to modify it, let it build into a non-project directory
//          then use a text comparison to sync any changes.

using ChasWare.Data.Classes.DTO;

namespace ChasWare.Data.Classes.TX
{
    public static class AddressTypeTX
    {
        #region public methods

        public static void ReadFromDTO(AddressTypeDTO source, AddressType target)
        {
            target.AddressTypeId = source.AddressTypeId;
            target.Name = source.Name;
            target.ModifiedDate = source.ModifiedDate;
        }

        public static AddressTypeDTO WriteToDTO(AddressType source)
        {
            AddressTypeDTO created = new AddressTypeDTO();
            created.AddressTypeId = source.AddressTypeId;
            created.Name = source.Name;
            created.ModifiedDate = source.ModifiedDate;
            return created;
        }

        #endregion
    }
}