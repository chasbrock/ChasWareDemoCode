select 'after', 'Addresses', count(*) from Addresses union
select 'after', 'AddressTypes', count(*) from AddressTypes union
select 'after', 'ContactDetails', count(*) from ContactDetails union
select 'after', 'ContactTypes', count(*) from ContactTypes union
select 'after', 'Customers', count(*) from Customers union
select 'after', 'Departments', count(*) from Departments union
select 'after', 'Entities', count(*) from Entities union
select 'after', 'EntityAddresses', count(*) from EntityAddresses union
select 'after', 'EntityContacts', count(*) from EntityContacts union
select 'after', 'Employees', count(*) from Employees union
select 'after', 'People', count(*) from People union
select 'after', 'Roles', count(*) from Roles union
select 'after', 'SalesPersons', count(*) from SalesPersons union
select 'after', 'SalesTerritories', count(*) from SalesTerritories union
select 'after', 'StateProvinces', count(*) from StateProvinces union
select 'after', 'Stores', count(*) from Stores  union

select 'before', 'Addresses', count(*) from AdventureWorks2016.Person.Address union
select 'before', 'AddressTypes', count(*) from AdventureWorks2016.Person.AddressType union
select 'before', 'Customers', count(*) from AdventureWorks2016.sales.Customer union
select 'before', 'Departments', count(*) from AdventureWorks2016.HumanResources.Department union
select 'before', 'Entities', count(*) from AdventureWorks2016.Person.BusinessEntity union
select 'before', 'Employees', count(*) from AdventureWorks2016.HumanResources.Employee union
select 'before', 'People', count(*) from AdventureWorks2016.Person.person union
select 'before', 'Roles', count(*) from AdventureWorks2016.Person.ContactType union
select 'before', 'SalesPersons', count(*) from AdventureWorks2016.sales.SalesPerson union
select 'before', 'SalesTerritories', count(*) from AdventureWorks2016.sales.SalesTerritory union
select 'before', 'StateProvinces', count(*) from AdventureWorks2016.Person.StateProvince union
select 'before', 'Stores', count(*) from AdventureWorks2016.sales.Store union
select 'before', 'Contacts - email', count(*) from AdventureWorks2016.Person.EmailAddress union
select 'before', 'Contacts - phone', count(*) from AdventureWorks2016.Person.PersonPhone 

order by 2,1