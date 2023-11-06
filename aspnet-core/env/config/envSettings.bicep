param environment string
param baseTime string = utcNow('u')
var settings = loadYamlContent('../../pipelines/variables.yml')

var environmentSettings = {
  dv: {
    name: 'Develop'
    app: {
      sku: 'F1'
    }
    tags: {
      Owner: settings.variables.Owner
      Project: settings.variables.projectAffix
      Environment: 'Develop'
      Customer: settings.variables.clientAffix
      Repository: settings.variables.repository
      'Service Connection': settings.variables.serviceConnection
      'Date Last Deployed': baseTime
    }
  }
  sg: {
    name: 'Staging'
    app: {
      sku: 'F1'
    }
    tags: {
      Owner: settings.variables.Owner
      Project: settings.variables.projectAffix
      Environment: 'Staging'
      Customer: settings.variables.clientAffix
      Repository: settings.variables.repository
      'Service Connection': settings.variables.serviceConnection
      'Date Last Deployed': baseTime
    }
  }
  pd: {
    name: 'Production'
    app: {
      sku: 'F1'
    }
    tags: {
      Owner: settings.variables.Owner
      Project: settings.variables.projectAffix
      Environment: 'Production'
      Customer: settings.variables.clientAffix
      Repository: settings.variables.Owner
      'Service Connection': settings.variables.serviceConnection
      'Date Last Deployed': baseTime
    }
  }
}

output settings object = environmentSettings[environment]
