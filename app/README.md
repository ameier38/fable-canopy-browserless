# Web App, Backend, and Tester
[Fable](https://fable.io/) web app with [Suave](https://suave.io/) backend
using [Fable.Remoting](https://github.com/Zaid-Ajaj/Fable.Remoting).

## Setup
1. Install [.NET Core SDK](https://dotnet.microsoft.com/download).
2. Install tools.
    ```
    dotnet tool restore
    ```

## Usage
Run a build target.
```
dotnet fake build -t <target>
```
where `<target>` is one of the available build targets:
```
❯ dotnet fake build --list
The following targets are available:
   BuildClient              --> Build the Fable app
   Clean                    --> Clean the build directories
   Default                  --> Noop
   PublishIntegrationTests  --> Publish the integration test project
   PublishServer            --> Publish the backend project
   Restore                  --> Restore the projects
   StartClient              --> Start the Fable app (navigate to http://localhost:3000)
   StartServer              --> Start the backend
   TestIntegrations         --> Run the integration tests
   TestUnits                --> Run the unit tests
```
