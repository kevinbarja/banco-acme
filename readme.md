# AcmeBank

## Setup project
https://learn.microsoft.com/en-us/ef/core/managing-schemas/scaffolding/?tabs=dotnet-core-cli

cd AcmeBank.Persistence

dotnet ef dbcontext scaffold "Name=ConnectionStrings:AcmeBank" Microsoft.EntityFrameworkCore.SqlServer --context AcmeBankDbContext --context-namespace AcmeBank.Persistence --startup-project ../AcmeBank.Api/ --project AcmeBank.Persistence.csproj --output-dir Entities --no-onconfiguring --force