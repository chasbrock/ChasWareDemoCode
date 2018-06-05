// --------------------------------------------------------------------------------------------------------------------
// <copyright file=DataContext.cs company="chas.brock@outlook.com">
//      copyright charlie brock 2018 
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.Data.Entity;
using ChasWare.Data.Classes;

namespace ChasWare.Data
{
    /// <inheritdoc />
    public class DataContext : DbContext
    {
        #region Constructors

        public DataContext()
            : base("ChasWareDemo")
        {
        }

        #endregion

        #region public properties

        /// <summary>
        ///     Gets or sets the addresses.
        /// </summary>
        public virtual DbSet<Address> Addresses { get; set; }

        /// <summary>
        ///     Gets or sets the address types.
        /// </summary>
        public virtual DbSet<AddressType> AddressTypes { get; set; }

        /// <summary>
        ///     Gets or sets the contact details.
        /// </summary>
        public virtual DbSet<ContactDetail> ContactDetails { get; set; }

        /// <summary>
        ///     Gets or sets the contact types.
        /// </summary>
        public virtual DbSet<ContactType> ContactTypes { get; set; }

        /// <summary>
        ///     Gets or sets the customers.
        /// </summary>
        public virtual DbSet<Customer> Customers { get; set; }

        /// <summary>
        ///     Gets or sets the departments.
        /// </summary>
        public virtual DbSet<Department> Departments { get; set; }

        /// <summary>
        ///     Gets or sets the employees.
        /// </summary>
        public virtual DbSet<Employee> Employees { get; set; }

        /// <summary>
        ///     Gets or sets the entity addresses.
        /// </summary>
        public virtual DbSet<EntityAddress> EntityAddresses { get; set; }

        /// <summary>
        ///     Gets or sets the entity contacts.
        /// </summary>
        public virtual DbSet<EntityContact> EntityContacts { get; set; }

        /// <summary>
        ///     Gets or sets the persons.
        /// </summary>
        public virtual DbSet<Person> Persons { get; set; }

        /// <summary>
        ///     Gets or sets the roles.
        /// </summary>
        public virtual DbSet<Role> Roles { get; set; }

        /// <summary>
        ///     Gets or sets the sales persons.
        /// </summary>
        public virtual DbSet<SalesPerson> SalesPersons { get; set; }

        /// <summary>
        ///     Gets or sets the sales territories.
        /// </summary>
        public virtual DbSet<SalesTerritory> SalesTerritories { get; set; }

        /// <summary>
        ///     Gets or sets the state provinces.
        /// </summary>
        public virtual DbSet<StateProvince> StateProvinces { get; set; }

        /// <summary>
        ///     Gets or sets the stores.
        /// </summary>
        public virtual DbSet<Store> Stores { get; set; }

        #endregion
    }
}