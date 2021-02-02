## Setup guide

### Project

- Create new:
    - Open Visual Studio and select from menu:
    - `File -> New -> Project`
    - Then go through steps:
    - `Create a new project -> Console App(.NET Core) -> Set project name -> Create`
        - **Project name:** solutions in this repository are called `App`
        - **Be aware:** steps may vary a little depending on your installation.
- Open location:
    - In Visual Studio menu go to:
    - `Tools -> Command Line -> Developer Command Prompt`
    - Terminal with project location will show up.
    - Type `cd YourProjectName`.
- Copy solution:
    - Copy content of one solution from this repository located  in `src` (e.g. `1.cs`).
    - Open project location.
    - Paste solution into `Program.cs`.
    - If your project name vary from `App` change namespace to `YourProjectName`.

### EntityFramework

Open terminal and install `EntityFramework` with command: `dotnet tool install --global dotnet-ef`

### Dependencies

Open project location in terminal. Install dependencies with following commands:

```
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
```

### Migrations

- Create
    - After copying `exercise.cs` solution from this repository to your project.
    - Open project location in terminal and use commands:
        - `dotnet ef migrations add InitDatabase`
        - `dotnet ef database update`
- Update
    - When you replace solution with other remember to update migrations.
    - Open project location in terminal and go through steps:
        - `del DatabaseName.db`
        - `dotnet ef migrations remove`
        - `dotnet ef migrations add InitDatabase`
        - `dotnet ef database update`
    - **Notice:**
        - In presented solutions database has one of following names:
            - `ProductsDatabase.db`
            - `CompaniesDatabase.db`
