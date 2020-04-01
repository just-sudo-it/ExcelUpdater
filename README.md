# ExcelUpdater
A .NET Core Console App that authenticates a user at "LoginUrl" and downloads a .xlsx file at "ExcelUrl".
Afterwards it updates SQL Server database with all the unique entries found in the file downloaded.

You just need to add your credentials( email, password) as well as a valid connection string at ExcelUpdater.ConsoleApp\appsetings.json.
