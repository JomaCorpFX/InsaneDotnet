
function Get-InternalPreference {
    param (
        
    )

    return @{
        ForceOverwriteConstans = $true
    }
    
}

function Set-GlobalConstant {
    param (
        [Parameter(Mandatory = $True, Position = 0, ValueFromPipeline = $true)]
        [ValidateNotNullOrEmpty()]
        [System.String]
        $Name,

        [Parameter(Mandatory = $True, Position = 1, ValueFromPipeline = $true)]
        [ValidateNotNullOrEmpty()]
        [System.String]
        $Value
    )
    Process {
        $Force = (Get-InternalPreference).ForceOverwriteConstans
        if (!(Get-Variable "$Name"  -ErrorAction 'Ignore')) {
            Set-Variable -Name "$Name" -Option Constant -Value "$Value" -Scope Global -Force:$Force
        }
    }
}

function Set-GlobalVariable {
    param (
        [Parameter(Mandatory = $True, Position = 0, ValueFromPipeline = $true)]
        [ValidateNotNullOrEmpty()]
        [System.String]
        $Name,

        [Parameter(Mandatory = $false, Position = 1, ValueFromPipeline = $true)]
        [System.String]
        $Value = ""
    )
    Process {
        Set-Variable -Name "$Name" -Value "$Value" -Scope Global
    }
}

function Write-TextColor {
    Param(
        [parameter(Position = 0, ValueFromPipeline = $true)]
        [Object]
        $Info,

        [parameter(Position = 1, ValueFromPipeline = $true)]
        [System.ConsoleColor]
        $ForegroundColor = [System.ConsoleColor]::White,
    
        [parameter(Position = 2, ValueFromPipeline = $true)]
        [Switch]
        $NoNewLine
    )

    Process {
        foreach ($value in $Info) {
            if ($NoNewLine) {
                Write-Host $value -ForegroundColor $ForegroundColor -NoNewline
            }
            else {
                Write-Host $value -ForegroundColor $ForegroundColor
            }
        }            
    }
}

function Write-PrettyKeyValue {
    [CmdletBinding()]
    Param(
        [parameter(Position = 0, ValueFromPipeline = $true)]
        [Object]
        $Key,

        [parameter(Position = 1, ValueFromPipeline = $true)]
        [Object]
        $Value,

        [parameter(Position = 2, ValueFromPipeline = $true)]
        [System.ConsoleColor]
        $LabelForegroudColor = [System.ConsoleColor]::Magenta,

        [parameter(Position = 3, ValueFromPipeline = $true)]
        [System.ConsoleColor]
        $InfoForegroundColor = [System.ConsoleColor]::White,

        [parameter(Position = 4, ValueFromPipeline = $true, Mandatory = $false)]
        [Switch]
        $NoNewLine,

        [Parameter(Position = 5, Mandatory = $false)]
        [System.Management.Automation.SwitchParameter]
        $IsDebug
    )
    
    Process {
        if ($IsDebug) {
            Write-InfoYellow "DEBUG: " -NoNewLine
        }
        Write-TextColor "$($Key): " $LabelForegroudColor -NoNewLine
        foreach ($value in $Value) {
            Write-TextColor $value $InfoForegroundColor -NoNewline:$NoNewLine
        }
    }
}

function Write-InfoRed {
    Param(
        [parameter(Mandatory = $true, Position = 0, ValueFromPipeline = $true)]
        [ValidateNotNullOrEmpty()]
        [Object]
        $Information,
    
        [parameter(Position = 1, ValueFromPipeline = $false)]
        [Switch]
        $NoNewLine
    )

    Process {
        Write-TextColor $Information Red $NoNewLine
    }
}

function Write-InfoDarkRed {
    Param(
        [parameter(Mandatory = $true, Position = 0, ValueFromPipeline = $true)]
        [ValidateNotNullOrEmpty()]
        [Object]
        $Information,
    
        [parameter(Position = 1, ValueFromPipeline = $false)]
        [Switch]
        $NoNewLine
    )

    Process {
        Write-TextColor $Information DarkRed $NoNewLine
    }
}

function Write-InfoYellow {
    Param(
        [parameter(Mandatory = $true, Position = 0, ValueFromPipeline = $true)]
        [ValidateNotNullOrEmpty()]
        [Object]
        $Information,
    
        [parameter(Position = 1, ValueFromPipeline = $true)]
        [Switch]
        $NoNewLine
    )

    Process {
        Write-TextColor $Information Yellow $NoNewLine
    }
}

function Write-InfoDarkYellow {
    Param(
        [parameter(Mandatory = $true, Position = 0, ValueFromPipeline = $true)]
        [ValidateNotNullOrEmpty()]
        [Object]
        $Information,
    
        [parameter(Position = 1, ValueFromPipeline = $true)]
        [Switch]
        $NoNewLine
    )

    Process {
        Write-TextColor $Information DarkYellow $NoNewLine
    }
}

function Write-InfoGray {
    Param(
        [parameter(Mandatory = $true, Position = 0, ValueFromPipeline = $true)]
        [ValidateNotNullOrEmpty()]
        [Object]
        $Information,
    
        [parameter(Position = 1, ValueFromPipeline = $true)]
        [Switch]
        $NoNewLine
    )

    Process {
        Write-TextColor $Information Gray $NoNewLine
    }
}

