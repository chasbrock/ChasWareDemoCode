
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
    public static class SalesTerritoryTX
    {
        public static void ReadFromDTO(SalesTerritory target, SalesTerritoryDTO source)
        {
            target.TerritoryId = source.TerritoryId;
            target.Name = source.Name;
            target.CountryRegionCode = source.CountryRegionCode;
            target.SalesGroup = source.SalesGroup;
            target.SalesYTD = source.SalesYTD;
            target.SalesLastYear = source.SalesLastYear;
            target.CostYTD = source.CostYTD;
            target.CostLastYear = source.CostLastYear;
            target.ModifiedDate = source.ModifiedDate;
        }

        public static SalesTerritoryDTO WriteToDTO(SalesTerritory source)
        {
            return new SalesTerritoryDTO
                {
                    TerritoryId = source.TerritoryId,
                    Name = source.Name,
                    CountryRegionCode = source.CountryRegionCode,
                    SalesGroup = source.SalesGroup,
                    SalesYTD = source.SalesYTD,
                    SalesLastYear = source.SalesLastYear,
                    CostYTD = source.CostYTD,
                    CostLastYear = source.CostLastYear,
                    ModifiedDate = source.ModifiedDate,
                };
        }

        public static int Compare(SalesTerritoryDTO lhs, SalesTerritory rhs)
        {
            return Compare(rhs, lhs) * -1;
        }

        public static int Compare(SalesTerritory lhs, SalesTerritoryDTO rhs)
        {
            if (ReferenceEquals(lhs, null))
            {
                return -1;
            }

            if (ReferenceEquals(rhs, null))
            {
                return 1;
            }

            return lhs.TerritoryId.CompareTo(lhs.TerritoryId);
        }

    }
}
