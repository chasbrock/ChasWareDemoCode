// WARNING: this code is auto generated and should not be modified.
// hint:    if you need to modify it, let it build into a non-project directory
//          then use a text comparison to sync any changes.

using System;

namespace ChasWare.Data.Classes.DTO
{
    public class CustomerDTO
    {
        #region public properties

        public string AccountNumber { get; set; }
        public int CustomerId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public PersonDTO Person { get; set; }
        public int? PersonId { get; set; }
        public SalesTerritoryDTO SalesTerritory { get; set; }
        public int? SalesTerritoryId { get; set; }
        public StoreDTO Store { get; set; }
        public int? StoreId { get; set; }

        #endregion
    }
}