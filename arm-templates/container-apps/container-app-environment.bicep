param location string

var config = json(loadTextContent('../configuration-map.json'))

resource workspace 'Microsoft.OperationalInsights/workspaces@2022-10-01' existing = {
  name: config.operationalInsights.workspace.name
}

resource env 'Microsoft.App/managedEnvironments@2022-03-01' = {
  name: config.containerAppEnvironment.name
  location: location
  properties: {
    appLogsConfiguration: {
      destination: 'log-analytics'
      logAnalyticsConfiguration: {
        customerId: workspace.properties.customerId
        sharedKey: workspace.listKeys().primarySharedKey
      }
    }
  }
}
