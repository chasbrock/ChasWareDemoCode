
// WARNING: this code is auto generated and should not be modified.
// hint:    if you need to modify it, let it build into a non-project directory
//          then use a text comparison tool to sync any changes.

using System;
using System.Collections.Generic;

namespace ChasWare.Data.Classes.DTO
{
  public class AddressDTO
{
    public int AddressId { get; set;}
    public string AddressLine1 { get; set;}
    public string AddressLine2 { get; set;}
    public string City { get; set;}
    public int StateProvinceId { get; set;}
    public string StateProvinceCode { get; set;}
    public string CountryRegionCode { get; set;}
    public bool IsOnlyStateProvinceFlag { get; set;}
    public string Name { get; set;}
    public int TerritoryId { get; set;}
    public string PostalCode { get; set;}
    public DateTime ModifiedDate { get; set;}
  }
}
