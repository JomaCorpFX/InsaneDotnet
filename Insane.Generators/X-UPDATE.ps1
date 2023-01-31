[CmdletBinding(DefaultParameterSetName = "seta")]
param (
    [Parameter(ParameterSetName = "seta")]
    [switch]
    $RemoveDeprecated,

    [Parameter(ParameterSetName = "seta")]
    [switch]
    $RemoveUnused,

    [Parameter(ParameterSetName = "setb")]
    [switch]
    $Reset,

    [switch]
    $Run
)
# ALL NEW
$ErrorActionPreference = "Stop"

$ZCoreFxsUri = "https://raw.githubusercontent.com/Satancito/PsCoreFxs/main/Z-CoreFxs.ps1"
if (!(Test-Path "./Z-CoreFxs.ps1" -PathType Leaf)) {
    Invoke-WebRequest -Uri "$ZCoreFxsUri" -OutFile "Z-CoreFxs.ps1"
}

Import-Module -Name "$(Get-Item "./Z-CoreFxs*.ps1")" -Force -NoClobber

Write-Host
$PsCoreFxs = "https://github.com/Satancito/PsCoreFxs.git"
$Path = "$X_TEMP_DIR"
if (!($Run.IsPresent)) {
    Write-InfoMagenta "███ Update - PsCoreFxs Scripts " 
    Set-GitRepository $PsCoreFxs $Path 
}



if ("$(Get-Location | Split-Path -Leaf)".Equals("$(Get-VariableName $PsCoreFxs)")) {
    Write-Warning -Message "WARNING. Cannot overwrite original directory of scripts."
    exit
}

$Z_CONFIG = "Z-Config.json"
$Z_CONFIG_LAST = "Z-Config.Last.json"
$ME = "X-UPDATE.ps1"

if ($Reset.IsPresent) {
    Remove-Item $Z_CONFIG -Force  -ErrorAction Ignore
}

if (!($Run.IsPresent)) {
    Copy-Item -Path "$Path/$(Get-VariableName $PsCoreFxs)/$ME" $ME -Force
    pwsh -Command "./$ME -Run $($RemoveDeprecated ? "-RemoveDeprecated" : [string]::Empty) $($RemoveUnused ? "-RemoveUnused" : [string]::Empty)"
    exit
}


if (!(Test-Path $Z_CONFIG -PathType Leaf)) {
    Copy-Item -Path "$Path/$(Get-VariableName $PsCoreFxs)/$Z_CONFIG" $Z_CONFIG -Force
    Write-PrettyKeyValue "Creating" "$Z_CONFIG"
}

$localJsonObject = Get-JsonObject $Z_CONFIG
$lastJsonObject = Get-JsonObject "$Path/$(Get-VariableName $PsCoreFxs)/$Z_CONFIG"

$localJsonObject.Files = ($null -eq $localJsonObject.Files ? ([array]$lastJsonObject.Files) : ([array]$localJsonObject.Files))
[array]$files = ($localJsonObject.Files | Where-Object { ($_ -notin $lastJsonObject.DeprecatedFiles) -and ($_ -in $lastJsonObject.Files) })
Add-Member -MemberType NoteProperty -Name "Files" -Value $files -InputObject $localJsonObject -Force
Add-Member -MemberType NoteProperty -Name "CoreFiles" -Value ([array]$lastJsonObject.CoreFiles) -InputObject $localJsonObject -Force
Add-Member -MemberType NoteProperty -Name "DeprecatedFiles" -Value ([array]$lastJsonObject.DeprecatedFiles) -InputObject $localJsonObject -Force
Add-Member -MemberType NoteProperty -Name "LastSupportedFiles" -Value ([array]$lastJsonObject.Files) -InputObject $localJsonObject -Force
Add-Member -MemberType NoteProperty -Name "Version" -Value $lastJsonObject.Version -InputObject $localJsonObject -Force

Set-JsonObject $localJsonObject $Z_CONFIG

if ($RemoveDeprecated.IsPresent) {
    $localJsonObject.DeprecatedFiles | ForEach-Object {
        if (Test-Path $_ -PathType Leaf) {
            Remove-Item $_ -Force -ErrorAction Ignore
            Write-PrettyKeyValue "Removed deprecated" "$_"
        }
    }
}

if ($RemoveUnused.IsPresent) {
    $lastJsonObject.Files | Where-Object { $_ -notin $localJsonObject.Files } | ForEach-Object {
        if (Test-Path $_ -PathType Leaf) {
            Remove-Item $_ -Force  -ErrorAction Ignore
            Write-PrettyKeyValue "Removed unused" "$_"
        }
    }
}

($localJsonObject.Files + $localJsonObject.CoreFiles) | ForEach-Object {
    $file = "$Path/$(Get-VariableName $PsCoreFxs)/$_"
    if ("$_".Equals($Z_CONFIG)) {
        Copy-Item -Path $file $Z_CONFIG_LAST -Force 
        Write-PrettyKeyValue "Updating" "$_"
    }
    else {
        if (Test-Path $file -PathType Leaf) {
            Copy-Item -Path $file -Destination $_ -Force 
            Write-PrettyKeyValue "Updating" "$_"
        }
        else {
            Write-PrettyKeyValue "Ignored" "$_"
        }
    }
}


Write-InfoMagenta "███ End - Update - PsCoreFxs Scripts " 