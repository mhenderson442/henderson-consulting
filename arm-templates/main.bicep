targetScope = 'subscription'

param epoch int = dateTimeToEpoch(utcNow())
var configuration = json(loadTextContent('./configuration-map.json'))

resource group 'Microsoft.Resources/resourceGroups@2022-09-01' = {
  name: configuration.resourceGroup.name
  location: configuration.location
}

module keyVault 'key-vault/hc-key-vault.bicep' = {
  scope: group
  name: '${configuration.keyVault.name}-${epoch}'
  params: {
    location: configuration.location
  }
}
