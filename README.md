﻿# Install Requried Packages.

- `dotnet add package Microsoft.EntityFrameworkCore.Design` 
- `dotnet add package Microsoft.EntityFrameworkCore.Tools`
- `dotnet add package Pomelo.EntityFrameworkCore.MySql --version 8.0.3`
- `dotnet add package Microsoft.Extensions.Configuration`
- `dotnet add package Microsoft.Extensions.Configuration.Json`

# Create `appsettings.json` File in root directory;

- Save Connection string
``` json

  {
     "ConnectionStrings": 
     {
        "DefaultConnection": 
        "
        Server=localhost;
        Database=TestDB;
        User=tester000;
        Password=tester000;
        "
     }
   }

```

# Create a Models Folder

- Create `Student`,`Course` and  `Grades` Model Classes.
- Also create `DTO - Data Transfer Object` to transfer data between Database layer and the API layer.
# Create a Data Folder

- Create a `AppDBConfig` Class which extends `DBContext`

# Apply migrations & Create the DataBase Schema

- ### Entity Framework Core (EF Core) creates and applies migrations to generate the database schema.
- ## Step 1 : Generating the migration.
- - `dotnet ef migrations add InitialCreate`
- ## Step 2 : Applying the Migration (Creating the Schema)
-  - `dotnet ef database update`

# Where is the Database Schema Created?
- The database schema is created based on the provider configured in DbContext
- - In our case DataBase server [ TestDB ]

# Verify your `.csproj` flie

```xml

<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="8.0.1" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="8.0.0">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
    <PackageReference Include="Microsoft.EntityFrameworkCore.Relational" Version="8.0.2" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="8.0.0">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
    <PackageReference Include="Pomelo.EntityFrameworkCore.MySql" Version="8.0.2" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="7.2.0" />
  </ItemGroup>

</Project>


```#   A S P . N E T _ R e s t _ A P I 
 
 