FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CompanyService/CompanyService.csproj", "CompanyService/"]
RUN dotnet restore "CompanyService/CompanyService.csproj"
COPY . .
WORKDIR "/src/CompanyService"
RUN dotnet build "CompanyService.csproj" -c $BUILD_CONFIGURATION -o /app/build

RUN dotnet nuget list source | grep -q "nuget.org" && dotnet nuget remove source "nuget.org"

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CompanyService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CompanyService.dll"]