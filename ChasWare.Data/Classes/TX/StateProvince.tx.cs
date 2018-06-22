
// WARNING: this code is auto generated and should not be modified.
// hint:    if you need to modify it, let it build into a non-project directory
//          then use a text comparison tool to sync any changes.

using System;
using System.Collections.Generic;
using System.Linq;
using ChasWare.Data.Classes;
using ChasWare.Data.Classes.DTO;

namespace ChasWare.Data.Classes.TX
{
 public static class StateProvinceTX
  {
    public static void ReadFromDTO(StateProvince target, StateProvinceDTO source)
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
       return  new StateProvinceDTO
       {
         StateProvinceId = source.StateProvinceId,
         StateProvinceCode = source.StateProvinceCode,
         CountryRegionCode = source.CountryRegionCode,
         IsOnlyStateProvinceFlag = source.IsOnlyStateProvinceFlag,
         Name = source.Name,
         TerritoryId = source.TerritoryId,
         ModifiedDate = source.ModifiedDate,
       };
    }

  }
}
