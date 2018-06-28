
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
    public static class SalesPersonTX
    {
        public static void ReadFromDTO(SalesPerson target, SalesPersonDTO source)
        {
            target.EntityId = source.EntityId;
            PersonTX.ReadFromDTO(target.Person, source.Person);
            SalesTerritoryTX.ReadFromDTO(target.SalesTerritory, source.SalesTerritory);
            target.SalesTerritoryId = source.SalesTerritoryId;
            target.SalesQuota = source.SalesQuota;
            target.Bonus = source.Bonus;
            target.CommissionPct = source.CommissionPct;
            target.SalesYTD = source.SalesYTD;
            target.SalesLastYear = source.SalesLastYear;
            target.ModifiedDate = source.ModifiedDate;
        }

        public static SalesPersonDTO WriteToDTO(SalesPerson source)
        {
            return new SalesPersonDTO
                {
                    EntityId = source.EntityId,
                    Person = PersonTX.WriteToDTO(source.Person),
                    SalesTerritory = SalesTerritoryTX.WriteToDTO(source.SalesTerritory),
                    SalesTerritoryId = source.SalesTerritoryId,
                    SalesQuota = source.SalesQuota,
                    Bonus = source.Bonus,
                    CommissionPct = source.CommissionPct,
                    SalesYTD = source.SalesYTD,
                    SalesLastYear = source.SalesLastYear,
                    ModifiedDate = source.ModifiedDate,
                };
        }

        public static int Compare(SalesPersonDTO lhs, SalesPerson rhs)
        {
            return Compare(rhs, lhs) * -1;
        }

        public static int Compare(SalesPerson lhs, SalesPersonDTO rhs)
        {
            if (ReferenceEquals(lhs, null))
            {
                return -1;
            }

            if (ReferenceEquals(rhs, null))
            {
                return 1;
            }

            return lhs.EntityId.CompareTo(lhs.EntityId);
        }

    }
}
