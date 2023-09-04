ARG PLATFORM

FROM mcr.microsoft.com/dotnet/framework/sdk:4.8-${PLATFORM} as build

LABEL org.opencontainers.image.source https://github.com/pbering/aspnet48sample

WORKDIR "/workspace"

COPY . .

RUN nuget restore
RUN msbuild ./src/aspnet48sample/aspnet48sample.csproj /p:Configuration=Release /m /v:m /p:DeployOnBuild=true /p:PublishProfile=local /p:LangVersion=latest

FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8-${PLATFORM} as deploy

WORKDIR /inetpub/wwwroot

COPY --from=build /workspace/src/aspnet48sample/bin/app.publish .