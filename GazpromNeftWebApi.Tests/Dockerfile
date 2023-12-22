#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GazpromNeftWebApi.Tests/GazpromNeftWebApi.Tests.csproj", "GazpromNeftWebApi.Tests/"]
COPY ["GazpromNeftWebApi/GazpromNeftWebApi.csproj", "GazpromNeftWebApi/"]
COPY ["GazpromNeftDomain/GazpromNeftDomain.csproj", "GazpromNeftDomain/"]
RUN dotnet restore "./GazpromNeftWebApi.Tests/./GazpromNeftWebApi.Tests.csproj"
COPY . .
WORKDIR "/src/GazpromNeftWebApi.Tests"
RUN dotnet build "./GazpromNeftWebApi.Tests.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GazpromNeftWebApi.Tests.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GazpromNeftWebApi.Tests.dll"]