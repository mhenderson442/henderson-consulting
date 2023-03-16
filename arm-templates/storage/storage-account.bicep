param location string

var configuration = json(loadTextContent('../configuration-map.json'))

resource storage 'Microsoft.Storage/storageAccounts@2022-09-01' = {
  name: configuration.storage.name
  location: location
  sku: {
    name: 'Standard_LRS'
  }
  kind: 'StorageV2'
}
