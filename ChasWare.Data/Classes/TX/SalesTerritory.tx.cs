// WARNING: this code is auto generated and should not be modified.
// hint:    if you need to modify it, let it build into a non-project directory
//          then use a text comparison to sync any changes.

using ChasWare.Data.Classes.DTO;

namespace ChasWare.Data.Classes.TX
{
    public static class SalesTerritoryTX
    {
        #region public methods

        public static void ReadFromDTO(SalesTerritoryDTO source, SalesTerritory target)
        {
            target.TerritoryId = source.TerritoryId;
            target.Name = source.Name;
            target.CountryRegionCode = source.CountryRegionCode;
            target.SalesGroup = source.SalesGroup;
            target.SalesYTD = source.SalesYTD;
            target.SalesLastYear = source.SalesLastYear;
            target.CostYTD = source.CostYTD;
            target.CostLastYear = source.CostLastYear;
            target.ModifiedDate = source.ModifiedDate;
        }

        public static SalesTerritoryDTO WriteToDTO(SalesTerritory source)
        {
            SalesTerritoryDTO created = new SalesTerritoryDTO();
            created.TerritoryId = source.TerritoryId;
            created.Name = source.Name;
            created.CountryRegionCode = source.CountryRegionCode;
            created.SalesGroup = source.SalesGroup;
            created.SalesYTD = source.SalesYTD;
            created.SalesLastYear = source.SalesLastYear;
            created.CostYTD = source.CostYTD;
            created.CostLastYear = source.CostLastYear;
            created.ModifiedDate = source.ModifiedDate;
            return created;
        }

        #endregion
    }
}