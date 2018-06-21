// WARNING: this code is auto generated and should not be modified.
// hint:    if you need to modify it, let it build into a non-project directory
//          then use a text comparison to sync any changes.

using ChasWare.Data.Classes.DTO;

namespace ChasWare.Data.Classes.TX
{
    public static class SalesPersonTX
    {
        #region public methods

        public static void ReadFromDTO(SalesPersonDTO source, SalesPerson target)
        {
            target.EntityId = source.EntityId;
            target.SalesTerritoryId = source.SalesTerritoryId;
            target.SalesQuota = source.SalesQuota;
            target.Bonus = source.Bonus;
            target.CommissionPct = source.CommissionPct;
            target.SalesYTD = source.SalesYTD;
            target.SalesLastYear = source.SalesLastYear;
            target.ModifiedDate = source.ModifiedDate;
        }

        public static SalesPersonDTO WriteToDTO(SalesPerson source)
        {
            SalesPersonDTO created = new SalesPersonDTO();
            created.EntityId = source.EntityId;
            created.SalesTerritoryId = source.SalesTerritoryId;
            created.SalesQuota = source.SalesQuota;
            created.Bonus = source.Bonus;
            created.CommissionPct = source.CommissionPct;
            created.SalesYTD = source.SalesYTD;
            created.SalesLastYear = source.SalesLastYear;
            created.ModifiedDate = source.ModifiedDate;
            return created;
        }

        #endregion
    }
}