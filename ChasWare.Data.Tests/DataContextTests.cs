// --------------------------------------------------------------------------------------------------------------------
// <copyright file=DataContextTests.cs company="chas.brock@outlook.com">
//      copyright charlie brock 2018 
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.Linq;
using ChasWare.Data.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChasWare.Data.Tests
{
    /// <summary>
    ///     The data context tests.
    /// </summary>
    [TestClass]
    public class DataContextTests
    {
        #region Constants and fields 

        private DataContext _dataContext;

        #endregion

        #region public methods

        /// <summary>
        ///     Make sure we have all the DataSets that we expect
        /// </summary>
        [TestMethod]
        public void CheckForDataSets()
        {
            Address address = _dataContext.Addresses.FirstOrDefault();
            Assert.IsNotNull(address);

            AddressType addressType = _dataContext.AddressTypes.FirstOrDefault();
            Assert.IsNotNull(addressType);

            ContactDetail contactDetail = _dataContext.ContactDetails.FirstOrDefault();
            Assert.IsNotNull(contactDetail);

            ContactType contactType = _dataContext.ContactTypes.FirstOrDefault();
            Assert.IsNotNull(contactType);

            Customer customer = _dataContext.Customers.FirstOrDefault();
            Assert.IsNotNull(customer);

            Department department = _dataContext.Departments.FirstOrDefault();
            Assert.IsNotNull(department);

            Employee employee = _dataContext.Employees.FirstOrDefault();
            Assert.IsNotNull(employee);

            EntityAddress entityAddress = _dataContext.EntityAddresses.FirstOrDefault();
            Assert.IsNotNull(entityAddress);

            EntityContact entityContact = _dataContext.EntityContacts.FirstOrDefault();
            Assert.IsNotNull(entityContact);

            Person person = _dataContext.Persons.FirstOrDefault();
            Assert.IsNotNull(person);

            Role role = _dataContext.Roles.FirstOrDefault();
            Assert.IsNotNull(role);

            SalesTerritory salesTerritory = _dataContext.SalesTerritories.FirstOrDefault();
            Assert.IsNotNull(salesTerritory);

            SalesPerson salesPerson = _dataContext.SalesPersons.FirstOrDefault();
            Assert.IsNotNull(salesPerson);

            StateProvince stateProvince = _dataContext.StateProvinces.FirstOrDefault();
            Assert.IsNotNull(stateProvince);

            Store store = _dataContext.Stores.FirstOrDefault();
            Assert.IsNotNull(store);
        }

        /// <summary>
        ///     The setup.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            _dataContext = new DataContext();
        }

        #endregion
    }
}