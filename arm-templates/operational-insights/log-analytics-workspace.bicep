param location string

var config = json(loadTextContent('../configuration-map.json'))

resource workspace 'Microsoft.OperationalInsights/workspaces@2022-10-01' = {
  name: config.operationalInsights.workspace.name
  location: location
  properties: any({
    retentionInDays: 30
    features: {
      searchVersion: 1
    }
    sku: {
      name: 'PerGB2018'
    }
  })
}
