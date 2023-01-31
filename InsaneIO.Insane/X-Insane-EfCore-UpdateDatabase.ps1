param(
    [Parameter(Mandatory=$true)]
    [ValidateSet("SqlServer", "PostgreSql", "MySql", "Oracle", "All")]
    [System.String]
    $Provider,

    [Parameter(Mandatory=$true)]
    [System.String]
    $Context,

    [Parameter(Mandatory=false)]
    [System.String]
    $Project = "./",

    [Parameter(Mandatory=$false)]
    [System.String]
    $StartupProject = "./"
)
    
Import-Module -Name "$(Get-Item "./Z-CoreFxs*.ps1")" -Force -NoClobber
$ErrorActionPreference = "Stop"
    
$StartupProject = "../Insane.Exe"
$Project = "../Insane"
 
Update-EfCoreDatabase -Provider $Provider -Project $Project -StartupProject $StartupProject -Context $Context