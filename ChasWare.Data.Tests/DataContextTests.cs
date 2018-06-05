// --------------------------------------------------------------------------------------------------------------------
// <copyright file=DataContextTests.cs company="chas.brock@outlook.com">
//      copyright charlie brock 2018 
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.Linq;
using ChasWare.Data.Classes;
using NUnit.Framework;

namespace ChasWare.Data.Tests
{
    /// <summary>
    ///     The data context tests.
    /// </summary>
    [TestFixture]
    public class DataContextTests
    {
        #region Constants and fields 

        private DataContext _dataContext;

        #endregion

        #region public methods

        /// <summary>
        ///     Make sure we have all the DataSets that we expect
        /// </summary>
        [Test]
        public void CheckForDataSets()
        {
            Address address = _dataContext.Addresses.FirstOrDefault();
            Assert.NotNull(address);

            AddressType addressType = _dataContext.AddressTypes.FirstOrDefault();
            Assert.NotNull(addressType);

            ContactDetail contactDetail = _dataContext.ContactDetails.FirstOrDefault();
            Assert.NotNull(contactDetail);

            ContactType contactType = _dataContext.ContactTypes.FirstOrDefault();
            Assert.NotNull(contactType);

            Customer customer = _dataContext.Customers.FirstOrDefault();
            Assert.NotNull(customer);

            Department department = _dataContext.Departments.FirstOrDefault();
            Assert.NotNull(department);

            Employee employee = _dataContext.Employees.FirstOrDefault();
            Assert.NotNull(employee);

            EntityAddress entityAddress = _dataContext.EntityAddresses.FirstOrDefault();
            Assert.NotNull(entityAddress);

            EntityContact entityContact = _dataContext.EntityContacts.FirstOrDefault();
            Assert.NotNull(entityContact);

            Person person = _dataContext.Persons.FirstOrDefault();
            Assert.NotNull(person);

            Role role = _dataContext.Roles.FirstOrDefault();
            Assert.NotNull(role);

            SalesTerritory salesTerritory = _dataContext.SalesTerritories.FirstOrDefault();
            Assert.NotNull(salesTerritory);

            SalesPerson salesPerson = _dataContext.SalesPersons.FirstOrDefault();
            Assert.NotNull(salesPerson);

            StateProvince stateProvince = _dataContext.StateProvinces.FirstOrDefault();
            Assert.NotNull(stateProvince);

            Store store = _dataContext.Stores.FirstOrDefault();
            Assert.NotNull(store);
        }

        /// <summary>
        ///     The setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _dataContext = new DataContext();
        }

        #endregion
    }
}