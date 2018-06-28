
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
        public static Entity ReadFromDTO(Entity target, EntityDTO source)
        {
            ReadAddressesFromDTO(target.Addresses, source.Addresses);
            ReadContactDetailsFromDTO(target.ContactDetails, source.ContactDetails);
            target.EntityId = source.EntityId;
            target.ModifiedDate = source.ModifiedDate;
            return target;
        }

        public static void ReadAddressesFromDTO(ICollection<EntityAddress> target, IEnumerable<EntityAddressDTO> source)
        {
            List<EntityAddress> existing = target.ToList();
            foreach (EntityAddressDTO item in source)
            {
                EntityAddress found = target.FirstOrDefault(t => EntityAddressTX.Compare(t, item) == 0);
                if (found != null)
                {
                    EntityAddressTX.ReadFromDTO(found, item);
                    existing.Remove(found);
                    continue;
                }
                 target.Add(EntityAddressTX.ReadFromDTO(new EntityAddress(), item));
            }
            foreach(EntityAddress deleted in existing)
            {
                target.Remove(deleted);
            }
        }

        public static void ReadContactDetailsFromDTO(ICollection<EntityContact> target, IEnumerable<EntityContactDTO> source)
        {
            List<EntityContact> existing = target.ToList();
            foreach (EntityContactDTO item in source)
            {
                EntityContact found = target.FirstOrDefault(t => EntityContactTX.Compare(t, item) == 0);
                if (found != null)
                {
                    EntityContactTX.ReadFromDTO(found, item);
                    existing.Remove(found);
                    continue;
                }
                 target.Add(EntityContactTX.ReadFromDTO(new EntityContact(), item));
            }
            foreach(EntityContact deleted in existing)
            {
                target.Remove(deleted);
            }
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
