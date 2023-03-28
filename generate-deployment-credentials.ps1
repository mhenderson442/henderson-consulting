$subscription = az account show | ConvertFrom-Json | Select-Object -Property id
$subscriptionId = $subscription.id

$resourceGroupObject = Get-Content `
    -Path .\arm-templates\configuration-map.json `
    | ConvertFrom-Json `
    | Select-Object -ExpandProperty resourceGroup -Property resourceGroup

$hendersonAppObject = Get-Content `
    -Path .\arm-templates\configuration-map.json `
    | ConvertFrom-Json `
    | Select-Object -ExpandProperty containerAppEnvironment -Property containerAppEnvironment

$hendersonApp = $hendersonAppObject.containerAppEnvironment.apps[0].name
$resourceGroup = $resourceGroupObject.name
$scopes = "/subscriptions/${subscriptionId}/resourceGroups/${resourceGroup}"

# Write-Host $hendersonApp
# Write-Host $scopes

az ad sp create-for-rbac `
    --name $hendersonApp `
    --role contributor `
    --scopes $scopes `
    --sdk-auth