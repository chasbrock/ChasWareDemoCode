
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
 public static class StoreTX
  {
    public static void ReadFromDTO(Store target, StoreDTO source)
    {
       SalesPersonTX.ReadFromDTO(target.SalesPerson, source.SalesPerson);
       target.SalesPersonId = source.SalesPersonId;
       target.StoreName = source.StoreName;
       target.EntityId = source.EntityId;
       //[TODO] target.Entity.Addresses = source.Addresses;
       //[TODO] target.Entity.ContactDetails = source.ContactDetails;
       target.ModifiedDate = source.ModifiedDate;
    }

    public static StoreDTO WriteToDTO(Store source)
    {
       return  new StoreDTO
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

  }
}
