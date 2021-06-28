param(
    [ValidateSet("SqlServer", "PostgreSql", "MySql", "Oracle", "All")]
    [System.String]
    $Provider = "All",

    [Parameter(Mandatory=$true)]
    [ValidateSet("Identity1")]
    [System.String]
    $Context,

    [switch]
    $Force
)
    
Import-Module -Name "$(Get-Item "./Z-CoreFxs*.ps1")" -Force -NoClobber
$ErrorActionPreference = "Stop"
    
$StartupProject = "../Insane.Exe"
$Project = "../Insane"
$contextPrefix = "Identity"
 
Remove-EfCoreMigration -Provider $Provider -Project $Project -StartupProject $StartupProject -Context $Context -Force:$Force