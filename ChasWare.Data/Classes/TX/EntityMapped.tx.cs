// WARNING: this code is auto generated and should not be modified.
// hint:    if you need to modify it, let it build into a non-project directory
//          then use a text comparison to sync any changes.

using ChasWare.Data.Classes.DTO;

namespace ChasWare.Data.Classes.TX
{
    public static class EntityMappedTX
    {
        #region public methods

        public static void ReadFromDTO(EntityMappedDTO source, EntityMapped target)
        {
            target.EntityId = source.EntityId;
            target.ModifiedDate = source.ModifiedDate;
        }

        public static EntityMappedDTO WriteToDTO(EntityMapped source)
        {
            EntityMappedDTO created = new EntityMappedDTO();
            created.EntityId = source.EntityId;
            created.ModifiedDate = source.ModifiedDate;
            return created;
        }

        #endregion
    }
}