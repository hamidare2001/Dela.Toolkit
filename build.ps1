#!/usr/bin/env pwsh
$ErrorActionPreference = "Stop"

# Try to kill processes that might lock files
Write-Host "Stopping potential file-locking processes..."
@("dotnet", "msbuild", "iisexpress") | ForEach-Object {
    try {
        Get-Process -Name $_ -ErrorAction SilentlyContinue | Stop-Process -Force -ErrorAction SilentlyContinue
        Write-Host "Stopped $_ processes"
    } catch {
        # Ignore errors if processes not running
    }
}

# Start the build
dotnet run --project "buildscript/_build.csproj" -- $args