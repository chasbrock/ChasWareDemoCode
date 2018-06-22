
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
 public static class AddressTypeTX
  {
    public static void ReadFromDTO(AddressType target, AddressTypeDTO source)
    {
       target.AddressTypeId = source.AddressTypeId;
       target.Name = source.Name;
       target.ModifiedDate = source.ModifiedDate;
    }

    public static AddressTypeDTO WriteToDTO(AddressType source)
    {
       return  new AddressTypeDTO
       {
         AddressTypeId = source.AddressTypeId,
         Name = source.Name,
         ModifiedDate = source.ModifiedDate,
       };
    }

  }
}
