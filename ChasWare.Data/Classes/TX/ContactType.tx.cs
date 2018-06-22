
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
 public static class ContactTypeTX
  {
    public static void ReadFromDTO(ContactType target, ContactTypeDTO source)
    {
       target.ContactTypeId = source.ContactTypeId;
       target.Type = source.Type;
       target.ModifiedDate = source.ModifiedDate;
    }

    public static ContactTypeDTO WriteToDTO(ContactType source)
    {
       return  new ContactTypeDTO
       {
         ContactTypeId = source.ContactTypeId,
         Type = source.Type,
         ModifiedDate = source.ModifiedDate,
       };
    }

  }
}
