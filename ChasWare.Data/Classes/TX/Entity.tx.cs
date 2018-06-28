
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
    public static class EntityTX
    {
        public static void ReadFromDTO(Entity target, EntityDTO source)
        {
            target.EntityId = source.EntityId;
            target.ModifiedDate = source.ModifiedDate;
        }

        public static EntityDTO WriteToDTO(Entity source)
        {
            return new EntityDTO
                {
                    Addresses = source.Addresses.Select(EntityAddressTX.WriteToDTO).ToArray(),
                    ContactDetails = source.ContactDetails.Select(EntityContactTX.WriteToDTO).ToArray(),
                    EntityId = source.EntityId,
                    ModifiedDate = source.ModifiedDate,
                };
        }

        public static int Compare(EntityDTO lhs, Entity rhs)
        {
            return Compare(rhs, lhs) * -1;
        }

        public static int Compare(Entity lhs, EntityDTO rhs)
        {
            if (ReferenceEquals(lhs, null))
            {
                return -1;
            }

            if (ReferenceEquals(rhs, null))
            {
                return 1;
            }

            return lhs.EntityId.CompareTo(lhs.EntityId);
        }

    }
}
