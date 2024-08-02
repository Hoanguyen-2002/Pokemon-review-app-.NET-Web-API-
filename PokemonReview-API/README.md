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
ConnectionString for MAC
<pre>
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=pokemonreview;User Id=sa;Password=123456aA@$;Trusted_Connection=False;TrustServerCertificate=True"
  }
</pre>
ConnectionString for WINDOWS
<pre>
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=pokemonreview;Trusted_Connection=True;TrustServerCertificate=True"
  }
</pre>
Run below command to seed data to local database from Seed.cs file
<pre>dotnet run seeddata</pre>
Run this below command to add AutoMapper (AutoMapper automatically mapping DTO(include necessary attribute of an object) and Entity objects(Models), reduce code duplicated when exchange data.)
<pre>dotnet add package AutoMapper</pre>
Right click and choose refactor for quick assign parameter