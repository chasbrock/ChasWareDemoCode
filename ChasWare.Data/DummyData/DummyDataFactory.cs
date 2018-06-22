// --------------------------------------------------------------------------------------------------------------------
// <copyright file=DummyDataFactory.cs company="chas.brock@outlook.com">
//      copyright charlie brock 2018 
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using ChasWare.Data.Classes;

namespace ChasWare.Data.DummyData
{
    /// <summary>
    ///     The dummy data factory.
    /// </summary>
    public static class DummyDataFactory
    {
        #region Constants and fields 

        private static List<Address> _addresses;
        private static List<AddressType> _addressTypes;
        private static List<ContactDetail> _contactDetails;
        private static List<ContactType> _contactTypes;
        private static List<Employee> _employees;
        private static List<Entity> _entities;
        private static List<EntityAddress> _entityAddress;
        private static List<EntityContact> _entityContacts;
        private static List<Person> _people;
        private static List<StateProvince> _stateProvince;

        #endregion

        #region public methods

        /// <summary>
        ///     The get addresses.
        /// </summary>
        /// <returns>
        ///     list of addresses <see cref="List{Address}" />.
        /// </returns>
        public static List<Address> GetAddresses()
        {
            Load();
            return _addresses;
        }

        /// <summary>
        ///     The get address types.
        /// </summary>
        /// <returns>
        ///     list of AddressTypes <see cref="List{AddressType}" />.
        /// </returns>
        public static List<AddressType> GetAddressTypes()
        {
            Load();
            return _addressTypes;
        }

        /// <summary>
        ///     The get contact types.
        /// </summary>
        /// <returns>
        ///     list of ContactTypes <see cref="List{ContactType}" />.
        /// </returns>
        public static List<ContactType> GetContactTypes()
        {
            Load();
            return _contactTypes;
        }

        public static Employee GetEmployee(int employeeId)
        {
            Load();
            return _employees.Find(e => e.PersonId == employeeId);
        }

        /// <summary>
        ///     The get entity.
        /// </summary>
        /// <param name="entityId">
        ///     The entity id.
        /// </param>
        /// <returns>
        ///     The <see cref="Entity" />.
        /// </returns>
        public static Entity GetEntity(int entityId)
        {
            Load();
            return _entities.Find(e => e.EntityId == entityId);
        }

        /// <summary>
        ///     The get entity address.
        /// </summary>
        /// <returns>
        ///     list of addresses <see cref="List{EntityAddress}" />.
        /// </returns>
        public static List<EntityAddress> GetEntityAddress()
        {
            Load();
            return _entityAddress;
        }

        /// <summary>
        ///     The get entity contacts.
        /// </summary>
        /// <returns>
        ///     list of EntityContact <see cref="List{EntityContact}" />.
        /// </returns>
        public static List<EntityContact> GetEntityContacts()
        {
            Load();
            return _entityContacts;
        }

        /// <summary>
        ///     The get state province.
        /// </summary>
        /// <returns>
        ///     list of StateProvince <see cref="List{StateProvince}" />.
        /// </returns>
        public static List<StateProvince> GetStateProvince()
        {
            Load();
            return _stateProvince;
        }

        #endregion

        #region other methods

        private static void LinkAddresses()
        {
            foreach (Address address in _addresses)
            {
                address.StateProvince = _stateProvince.Find(sp => sp.StateProvinceId == address.StateProvinceId);
            }
        }

        private static void LinkContactDetails()
        {
            foreach (ContactDetail contactDetail in _contactDetails)
            {
                contactDetail.EntityContact = _entityContacts.Find(ec => ec.ContactId == contactDetail.ContactId);
                contactDetail.EntityContact.ContactDetail = contactDetail;
            }
        }

        private static void LinkEmployees()
        {
            foreach (Employee employee in _employees)
            {
                employee.Person = _people.Find(p => p.EntityId == employee.PersonId);
            }
        }

        private static void LinkEntityAddresss()
        {
            foreach (EntityAddress entityAddress in _entityAddress)
            {
                entityAddress.Entity = _entities.Find(e => e.EntityId == entityAddress.EntityId);
                entityAddress.Entity.Addresses.Add(entityAddress);
                entityAddress.AddressType = _addressTypes.Find(ct => ct.AddressTypeId == entityAddress.AddressTypeId);
                entityAddress.Address = _addresses.Find(ct => ct.AddressId == entityAddress.AddressId);
                entityAddress.Address.EntityAddress = entityAddress;
            }
        }

