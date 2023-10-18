# Initial Database with SQL Server Management Studio
## Create a new database using SQL Server Management Studio
- open the file PropertyRentalDB.sql
- execute the script
- check the database PropertyRentalDB
- check the tables
- check the data

# Initial Visual Studio ASP.NET Core Project BD first

## Create a new project
- Create a new project in Visual Studio 
- Select ASP.NET Core Web Application 
- Enter the project name **PropertyRental** and chose the **Repo** location of the project 
- Connect the Application to the Database
- Install the required NuGet Packages
   - Microsoft.EntityFrameworkCore.Sqlite Version : 6.0.23
   - Microsoft.VisualStudio.Web.CodeGeneration.Design Version : 6.0.16
   - Microsoft.EntityFrameworkCore.SqlServer Version : 6.0.23
   - Microsoft.EntityFrameworkCore.Tools Version : 6.0.23
- Create a Model from the Database
   - enter the Scaffold-DbContext command in the Package Manager Console:
   ```
   Scaffold-DbContext "Data Source=WINDOWS11JM\SQLEXPRESS;Initial Catalog=PropertyRentalDB;Integrated Security=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Tables Roles,Addresses,MessageStatus,Statuses,EventTypes,Users,Login,Apartments,ApartmentImages,Buildings,Appointments,Messages,Events
   ```
   - **replace the Data Source and with your own SQL Server name** (in my case, it is WINDOWS11JM\SQLEXPRESS)
   - Modify the Generate Code in PropertyRentalDBContext.cs
   ```
   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   {
       if (!optionsBuilder.IsConfigured)
       {
           optionsBuilder.UseSqlServer("Data Source=WINDOWS11JM\\SQLEXPRESS;Initial Catalog=PropertyRentalDB;Integrated Security=True");
       }
   }
   ```
   - move the above connection string to the appsettings.json
   - before
   ```
   {
   "Logging": {
      "LogLevel": {
         "Default": "Information",
         "Microsoft": "Warning",
         "Microsoft.Hosting.Lifetime": "Information"
         } 
      }
   }
   ```
   - after
   ```
   {
   "Logging": {
      "LogLevel": {
         "Default": "Information",
         "Microsoft": "Warning",
         "Microsoft.Hosting.Lifetime": "Information"
         } 
      },
      "AllowedHosts": "*",
      "ConnectionStrings": {
         "PropertyRentalDBContextConnection": "Data Source=WINDOWS11JM\\SQLEXPRESS;Initial Catalog=PropertyRentalDB;Integrated Security=True"
      }
   }
   ```
   - modify the Program.cs
   ```
   var connection = builder.Configuration.GetConnectionString("PropertyRentalDBContextConnection");
   builder.Services.AddDbContext<PropertyRental.Models.PropertyRentalDBContext>(options => options.UseSqlServer(connection));
   ```

- Create a Controller
   - Right-click on the Controllers folder
   - Select Add
   - Select Controller
   - Select MVC Controller with views, using Entity Framework
   - Select the Model Class
   - Select the Data Context Class
   - Select the Views Layout (Views/Shared/_Layout.cshtml)
   - Select the Views Folder
   - Select the Reference Script Libraries
   - Click Add
- repeat the above steps to create all the controllers: Apartment, Building, Message, PropertyManager, Role, Status, Tenant, User