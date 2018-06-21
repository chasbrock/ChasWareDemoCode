// WARNING: this code is auto generated and should not be modified.
// hint:    if you need to modify it, let it build into a non-project directory
//          then use a text comparison to sync any changes.

using ChasWare.Data.Classes.DTO;

namespace ChasWare.Data.Classes.TX
{
    public static class AddressTX
    {
        #region public methods

        public static void ReadFromDTO(AddressDTO source, Address target)
        {
            target.AddressId = source.AddressId;
            target.AddressLine1 = source.AddressLine1;
            target.AddressLine2 = source.AddressLine2;
            target.City = source.City;
            target.StateProvinceId = source.StateProvinceId;
            target.PostalCode = source.PostalCode;
            target.ModifiedDate = source.ModifiedDate;
        }

        public static AddressDTO WriteToDTO(Address source)
        {
            AddressDTO created = new AddressDTO
                {
                    AddressId = source.AddressId,
                    AddressLine1 = source.AddressLine1,
                    AddressLine2 = source.AddressLine2,
                    City = source.City,
                    StateProvinceId = source.StateProvinceId,
                    PostalCode = source.PostalCode,
                    ModifiedDate = source.ModifiedDate
                };
            return created;
        }

        #endregion
    }
}