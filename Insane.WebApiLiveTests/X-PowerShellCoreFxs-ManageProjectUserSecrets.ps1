[CmdletBinding(DefaultParameterSetName = "setc")]
param (
    [Parameter(ParameterSetName = "seta")]
    [switch]
    $Edit,

    [Parameter(ParameterSetName = "setb")]
    [switch]
    $List,
    
    [string]
    [Parameter(ParameterSetName = "seta")]
    $Editor = "code",

    [string]
    [Parameter()]
    $Project = "*.csproj"

)
    
$ErrorActionPreference = "Stop"
Import-Module -Name "$(Get-Item "Z-CoreFxs*.ps1")" -Force -NoClobber

if ($Edit.IsPresent) { 
    Edit-ProjectUserSecrets -Project $Project -Editor $Editor
    return
}
    
Show-ProjectUserSecrets -Project $Project 

