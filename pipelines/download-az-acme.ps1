param (
    $downloadedFile,
    $defaultWorkingDirectory
)

$azAcmeUserUri = "https://github.com/az-acme/az-acme-cli/releases/download/v0.3/cli-v0.3-win-x64.zip"

Write-Host "Get az-acme"
Invoke-WebRequest -Uri $azAcmeUserUri -OutFile $downloadedFile

Write-Host "Unzip file"
Write-Host downloadedFile
Write-Host $defaultWorkingDirectosry
Expand-Archive -Path $downloadedFile -DestinationPath $defaultWorkingDirectory
