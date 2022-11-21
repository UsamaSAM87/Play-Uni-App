FROM  mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY *.csproj ./

COPY . ./

FROM mcr.microsoft.com/dotnet/runtime:6.0
WORKDIR /app

ENTRYPOINT [ "dotnet", "Microsoft.OpenApi.dll"]