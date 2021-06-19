param(
    [ValidateSet("SqlServer", "PostgreSql", "MySql", "Oracle", "All")]
    [System.String]
    $Provider = "All"
)
    
Import-Module -Name "$(Get-Item "./Z-CoreFxs*.ps1")" -Force -NoClobber
$ErrorActionPreference = "Stop"
    
$StartupProject = "../Insane.Exe"
$Project = "../Insane"
$contextPrefix = "Identity"
 
Update-EfCore-Database -Provider $Provider -Project $Project -StartupProject $StartupProject -Context $contextPrefix
Test-LastExitCode
Update-EfCore-Database -Provider $Provider -Project $Project -StartupProject $StartupProject -Context $contextPrefix