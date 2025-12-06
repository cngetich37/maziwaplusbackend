<#
  Update-Database.ps1
  - Installs dotnet-ef tool if missing
  - Runs `dotnet ef migrations add InitialCreate` if no migrations exist
  - Runs `dotnet ef database update`

  Usage: run from repository root in PowerShell (pwsh)
    ./scripts/Update-Database.ps1 -ProjectPath "src/MaziwaPlus.Api"
#>

param(
    [string]$ProjectPath = 'src/MaziwaPlus.Api',
    [string]$MigrationName = 'InitialCreate'
)

Write-Host "Using project path: $ProjectPath"

# Ensure dotnet is available
if (-not (Get-Command dotnet -ErrorAction SilentlyContinue)) {
    Write-Error "dotnet CLI not found in PATH. Install .NET 8 SDK first: https://dotnet.microsoft.com/en-us/download"
    exit 1
}

# Ensure dotnet-ef tool
$ef = Get-Command dotnet-ef -ErrorAction SilentlyContinue
if (-not $ef) {
    Write-Host "dotnet-ef not found. Installing global tool dotnet-ef (v8.0.0)..."
    dotnet tool install --global dotnet-ef --version 8.0.0
    if ($LASTEXITCODE -ne 0) { Write-Error "Failed to install dotnet-ef."; exit 1 }
}

Push-Location $ProjectPath

try {
    # Add migration if migrations folder missing or empty
    $migrationsDir = Join-Path -Path (Get-Location) -ChildPath "Migrations"
    $needMigration = -not (Test-Path $migrationsDir) -or ((Get-ChildItem -Path $migrationsDir -File -ErrorAction SilentlyContinue) -eq $null)

    if ($needMigration) {
        Write-Host "Creating migration: $MigrationName"
        dotnet ef migrations add $MigrationName
        if ($LASTEXITCODE -ne 0) { Write-Error "Failed to add migration."; exit 1 }
    } else {
        Write-Host "Existing migrations found; skipping 'add migration' step."
    }

    Write-Host "Applying migrations to database..."
    dotnet ef database update
    if ($LASTEXITCODE -ne 0) { Write-Error "Failed to update database."; exit 1 }

    Write-Host "Database update completed."
}
finally {
    Pop-Location
}
