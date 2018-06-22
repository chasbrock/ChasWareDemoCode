
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
 public static class EntityTX
  {
    public static void ReadFromDTO(Entity target, EntityDTO source)
    {
       //[TODO] target.Addresses = source.Addresses;
       //[TODO] target.ContactDetails = source.ContactDetails;
       target.EntityId = source.EntityId;
       target.ModifiedDate = source.ModifiedDate;
    }

    public static EntityDTO WriteToDTO(Entity source)
    {
       return  new EntityDTO
       {
         Addresses = source.Addresses.Select(EntityAddressTX.WriteToDTO).ToArray(),
         ContactDetails = source.ContactDetails.Select(EntityContactTX.WriteToDTO).ToArray(),
         EntityId = source.EntityId,
         ModifiedDate = source.ModifiedDate,
       };
    }

  }
}
