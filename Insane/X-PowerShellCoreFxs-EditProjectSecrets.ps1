$ErrorActionPreference = "Stop"
Import-Module -Name "$(Get-Item "./Z-CoreFxs*.ps1")" -Force -NoClobber

$ProjectName = "$(Get-Item -Path "./*.csproj" | Split-Path -Leaf)" #.Replace(".csproj", [String]::Empty)
$SecretsFileName = "$(Get-UserHome)/$ProjectName.Secrets.json"
Write-Host "SecretFilename: $SecretsFileName" -ForegroundColor Blue

if (!(Test-Path -Path $secretsFileName -PathType Leaf)) {
    New-Item -Path $secretsFileName -Value "{}" | Out-Null
}
code $SecretsFileName 