@description('A 3-4 letter customer affix')
param clientAffix string
@description('A 2 letter designator for the environment')
param env string = 'dv'
@description('The component of the project')
#disable-next-line no-unused-params
param componentAffix string = 'Platform'
@description('The environment index - 0 is the default for the initial environment')
@minValue(0)
@maxValue(9)
param envIndex int = 0
#disable-next-line no-unused-params
param servicePrincipalObjectId string = 'bca09542-e5b8-4e92-a1e8-7ff66718d69a'
@description('A 3-9 letter project affix')
param projectAffix string
@description('Set to true when the project affix is generic ie Remote Monitoring (rmtmon), traXsmart (txs), etc ')
param qualifyProjectAffix bool = false
param location string = resourceGroup().location
param sharedServicesClientId string
@secure()
param sharedServicesClientSecret string

var clientAffixTitleCase = '${toUpper(first(clientAffix))}${toLower(substring(clientAffix, 1, (length(clientAffix) - 1)))}'
var envIndexVar = ((envIndex > 0) ? envIndex : '')
var projectAffixTitleCase = '${toUpper(first(projectAffix))}${toLower(substring(projectAffix, 1, (length(projectAffix) - 1)))}'
var namingPrefix = '${env}${(qualifyProjectAffix ? '${clientAffixTitleCase}${toLower(projectAffixTitleCase)}' : projectAffixTitleCase)}${envIndexVar}'

var resourceNames = {
  appInsights: '${namingPrefix}Ai'
  appService: '${namingPrefix}Wa'
  appServicePlan: '${namingPrefix}Asp'
  keyVault: '${namingPrefix}Kv'
  resourceGroup: 'rg-${clientAffix}-${env}'
  sqlServer: toLower('${namingPrefix}Sql')
  storage: toLower('${namingPrefix}eusSa')
}

module environment 'config/envSettings.bicep' = {
  name: 'environment'
  params: {
    environment: toLower(env)
  }
}

module app 'modules/app.bicep' = {
  name: resourceNames.appServicePlan
  params: {
    appInsightsName: resourceNames.appInsights
    appName: resourceNames.appService
    appServicePlanName: resourceNames.appServicePlan
    keyVaultName: resourceNames.keyVault
    location: location
    tags: environment.outputs.settings.tags
    sharedServicesClientId: sharedServicesClientId
    sharedServicesClientSecret: sharedServicesClientSecret
    storageName: resourceNames.storage
  }
}

output appServiceName string = app.outputs.appServiceName
output keyVaultName string = app.outputs.keyVaultName
output sqlServerName string = resourceNames.sqlServer
output resourceGroup string = resourceNames.resourceGroup
output appInsightsName string = resourceNames.appInsights
output appInsightsInstrumentationKey string = app.outputs.appInsightsInstrumentationKey
output appInsightsConnectionString string = app.outputs.appInsightsConnectionString
output storageName string = app.outputs.storageName
