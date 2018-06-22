
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
 public static class RoleTX
  {
    public static void ReadFromDTO(Role target, RoleDTO source)
    {
       target.RoleId = source.RoleId;
       target.Name = source.Name;
       target.ModifiedDate = source.ModifiedDate;
    }

    public static RoleDTO WriteToDTO(Role source)
    {
       return  new RoleDTO
       {
         RoleId = source.RoleId,
         Name = source.Name,
         ModifiedDate = source.ModifiedDate,
       };
    }

  }
}
