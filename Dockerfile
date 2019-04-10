FROM microsoft/dotnet:2.2-aspnetcore-runtime-alpine AS base
WORKDIR /app

FROM microsoft/dotnet:2.2-sdk-alpine AS build
MAINTAINER Tiago de Oliveira
WORKDIR /src
COPY ["Api/TAS.SA.Api/TAS.SA.Api.csproj", "Api/TAS.SA.Api/"]
RUN dotnet restore "Api/TAS.SA.Api/TAS.SA.Api.csproj"
COPY . .
WORKDIR "/src/Api/TAS.SA.Api/"
RUN dotnet build "TAS.SA.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TAS.SA.Api.csproj" -c Release -o /app

ENV ASPNETCORE_URLS http://*:8080

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TAS.SA.Api.dll"]
