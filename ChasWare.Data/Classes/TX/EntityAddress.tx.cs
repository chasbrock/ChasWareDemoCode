
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
 public static class EntityAddressTX
  {
    public static void ReadFromDTO(EntityAddress target, EntityAddressDTO source)
    {
       AddressTX.ReadFromDTO(target.Address, source.Address);
       AddressTypeTX.ReadFromDTO(target.AddressType, source.AddressType);
       EntityTX.ReadFromDTO(target.Entity, source.Entity);
       target.AddressId = source.AddressId;
       target.EntityId = source.EntityId;
       target.AddressTypeId = source.AddressTypeId;
       target.ModifiedDate = source.ModifiedDate;
    }

    public static EntityAddressDTO WriteToDTO(EntityAddress source)
    {
       return  new EntityAddressDTO
       {
         Address = AddressTX.WriteToDTO(source.Address),
         AddressType = AddressTypeTX.WriteToDTO(source.AddressType),
         Entity = EntityTX.WriteToDTO(source.Entity),
         AddressId = source.AddressId,
         EntityId = source.EntityId,
         AddressTypeId = source.AddressTypeId,
         ModifiedDate = source.ModifiedDate,
       };
    }

  }
}
