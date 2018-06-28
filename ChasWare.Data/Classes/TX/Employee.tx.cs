
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
    public static class EmployeeTX
    {
        public static Employee ReadFromDTO(Employee target, EmployeeDTO source)
        {
            target.PersonId = source.PersonId;
            PersonTX.ReadFromDTO(target.Person, source.Person);
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
            return target;
        }

        public static EmployeeDTO WriteToDTO(Employee source)
        {
            return new EmployeeDTO
                {
                    PersonId = source.PersonId,
                    Person = PersonTX.WriteToDTO(source.Person),
                    LoginId = source.LoginId,
                    JobTitle = source.JobTitle,
                    CurrentFlag = source.CurrentFlag,
                    HireDate = source.HireDate,
                    BirthDate = source.BirthDate,
                    Gender = source.Gender,
                    MaritalStatus = source.MaritalStatus,
                    NationalIDNumber = source.NationalIDNumber,
                    SalariedFlag = source.SalariedFlag,
                    SickLeaveHours = source.SickLeaveHours,
                    VacationHours = source.VacationHours,
                    ModifiedDate = source.ModifiedDate,
                };
        }

        public static int Compare(EmployeeDTO lhs, Employee rhs)
        {
            return Compare(rhs, lhs) * -1;
        }

        public static int Compare(Employee lhs, EmployeeDTO rhs)
        {
            if (ReferenceEquals(lhs, null))
            {
                return -1;
            }

            if (ReferenceEquals(rhs, null))
            {
                return 1;
            }

            return lhs.PersonId.CompareTo(lhs.PersonId);
        }

    }
}
