param location string

var config = json(loadTextContent('../configuration-map.json'))



resource env 'Microsoft.App/managedEnvironments@2022-03-01' existing = {
  name: config.containerAppEnvironment.name
  scope: resourceGroup()
}

resource acr 'Microsoft.ContainerRegistry/registries@2021-09-01' existing = {
  name: config.acrRegistry.name
  scope: resourceGroup()
}

resource app 'Microsoft.App/containerApps@2022-10-01' = {
  name: config.containerAppEnvironment.apps[0].name
  location: location
  identity: {
    type: 'SystemAssigned'
  } 
  properties:{
    environmentId: env.id
    managedEnvironmentId: env.id
    template:{
      containers:[
        {
          image: '${acr.name}.azurecr.io/${config.containerAppEnvironment.apps[0].container}'
          name: config.containerAppEnvironment.apps[0].name
          resources:{
            cpu:  json('0.5')
            memory: '1Gi'     
          }
        }
      ]
      scale: {
        maxReplicas: 1
        minReplicas: 1
      }
    }
    configuration:{
      secrets:[
        {
          name: config.containerAppEnvironment.apps[0].secrets[0].name
          value: acr.listCredentials().passwords[0].value
        }
      ]
      activeRevisionsMode: 'Single'
      registries:[
        {
          passwordSecretRef: config.containerAppEnvironment.apps[0].secrets[0].name
          username: acr.name
          server: '${acr.name}.azurecr.io'
        }
      ]
      ingress:{
        external: true
        exposedPort: 0
        targetPort: 80
        allowInsecure: false
        traffic:[
          {
            weight:100
            latestRevision: true
          }
        ]

      } 
    }

  }
}
