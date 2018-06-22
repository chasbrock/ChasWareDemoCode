
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
 public static class DepartmentTX
  {
    public static void ReadFromDTO(Department target, DepartmentDTO source)
    {
       target.DepartmentId = source.DepartmentId;
       target.Name = source.Name;
       target.GroupName = source.GroupName;
       target.ModifiedDate = source.ModifiedDate;
    }

    public static DepartmentDTO WriteToDTO(Department source)
    {
       return  new DepartmentDTO
       {
         DepartmentId = source.DepartmentId,
         Name = source.Name,
         GroupName = source.GroupName,
         ModifiedDate = source.ModifiedDate,
       };
    }

  }
}
