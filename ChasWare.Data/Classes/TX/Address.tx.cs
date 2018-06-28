
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
    public static class AddressTX
    {
        public static Address ReadFromDTO(Address target, AddressDTO source)
        {
            target.AddressId = source.AddressId;
            target.AddressLine1 = source.AddressLine1;
            target.AddressLine2 = source.AddressLine2;
            target.City = source.City;
            target.StateProvinceId = source.StateProvinceId;
            target.StateProvince.StateProvinceCode = source.StateProvinceCode;
            target.StateProvince.CountryRegionCode = source.CountryRegionCode;
            target.StateProvince.IsOnlyStateProvinceFlag = source.IsOnlyStateProvinceFlag;
            target.StateProvince.Name = source.Name;
            target.StateProvince.TerritoryId = source.TerritoryId;
            target.PostalCode = source.PostalCode;
            target.ModifiedDate = source.ModifiedDate;
            return target;
        }

        public static AddressDTO WriteToDTO(Address source)
        {
            return new AddressDTO
                {
                    AddressId = source.AddressId,
                    AddressLine1 = source.AddressLine1,
                    AddressLine2 = source.AddressLine2,
                    City = source.City,
                    StateProvinceId = source.StateProvinceId,
                    StateProvinceCode = source.StateProvince.StateProvinceCode,
                    CountryRegionCode = source.StateProvince.CountryRegionCode,
                    IsOnlyStateProvinceFlag = source.StateProvince.IsOnlyStateProvinceFlag,
                    Name = source.StateProvince.Name,
                    TerritoryId = source.StateProvince.TerritoryId,
                    PostalCode = source.PostalCode,
                    ModifiedDate = source.ModifiedDate,
                };
        }

        public static int Compare(AddressDTO lhs, Address rhs)
        {
            return Compare(rhs, lhs) * -1;
        }

        public static int Compare(Address lhs, AddressDTO rhs)
        {
            if (ReferenceEquals(lhs, null))
            {
                return -1;
            }

            if (ReferenceEquals(rhs, null))
            {
                return 1;
            }

            return lhs.AddressId.CompareTo(lhs.AddressId);
        }

    }
}
