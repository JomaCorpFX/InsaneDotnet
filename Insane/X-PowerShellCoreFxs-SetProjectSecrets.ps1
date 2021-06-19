$ErrorActionPreference = "Stop"
Import-Module -Name "$(Get-Item "./Z-CoreFxs*.ps1")" -Force -NoClobber

$ProjectName = "$(Get-Item -Path "./*.csproj" | Split-Path -Leaf)" #.Replace(".csproj", [String]::Empty)
$SecretsFileName = "$(Get-UserHome)/$ProjectName.Secrets.json"

Write-PrettyKeyValue "███ Updating secrets for project" "`"$($ProjectName)`""
Write-PrettyKeyValue "SecretsFilename" "$SecretsFileName"

if (!(Test-Path -Path $SecretsFileName -PathType Leaf)) {
    Write-Host
    Write-InfoBlue "█ Creating secrets file"
    Write-PrettyKeyValue "SecretsFilename" "$SecretsFileName"
    New-Item -Path $SecretsFileName -Value "{}"
}



Write-Host
Write-InfoBlue "█ Adding secrets" 
dotnet add "$ProjectName" package "Microsoft.Extensions.Configuration.UserSecrets"
dotnet user-secrets init
dotnet user-secrets clear
Get-Content "$SecretsFileName" | dotnet user-secrets set
    
Write-InfoBlue "█ Listing secrets"
dotnet user-secrets list
