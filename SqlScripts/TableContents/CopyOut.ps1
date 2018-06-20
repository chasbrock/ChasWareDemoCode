
# powershell Set-ExecutionPolicy -ExecutionPolicy Unrestricted


Remove-Item *.bcp

# static data 
bcp ChasWareDemo.dbo.Roles OUT  Roles.bcp -S CB-PC\SQLEXPRESS -T -n  
bcp ChasWareDemo.dbo.AddressTypes OUT  AddressTypes.bcp -S CB-PC\SQLEXPRESS -T -n  
bcp ChasWareDemo.dbo.SalesTerritories OUT  SalesTerritories.bcp -S CB-PC\SQLEXPRESS -T -n  
bcp ChasWareDemo.dbo.StateProvinces OUT  StateProvinces.bcp -S CB-PC\SQLEXPRESS -T -n  
bcp ChasWareDemo.dbo.Departments OUT  Departments.bcp -S CB-PC\SQLEXPRESS -T -n  

# active data
bcp ChasWareDemo.dbo.Entities OUT  Entities.bcp -S CB-PC\SQLEXPRESS -T -n  
bcp ChasWareDemo.dbo.People OUT  People.bcp -S CB-PC\SQLEXPRESS -T -n  
bcp ChasWareDemo.dbo.SalesPersons OUT  SalesPersons.bcp -S CB-PC\SQLEXPRESS -T -n  
bcp ChasWareDemo.dbo.Stores OUT  Stores.bcp -S CB-PC\SQLEXPRESS -T -n  
bcp ChasWareDemo.dbo.Employees OUT  Employees.bcp -S CB-PC\SQLEXPRESS -T -n  
bcp ChasWareDemo.dbo.Customers OUT  Customers.bcp -S CB-PC\SQLEXPRESS -T -n  
bcp ChasWareDemo.dbo.Addresses OUT  Addresses.bcp -S CB-PC\SQLEXPRESS -T -n  
bcp ChasWareDemo.dbo.EntityAddresses OUT  EntityAddresses.bcp -S CB-PC\SQLEXPRESS -T -n  

bcp ChasWareDemo.dbo.ContactDetails OUT  ContactDetails.bcp -S CB-PC\SQLEXPRESS -T -n  
bcp ChasWareDemo.dbo.EntityContacts OUT  EntityContacts.bcp -S CB-PC\SQLEXPRESS -T -n  

