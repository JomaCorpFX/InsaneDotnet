param(
    [System.String]
    [ValidateNotNullOrEmpty()]
    $Name,
    
    [ValidateSet("SqlServer", "PostgreSql", "MySql", "Oracle", "All")]
    [System.String]
    $Provider = "All"
)
    
Import-Module -Name "$(Get-Item "./Z-CoreFxs*.ps1")" -Force -NoClobber
$ErrorActionPreference = "Stop"
    
$StartupProject = "../Insane.Exe"
$Project = "../Insane"
$contextPrefix = "Identity"
 
Add-EfCore-Migration -Name $Name -Provider $Provider -Project $Project -StartupProject $StartupProject -Context $contextPrefix
Test-LastExitCode
New-EfCore-MigrationScript -Name $Name -Provider $Provider -Project $Project -StartupProject $StartupProject -Context $contextPrefix