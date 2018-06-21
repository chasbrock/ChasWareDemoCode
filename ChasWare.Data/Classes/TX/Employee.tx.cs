// WARNING: this code is auto generated and should not be modified.
// hint:    if you need to modify it, let it build into a non-project directory
//          then use a text comparison to sync any changes.

using ChasWare.Data.Classes.DTO;

namespace ChasWare.Data.Classes.TX
{
    public static class EmployeeTX
    {
        #region public methods

        public static void ReadFromDTO(EmployeeDTO source, Employee target)
        {
            target.PersonId = source.PersonId;
            target.LoginId = source.LoginId;
            target.JobTitle = source.JobTitle;
            target.CurrentFlag = source.CurrentFlag;
            target.HireDate = source.HireDate;
            target.BirthDate = source.BirthDate;
            target.Gender = source.Gender;
            target.MaritalStatus = source.MaritalStatus;
            target.NationalIDNumber = source.NationalIDNumber;
            target.SalariedFlag = source.SalariedFlag;
            target.SickLeaveHours = source.SickLeaveHours;
            target.VacationHours = source.VacationHours;
            target.ModifiedDate = source.ModifiedDate;
        }

        public static EmployeeDTO WriteToDTO(Employee source)
        {
            EmployeeDTO created = new EmployeeDTO();
            created.PersonId = source.PersonId;
            created.LoginId = source.LoginId;
            created.JobTitle = source.JobTitle;
            created.CurrentFlag = source.CurrentFlag;
            created.HireDate = source.HireDate;
            created.BirthDate = source.BirthDate;
            created.Gender = source.Gender;
            created.MaritalStatus = source.MaritalStatus;
            created.NationalIDNumber = source.NationalIDNumber;
            created.SalariedFlag = source.SalariedFlag;
            created.SickLeaveHours = source.SickLeaveHours;
            created.VacationHours = source.VacationHours;
            created.ModifiedDate = source.ModifiedDate;
            return created;
        }

        #endregion
    }
}