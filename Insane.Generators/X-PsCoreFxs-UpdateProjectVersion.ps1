param (
        [Parameter(Position = 0, ParameterSetName = "Default")]
        [Parameter(Position = 0, ParameterSetName = "Major")]
        [Parameter(Position = 0, ParameterSetName = "Minor")]
        [Parameter(Position = 0, ParameterSetName = "Patch")]
        [System.String]
        $ProjectFileName = "*.csproj",

        [Parameter(Mandatory = $True, ParameterSetName = "Major")]
        [Switch]
        $Major,

        [Parameter(Mandatory = $True, ParameterSetName = "Minor")]
        [Switch]
        $Minor,

        [Parameter(Mandatory = $True, ParameterSetName = "Patch")]
        [Switch]
        $Patch
)

$ErrorActionPreference = "Stop"
Import-Module -Name "$(Get-Item "./Z-CoreFxs*.ps1")" -Force -NoClobber

if($Major.IsPresent)
{
    Update-ProjectVersion -ProjectFilename $ProjectfileName -Major
    return
}

if($Minor.IsPresent)
{
    Update-ProjectVersion -ProjectFilename $ProjectfileName -Minor
    return
}

if($Patch.IsPresent)
{
    Update-ProjectVersion -ProjectFilename $ProjectfileName -Patch
    return
}

Update-ProjectVersion -ProjectFilename $ProjectfileName 
