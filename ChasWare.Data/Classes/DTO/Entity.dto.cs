// WARNING: this code is auto generated and should not be modified.
// hint:    if you need to modify it, let it build into a non-project directory
//          then use a text comparison to sync any changes.

using System;
using System.Collections.Generic;

namespace ChasWare.Data.Classes.DTO
{
    public class EntityDTO
    {
        #region public properties

        public ICollection<EntityAddressDTO> Addresses { get; set; }
        public ICollection<EntityContactDTO> ContactDetails { get; set; }
        public int EntityId { get; set; }
        public DateTime ModifiedDate { get; set; }

        #endregion
    }
}