function Write-InfoDarkGray {
    Param(
        [parameter(Mandatory = $true, Position = 0, ValueFromPipeline = $true)]
        [ValidateNotNullOrEmpty()]
        [Object]
        $Information,
    
        [parameter(Position = 1, ValueFromPipeline = $true)]
        [Switch]
        $NoNewLine
    )

    Process {
        Write-TextColor $Information DarkGray $NoNewLine
    }
}

function Write-InfoGreen {
    Param(
        [parameter(Mandatory = $true, Position = 0, ValueFromPipeline = $true)]
        [ValidateNotNullOrEmpty()]
        [Object]
        $Information,
    
        [parameter(Position = 1, ValueFromPipeline = $false)]
        [Switch]
        $NoNewLine
    )

    Process {
        Write-TextColor $Information Green $NoNewLine
    }
}

function Write-InfoDarkGreen {
    Param(
        [parameter(Mandatory = $true, Position = 0, ValueFromPipeline = $true)]
        [ValidateNotNullOrEmpty()]
        [Object]
        $Information,
    
        [parameter(Position = 1, ValueFromPipeline = $true)]
        [Switch]
        $NoNewLine
    )

    Process {
        Write-TextColor $Information DarkGreen $NoNewLine
    }
}

function Write-InfoMagenta {
    Param(
        [parameter(Mandatory = $true, Position = 0, ValueFromPipeline = $true)]
        [ValidateNotNullOrEmpty()]
        [Object]
        $Information,
    
        [parameter(Position = 1, ValueFromPipeline = $true)]
        [Switch]
        $NoNewLine
    )

    Process {
        Write-TextColor $Information Magenta $NoNewLine
    }
}

function Write-InfoDarkMagenta {
    Param(
        [parameter(Mandatory = $true, Position = 0, ValueFromPipeline = $true)]
        [ValidateNotNullOrEmpty()]
        [Object]
        $Information,
    
        [parameter(Position = 1, ValueFromPipeline = $true)]
        [Switch]
        $NoNewLine
    )

    Process {
        Write-TextColor $Information DarkMagenta $NoNewLine
    }
}

function Write-InfoWhite {
    Param(
        [parameter(Mandatory = $true, Position = 0, ValueFromPipeline = $true)]
        [ValidateNotNullOrEmpty()]
        [Object]
        $Information,
    
        [parameter(Position = 1, ValueFromPipeline = $true)]
        [Switch]
        $NoNewLine
    )

    Process {
        Write-TextColor $Information White $NoNewLine
    }
}

function Write-InfoBlue {
    Param(
        [parameter(Mandatory = $true, Position = 0, ValueFromPipeline = $true)]
        [ValidateNotNullOrEmpty()]
        [Object]
        $Information,
    
        [parameter(Position = 1, ValueFromPipeline = $true)]
        [Switch]
        $NoNewLine
    )

    Process {
        Write-TextColor $Information Blue $NoNewLine
    }
}

function Write-InfoDarkBlue {
    Param(
        [parameter(Mandatory = $true, Position = 0, ValueFromPipeline = $true)]
        [ValidateNotNullOrEmpty()]
        [Object]
        $Information,
    
        [parameter(Position = 1, ValueFromPipeline = $true)]
        [Switch]
        $NoNewLine
    )

    Process {
        Write-TextColor $Information DarkBlue $NoNewLine
    }
}

function Write-InfoCyan {
    Param(
        [parameter(Mandatory = $true, Position = 0, ValueFromPipeline = $true)]
        [ValidateNotNullOrEmpty()]
        [Object]
        $Information,
    
        [parameter(Position = 1, ValueFromPipeline = $true)]
        [Switch]
        $NoNewLine
    )

    Process {
        Write-TextColor $Information Cyan $NoNewLine
    }
}

function Write-InfoDarkCyan {
    Param(
        [parameter(Mandatory = $true, Position = 0, ValueFromPipeline = $true)]
        [ValidateNotNullOrEmpty()]
        [Object]
        $Information,
    
        [parameter(Position = 1, ValueFromPipeline = $true)]
        [Switch]
        $NoNewLine
    )

    Process {
        Write-TextColor $Information Cyan $NoNewLine
    }
}

function Write-InfoBlack {
    Param(
        [parameter(Mandatory = $true, Position = 0, ValueFromPipeline = $true)]
        [ValidateNotNullOrEmpty()]
        [Object]
        $Information,
    
        [parameter(Position = 1, ValueFromPipeline = $true)]
        [Switch]
        $NoNewLine
    )

    Process {
        Write-TextColor $Information Black $NoNewLine
    }
}

function Test-OnlyWindows {
    param (
    )
    process {
        if (!$IsWindows) {
            Write-Error "Windows Operating system is required for run this function."
            exit
        }
    }
}

function Test-OnlyLinux {
    param (
    )
    process {
        if (!$IsLinux) {
            Write-Error "Linux Operating system is required for run this function."
            exit
        }
    }
}

function Test-OnlyMacOS {
    param (
    )
    process {
        if (!$IsMacOS) {
            Write-Error "MacOS Operating system is required for run this function."
            exit
        }
    }
}

