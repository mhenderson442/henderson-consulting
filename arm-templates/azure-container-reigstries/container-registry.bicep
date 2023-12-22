param location string

var config = json(loadTextContent('../configuration-map.json'))

resource acr 'Microsoft.ContainerRegistry/registries@2021-09-01' = {
  name: config.acrRegistry.name
  location: location
  identity: {
    type: 'SystemAssigned'
  }
  sku: {
    name: 'Basic'
  }
  properties: {
    adminUserEnabled: true
    dataEndpointEnabled: false
    encryption:{
      status: 'disabled'
    }
  }
}
