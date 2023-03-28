targetScope = 'subscription'

param epoch int = dateTimeToEpoch(utcNow())
var configuration = json(loadTextContent('./configuration-map.json'))

resource group 'Microsoft.Resources/resourceGroups@2022-09-01' = {
  name: configuration.resourceGroup.name
  location: configuration.location
}

module insightsWorkspace 'operational-insights/log-analytics-workspace.bicep' = {
  scope: group
  name: '${configuration.operationalInsights.workspace.name}-${epoch}'
  params: {
    location: configuration.location
  }
}

module keyVault 'key-vault/hc-key-vault.bicep' = {
  scope: group
  name: '${configuration.keyVault.name}-${epoch}'
  params: {
    location: configuration.location
  }
}

module storage 'storage/storage-account.bicep' = {
  scope: group
  name: '${configuration.storage.name}-${epoch}'
  params: {
    location: configuration.location
  }
}

module acr 'azure-container-reigstries/container-registry.bicep' = {
  scope: group
  name: '${configuration.acrRegistry.name}-${epoch}'
  params: {
    location: configuration.location
  }
}

module containerEnvironment 'container-apps/container-app-environment.bicep' = {
  scope: group
  dependsOn: [
    insightsWorkspace
  ]
  name: '${configuration.containerAppEnvironment.name}-${epoch}'
  params: {
    location: configuration.location
  }
}

module app 'container-apps/container-app.bicep' = {
  scope: group
  name:  '${configuration.containerAppEnvironment.apps[0].name}-${epoch}'
  dependsOn: [
    containerEnvironment
    acr
  ]
  params: {
    location: configuration.location
  }
}
