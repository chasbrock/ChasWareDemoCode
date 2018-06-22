
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
 public static class EntityMappedTX
  {
    public static void ReadFromDTO(EntityMapped target, EntityMappedDTO source)
    {
       target.EntityId = source.EntityId;
       //[TODO] target.Entity.Addresses = source.Addresses;
       //[TODO] target.Entity.ContactDetails = source.ContactDetails;
       target.ModifiedDate = source.ModifiedDate;
    }

    public static EntityMappedDTO WriteToDTO(EntityMapped source)
    {
       return  new EntityMappedDTO
       {
         EntityId = source.EntityId,
         Addresses = source.Entity.Addresses.Select(EntityAddressTX.WriteToDTO).ToArray(),
         ContactDetails = source.Entity.ContactDetails.Select(EntityContactTX.WriteToDTO).ToArray(),
         ModifiedDate = source.ModifiedDate,
       };
    }

  }
}
