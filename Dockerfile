FROM microsoft/dotnet:2.2-aspnetcore-runtime-alpine AS base
WORKDIR /app
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk-alpine AS build
WORKDIR /src
COPY ["Api/TAS.SA.Api/TAS.SA.Api.csproj", "Api/TAS.SA.Api/"]
RUN dotnet restore "Api/TAS.SA.Api/TAS.SA.Api.csproj"
COPY . .
WORKDIR "/src/Api/TAS.SA.Api"
RUN dotnet build "TAS.SA.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TAS.SA.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TAS.SA.Api.dll"]

FROM nginx
EXPOSE 80
RUN rm /etc/nginx/nginx.conf
COPY nginx.conf /etc/nginx/nginx.conf
