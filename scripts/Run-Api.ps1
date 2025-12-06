<#
  Run-Api.ps1

  Simple helper to run the API project from the repository root.

  Usage:
    ./scripts/Run-Api.ps1
    ./scripts/Run-Api.ps1 -ProjectPath "src/MaziwaPlus.Api"
#>

param(
    [string]$ProjectPath = 'src/MaziwaPlus.Api'
)

Write-Host "Running project: $ProjectPath"

if (-not (Test-Path $ProjectPath)) {
    Write-Error "Project path '$ProjectPath' not found. Run from repository root or pass --ProjectPath.`n`nExample: ./scripts/Run-Api.ps1 -ProjectPath \"src/MaziwaPlus.Api\""
    exit 1
}

Push-Location $ProjectPath

try {
    dotnet run
    if ($LASTEXITCODE -ne 0) {
        Write-Error "dotnet run failed with exit code $LASTEXITCODE"
        exit $LASTEXITCODE
    }
}
finally {
    Pop-Location
}
