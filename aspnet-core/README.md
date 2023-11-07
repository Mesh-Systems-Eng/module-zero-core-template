# Introduction

This is a template to create **ASP.NET Core MVC / Angular** based startup projects for [ASP.NET Boilerplate](https://aspnetboilerplate.com/Pages/Documents). It has two different versions:

1. [ASP.NET Core MVC & jQuery](https://aspnetboilerplate.com/Pages/Documents/Zero/Startup-Template-Core) (server rendered multi-page application).
2. [ASP.NET Core & Angular](https://aspnetboilerplate.com/Pages/Documents/Zero/Startup-Template-Angular) (single page application).
 
User Interface is based on [AdminLTE theme](https://github.com/ColorlibHQ/AdminLTE).

Mesh's Codebase has been forked and is available [here.](https://github.com/Mesh-Systems-Eng/module-zero-core-template)

### NOTE: The primary focus has been on the ASP.NET Core MVC version. It includes DevOps components that allow for rapid deployment when expected shared Azure resources are available. The Mesh version includes React and Vue templates based on an older version of the boilerplate. These versions are outdated and should be used with caution as several dependencies are outdated.

# Mesh DevOps

#### Several assumptions have been made to what is Azure Resources are already available and referenced in the deployment:

- Application Insights
- Key Vault
- Resource Group
- SQL Server
- Storage Account

#### If those are not available, they will need created or the /env/modules/app.bicep will need modified.

#### The Default DB connection string is expected to be in keyvault. To run locally prior to this, the appsettings.json in the MVC projects folder can be adjusted to point to the targeted database.

#### Additionally, an Azure DevOps service principle is required. It can be create or located here:
![](docs/Service_Principal.png)

#### Navigate to the /pipelines folder and open the variables.yml file.
![](docs/Rename_Variables_BEFORE.png)

#### Edit the variables.yml file and fill in the following:
##### (Can be found in the existing resource group. * )
- ADD_SERVICE_CONNECTION_NAME *
- ADD_AZURE_SUBSCRIPTION *
- CLIENT-AFFIX *
- PROJECT-AFFIX *
- ADD_OWNER_NAME (Usually the TP or Tech Lead)
- AbpCompanyName (In the example: Mesh)
- AbpProjectName (In the example: Vista)

![](docs/Rename_Variables_AFTER.png)
