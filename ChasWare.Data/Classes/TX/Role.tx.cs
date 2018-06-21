// WARNING: this code is auto generated and should not be modified.
// hint:    if you need to modify it, let it build into a non-project directory
//          then use a text comparison to sync any changes.

using ChasWare.Data.Classes.DTO;

namespace ChasWare.Data.Classes.TX
{
    public static class RoleTX
    {
        #region public methods

        public static void ReadFromDTO(RoleDTO source, Role target)
        {
            target.RoleId = source.RoleId;
            target.Name = source.Name;
            target.ModifiedDate = source.ModifiedDate;
        }

        public static RoleDTO WriteToDTO(Role source)
        {
            RoleDTO created = new RoleDTO();
            created.RoleId = source.RoleId;
            created.Name = source.Name;
            created.ModifiedDate = source.ModifiedDate;
            return created;
        }

        #endregion
    }
}