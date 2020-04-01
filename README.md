# ExcelUpdater
A .NET Core Console App that authenticates a user at "LoginUrl" and updates an SQL Server database with all the unique entries found in the Excel file( .xlsx) at "ExcelUrl".

If given,the file is also downloaded locally at "ExcelPath".
 
You just need to add your credentials( email, password) as well as a valid connection string at ExcelUpdater.ConsoleApp\appsetings.json.
