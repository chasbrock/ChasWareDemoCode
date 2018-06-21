// WARNING: this code is auto generated and should not be modified.
// hint:    if you need to modify it, let it build into a non-project directory
//          then use a text comparison to sync any changes.

using ChasWare.Data.Classes.DTO;

namespace ChasWare.Data.Classes.TX
{
    public static class DepartmentTX
    {
        #region public methods

        public static void ReadFromDTO(DepartmentDTO source, Department target)
        {
            target.DepartmentId = source.DepartmentId;
            target.Name = source.Name;
            target.GroupName = source.GroupName;
            target.ModifiedDate = source.ModifiedDate;
        }

        public static DepartmentDTO WriteToDTO(Department source)
        {
            DepartmentDTO created = new DepartmentDTO();
            created.DepartmentId = source.DepartmentId;
            created.Name = source.Name;
            created.GroupName = source.GroupName;
            created.ModifiedDate = source.ModifiedDate;
            return created;
        }

        #endregion
    }
}