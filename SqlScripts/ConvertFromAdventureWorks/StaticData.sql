-- contact details

delete from ContactTypes

insert into ContactTypes values ('Office Phone', GetDate())
insert into ContactTypes values ('Office Email', GetDate())
insert into ContactTypes values ('Office Mobile', GetDate())
insert into ContactTypes values ('Home Phone', GetDate())
insert into ContactTypes values ('Home Email', GetDate())
insert into ContactTypes values ('Home Mobile', GetDate())


-- roles
delete from roles

insert into Roles
     select c.Name, GetDate() 
	  from AdventureWorks2016.Person.ContactType c 
	 order by Name

	 
-- address types
delete from AddressTypes

insert into AddressTypes
     select c.Name, GetDate() 
	   from AdventureWorks2016.Person.AddressType c 
	  order by Name

	  
-- sales territories
delete from SalesTerritories

insert into SalesTerritories
     select s.Name, s.CountryRegionCode, s."Group", s.SalesYTD, s.SalesLastYear, s.CostYTD, s.CostLastYear, GETDATE() 
       from AdventureWorks2016.Sales.SalesTerritory s order by Name

	   
-- sales StateProvinces
delete from StateProvinces

insert into StateProvinces
     select s.StateProvinceCode, s.CountryRegionCode, s.IsOnlyStateProvinceFlag, s.Name, s. TerritoryID, getdate() 
       from AdventureWorks2016.Person.StateProvince s

-- departments
delete from Departments

insert into Departments
     select h.name, h.GroupName, getdate() 
       from AdventureWorks2016.HumanResources.Department h

