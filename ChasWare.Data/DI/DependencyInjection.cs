// --------------------------------------------------------------------------------------------------------------------
// <copyright file=DependencyInjection.cs company="chas.brock@outlook.com">
//      copyright charlie brock 2018 
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using ChasWare.Data.Classes;
using Common;
using Spring.Context.Attributes;
using Spring.Objects.Factory.Support;

namespace ChasWare.Data.DI
{
    [Configuration]
    public class DependencyInjection
    {
        #region public methods

        [Definition, Scope(ObjectScope.Prototype)]
        public virtual IEnumerable<Address> GetAddresses()
        {
            return GetDataContext().Addresses;
        }

        [Definition, Scope(ObjectScope.Prototype)]
        public virtual IEnumerable<AddressType> GetAddressTypes()
        {
            return GetDataContext().AddressTypes;
        }

        [Definition, Scope(ObjectScope.Prototype)]
        public virtual IEnumerable<ContactDetail> GetContactDetails()
        {
            return GetDataContext().ContactDetails;
        }

        [Definition, Scope(ObjectScope.Prototype)]
        public virtual IEnumerable<ContactType> GetContactTypes()
        {
            return GetDataContext().ContactTypes;
        }

        [Definition, Scope(ObjectScope.Prototype)]
        public virtual IEnumerable<Customer> GetCustomers()
        {
            return GetDataContext().Customers;
        }

        [Definition, Scope(ObjectScope.Prototype)]
        public virtual IEnumerable<Department> GetDepartments()
        {
            return GetDataContext().Departments;
        }

        [Definition, Scope(ObjectScope.Prototype)]
        public virtual IEnumerable<Employee> GetEmployees()
        {
            return GetDataContext().Employees;
        }

        [Definition, Scope(ObjectScope.Prototype)]
        public virtual IEnumerable<Person> GetPersons()
        {
            return GetDataContext().Persons;
        }

        [Definition, Scope(ObjectScope.Prototype)]
        public virtual IEnumerable<Role> GetRoles()
        {
            return GetDataContext().Roles;
        }

        [Definition, Scope(ObjectScope.Prototype)]
        public virtual IEnumerable<SalesPerson> GetSalesPersons()
        {
            return GetDataContext().SalesPersons;
        }

        [Definition, Scope(ObjectScope.Prototype)]
        public virtual IEnumerable<SalesTerritory> GetSalesTerritories()
        {
            return GetDataContext().SalesTerritories;
        }

        [Definition, Scope(ObjectScope.Prototype)]
        public virtual IEnumerable<StateProvince> GetStateProvinces()
        {
            return GetDataContext().StateProvinces;
        }

        [Definition, Scope(ObjectScope.Prototype)]
        public virtual IEnumerable<Store> GetStores()
        {
            return GetDataContext().Stores;
        }

        #endregion

        #region other methods

        /// <summary>
        ///     Gets a new DataContext
        /// </summary>
        /// <returns>
        ///     returns a new datacontext <see cref="DataContext" />.
        /// </returns>
        [Definition(Names = Constants.DbContext), Scope(ObjectScope.Prototype)]
        protected virtual DataContext GetDataContext()
        {
            return new DataContext();
        }

        #endregion
    }
}