        private static void LinkEntityContacts()
        {
            foreach (EntityContact entityContact in _entityContacts)
            {
                entityContact.Entity = _entities.Find(e => e.EntityId == entityContact.EntityId);
                entityContact.Entity.ContactDetails.Add(entityContact);
                entityContact.ContactType = _contactTypes.Find(ct => ct.ContactTypeId == entityContact.ContactTypeId);
            }
        }

        private static void LinkPeople()
        {
            foreach (Person person in _people)
            {
                person.Entity = _entities.Find(e => e.EntityId == person.EntityId);
            }
        }

        private static void Load()
        {
            if (_contactTypes != null)
            {
                return;
            }

            _stateProvince = LoadStateProvince();
            _contactTypes = LoadContactTypes();
            _addressTypes = LoadAddressTypes();
            _addresses = LoadAddresses();
            _contactDetails = LoadContactDetails();
            _entityContacts = LoadEntityContact();
            _entityAddress = LoadEntityAddress();
            _people = LoadPeople();
            _employees = LoadEmployee();

            _entities = new List<Entity>();
            foreach (int entityId in _entityContacts.Select(ec => ec.EntityId).Distinct())
            {
                _entities.Add(new Entity {EntityId = entityId, Addresses = new List<EntityAddress>(), ContactDetails = new List<EntityContact>()});
            }

            LinkAddresses();
            LinkContactDetails();
            LinkEntityContacts();
            LinkEntityAddresss();
            LinkPeople();
            LinkEmployees();
        }

        private static List<Address> LoadAddresses()
        {
            return new List<Address>
                {
                    new Address {AddressId = 249, AddressLine1 = "4350 Minute Dr.", AddressLine2 = string.Empty, City = "Newport Hills", PostalCode = "98006", StateProvinceId = 79},
                    new Address {AddressId = 15, AddressLine1 = "4912 La Vuelta", AddressLine2 = string.Empty, City = "Bothell", PostalCode = "98011", StateProvinceId = 79},
                    new Address {AddressId = 19, AddressLine1 = "3148 Rose Street", AddressLine2 = string.Empty, City = "Bothell", PostalCode = "98011", StateProvinceId = 79},
                    new Address {AddressId = 41, AddressLine1 = "793 Crawford Street", AddressLine2 = string.Empty, City = "Kenmore", PostalCode = "98028", StateProvinceId = 79},
                    new Address {AddressId = 236, AddressLine1 = "9008 Creekside Drive", AddressLine2 = string.Empty, City = "Everett", PostalCode = "98201", StateProvinceId = 79},
                    new Address {AddressId = 11384, AddressLine1 = "500 35th Ave NE", AddressLine2 = string.Empty, City = "Los Angeles", PostalCode = "90012", StateProvinceId = 9},
                    new Address {AddressId = 14159, AddressLine1 = "4039 Elkwood Dr.", AddressLine2 = string.Empty, City = "Ballard", PostalCode = "98107", StateProvinceId = 79},
                    new Address {AddressId = 14514, AddressLine1 = "3993 Jabber Place", AddressLine2 = string.Empty, City = "Los Angeles", PostalCode = "90012", StateProvinceId = 9},
                    new Address {AddressId = 14703, AddressLine1 = "3141 Jabber Place", AddressLine2 = string.Empty, City = "Ballard", PostalCode = "98107", StateProvinceId = 79},
                    new Address {AddressId = 20137, AddressLine1 = "3443 Centennial Way", AddressLine2 = string.Empty, City = "Seattle", PostalCode = "98104", StateProvinceId = 79},
                    new Address {AddressId = 26384, AddressLine1 = "1946 Bayside Way", AddressLine2 = string.Empty, City = "Everett", PostalCode = "98201", StateProvinceId = 79}
                };
        }

        private static List<AddressType> LoadAddressTypes()
        {
            return new List<AddressType> {new AddressType {AddressTypeId = 1, Name = "Archive"}, new AddressType {AddressTypeId = 2, Name = "Billing"}, new AddressType {AddressTypeId = 3, Name = "Home"}, new AddressType {AddressTypeId = 4, Name = "Main Office"}, new AddressType {AddressTypeId = 5, Name = "Primary"}, new AddressType {AddressTypeId = 6, Name = "Shipping"}};
        }

        private static List<ContactDetail> LoadContactDetails()
        {
            return new List<ContactDetail>
                {
                    new ContactDetail {ContactId = 11741, Details = "ken0@adventure-works.com"},
                    new ContactDetail {ContactId = 36802, Details = "697-555-0142"},
                    new ContactDetail {ContactId = 10503, Details = "jose17@adventure-works.com"},
                    new ContactDetail {ContactId = 10555, Details = "josé64@adventure-works.com"},
                    new ContactDetail {ContactId = 10755, Details = "julia43@adventure-works.com"},
                    new ContactDetail {ContactId = 11166, Details = "karl3@adventure-works.com"},
                    new ContactDetail {ContactId = 12954, Details = "luis31@adventure-works.com"},
                    new ContactDetail {ContactId = 34756, Details = "508-555-0163"},
                    new ContactDetail {ContactId = 34971, Details = "528-555-0143"},
                    new ContactDetail {ContactId = 35747, Details = "599-555-0171"},
                    new ContactDetail {ContactId = 37015, Details = "716-555-0173"},
                    new ContactDetail {ContactId = 38630, Details = "870-555-0110"}
                };
        }

