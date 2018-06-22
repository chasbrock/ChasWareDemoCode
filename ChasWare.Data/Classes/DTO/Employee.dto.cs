
// WARNING: this code is auto generated and should not be modified.
// hint:    if you need to modify it, let it build into a non-project directory
//          then use a text comparison tool to sync any changes.

using System;
using System.Collections.Generic;

namespace ChasWare.Data.Classes.DTO
{
  public class EmployeeDTO
{
    public int PersonId { get; set;}
    public PersonDTO Person { get; set;}
    public string LoginId { get; set;}
    public string JobTitle { get; set;}
    public bool CurrentFlag { get; set;}
    public DateTime HireDate { get; set;}
    public DateTime BirthDate { get; set;}
    public string Gender { get; set;}
    public string MaritalStatus { get; set;}
    public string NationalIDNumber { get; set;}
    public bool SalariedFlag { get; set;}
    public short SickLeaveHours { get; set;}
    public short VacationHours { get; set;}
    public DateTime ModifiedDate { get; set;}
  }
}
