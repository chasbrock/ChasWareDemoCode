
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
    public static class AddressTypeTX
    {
        public static AddressType ReadFromDTO(AddressType target, AddressTypeDTO source)
        {
            target.AddressTypeId = source.AddressTypeId;
            target.Name = source.Name;
            target.ModifiedDate = source.ModifiedDate;
            return target;
        }

        public static AddressTypeDTO WriteToDTO(AddressType source)
        {
            return new AddressTypeDTO
                {
                    AddressTypeId = source.AddressTypeId,
                    Name = source.Name,
                    ModifiedDate = source.ModifiedDate,
                };
        }

        public static int Compare(AddressTypeDTO lhs, AddressType rhs)
        {
            return Compare(rhs, lhs) * -1;
        }

        public static int Compare(AddressType lhs, AddressTypeDTO rhs)
        {
            if (ReferenceEquals(lhs, null))
            {
                return -1;
            }

            if (ReferenceEquals(rhs, null))
            {
                return 1;
            }

            return lhs.AddressTypeId.CompareTo(lhs.AddressTypeId);
        }

    }
}
