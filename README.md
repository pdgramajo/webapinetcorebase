# webapinetcorebase
net core 2.2

comandos importantes:
```
dotnet ef migrations add InitialCreate

dotnet ef database update
```
link to learn migrations: `https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/`

crear una carpeta Properties
y crear un archivo launchSettings.json
deveria de quedar asi:
Properties/launchSettings.json

copiar en el archivo este contenido reemplazando 
el valor de Super_Secret_Key 
```
{
  "$schema": "http://json.schemastore.org/launchsettings.json",
  "iisSettings": {
    "windowsAuthentication": false, 
    "anonymousAuthentication": true, 
    "iisExpress": {
      "applicationUrl": "http://localhost:34716",
      "sslPort": 44347
    }
  },
  "profiles": {
    "IIS Express": {
      "commandName": "IISExpress",
      "launchBrowser": true,
      "launchUrl": "index.html",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development",
        "Super_Secret_Key": "MY_SUPER_SECRET_KEY"
      }
    },
    "webapinetcorebase": {
      "commandName": "Project",
      "launchBrowser": true,
      "launchUrl": "index.html",
      "applicationUrl": "https://localhost:5001;http://localhost:5000",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development",
        "Super_Secret_Key": "MY_SUPER_SECRET_KEY"
      }
    }
  }
}
```

para agregar swagger :

```
    dotnet add package Swashbuckle.AspNetCore.Swagger 
    dotnet add package Swashbuckle.AspNetCore.SwaggerGen 
    dotnet add package Swashbuckle.AspNetCore.SwaggerUi 
```


