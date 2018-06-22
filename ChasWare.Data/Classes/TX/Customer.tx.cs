
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
 public static class CustomerTX
  {
    public static void ReadFromDTO(Customer target, CustomerDTO source)
    {
       target.CustomerId = source.CustomerId;
       target.SalesTerritoryId = source.SalesTerritoryId;
       SalesTerritoryTX.ReadFromDTO(target.SalesTerritory, source.SalesTerritory);
       target.StoreId = source.StoreId;
       StoreTX.ReadFromDTO(target.Store, source.Store);
       target.PersonId = source.PersonId;
       PersonTX.ReadFromDTO(target.Person, source.Person);
       target.AccountNumber = source.AccountNumber;
       target.ModifiedDate = source.ModifiedDate;
    }

    public static CustomerDTO WriteToDTO(Customer source)
    {
       return  new CustomerDTO
       {
         CustomerId = source.CustomerId,
         SalesTerritoryId = source.SalesTerritoryId,
         SalesTerritory = SalesTerritoryTX.WriteToDTO(source.SalesTerritory),
         StoreId = source.StoreId,
         Store = StoreTX.WriteToDTO(source.Store),
         PersonId = source.PersonId,
         Person = PersonTX.WriteToDTO(source.Person),
         AccountNumber = source.AccountNumber,
         ModifiedDate = source.ModifiedDate,
       };
    }

  }
}
