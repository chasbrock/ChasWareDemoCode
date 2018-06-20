
#truncate where not FKeys
SQLCMD -Q "TRUNCATE TABLE ChasWareDemo.dbo.EntityContacts;" -S CB-PC\SQLEXPRESS
SQLCMD -Q "TRUNCATE TABLE ChasWareDemo.dbo.Customers;" -S CB-PC\SQLEXPRESS
SQLCMD -Q "TRUNCATE TABLE ChasWareDemo.dbo.Employees;" -S CB-PC\SQLEXPRESS
SQLCMD -Q "TRUNCATE TABLE ChasWareDemo.dbo.EntityAddresses;" -S CB-PC\SQLEXPRESS

# otherwise delete
SQLCMD -Q "DELETE FROM ChasWareDemo.dbo.ContactDetails;" -S CB-PC\SQLEXPRESS
SQLCMD -Q "DELETE FROM ChasWareDemo.dbo.Addresses;" -S CB-PC\SQLEXPRESS
SQLCMD -Q "DELETE FROM ChasWareDemo.dbo.Stores;" -S CB-PC\SQLEXPRESS
SQLCMD -Q "DELETE FROM ChasWareDemo.dbo.SalesPersons;" -S CB-PC\SQLEXPRESS
SQLCMD -Q "DELETE FROM ChasWareDemo.dbo.People;" -S CB-PC\SQLEXPRESS
SQLCMD -Q "DELETE FROM ChasWareDemo.dbo.Entities;" -S CB-PC\SQLEXPRESS
SQLCMD -Q "DELETE FROM ChasWareDemo.dbo.Departments;" -S CB-PC\SQLEXPRESS
SQLCMD -Q "DELETE FROM ChasWareDemo.dbo.StateProvinces;" -S CB-PC\SQLEXPRESS
SQLCMD -Q "DELETE FROM ChasWareDemo.dbo.SalesTerritories;" -S CB-PC\SQLEXPRESS
SQLCMD -Q "DELETE FROM ChasWareDemo.dbo.AddressTypes;" -S CB-PC\SQLEXPRESS 
SQLCMD -Q "DELETE FROM ChasWareDemo.dbo.Roles;" -S CB-PC\SQLEXPRESS

# ... then reset table

$tableName = "ContactDetails"
Sqlcmd -Q "DBCC CHECKIDENT ($tableName, RESEED, 1)" -S CB-PC\SQLEXPRESS -d ChasWareDemo
$tableName = "Addresses"
Sqlcmd -Q "DBCC CHECKIDENT ($tableName, RESEED, 1)" -S CB-PC\SQLEXPRESS -d ChasWareDemo
$tableName = "Entities"
Sqlcmd -Q "DBCC CHECKIDENT ($tableName, RESEED, 1)" -S CB-PC\SQLEXPRESS -d ChasWareDemo
$tableName = "Departments"
Sqlcmd -Q "DBCC CHECKIDENT ($tableName, RESEED, 1)" -S CB-PC\SQLEXPRESS -d ChasWareDemo
$tableName = "StateProvinces"
Sqlcmd -Q "DBCC CHECKIDENT ($tableName, RESEED, 1)" -S CB-PC\SQLEXPRESS -d ChasWareDemo
$tableName = "SalesTerritories"
Sqlcmd -Q "DBCC CHECKIDENT ($tableName, RESEED, 1)" -S CB-PC\SQLEXPRESS -d ChasWareDemo
$tableName = "AddressTypes"
Sqlcmd -Q "DBCC CHECKIDENT ($tableName, RESEED, 1)" -S CB-PC\SQLEXPRESS -d ChasWareDemo
$tableName = "Roles"
Sqlcmd -Q "DBCC CHECKIDENT ($tableName, RESEED, 1)" -S CB-PC\SQLEXPRESS -d ChasWareDemo


# static data 
bcp ChasWareDemo.dbo.Roles IN  Roles.bcp -E -T -n -S CB-PC\SQLEXPRESS
bcp ChasWareDemo.dbo.AddressTypes IN  AddressTypes.bcp  -E -T -n -S CB-PC\SQLEXPRESS
bcp ChasWareDemo.dbo.SalesTerritories IN  SalesTerritories.bcp -E -T -n -S CB-PC\SQLEXPRESS
bcp ChasWareDemo.dbo.StateProvinces IN  StateProvinces.bcp -E -T -n -S CB-PC\SQLEXPRESS
bcp ChasWareDemo.dbo.Departments IN  Departments.bcp  -E -T -n -S CB-PC\SQLEXPRESS

# active data
bcp ChasWareDemo.dbo.Entities IN  Entities.bcp -E -T -n -S CB-PC\SQLEXPRESS
bcp ChasWareDemo.dbo.People IN  People.bcp -E -T -n -S CB-PC\SQLEXPRESS
bcp ChasWareDemo.dbo.SalesPersons IN  SalesPersons.bcp  -E -T -n -S CB-PC\SQLEXPRESS
bcp ChasWareDemo.dbo.Stores IN  Stores.bcp -E -T -n -S CB-PC\SQLEXPRESS
bcp ChasWareDemo.dbo.Employees IN  Employees.bcp -E -T -n -S CB-PC\SQLEXPRESS
bcp ChasWareDemo.dbo.Customers IN  Customers.bcp -E -T -n -S CB-PC\SQLEXPRESS
bcp ChasWareDemo.dbo.Addresses IN  Addresses.bcp -E -T -n -S CB-PC\SQLEXPRESS
bcp ChasWareDemo.dbo.EntityAddresses IN  EntityAddresses.bcp -E -T -n -S CB-PC\SQLEXPRESS
bcp ChasWareDemo.dbo.ContactDetails IN  ContactDetails.bcp -E -T -n -S CB-PC\SQLEXPRESS
bcp ChasWareDemo.dbo.EntityContacts IN  EntityContacts.bcp -E -T -n -S CB-PC\SQLEXPRESS


