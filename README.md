Type <pre>dotnet run</pre> to build app
Change ConnectionString in appsetting.json to connect in device database
Dotnet Command EF+Migration
<pre>
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet ef migrations add InitialCreate
</pre>
Add database into SQL Server Management
<pre>dotnet ef database update [MigrationName]</pre>
