ARG BUILD_IMAGE
ARG RUNTIME_IMAGE

FROM ${BUILD_IMAGE} as build

LABEL org.opencontainers.image.source https://github.com/pbering/aspnet48sample

WORKDIR "/workspace"

COPY . .

RUN nuget restore
RUN msbuild ./src/aspnet48sample/aspnet48sample.csproj /p:Configuration=Release /m /v:m /p:DeployOnBuild=true /p:PublishProfile=local /p:LangVersion=latest

FROM ${RUNTIME_IMAGE} as deploy

WORKDIR /inetpub/wwwroot

COPY --from=build /workspace/src/aspnet48sample/bin/app.publish .