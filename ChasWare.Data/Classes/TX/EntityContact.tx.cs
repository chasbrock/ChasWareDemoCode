
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
 public static class EntityContactTX
  {
    public static void ReadFromDTO(EntityContact target, EntityContactDTO source)
    {
       ContactDetailTX.ReadFromDTO(target.ContactDetail, source.ContactDetail);
       ContactTypeTX.ReadFromDTO(target.ContactType, source.ContactType);
       EntityTX.ReadFromDTO(target.Entity, source.Entity);
       target.ContactId = source.ContactId;
       target.EntityId = source.EntityId;
       target.ContactTypeId = source.ContactTypeId;
       target.ModifiedDate = source.ModifiedDate;
    }

    public static EntityContactDTO WriteToDTO(EntityContact source)
    {
       return  new EntityContactDTO
       {
         ContactDetail = ContactDetailTX.WriteToDTO(source.ContactDetail),
         ContactType = ContactTypeTX.WriteToDTO(source.ContactType),
         Entity = EntityTX.WriteToDTO(source.Entity),
         ContactId = source.ContactId,
         EntityId = source.EntityId,
         ContactTypeId = source.ContactTypeId,
         ModifiedDate = source.ModifiedDate,
       };
    }

  }
}
