
// WARNING: this code is auto generated and should not be modified.
// hint:    if you need to modify it, let it build into a non-project directory
//          then use a text comparison tool to sync any changes.
// (c) Chas.Brock@gmail.com 2018

using System;
using System.Collections.Generic;

namespace ChasWare.Data.Classes.DTO
{
    public class PersonDTO
    {
        public int? RoleId { get; set;}
        public string Name { get; set;}
        public string PersonType { get; set;}
        public string Title { get; set;}
        public string FirstName { get; set;}
        public string MiddleName { get; set;}
        public string LastName { get; set;}
        public string Suffix { get; set;}
        public int EntityId { get; set;}
        public ICollection<EntityAddressDTO> Addresses { get; set;}
        public ICollection<EntityContactDTO> ContactDetails { get; set;}
        public DateTime ModifiedDate { get; set;}
    }
}