class DbProviderSet : System.Management.Automation.IValidateSetValuesGenerator {
    [String[]] GetValidValues() {
        return @($Global:SQLSERVER_PROVIDER, $Global:POSTGRESQL_PROVIDER, $Global:MYSQL_PROVIDER, $Global:ORACLE_PROVIDER, $Global:ALL_PROVIDER)
    }
}

function Stop-WhenIsDbProviderName {
    param (
        [Parameter(Mandatory = $true, Position = 0)]
        [System.String]
        $Value
    )
    switch ($Value) {
        { $_ -in [DbProviderSet]::new().GetValidValues() } {
            throw "Value cannot be a db Provider"
        }
        default {
            return;
        }       
    }
}

function Install-EfCoreTools {
    param (
        
    )
    Write-Host "██ Try Install Entity Framework Core Tools" -ForegroundColor Blue
    if (Get-Command dotnet-ef -ErrorAction Ignore) {
        "Updating..."
        dotnet tool update --global dotnet-ef
    }
    else {
        "Installing..."
        dotnet tool install --global dotnet-ef
        
    }
    dotnet-ef --version
}

function Add-EfCoreMigration {
    param (
        [Parameter(Mandatory = $true)]
        [System.String]
        $Name,

        [Parameter(Mandatory = $true)]
        [System.String]
        [ValidateSet([DbProviderSet], IgnoreCase = $false, ErrorMessage = "Value `"{0}`" is invalid. Try one of: `"{1}`"")]
        $Provider,

        [Parameter(Mandatory = $true)]
        [System.String]
        $Project,

        [Parameter(Mandatory = $true)]
        [System.String]
        $StartupProject,

        [Parameter(Mandatory = $false)]
        [System.String]
        $Context = "",

        [switch]
        $InstallEfCoreTools
    )
    if ($InstallEfCoreTools.IsPresent) {
        Install-EfCoreTools
    }
    Stop-WhenIsDbProviderName -Value $Name

    $projectFile = "$(Get-Item -Path "$Project/*.csproj" | Split-Path -Leaf)"
    $startupProjectFile = "$(Get-Item -Path "$StartupProject/*.csproj" | Split-Path -Leaf)" 


    switch ($Provider) {
        { $_ -in @($SQLSERVER_PROVIDER, $POSTGRESQL_PROVIDER, $MYSQL_PROVIDER, $ORACLE_PROVIDER) } { 
            $Context = "$($Context)$($Provider)DbContext"
            $outputDir = "Migrations/$Provider/$($Context)_"
        }

        ($ALL_PROVIDER) {
            Add-EfCoreMigration -Name $Name -Provider $SQLSERVER_PROVIDER -Project $project -StartupProject $startupProject -Context $Context
            Add-EfCoreMigration -Name $Name -Provider $POSTGRESQL_PROVIDER -Project $project -StartupProject $startupProject -Context $Context
            Add-EfCoreMigration -Name $Name -Provider $MYSQL_PROVIDER -Project $project -StartupProject $startupProject -Context $Context
            Add-EfCoreMigration -Name $Name -Provider $ORACLE_PROVIDER -Project $project -StartupProject $startupProject -Context $Context
            return
        } 

        Default {
            Write-Error "Invalid Provider"
            exit
        }

    }
    Write-Host "█ Add Migration - $context - $outputDir" -ForegroundColor Magenta
    dotnet add "$StartupProject/$startupProjectFile" package "Microsoft.EntityFrameworkCore.Design"
    if($projectFile -cne $startupProjectFile)
    {
        dotnet add "$StartupProject/$startupProjectFile" reference "$Project/$projectFile"
    }
    dotnet-ef migrations add "Migration_$($context)_$Name" --startup-project "$StartupProject" --project "$Project" --context "$context" --output-dir "$outputDir" --verbose
}

function Remove-EfCoreMigration {
    param ( 
        [Parameter(Mandatory = $false, Position = 0)]
        [ValidateSet([DbProviderSet], IgnoreCase = $false, ErrorMessage = "Value `"{0}`" is invalid. Try one of: `"{1}`"")]
        [System.String]
        $Provider = "All",

        [Parameter(Mandatory = $true)]
        [System.String]
        $Project,
        
        [Parameter(Mandatory = $true)]
        [System.String]
        $StartupProject,

        [Parameter(Mandatory = $false)]
        [System.String]
        $Context = "",

        [switch]
        $Force,

        [switch]
        $InstallEfCoreTools
    )
    if ($InstallEfCoreTools.IsPresent) {
        Install-EfCoreTools
    }

    $projectFile = "$(Get-Item -Path "$Project/*.csproj" | Split-Path -Leaf)"
    $startupProjectFile = "$(Get-Item -Path "$StartupProject/*.csproj" | Split-Path -Leaf)" 

    switch ($Provider) {
        { $_ -in @($SQLSERVER_PROVIDER, $POSTGRESQL_PROVIDER, $MYSQL_PROVIDER, $ORACLE_PROVIDER) } { 
            $Context = "$($Context)$($Provider)DbContext"
        }

        ($ALL_PROVIDER) {
            Remove-EfCoreMigration -Provider $SQLSERVER_PROVIDER -Project $Project -StartupProject $StartupProject -Context $context
            Remove-EfCoreMigration -Provider $POSTGRESQL_PROVIDER -Project $Project -StartupProject $StartupProject -Context $Context
            Remove-EfCoreMigration -Provider $MYSQL_PROVIDER -Project $Project -StartupProject $StartupProject -Context $Context
            Remove-EfCoreMigration -Provider $ORACLE_PROVIDER -Project $Project -StartupProject $StartupProject -Context $Context
            return
        } 

        Default {
            Write-Error "Invalid Provider"
            exit
        }
    }
    Write-Host "█ Remove Migration - $context" -ForegroundColor Magenta
    dotnet add "$StartupProject/$startupProjectFile" package "Microsoft.EntityFrameworkCore.Design"
    if($projectFile -cne $startupProjectFile)
    {
        dotnet add "$StartupProject/$startupProjectFile" reference "$Project/$projectFile"
    }
    #Con el parámetro --force Elimina la migración desde código y desde la base de datos.
    dotnet ef migrations remove --startup-project "$startupProject" --project "$project" --context "$context" --verbose "$($Force.IsPresent ? "--force" : ([string]::Empty))"
    
}

function Remove-EfCoreDatabase {
    param (
        [System.String]
        [ValidateSet([DbProviderSet], IgnoreCase = $false, ErrorMessage = "Value `"{0}`" is invalid. Try one of: `"{1}`"")]
        $Provider = "All",
        [Parameter(Mandatory = $true)]
        [System.String]
        $Project,
        [Parameter(Mandatory = $true)]
        [System.String]
        $StartupProject,

        [Parameter(Mandatory = $false)]
        [System.String]
        $Context = "",

        [switch]
        $InstallEfCoreTools
    )
    if ($InstallEfCoreTools.IsPresent) {
        Install-EfCoreTools
    }

    $projectFile = "$(Get-Item -Path "$Project/*.csproj" | Split-Path -Leaf)"
    $startupProjectFile = "$(Get-Item -Path "$StartupProject/*.csproj" | Split-Path -Leaf)" 

    switch ($Provider) {
        { $_ -in @($SQLSERVER_PROVIDER, $POSTGRESQL_PROVIDER, $MYSQL_PROVIDER, $ORACLE_PROVIDER) } { 
            $Context = "$($Context)$($Provider)DbContext"
        }

        ($ALL_PROVIDER) {
            Remove-EfCoreDatabase -Provider $SQLSERVER_PROVIDER -Project $project -StartupProject $startupProject -Context $Context
            Remove-EfCoreDatabase -Provider $POSTGRESQL_PROVIDER -Project $project -StartupProject $startupProject -Context $Context
            Remove-EfCoreDatabase -Provider $MYSQL_PROVIDER -Project $project -StartupProject $startupProject -Context $Context
            Remove-EfCoreDatabase -Provider $ORACLE_PROVIDER -Project $project -StartupProject $startupProject -Context $Context
            return;
        } 

        Default {
            Write-Error "Invalid Provider"
            exit
        }

    }
    Write-Host "█ Remove Database - $context" -ForegroundColor Magenta
    dotnet add "$StartupProject/$startupProjectFile" package "Microsoft.EntityFrameworkCore.Design"
    if($projectFile -cne $startupProjectFile)
    {
        dotnet add "$StartupProject/$startupProjectFile" reference "$Project/$projectFile"
    }
    dotnet-ef database drop --startup-project "$startupProject" --context "$context" --project "$project" --force --verbose
}

function Update-EfCoreDatabase {
    param (
        [System.String]
        [ValidateSet([DbProviderSet], IgnoreCase = $false, ErrorMessage = "Value `"{0}`" is invalid. Try one of: `"{1}`"")]
        $Provider = "All",
        
        [Parameter(Mandatory = $true)]
        [System.String]
        $Project,
        
        [Parameter(Mandatory = $true)]
        [System.String]
        $StartupProject,

        [System.String]
        $Context = "",

        [switch]
        $InstallEfCoreTools
    )
    if ($InstallEfCoreTools.IsPresent) {
        Install-EfCoreTools
    }

    $projectFile = "$(Get-Item -Path "$Project/*.csproj" | Split-Path -Leaf)"
    $startupProjectFile = "$(Get-Item -Path "$StartupProject/*.csproj" | Split-Path -Leaf)" 

    switch ($Provider) {
        { $_ -in @($SQLSERVER_PROVIDER, $POSTGRESQL_PROVIDER, $MYSQL_PROVIDER, $ORACLE_PROVIDER) } { 
            $Context = "$($Context)$($Provider)DbContext"
        }

        ($ALL_PROVIDER) {
            Update-EfCoreDatabase -Provider $SQLSERVER_PROVIDER -Project $Project -StartupProject $StartupProject -Context $Context
            Update-EfCoreDatabase -Provider $POSTGRESQL_PROVIDER -Project $Project -StartupProject $StartupProject -Context $Context
            Update-EfCoreDatabase -Provider $MYSQL_PROVIDER -Project $Project -StartupProject $StartupProject -Context $Context
            Update-EfCoreDatabase -Provider $ORACLE_PROVIDER -Project $Project -StartupProject $StartupProject -Context $Context
            return
        }

        Default {
            Write-Error "Invalid Provider"
            exit
        }
    }
    Write-Host "█ Update database - $context" -ForegroundColor Magenta
    dotnet add "$StartupProject/$startupProjectFile" package "Microsoft.EntityFrameworkCore.Design"
    if($projectFile -cne $startupProjectFile)
    {
        dotnet add "$StartupProject/$startupProjectFile" reference "$Project/$projectFile"
    }
    dotnet-ef database update --startup-project "$StartupProject" --context "$context" --project "$Project" --verbose
}

function New-EfCoreMigrationScript {
    param (
        [System.String]
        $Name = [String]::Empty,
        
        [System.String]
        [ValidateSet([DbProviderSet], IgnoreCase = $false, ErrorMessage = "Value `"{0}`" is invalid. Try one of: `"{1}`"")]
        $Provider = "All",
        
        [Parameter(Mandatory = $true)]
        [System.String]
        $Project,
        
        [Parameter(Mandatory = $true)]
        [System.String]
        $StartupProject,

        [System.String]
        $Context = [string]::Empty,

        [switch]
        $Idempotent,

        [switch]
        $InstallEfCoreTools
    )

    if ($InstallEfCoreTools.IsPresent) {
        Install-EfCoreTools
    }
    Stop-WhenIsDbProviderName -Value $Name

    $projectFile = "$(Get-Item -Path "$Project/*.csproj" | Split-Path -Leaf)"
    $startupProjectFile = "$(Get-Item -Path "$StartupProject/*.csproj" | Split-Path -Leaf)" 

    switch ($Provider) {
        { $_ -in @($SQLSERVER_PROVIDER, $POSTGRESQL_PROVIDER, $MYSQL_PROVIDER, $ORACLE_PROVIDER) } { 
            $Context = "$($Context)$($Provider)DbContext"
            $outputFile = "$Project/MigrationScripts/$Provider/$Context/Migration_$($context)_$([string]::IsNullOrWhiteSpace($Name) ? "$([DateTime]::Now.ToString("yyyyMMddHHmmssfff"))" : $Name).sql"
            break
        }

        ($ALL_PROVIDER) {
            New-EfCoreMigrationScript -Name $Name -Provider $SQLSERVER_PROVIDER -Project $project -StartupProject $startupProject -Context $Context -Idempotent:$Idempotent
            New-EfCoreMigrationScript -Name $Name -Provider $POSTGRESQL_PROVIDER -Project $project -StartupProject $startupProject -Context $Context -Idempotent:$Idempotent
            New-EfCoreMigrationScript -Name $Name -Provider $MYSQL_PROVIDER -Project $project -StartupProject $startupProject -Context $Context -Idempotent:$Idempotent
            New-EfCoreMigrationScript -Name $Name -Provider $ORACLE_PROVIDER -Project $project -StartupProject $startupProject -Context $Context -Idempotent:$Idempotent
            return
        }

        Default {
            Write-Error "Invalid Provider"
            exit
        }

    }
    Write-Host "█ Creating Sql Script - $context - $outputFile" -ForegroundColor Magenta
    dotnet add "$StartupProject/$startupProjectFile" package "Microsoft.EntityFrameworkCore.Design"
    if($projectFile -cne $startupProjectFile)
    {
        dotnet add "$StartupProject/$startupProjectFile" reference "$Project/$projectFile"
    }
    dotnet ef migrations script --output "$outputFile" --context "$context" --project "$project" --startup-project "$startupProject" --verbose ($Idempotent.IsPresent? "--idempotent" : [string]::Empty)
}

function Update-Version {
    param (
        [System.String]
        $ProjectFileName,

        [System.String]
        [ValidateSet("Major", "Minor", "Build", "Revision")]
        $ValueType
    )

    if (!(Test-Path $ProjectFileName -PathType Leaf) -and $ProjectFileName.EndsWith(".csproj")) {
        Write-Error "Invalid file `"$ProjectFileName`"."
    }

    [System.Xml.XmlDocument] $doc = [System.Xml.XmlDocument]::new()
    $doc.Load($ProjectFileName)
    #[System.Xml.XmlElement]
    $version = $doc.DocumentElement.SelectSingleNode("//Project/PropertyGroup/Version") 
    if ($null -eq $version) {
        [System.Xml.XmlElement]$v = $doc.CreateElement("Version")
        $v.InnerText = "1.0.0.0"
        $doc.DocumentElement.SelectSingleNode("//Project/PropertyGroup").AppendChild($v);
        $doc.Save($ProjectFileName)
        return
    }

    if ([String]::IsNullOrWhiteSpace($version.InnerText)) {
        $version.InnerText = "1.0.0.0" 
        $doc.Save($ProjectFileName)
        return
    }


    $avMajor, $avMinor, $avBuild, $avRevision = $version.InnerText.Split(".")
    $avMajor = [System.String]::IsNullOrWhiteSpace($avMajor) ? "0" : $avMajor
    $avMinor = [System.String]::IsNullOrWhiteSpace($avMinor) ? "0" : $avMinor
    $avBuild = [System.String]::IsNullOrWhiteSpace($avBuild) ? "0" : $avBuild
    $avRevision = [System.String]::IsNullOrWhiteSpace($avRevision) ? "0" : $avRevision
    switch ($ValueType) {
        ("Major") {  
            $avMajor = [Convert]::ToInt32($avMajor, 10) + 1;
            $avMinor = 0
            $avBuild = 0
            $avRevision = 0
        }
        ("Minor") {  
            $avMinor = [Convert]::ToInt32($avMinor, 10) + 1;
            $avBuild = 0
            $avRevision = 0
        }
        ("Build") {  
            $avBuild = [Convert]::ToInt32($avBuild, 10) + 1;
            $avRevision = 0
        }
        ("Revision") {  
            $avRevision = [Convert]::ToInt32($avRevision, 10) + 1;
        }
        Default {
            Write-Error "Unknown ValueType `"$ValueType`"."
        }
    }
    $result = "$avMajor.$avMinor.$avBuild.$avRevision"
    $version.InnerText = $result

    $doc.Save($ProjectFileName)

    return $result
}

function Get-NextVersion {
    param (
        [System.String]
        $Version,

        [System.String]
        [ValidateSet("Major", "Minor", "Build", "Revision")]
        $ValueType,

        [switch]
        $Full
    )

    

    $avMajor, $avMinor, $avBuild, $avRevision = $version.Split(".")
    $avMajor = [System.String]::IsNullOrWhiteSpace($avMajor) ? "0" : $avMajor
    $avMinor = [System.String]::IsNullOrWhiteSpace($avMinor) ? "0" : $avMinor
    $avBuild = [System.String]::IsNullOrWhiteSpace($avBuild) ? "0" : $avBuild
    $avRevision = [System.String]::IsNullOrWhiteSpace($avRevision) ? "0" : $avRevision
    switch ($ValueType) {
        ("Major") {  
            $avMajor = "$([Convert]::ToInt32($avMajor, 10) + 1)";
            $avMinor = ($full.IsPresent ? ".0" : [String]::Empty)
            $avBuild = ($full.IsPresent ? ".0" : [String]::Empty)
            $avRevision = ($full.IsPresent ? ".0" : [String]::Empty)
        }
        ("Minor") {  
            $avMinor = ".$([Convert]::ToInt32($avMinor, 10) + 1)";
            $avBuild = ($full.IsPresent ? ".0" : [String]::Empty)
            $avRevision = ($full.IsPresent ? ".0" : [String]::Empty)
        }
        ("Build") {  
            $avMinor = ".$avMinor"
            $avBuild = ".$([Convert]::ToInt32($avBuild, 10) + 1)";
            $avRevision = ($full.IsPresent ? ".0" : [String]::Empty)
        }
        ("Revision") {  
            $avMinor = ".$avMinor"
            $avBuild = ".$avBuild"
            $avRevision = ".$([Convert]::ToInt32($avRevision, 10) + 1)";
        }

        Default {
            Write-Error "Unknown ValueType `"$ValueType`"."
        }
    }

    return "$avMajor$avMinor$avBuild$avRevision"
}

function Read-Key {
    [CmdletBinding()]
    param (
        [Parameter()]
        [String]
        $Prompt = "Press any key to continue",
    
        [Parameter()]
        [bool]
        $Display = $false,

        [Parameter()]
        [bool]
        $Discard = $true
    )
    Write-Host "$Prompt " -NoNewline -ForegroundColor DarkGray
    $key = [Console]::ReadKey(!$Display)
    if (!(($key -eq 13) -or ($key -eq 10))) {
        [Console]::WriteLine()
    }

    if (!$Discard) {
        return $key
    }
}


function Get-VariableName {
    Param(
        [Parameter()]    
        [System.Object]
        $Variable
    )
    $Line = @(Get-PSCallStack)[1].Position.Text
        
    if ($Line -match '(.*)(Get-VariableName)([ ]+)(-Variable[ ]+)*\$(?<varName>([\w]+:)*[\w]*)(.*)') {
        #https://regex101.com/r/Uc6asf/1
        return $Matches['varName'] 
    }
} 
    
function Test-LastExitCode {
    param (
        [Parameter()]
        [switch]
        $NoThrowError
    )
    if (($LASTEXITCODE -ne 0) -or (-not $?)) {
        if ($NoThrowError.IsPresent) {
            return $false
        }
        throw "ERROR: When execute last command. Check and try again. ExitCode = $($LASTEXITCODE)."
    }  
    if ($NoThrowError.IsPresent) {
        return $true
    }

}
    
function Select-ValueByPlatform {
    param (
        [parameter(Mandatory = $true)]
        [System.Object]
        $WindowsValue,
        
        [parameter(Mandatory = $true)]
        [System.Object]
        $LinuxValue,
        
        [parameter(Mandatory = $true)]
        [System.Object]
        $MacOSValue
        
    )
    if ($IsWindows) {
        return $WindowsValue
    }
    if ($IsLinux) {
        return $LinuxValue
    }
    if ($IsMacOS) {
        return $MacOSValue
    }
        
    throw "Invalid Platform."
}
    
function Get-UserHome {
    return "$(Select-ValueByPlatform "$env:USERPROFILE" "$env:HOME" "$env:HOME")";
}

function Set-LocalEnvironmentVariable {
    param (
        [Parameter()]
        [System.String]
        $Name,

        [Parameter()]
        [System.String]
        $Value,

        [Parameter()]
        [Switch]
        $Append
    )
    if ($Append.IsPresent) {
        if (Test-Path "env:$Name") {
            $Value = (Get-Item "env:$Name").Value + $Value
        }
    }
    Set-Item env:$Name -Value "$value" | Out-Null
}

function Set-PersistentEnvironmentVariable {
    param (
        [Parameter()]
        [System.String]
        $Name,
    
        [Parameter()]
        [System.String]
        $Value,
    
        [Parameter()]
        [Switch]
        $Append        
    )
    
    Set-LocalEnvironmentVariable -Name $Name -Value $Value -Append:$Append
    if ($Append.IsPresent) {
        $value = (Get-Item "env:$Name").Value
    }
    
    if ($IsWindows) {
        setx "$Name" "$Value" | Out-Null
        return
    }
    $pattern = "\s*export[ \t]+$Name=[\w]*[ \t]*>[ \t]*\/dev\/null[ \t]*;[ \t]*#[ \t]*$Name\s*"
    
    if ($IsLinux) {
        $file = "~/.bash_profile"
        $content = (Get-Content "$file" -ErrorAction Ignore -Raw) + [System.String]::Empty
        $content = [System.Text.RegularExpressions.Regex]::Replace($content, $pattern, [String]::Empty);
        $content += [System.Environment]::NewLine + [System.Environment]::NewLine + "export $Name=$Value > /dev/null ;  # $Name"
        Set-Content "$file" -Value $content -Force
        return
    }
    if ($IsMacOS) {
        $file = "~/.zprofile"
        $content = (Get-Content "$file" -ErrorAction Ignore -Raw) + [System.String]::Empty
        $content = [System.Text.RegularExpressions.Regex]::Replace($content, $pattern, [String]::Empty);
        $content += [System.Environment]::NewLine + [System.Environment]::NewLine + "export $Name=$Value > /dev/null ;  # $Name"
        Set-Content "$file" -Value $content -Force
        return
    }
    throw "Invalid platform."
}

function Get-JsonObject {
    param (
        [String]
        [ValidateNotNullOrEmpty]
        [parameter(Mandatory = $true)]
        $Filename
    )

    if (Test-Path $Filename -PathType Leaf) {
        return (Get-Content -Path $Filename | ConvertFrom-Json)
    }
    
    return ConvertFrom-Json $Filename
}

function Set-JsonObject {
    param (
        [object]
        [parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty]
        $Value,

        [String]
        [ValidateNotNullOrEmpty]
        [parameter(Mandatory = $true)]
        $Filename
    )
    
    return ( ConvertTo-Json $Value | Set-Content $Filename)
}

function Get-ItemTree() {
    param (
        [Parameter()]
        [System.String]
        $Path = ".",

        [Parameter()]
        [System.String]
        $Include = "*",

        [Parameter()]
        [switch]
        $IncludePath,

        [Parameter()]
        [switch]
        $Force

    )
    $result = @()
    if (!(Test-Path $Path)) {
        throw "Invalid path. The path `"$Path`" doesn't exist." #Test if path is valid.
    }
    if (Test-Path $Path -PathType Container) {
        $result += (Get-ChildItem "$Path" -Include "$Include" -Force:$Force -Recurse) # Add all items inside of a container, if path is a container.
    }
    if ($IncludePath.IsPresent) {
        $result += @(Get-Item $Path -Force) # Add the $Path in the result.
    }
    $result = , @($result | Sort-Object -Descending -Unique -Property "PSPath") # Sort elements by PSPath property, order in descending, remove duplicates with unique.
    return  $result
}

function Remove-ItemTree {
    param (
        [Parameter()]
        [System.String]
        $Path, 

        [Parameter()]
        [switch]
        $ForceDebug
    )
    (Get-ItemTree -Path $Path -Force -IncludePath) | ForEach-Object {
        Remove-Item "$($_.PSPath)" -Force
        if ($PSBoundParameters.Debug.IsPresent) {
            Write-Debug -Message "Deleted: $($_.PSPath)" -Debug:$ForceDebug
        }
    }
}

function Test-GitRepository {
    param (
        [Parameter()]
        [System.String]
        $Path
    )
    if (!(Test-Path $Path -PathType Container)) {
        return $false
    }
    try {
        Push-Location $Path
        git rev-parse --is-inside-work-tree --quiet | Out-Null
        $result = Test-LastExitCode -NoThrowError
        return $result
    }
    finally {
        Pop-Location
    }
}

function Set-GitRepository {
    param (
        [Parameter()]
        [System.String]
        $RepositoryUrl,

        [Parameter()]
        [System.String]
        $Path = [System.String]::Empty
    )
    $Path = ([System.String]::IsNullOrWhiteSpace($Path) ? "$X_TEMP_DIR" : $Path)
    $folderName = ($RepositoryUrl | Split-Path -Leaf).Replace(".git", [String]::Empty)  
    New-Item "$Path" -Force -ItemType Container | Out-Null
    Remove-ItemTree "$Path/$folderName" -ErrorAction Ignore
    try {
        Push-Location $Path
        git clone $RepositoryUrl
        Test-LastExitCode
    }
    finally {
        Pop-Location
    }
    
}

function Set-ProjectSecrets {
    param (
        [string]
        [Parameter()]
        $Project = "*.csproj"
    )
    
    $projectItem = Get-Item -Path $Project
    $projectFilename = $projectItem.FullName
    Write-PrettyKeyValue "██ Setting up secrets for project" "`"$projectFilename`"" -LabelForegroudColor Blue
    
    $REFERENCE = "Microsoft.Extensions.Configuration.UserSecrets"
    Write-InfoBlue "█ Adding package reference to project"
    Write-PrettyKeyValue "Reference" $REFERENCE -LabelForegroudColor Blue
    dotnet add $projectFilename package $REFERENCE
    
    Write-InfoBlue "█ Initializing secrets"
    dotnet user-secrets init --project $projectFilename
    [System.Xml.Linq.XDocument] $xml = [System.Xml.Linq.XDocument]::Parse((Get-Content -Path $projectFilename -Raw))
    $secretsId = $xml.Root.Elements("PropertyGroup").Elements("UserSecretsId").Value;
    Write-PrettyKeyValue "UserSecretsId" $secretsId -LabelForegroudColor Blue
    
    
    $SecretsFileName = "$(Get-UserHome)/$($projectItem.Name).$secretsId.Secrets.json"
    if (!(Test-Path -Path $SecretsFileName -PathType Leaf)) {
        Write-InfoBlue "█ Creating secrets file"
        Write-PrettyKeyValue "SecretsFilename" "$SecretsFileName" -LabelForegroudColor Blue
        New-Item -Path $SecretsFileName -Value "{}"
    }

    Write-InfoBlue "█ Setting up secrets" 
    Write-PrettyKeyValue "SecretsFilename" "$SecretsFileName" -LabelForegroudColor Blue
    dotnet user-secrets clear --project $projectFilename
    Get-Content "$SecretsFileName" | dotnet user-secrets set --project $projectFilename
    dotnet user-secrets list --project $projectFilename
}

function Edit-ProjectSecrets {
    param (
        [string]
        [Parameter()]
        $Project = "*.csproj",

        [string]
        [Parameter()]
        $Editor = "code"
    )
    
    $projectItem = Get-Item -Path $Project
    $projectFilename = $projectItem.FullName
    Write-PrettyKeyValue "██ Opening secrets for project" "`"$projectFilename`"" -LabelForegroudColor Blue
    
    $REFERENCE = "Microsoft.Extensions.Configuration.UserSecrets"
    Write-InfoBlue "█ Adding package reference to project"
    Write-PrettyKeyValue "Reference" $REFERENCE -LabelForegroudColor Blue
    dotnet add $projectFilename package $REFERENCE
    
    Write-InfoBlue "█ Initializing secrets"
    dotnet user-secrets init --project $projectFilename
    [System.Xml.Linq.XDocument] $xml = [System.Xml.Linq.XDocument]::Parse((Get-Content -Path $projectFilename -Raw))
    $secretsId = $xml.Root.Elements("PropertyGroup").Elements("UserSecretsId").Value;
    Write-PrettyKeyValue "UserSecretsId" $secretsId -LabelForegroudColor Blue

    $SecretsFileName = "$(Get-UserHome)/$($projectItem.Name).$secretsId.Secrets.json"
    if (!(Test-Path -Path $SecretsFileName -PathType Leaf)) {
        Write-InfoBlue "█ Creating secrets file"
        Write-PrettyKeyValue "SecretsFilename" "$SecretsFileName" -LabelForegroudColor Blue
        New-Item -Path $SecretsFileName -Value "{}"
    }

    & $editor $SecretsFileName 
}

function Show-ProjectSecrets {
    param (
        [string]
        [Parameter()]
        $Project = "*.csproj"
    )
    
    $projectItem = Get-Item -Path $Project
    $projectFilename = $projectItem.FullName
    Write-PrettyKeyValue "██ Listing secrets for project" "`"$projectFilename`"" -LabelForegroudColor Blue
    
    $REFERENCE = "Microsoft.Extensions.Configuration.UserSecrets"
    Write-InfoBlue "█ Adding package reference to project"
    Write-PrettyKeyValue "Reference" $REFERENCE -LabelForegroudColor Blue
    dotnet add $projectFilename package $REFERENCE
    
    Write-InfoBlue "█ Initializing secrets"
    dotnet user-secrets init --project $projectFilename
    [System.Xml.Linq.XDocument] $xml = [System.Xml.Linq.XDocument]::Parse((Get-Content -Path $projectFilename -Raw))
    $secretsId = $xml.Root.Elements("PropertyGroup").Elements("UserSecretsId").Value;
    Write-PrettyKeyValue "UserSecretsId" $secretsId -LabelForegroudColor Blue
    
    Write-InfoBlue "█ Secrets"
    dotnet user-secrets list --project $projectFilename
}

Set-GlobalConstant -Name "X_TEMP_DIR_NAME" -Value ".X-TEMP"
Set-GlobalConstant -Name "X_TEMP_DIR" -Value "$(Get-UserHome)/$X_TEMP_DIR_NAME"

Set-GlobalConstant -Name "SQLSERVER_PROVIDER" -Value "SqlServer"
Set-GlobalConstant -Name "POSTGRESQL_PROVIDER" -Value "PostgreSql"
Set-GlobalConstant -Name "MYSQL_PROVIDER" -Value "MySql"
Set-GlobalConstant -Name "ORACLE_PROVIDER" -Value "Oracle"
Set-GlobalConstant -Name "ALL_PROVIDER" -Value "All"
