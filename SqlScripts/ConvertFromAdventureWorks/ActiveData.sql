-- cleanup
delete from EntityAddresses
delete from Addresses
delete from EntityContacts
delete from ContactDetails
delete from Stores
delete from salespersons
delete from People
delete from Employees
delete from customers
delete from Entities


-- Entities
SET IDENTITY_INSERT Entities ON
 
insert into Entities (EntityId, ModifiedDate)
     select  be.BusinessEntityID, GetDate()
     from AdventureWorks2016.Person.BusinessEntity be

SET IDENTITY_INSERT Entities OFF


-- people
 insert into people
 select p.BusinessEntityID, ro.RoleId, p.PersonType, p.Title, p.FirstName, p.MiddleName, p.LastName,p.Suffix, GetDate()
       from AdventureWorks2016.Person.Person p
	   left join AdventureWorks2016.Person.BusinessEntityContact bec
	     on p.BusinessEntityID = bec.PersonID
	   left join AdventureWorks2016.Person.ContactType ct
	     on ct.ContactTypeID = bec.ContactTypeID
       left join Roles ro
	     on ct.Name = ro.Name  COLLATE DATABASE_DEFAULT
	
		
-- salespersons	
insert into SalesPersons
     select sp.BusinessEntityID, sp.TerritoryID, sp.SalesQuota, sp.Bonus, sp.CommissionPct, sp.SalesYTD, sp.SalesLastYear, GetDAte()
       from AdventureWorks2016.Sales.SalesPerson sp
  		  
-- stores
insert into Stores
     select s.BusinessEntityID, s.SalesPersonID,  s.Name, GETDATE()
       from AdventureWorks2016.sales.Store s

	  		
-- employees
insert into Employees
     select e.BusinessEntityID, e.LoginID, e.JobTitle, e.CurrentFlag, e.HireDate, e.BirthDate, 
            e.Gender,e.MaritalStatus,e.NationalIDNumber,e.SalariedFlag, e.SickLeaveHours, e.VacationHours, GetDAte()
       from AdventureWorks2016.HumanResources.Employee e
               
-- customers 
insert into Customers
     select c.TerritoryID, c.StoreID, c.PersonID, c.AccountNumber, GetDate()
      from AdventureWorks2016.Sales.Customer c

-- contact details (email)

insert into ContactDetails
     select e.EmailAddress, GetDate()
       from AdventureWorks2016.Person.EmailAddress e

insert into EntityContacts
     select cd.ContactId, e.BusinessEntityID, 2, GetDate()
       from AdventureWorks2016.Person.EmailAddress e
       join ContactDetails cd
         on cd.Details = e.EmailAddress COLLATE DATABASE_DEFAULT
       
-- contact details (phone)
 
ALTER TABLE dbo.ContactDetails ADD OldId int NULL
go

insert into ContactDetails
    select  ph.PhoneNumber, GETDATE(), ph.BusinessEntityID
      from  AdventureWorks2016.Person.PersonPhone ph
   
insert into EntityContacts
     select cd.ContactId, ph.BusinessEntityID, case ph.PhoneNumberTypeID when 1 then 3  when 2 then 4  when 3 then 1 end, GetDate()
       from AdventureWorks2016.Person.PersonPhone ph
       join ContactDetails cd
         on cd.OldId = ph.BusinessEntityID 
       
ALTER TABLE dbo.ContactDetails drop column OldId 
go

-- addresses

SET IDENTITY_INSERT Addresses ON

insert into Addresses (AddressID, AddressLine1, AddressLine2, City, StateProvinceID, PostalCode, ModifiedDate)
    select  a.AddressID, a.AddressLine1, a.AddressLine2, a.City, a.StateProvinceID, a.PostalCode, GetDate()
      from  AdventureWorks2016.Person.Address a
   
SET IDENTITY_INSERT Addresses OFF


insert into EntityAddresses
     select bea.AddressID, bea.BusinessEntityID, bea.AddressTypeID, GetDate()
       from AdventureWorks2016.Person.BusinessEntityAddress bea

