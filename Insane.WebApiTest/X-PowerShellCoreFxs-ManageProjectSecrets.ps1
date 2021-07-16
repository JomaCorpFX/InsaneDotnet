[CmdletBinding(DefaultParameterSetName = "setc")]
param (
    [Parameter(ParameterSetName = "seta")]
    [switch]
    $Edit,

    [Parameter(ParameterSetName = "setb")]
    [switch]
    $Set,

    [Parameter(ParameterSetName = "setc")]
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
    Edit-ProjectSecrets -Project $Project -Editor $Editor
    return
}

if ($Set.IsPresent) {  
    Set-ProjectSecrets -Project $Project
    return
}
    
Show-ProjectSecrets -Project $Project

