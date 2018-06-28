
// WARNING: this code is auto generated and should not be modified.
// hint:    if you need to modify it, let it build into a non-project directory
//          then use a text comparison tool to sync any changes.
// (c) Chas.Brock@gmail.com 2018

using System;
using System.Collections.Generic;
using System.Linq;
using ChasWare.Data.Classes;
using ChasWare.Data.Classes.DTO;

namespace ChasWare.Data.Classes.TX
{
    public static class DepartmentTX
    {
        public static Department ReadFromDTO(Department target, DepartmentDTO source)
        {
            target.DepartmentId = source.DepartmentId;
            target.Name = source.Name;
            target.GroupName = source.GroupName;
            target.ModifiedDate = source.ModifiedDate;
            return target;
        }

        public static DepartmentDTO WriteToDTO(Department source)
        {
            return new DepartmentDTO
                {
                    DepartmentId = source.DepartmentId,
                    Name = source.Name,
                    GroupName = source.GroupName,
                    ModifiedDate = source.ModifiedDate,
                };
        }

        public static int Compare(DepartmentDTO lhs, Department rhs)
        {
            return Compare(rhs, lhs) * -1;
        }

        public static int Compare(Department lhs, DepartmentDTO rhs)
        {
            if (ReferenceEquals(lhs, null))
            {
                return -1;
            }

            if (ReferenceEquals(rhs, null))
            {
                return 1;
            }

            return lhs.DepartmentId.CompareTo(lhs.DepartmentId);
        }

    }
}
