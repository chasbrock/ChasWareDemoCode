
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
    public static class ContactDetailTX
    {
        public static ContactDetail ReadFromDTO(ContactDetail target, ContactDetailDTO source)
        {
            target.ContactId = source.ContactId;
            target.Details = source.Details;
            target.ModifiedDate = source.ModifiedDate;
            return target;
        }

        public static ContactDetailDTO WriteToDTO(ContactDetail source)
        {
            return new ContactDetailDTO
                {
                    ContactId = source.ContactId,
                    Details = source.Details,
                    ModifiedDate = source.ModifiedDate,
                };
        }

        public static int Compare(ContactDetailDTO lhs, ContactDetail rhs)
        {
            return Compare(rhs, lhs) * -1;
        }

        public static int Compare(ContactDetail lhs, ContactDetailDTO rhs)
        {
            if (ReferenceEquals(lhs, null))
            {
                return -1;
            }

            if (ReferenceEquals(rhs, null))
            {
                return 1;
            }

            return lhs.ContactId.CompareTo(lhs.ContactId);
        }

    }
}
