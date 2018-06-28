
// WARNING: this code is auto generated and should not be modified.
// hint:    if you need to modify it, let it build into a non-project directory
//          then use a text comparison tool to sync any changes.
// (c) Chas.Brock@gmail.com 2018

using System;
using System.Collections.Generic;

namespace ChasWare.Data.Classes.DTO
{
    public class CustomerDTO
    {
        public int CustomerId { get; set;}
        public int? SalesTerritoryId { get; set;}
        public SalesTerritoryDTO SalesTerritory { get; set;}
        public int? StoreId { get; set;}
        public StoreDTO Store { get; set;}
        public int? PersonId { get; set;}
        public PersonDTO Person { get; set;}
        public string AccountNumber { get; set;}
        public DateTime ModifiedDate { get; set;}
    }
}
