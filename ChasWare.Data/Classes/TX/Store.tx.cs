
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
    public static class StoreTX
    {
        public static void ReadFromDTO(Store target, StoreDTO source)
        {
            SalesPersonTX.ReadFromDTO(target.SalesPerson, source.SalesPerson);
            target.SalesPersonId = source.SalesPersonId;
            target.StoreName = source.StoreName;
            target.EntityId = source.EntityId;
            target.ModifiedDate = source.ModifiedDate;
        }

        public static StoreDTO WriteToDTO(Store source)
        {
            return new StoreDTO
                {
                    SalesPerson = SalesPersonTX.WriteToDTO(source.SalesPerson),
                    SalesPersonId = source.SalesPersonId,
                    StoreName = source.StoreName,
                    EntityId = source.EntityId,
                    Addresses = source.Entity.Addresses.Select(EntityAddressTX.WriteToDTO).ToArray(),
                    ContactDetails = source.Entity.ContactDetails.Select(EntityContactTX.WriteToDTO).ToArray(),
                    ModifiedDate = source.ModifiedDate,
                };
        }

        public static int Compare(StoreDTO lhs, Store rhs)
        {
            return Compare(rhs, lhs) * -1;
        }

        public static int Compare(Store lhs, StoreDTO rhs)
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
