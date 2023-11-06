param appServicePlanName string
param location string
param tags object
param appName string
param keyVaultName string
param appInsightsName string
param storageName string
param sharedServicesClientId string
@secure()
param sharedServicesClientSecret string

var sharedServicesClientSecretDecoded = replace(sharedServicesClientSecret, '//', '/')

resource appService 'Microsoft.Web/sites@2020-06-01' = {
  name: appName
  location: location
  identity: {
    type: 'SystemAssigned'
  }
  kind: 'app'
  properties: {
    serverFarmId: appServicePlan.id
    httpsOnly: true
    siteConfig: {
      alwaysOn: true
      appSettings: [
        {
          name: 'ApplicationInsightsAgent_EXTENSION_VERSION'
          value: 'latest'
        }
        {
          name: 'APPINSIGHTS_INSTRUMENTATIONKEY'
          value: appInsights.properties.InstrumentationKey
        }
        {
          name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
          value: appInsights.properties.ConnectionString
        }
        {
          name: 'KeyVaultName'
          value: sharedKeyVault.name
        }
        {
          name: 'SharedServicesConfiguration__ClientId'
          value: sharedServicesClientId
        }
        {
          name: 'SharedServicesConfiguration__ClientSecret'
          value: sharedServicesClientSecretDecoded
        }
        {
          name: 'BlobStorageConfiguration__ConnectionString'
          value: 'DefaultEndpointsProtocol=https;AccountName=${storageAccount.name};AccountKey=${storageAccount.listKeys().keys[0].value};EndpointSuffix=${environment().suffixes.storage}'
        }
        {
          name: 'LogEntryConfiguration__HostSuffix'
          value: 'blob.${environment().suffixes.storage}'
        }
      ]
      connectionStrings: [
        {
          name: 'default'
          connectionString: '@Microsoft.KeyVault(VaultName=${sharedKeyVault.name};SecretName=ConnectionStrings--DbConnection)'
        }
      ]
    }
  }
  tags: tags
}

resource appServiceAccessPolicy 'Microsoft.KeyVault/vaults/accessPolicies@2022-07-01' = {
  name: 'add'
  parent: sharedKeyVault
  properties: {
    accessPolicies: [
      {
        objectId: appService.identity.principalId
        permissions: {
          secrets: [
            'get'
            'list'
            'set'
          ]
        }
        tenantId: subscription().tenantId
      }
    ]
  }
}

resource appServicePlan 'Microsoft.Web/serverfarms@2020-06-01' = {
  name: appServicePlanName
  location: location
  properties: {
    reserved: false
  }
  sku: {
    name: 'S1'
    tier: 'Standard'
    size: 'S1'
    family: 'S'
    capacity: 1
  }
  kind: 'app'
  tags: tags 
}

resource appInsights 'Microsoft.Insights/components@2020-02-02' existing = {
  name: appInsightsName
}
resource sharedKeyVault 'Microsoft.KeyVault/vaults@2021-06-01-preview' existing = {
  name: keyVaultName 
}
resource storageAccount 'Microsoft.Storage/storageAccounts@2021-02-01' existing = {
  name: storageName
}

output appServiceName string = appService.name
output appServicePlanName string = appServicePlan.name
output keyVaultName string = sharedKeyVault.name
output appInsightsInstrumentationKey string = appInsights.properties.InstrumentationKey
output appInsightsConnectionString string = appInsights.properties.ConnectionString
output storageName string = storageAccount.name
