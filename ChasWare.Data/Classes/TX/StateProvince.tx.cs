// WARNING: this code is auto generated and should not be modified.
// hint:    if you need to modify it, let it build into a non-project directory
//          then use a text comparison to sync any changes.

using ChasWare.Data.Classes.DTO;

namespace ChasWare.Data.Classes.TX
{
    public static class StateProvinceTX
    {
        #region public methods

        public static void ReadFromDTO(StateProvinceDTO source, StateProvince target)
        {
            target.StateProvinceId = source.StateProvinceId;
            target.StateProvinceCode = source.StateProvinceCode;
            target.CountryRegionCode = source.CountryRegionCode;
            target.IsOnlyStateProvinceFlag = source.IsOnlyStateProvinceFlag;
            target.Name = source.Name;
            target.TerritoryId = source.TerritoryId;
            target.ModifiedDate = source.ModifiedDate;
        }

        public static StateProvinceDTO WriteToDTO(StateProvince source)
        {
            StateProvinceDTO created = new StateProvinceDTO();
            created.StateProvinceId = source.StateProvinceId;
            created.StateProvinceCode = source.StateProvinceCode;
            created.CountryRegionCode = source.CountryRegionCode;
            created.IsOnlyStateProvinceFlag = source.IsOnlyStateProvinceFlag;
            created.Name = source.Name;
            created.TerritoryId = source.TerritoryId;
            created.ModifiedDate = source.ModifiedDate;
            return created;
        }

        #endregion
    }
}