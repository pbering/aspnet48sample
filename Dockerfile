FROM mcr.microsoft.com/dotnet/framework/sdk:4.8-windowsservercore-ltsc2019 as build

WORKDIR "/workspace"

COPY . .

RUN nuget restore
RUN msbuild ./src/aspnet48sample/aspnet48sample.csproj /p:Configuration=Release /m /v:m /p:DeployOnBuild=true /p:PublishProfile=local

FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8-windowsservercore-ltsc2019 as deploy

WORKDIR /inetpub/wwwroot

COPY --from=build /workspace/src/aspnet48sample/bin/app.publish .