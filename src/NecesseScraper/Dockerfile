FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/NecesseScraper/NecesseScraper.csproj", "src/NecesseScraper/"]
RUN dotnet restore "src/NecesseScraper/NecesseScraper.csproj"
COPY . .
WORKDIR "/src/src/NecesseScraper"
RUN dotnet build "NecesseScraper.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NecesseScraper.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NecesseScraper.dll"]
