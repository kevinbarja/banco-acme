# Banco Acme

Al ser un repositorio público, intencionalmente no se detalló el readme.

## Enfoque database first
Las entidades fueron creadas usando el enfoque database first, 
en caso que desee re-crearlas debe moverse al directorio:   

```dotnetcli 
cd banco-acme\src\AcmeBank.Infrastructure\
```

Luego ejecutar el siguiente [comando](https://learn.microsoft.com/en-us/ef/core/managing-schemas/scaffolding/?tabs=dotnet-core-cli):

```dotnetcli 
dotnet ef dbcontext scaffold "Name=ConnectionStrings:AcmeBank" Microsoft.EntityFrameworkCore.SqlServer --context AcmeBankDbContext --context-namespace AcmeBank.Persistence --startup-project ../AcmeBank.Api/ --project AcmeBank.Persistence.csproj --output-dir Entities --no-onconfiguring --force
```
Tomar en cuenta que debe actualizar la cadena de conexión en el archivo de configuración.