param(
    [Parameter(Mandatory=$true)]
    [System.String]
    [ValidateNotNullOrEmpty()]
    $Name,
    
    [ValidateSet("SqlServer", "PostgreSql", "MySql", "Oracle", "All")]
    [ValidateNotNullOrEmpty()]
    [System.String]
    $Provider,

    [Parameter(Mandatory=$true)]
    [ValidateSet("Identity1")]
    [System.String]
    $Context
)
    
Import-Module -Name "$(Get-Item "./Z-CoreFxs*.ps1")" -Force -NoClobber
$ErrorActionPreference = "Stop"
    
$StartupProject = "../Insane.Exe"
$Project = "../Insane"
 
Add-EfCoreMigration -Name $Name -Provider $Provider -Project $Project -StartupProject $StartupProject -Context $Context
Test-LastExitCode
New-EfCoreMigrationScript -Name $Name -Provider $Provider -Project $Project -StartupProject $StartupProject -Context $Context