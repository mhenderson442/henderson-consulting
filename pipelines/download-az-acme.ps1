param (
    $defaultWorkingDirectory
)

$azAcmeUserUri = "https://github.com/az-acme/az-acme-cli/releases/download/v0.3/cli-v0.3-win-x64.zip"
$downloadedFile = "${defaultWorkingDirectory}/cli-v0.3-win-x64.zip"

Write-Host "Get az-acme"
Invoke-WebRequest -Uri $azAcmeUserUri -OutFile $downloadedFile


Write-Host "Unzip file"
Expand-Archive -Path $downloadedFile $defaultWorkingDirectory
