param (
    [System.String]
    $ProjectFileName = "*.csproj",

    [System.String]
    [ValidateSet("Release", "Debug")]
    $Configuration = "Release",

    [string]
    $NugetPushApiKey = [string]::Empty,

    [string]
    $ApiKeyEnvVariableName = [string]::Empty
)

$ErrorActionPreference = "Stop"
Import-Module -Name "$(Get-Item "./Z-PsCoreFxs.ps1")" -Force -NoClobber

$ProjectFilename = Get-Item $ProjectFilename
if (!(Test-Path $ProjectFilename -PathType Leaf) -or (!"$ProjectFilename".EndsWith(".csproj"))) {
    throw "Invalid file `"$ProjectFilename`"."
}

Remove-ItemTree -Path "$($ProjectFileName | Split-Path)/bin/$Configuration" -ForceDebug -ErrorAction Ignore

dotnet build $ProjectFileName --configuration $Configuration
dotnet pack $ProjectFileName --configuration $Configuration

$nupkg = Get-Item "$($ProjectFileName | Split-Path)/bin/$Configuration/*.nupkg"
Write-Host "Package: $nupkg"

$ApiKeyEnvVariableName = Get-StringCoalesce $ApiKeyEnvVariableName "Unknown-$([Guid]::NewGuid())"
$ApiKeyEnvVariableValue = "$(Get-Item "env:$ApiKeyEnvVariableName" -ErrorAction Ignore)"
$DefaultValue = "$($env:NUGETORG_PUSH_API_KEY)"

$NugetPushApiKey = Get-StringCoalesce $NugetPushApiKey (Get-StringCoalesce  $ApiKeyEnvVariableValue (Get-StringCoalesce $DefaultValue))

Write-Host $NugetPushApiKey
#$NugetPushApiKey = Get-StringCoalesce $NugetPushApiKey (Get-Item "env:")

#dotnet nuget push "$nupkg" --api-key "$([string]::IsNullOrWhiteSpace($NugetPushApiKey)? ([string]::IsNullOrWhiteSpace($ApiKeyEnvVariableName)? ($env:NUGETORG_PUSH_API_KEY ?? [string]::Empty ) : (Get-Item "env:$ApiKeyEnvVariableName").Value) : $NugetPushApiKey)" --source "https://api.nuget.org/v3/index.json" --no-symbols