// WARNING: this code is auto generated and should not be modified.
// hint:    if you need to modify it, let it build into a non-project directory
//          then use a text comparison to sync any changes.

using ChasWare.Data.Classes.DTO;

namespace ChasWare.Data.Classes.TX
{
    public static class CustomerTX
    {
        #region public methods

        public static void ReadFromDTO(CustomerDTO source, Customer target)
        {
            target.CustomerId = source.CustomerId;
            target.SalesTerritoryId = source.SalesTerritoryId;
            target.SalesTerritory = source.SalesTerritory;
            target.StoreId = source.StoreId;
            target.Store = source.Store;
            target.PersonId = source.PersonId;
            target.Person = source.Person;
            target.AccountNumber = source.AccountNumber;
            target.ModifiedDate = source.ModifiedDate;
        }

        public static CustomerDTO WriteToDTO(Customer source)
        {
            CustomerDTO created = new CustomerDTO();
            created.CustomerId = source.CustomerId;
            created.SalesTerritoryId = source.SalesTerritoryId;
            created.SalesTerritory = source.SalesTerritory;
            created.StoreId = source.StoreId;
            created.Store = source.Store;
            created.PersonId = source.PersonId;
            created.Person = source.Person;
            created.AccountNumber = source.AccountNumber;
            created.ModifiedDate = source.ModifiedDate;
            return created;
        }

        #endregion
    }
}