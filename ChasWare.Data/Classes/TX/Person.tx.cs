
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
 public static class PersonTX
  {
    public static void ReadFromDTO(Person target, PersonDTO source)
    {
       target.RoleId = source.RoleId;
       target.Role.Name = source.Name;
       target.PersonType = source.PersonType;
       target.Title = source.Title;
       target.FirstName = source.FirstName;
       target.MiddleName = source.MiddleName;
       target.LastName = source.LastName;
       target.Suffix = source.Suffix;
       target.EntityId = source.EntityId;
       //[TODO] target.Entity.Addresses = source.Addresses;
       //[TODO] target.Entity.ContactDetails = source.ContactDetails;
       target.ModifiedDate = source.ModifiedDate;
    }

    public static PersonDTO WriteToDTO(Person source)
    {
       return  new PersonDTO
       {
         RoleId = source.RoleId,
         Name = source.Role.Name,
         PersonType = source.PersonType,
         Title = source.Title,
         FirstName = source.FirstName,
         MiddleName = source.MiddleName,
         LastName = source.LastName,
         Suffix = source.Suffix,
         EntityId = source.EntityId,
         Addresses = source.Entity.Addresses.Select(EntityAddressTX.WriteToDTO).ToArray(),
         ContactDetails = source.Entity.ContactDetails.Select(EntityContactTX.WriteToDTO).ToArray(),
         ModifiedDate = source.ModifiedDate,
       };
    }

  }
}
