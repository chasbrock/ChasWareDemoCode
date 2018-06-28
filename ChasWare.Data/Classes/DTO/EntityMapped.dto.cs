
// WARNING: this code is auto generated and should not be modified.
// hint:    if you need to modify it, let it build into a non-project directory
//          then use a text comparison tool to sync any changes.
// (c) Chas.Brock@gmail.com 2018

using System;
using System.Collections.Generic;

namespace ChasWare.Data.Classes.DTO
{
    public class EntityMappedDTO
    {
        public int EntityId { get; set;}
        public ICollection<EntityAddressDTO> Addresses { get; set;}
        public ICollection<EntityContactDTO> ContactDetails { get; set;}
        public DateTime ModifiedDate { get; set;}
    }
}
