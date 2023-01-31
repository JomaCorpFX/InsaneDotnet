param (
    [System.String]
    $ProjectFileName = "*.csproj",

    [System.String]
    $Configuration = "Release",

    [string]
    $NugetPushApiKey = [string]::Empty
)

$ErrorActionPreference = "Stop"
Import-Module -Name "$(Get-Item "./Z-CoreFxs*.ps1")" -Force -NoClobber

$ProjectFilename = Get-Item $ProjectFilename
if (!(Test-Path $ProjectFilename -PathType Leaf) -or (!"$ProjectFilename".EndsWith(".csproj"))) {
    throw "Invalid file `"$ProjectFilename`"."
}


Remove-ItemTree -Path "$($ProjectFileName | Split-Path)/bin/$Configuration" -ForceDebug -ErrorAction Ignore
dotnet build $ProjectFileName --configuration $Configuration
dotnet pack $ProjectFileName --configuration $Configuration

$nupkg = Get-Item "$($ProjectFileName | Split-Path)/bin/$Configuration/*.nupkg"
Write-Host "Package: $nupkg"
dotnet nuget push "$nupkg" --api-key "$([string]::IsNullOrWhiteSpace($NugetPushApiKey)? $env:NUGETORG_PUSH_API_KEY : $NugetPushApiKey)" --source "https://api.nuget.org/v3/index.json" --no-symbols