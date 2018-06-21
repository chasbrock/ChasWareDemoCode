// WARNING: this code is auto generated and should not be modified.
// hint:    if you need to modify it, let it build into a non-project directory
//          then use a text comparison to sync any changes.

using ChasWare.Data.Classes.DTO;

namespace ChasWare.Data.Classes.TX
{
    public static class StoreTX
    {
        #region public methods

        public static void ReadFromDTO(StoreDTO source, Store target)
        {
            target.SalesPerson = source.SalesPerson;
            target.SalesPersonId = source.SalesPersonId;
            target.StoreName = source.StoreName;
            target.Store.EntityId = source.EntityId;
            target.Store.ModifiedDate = source.ModifiedDate;
        }

        public static StoreDTO WriteToDTO(Store source)
        {
            StoreDTO created = new StoreDTO();
            created.SalesPerson = source.SalesPerson;
            created.SalesPersonId = source.SalesPersonId;
            created.StoreName = source.StoreName;
            created.EntityId = source.Store.EntityId;
            created.ModifiedDate = source.Store.ModifiedDate;
            return created;
        }

        #endregion
    }
}