
// WARNING: this code is auto generated and should not be modified.
// hint:    if you need to modify it, let it build into a non-project directory
//          then use a text comparison tool to sync any changes.

using System;
using System.Collections.Generic;

namespace ChasWare.Data.Classes.DTO
{
  public class SalesPersonDTO
{
    public int EntityId { get; set;}
    public PersonDTO Person { get; set;}
    public SalesTerritoryDTO SalesTerritory { get; set;}
    public int? SalesTerritoryId { get; set;}
    public Decimal? SalesQuota { get; set;}
    public Decimal Bonus { get; set;}
    public Decimal CommissionPct { get; set;}
    public Decimal SalesYTD { get; set;}
    public Decimal SalesLastYear { get; set;}
    public DateTime ModifiedDate { get; set;}
  }
}
