// WARNING: this code is auto generated and should not be modified.
// hint:    if you need to modify it, let it build into a non-project directory
//          then use a text comparison to sync any changes.

using System;

namespace ChasWare.Data.Classes.DTO
{
    public class EntityContactDTO
    {
        #region public properties

        public ContactDetailDTO ContactDetail { get; set; }
        public int ContactId { get; set; }
        public ContactTypeDTO ContactType { get; set; }
        public int ContactTypeId { get; set; }
        public EntityDTO Entity { get; set; }
        public int EntityId { get; set; }
        public DateTime ModifiedDate { get; set; }

        #endregion
    }
}