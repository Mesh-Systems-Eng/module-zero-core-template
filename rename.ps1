param(
    [Parameter(Mandatory=$true)]
    [ValidateScript({
        if ([string]::IsNullOrEmpty($_)) {
            throw "Company name cannot be null or empty. Example: Mesh"
        }
        return $true
    })]
    [string]$newCompanyName,

    [Parameter(Mandatory=$true)]
    [ValidateScript({
        if ([string]::IsNullOrEmpty($_)) {
            throw "Project name cannot be null or empty. Example: Vista"
        }
        return $true
    })]
    [string]$newProjectName
)

# set output encoding
$OutputEncoding = [Text.UTF8Encoding]::UTF8

# company name placeholder 
$oldCompanyName="AbpCompanyName"

# project name placeholder
$oldProjectName="AbpProjectName"


# type of ui project - reactjs, angular, vue, mvc
$uiType="mvc"

# file type
$fileType="FileInfo"

# directory type
$dirType="DirectoryInfo"

# copy 
Write-Host 'Start copy folders...'
$newRoot=$newCompanyName+"."+$newProjectName

if (Test-Path $newRoot -PathType Container) {
    Write-Host "Directory $newRoot exists. Change the parameters or remove or rename the directory."
    exit
}

mkdir $newRoot
Copy-Item -Recurse -Force .\aspnet-core\ .\$newRoot\

if ($uiType -ne "mvc")
{
	Copy-Item -Recurse .\$uiType\ .\$newRoot\
}

Copy-Item .gitignore .\$newRoot\
Copy-Item LICENSE .\$newRoot\
Copy-Item README.md .\$newRoot\

# folders to deal with
$slnFolder = (Get-Item -Path "./$newRoot/aspnet-core/" -Verbose).FullName

if ($uiType -ne "mvc")
{
	$uiFolder = (Get-Item -Path "./$newRoot/$uiType/" -Verbose).FullName
}

function Rename {
	param (
		$TargetFolder,
		$PlaceHolderCompanyName,
		$PlaceHolderProjectName,
		$NewCompanyName,
		$NewProjectName
	)
	# file extensions to deal with
	$include=@("*.cs","*.cshtml","*.asax","*.ps1","*.ts","*.csproj","*.sln","*.xaml","*.json","*.js","*.xml","*.config","Dockerfile")

	$elapsed = [System.Diagnostics.Stopwatch]::StartNew()

	Write-Host "[$TargetFolder]Start rename folder..."
	# rename folder
	Ls $TargetFolder -Recurse | Where { $_.GetType().Name -eq $dirType -and ($_.Name.Contains($PlaceHolderCompanyName) -or $_.Name.Contains($PlaceHolderProjectName)) } | ForEach-Object{
		Write-Host 'directory ' $_.FullName
		$newDirectoryName=$_.Name.Replace($PlaceHolderCompanyName,$NewCompanyName).Replace($PlaceHolderProjectName,$NewProjectName)
		Rename-Item $_.FullName $newDirectoryName
	}
	Write-Host "[$TargetFolder]End rename folder."
	Write-Host '-------------------------------------------------------------'


	# replace file content and rename file name
	Write-Host "[$TargetFolder]Start replace file content and rename file name..."
	Ls $TargetFolder -Include $include -Recurse | Where { $_.GetType().Name -eq $fileType} | ForEach-Object{
		$fileText = Get-Content $_ -Raw -Encoding UTF8
		if($fileText.Length -gt 0 -and ($fileText.contains($PlaceHolderCompanyName) -or $fileText.contains($PlaceHolderProjectName))){
			$fileText.Replace($PlaceHolderCompanyName,$NewCompanyName).Replace($PlaceHolderProjectName,$NewProjectName) | Set-Content $_ -NoNewline -Encoding UTF8
			Write-Host 'file(change text) ' $_.FullName
		}
		If($_.Name.contains($PlaceHolderCompanyName) -or $_.Name.contains($PlaceHolderProjectName)){
			$newFileName=$_.Name.Replace($PlaceHolderCompanyName,$NewCompanyName).Replace($PlaceHolderProjectName,$NewProjectName)
			Rename-Item $_.FullName $newFileName
			Write-Host 'file(change name) ' $_.FullName
		}
	}
	Write-Host "[$TargetFolder]End replace file content and rename file name."
	Write-Host '-------------------------------------------------------------'

	$elapsed.stop()
	write-host "[$TargetFolder]Total Time Cost: $($elapsed.Elapsed.ToString())"
}

Rename -TargetFolder $slnFolder -PlaceHolderCompanyName $oldCompanyName -PlaceHolderProjectName $oldProjectName -NewCompanyName $newCompanyName -NewProjectName $newProjectName

if ($uiType -ne "mvc")
{
	Rename -TargetFolder $uiFolder -PlaceHolderCompanyName $oldCompanyName -PlaceHolderProjectName $oldProjectName -NewCompanyName $newCompanyName -NewProjectName $newProjectName
	Remove-Item -Force -Recurse -Path ".\$newRoot\aspnet-core\src\$newRoot.Web.Mvc\*"
	Remove-Item -Force -Recurse -Path ".\$newRoot\aspnet-core\src\$newRoot.Web.Mvc"
} 

cd $slnFolder
dotnet format --no-restore
