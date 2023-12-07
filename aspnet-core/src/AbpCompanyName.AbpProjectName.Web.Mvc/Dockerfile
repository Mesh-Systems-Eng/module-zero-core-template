FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

RUN dotnet tool install -g Microsoft.Web.LibraryManager.Cli
RUN ln -s ~/.dotnet/tools/libman /usr/local/bin/libman

# Configure Nuget to use the Azure Artifacts credential provider
ARG FEED_ACCESSTOKEN
ARG PRIVATE_FEED_URL="https://pkgs.dev.azure.com/meshsystems/906251ef-583e-4dec-a809-f74c118f3cca/_packaging/Mesh.One/nuget/v3/index.json"
ENV NUGET_CREDENTIALPROVIDER_SESSIONTOKENCACHE_ENABLED true
ENV VSS_NUGET_EXTERNAL_FEED_ENDPOINTS "{\"endpointCredentials\": [{\"endpoint\":\"${PRIVATE_FEED_URL}\", \"password\":\"${FEED_ACCESSTOKEN}\"}]}"
RUN apt-get update && apt-get install -y wget
RUN wget https://raw.githubusercontent.com/Microsoft/artifacts-credprovider/master/helpers/installcredprovider.sh \
    && chmod +x installcredprovider.sh \
    && ./installcredprovider.sh

WORKDIR /src
COPY ["src/AbpCompanyName.AbpProjectName.Web.Mvc/AbpCompanyName.AbpProjectName.Web.Mvc.csproj", "src/AbpCompanyName.AbpProjectName.Web.Mvc/"]
COPY ["src/AbpCompanyName.AbpProjectName.Web.Core/AbpCompanyName.AbpProjectName.Web.Core.csproj", "src/AbpCompanyName.AbpProjectName.Web.Core/"]
COPY ["src/AbpCompanyName.AbpProjectName.Application/AbpCompanyName.AbpProjectName.Application.csproj", "src/AbpCompanyName.AbpProjectName.Application/"]
COPY ["src/AbpCompanyName.AbpProjectName.Core/AbpCompanyName.AbpProjectName.Core.csproj", "src/AbpCompanyName.AbpProjectName.Core/"]
COPY ["src/AbpCompanyName.AbpProjectName.EntityFrameworkCore/AbpCompanyName.AbpProjectName.EntityFrameworkCore.csproj", "src/AbpCompanyName.AbpProjectName.EntityFrameworkCore/"]
WORKDIR "/src/src/AbpCompanyName.AbpProjectName.Web.Mvc"
RUN dotnet restore -s "${PRIVATE_FEED_URL}"

WORKDIR /src
COPY ["src/AbpCompanyName.AbpProjectName.Web.Mvc", "src/AbpCompanyName.AbpProjectName.Web.Mvc"]
COPY ["src/AbpCompanyName.AbpProjectName.Web.Core", "src/AbpCompanyName.AbpProjectName.Web.Core"]
COPY ["src/AbpCompanyName.AbpProjectName.Application", "src/AbpCompanyName.AbpProjectName.Application"]
COPY ["src/AbpCompanyName.AbpProjectName.Core", "src/AbpCompanyName.AbpProjectName.Core"]
COPY ["src/AbpCompanyName.AbpProjectName.EntityFrameworkCore", "src/AbpCompanyName.AbpProjectName.EntityFrameworkCore"]
WORKDIR "/src/src/AbpCompanyName.AbpProjectName.Web.Mvc"
RUN libman restore
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "AbpCompanyName.AbpProjectName.Web.Mvc.dll"]
