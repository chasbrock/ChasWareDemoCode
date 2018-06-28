
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
    public static class RoleTX
    {
        public static Role ReadFromDTO(Role target, RoleDTO source)
        {
            target.RoleId = source.RoleId;
            target.Name = source.Name;
            target.ModifiedDate = source.ModifiedDate;
            return target;
        }

        public static RoleDTO WriteToDTO(Role source)
        {
            return new RoleDTO
                {
                    RoleId = source.RoleId,
                    Name = source.Name,
                    ModifiedDate = source.ModifiedDate,
                };
        }

        public static int Compare(RoleDTO lhs, Role rhs)
        {
            return Compare(rhs, lhs) * -1;
        }

        public static int Compare(Role lhs, RoleDTO rhs)
        {
            if (ReferenceEquals(lhs, null))
            {
                return -1;
            }

            if (ReferenceEquals(rhs, null))
            {
                return 1;
            }

            return lhs.RoleId.CompareTo(lhs.RoleId);
        }

    }
}
