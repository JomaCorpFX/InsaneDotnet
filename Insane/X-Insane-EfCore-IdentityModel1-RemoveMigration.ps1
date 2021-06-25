param(
    [ValidateSet("SqlServer", "PostgreSql", "MySql", "Oracle", "All")]
    [System.String]
    $Provider = "All",

    [switch]
    $Force
)
    
Import-Module -Name "$(Get-Item "./Z-CoreFxs*.ps1")" -Force -NoClobber
$ErrorActionPreference = "Stop"
    
$StartupProject = "../Insane.Exe"
$Project = "../Insane"
$contextPrefix = "Identity"
 
Remove-EfCoreMigration -Provider $Provider -Project $Project -StartupProject $StartupProject -Context $contextPrefix -Force:$Force