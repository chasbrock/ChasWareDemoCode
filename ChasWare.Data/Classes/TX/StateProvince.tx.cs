
// WARNING: this code is auto generated and should not be modified.
// hint:    if you need to modify it, let it build into a non-project directory
//          then use a text comparison tool to sync any changes.
// (c) Chas.Brock@gmail.com 2018

using System;
using System.Collections.Generic;
using System.Linq;
using ChasWare.Data.Classes;
using ChasWare.Data.Classes.DTO;

namespace ChasWare.Data.Classes.TX
{
    public static class StateProvinceTX
    {
        public static StateProvince ReadFromDTO(StateProvince target, StateProvinceDTO source)
        {
            target.StateProvinceId = source.StateProvinceId;
            target.StateProvinceCode = source.StateProvinceCode;
            target.CountryRegionCode = source.CountryRegionCode;
            target.IsOnlyStateProvinceFlag = source.IsOnlyStateProvinceFlag;
            target.Name = source.Name;
            target.TerritoryId = source.TerritoryId;
            target.ModifiedDate = source.ModifiedDate;
            return target;
        }

        public static StateProvinceDTO WriteToDTO(StateProvince source)
        {
            return new StateProvinceDTO
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

        public static int Compare(StateProvinceDTO lhs, StateProvince rhs)
        {
            return Compare(rhs, lhs) * -1;
        }

        public static int Compare(StateProvince lhs, StateProvinceDTO rhs)
        {
            if (ReferenceEquals(lhs, null))
            {
                return -1;
            }

            if (ReferenceEquals(rhs, null))
            {
                return 1;
            }

            return lhs.StateProvinceId.CompareTo(lhs.StateProvinceId);
        }

    }
}
