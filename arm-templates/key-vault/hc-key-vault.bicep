param location string

var configuration = json(loadTextContent('../configuration-map.json'))

resource vault 'Microsoft.KeyVault/vaults@2022-11-01' =  {
  name: configuration.keyVault.name
  location: location
  properties: {
    enableRbacAuthorization: true
    sku: {
      family: 'A'
      name: 'standard'
    }
    tenantId: subscription().tenantId
  }
}