        private static List<ContactType> LoadContactTypes()
        {
            return new List<ContactType>
                {
                    new ContactType {ContactTypeId = 1, Type = "Office Phone"},
                    new ContactType {ContactTypeId = 2, Type = "Office Email"},
                    new ContactType {ContactTypeId = 3, Type = "Office Mobile"},
                    new ContactType {ContactTypeId = 4, Type = "Home Phone"},
                    new ContactType {ContactTypeId = 5, Type = "Home Email"},
                    new ContactType {ContactTypeId = 6, Type = "Home Mobile"}
                };
        }

        private static List<Employee> LoadEmployee()
        {
            return new List<Employee>
                {
                    new Employee
                        {
                            PersonId = 1,
                            LoginId = "adventure-works\\ken0",
                            JobTitle = "Chief Executive Officer",
                            CurrentFlag = true,
                            Gender = "M",
                            MaritalStatus = "M",
                            NationalIDNumber = "295847284"
                        }
                };
        }

        private static List<EntityAddress> LoadEntityAddress()
        {
            return new List<EntityAddress>
                {
                    new EntityAddress {EntityId = 1, AddressId = 249, AddressTypeId = 2},
                    new EntityAddress {EntityId = 5124, AddressId = 15, AddressTypeId = 5},
                    new EntityAddress {EntityId = 11072, AddressId = 19, AddressTypeId = 5},
                    new EntityAddress {EntityId = 5668, AddressId = 41, AddressTypeId = 5},
                    new EntityAddress {EntityId = 17298, AddressId = 236, AddressTypeId = 5},
                    new EntityAddress {EntityId = 5479, AddressId = 11384, AddressTypeId = 5},
                    new EntityAddress {EntityId = 5124, AddressId = 14159, AddressTypeId = 2},
                    new EntityAddress {EntityId = 5479, AddressId = 14514, AddressTypeId = 2},
                    new EntityAddress {EntityId = 5668, AddressId = 14703, AddressTypeId = 2},
                    new EntityAddress {EntityId = 11072, AddressId = 20137, AddressTypeId = 2},
                    new EntityAddress {EntityId = 17298, AddressId = 26384, AddressTypeId = 2}
                };
        }

        private static List<EntityContact> LoadEntityContact()
        {
            return new List<EntityContact>
                {
                    new EntityContact {EntityId = 1, ContactId = 11741, ContactTypeId = 2},
                    new EntityContact {EntityId = 1, ContactId = 36802, ContactTypeId = 3},
                    new EntityContact {EntityId = 5668, ContactId = 10503, ContactTypeId = 2},
                    new EntityContact {EntityId = 17298, ContactId = 10555, ContactTypeId = 2},
                    new EntityContact {EntityId = 11072, ContactId = 10755, ContactTypeId = 2},
                    new EntityContact {EntityId = 5124, ContactId = 11166, ContactTypeId = 2},
                    new EntityContact {EntityId = 5479, ContactId = 12954, ContactTypeId = 2},
                    new EntityContact {EntityId = 5124, ContactId = 34756, ContactTypeId = 4},
                    new EntityContact {EntityId = 17298, ContactId = 34971, ContactTypeId = 4},
                    new EntityContact {EntityId = 5668, ContactId = 35747, ContactTypeId = 4},
                    new EntityContact {EntityId = 5479, ContactId = 37015, ContactTypeId = 3},
                    new EntityContact {EntityId = 11072, ContactId = 38630, ContactTypeId = 4}
                };
        }

        private static List<Person> LoadPeople()
        {
            return new List<Person>
                {
                    new Person {EntityId = 1, PersonType = "EM", FirstName = "Ken", MiddleName = "J", LastName = "Sánchez"}
                };
        }

        private static List<StateProvince> LoadStateProvince()
        {
            return new List<StateProvince> {new StateProvince {StateProvinceId = 9, Name = "California", StateProvinceCode = "CA ", CountryRegionCode = "US", TerritoryId = 4}, new StateProvince {StateProvinceId = 79, Name = "Washington", StateProvinceCode = "WA ", CountryRegionCode = "US", TerritoryId = 1}};
        }

        #endregion
    }
}