
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
 public static class ContactDetailTX
  {
    public static void ReadFromDTO(ContactDetail target, ContactDetailDTO source)
    {
       target.ContactId = source.ContactId;
       target.Details = source.Details;
       target.ModifiedDate = source.ModifiedDate;
    }

    public static ContactDetailDTO WriteToDTO(ContactDetail source)
    {
       return  new ContactDetailDTO
       {
         ContactId = source.ContactId,
         Details = source.Details,
         ModifiedDate = source.ModifiedDate,
       };
    }

  }
}
