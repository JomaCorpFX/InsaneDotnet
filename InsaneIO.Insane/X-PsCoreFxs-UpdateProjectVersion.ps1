param (
    [Parameter(Position = 0, ParameterSetName = "Default")]
    [Parameter(Position = 0, ParameterSetName = "Major")]
    [Parameter(Position = 0, ParameterSetName = "Minor")]
    [Parameter(Position = 0, ParameterSetName = "Patch")]
    [System.String]
    $ProjectFileName = "*.csproj",

    [Parameter(ParameterSetName = "Major", Mandatory = $true)]
    [Switch]
    $Major,

    [Parameter(ParameterSetName = "Minor", Mandatory = $true)]
    [Switch]
    $Minor,

    [Parameter(ParameterSetName = "Patch", Mandatory = $true)]
    [Switch]
    $Patch,

    [System.String]
    $Suffix = [string]::Empty,

    [Switch]
    $Force,

    [Switch]
    $UpdateBuildNumber
)

$ErrorActionPreference = "Stop"
Import-Module -Name "$(Get-Item "./Z-PsCoreFxs.ps1")" -Force -NoClobber

if ($Major.IsPresent) {
    Update-ProjectVersion -ProjectFilename $ProjectfileName -Major -Force:$Force -Suffix $Suffix -UpdateBuildNumber:$UpdateBuildNumber
    return
}

if ($Minor.IsPresent) {
    Update-ProjectVersion -ProjectFilename $ProjectfileName -Minor -Force:$Force -Suffix $Suffix -UpdateBuildNumber:$UpdateBuildNumber
    return
}

if ($Patch.IsPresent) {
    Update-ProjectVersion -ProjectFilename $ProjectfileName -Patch -Force:$Force -Suffix $Suffix -UpdateBuildNumber:$UpdateBuildNumber
    return
}

Update-ProjectVersion -ProjectFilename $ProjectfileName -Patch -Force:$Force -Suffix $Suffix -UpdateBuildNumber:$UpdateBuildNumber
