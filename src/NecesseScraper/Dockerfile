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

# Update local pacakges
RUN apt-get update --no-install-recommends

# Install required packages for headless chrome
RUN apt-get install -y libglib2.0-0
RUN apt-get install -y libnss3
RUN apt-get install -y libxss1
RUN apt-get install -y libasound2
RUN apt-get install -y wget

# Install chrome
RUN wget https://dl.google.com/linux/direct/google-chrome-stable_current_amd64.deb
RUN apt-get install -y ./google-chrome-stable_current_amd64.deb

# Cleanup the local pacakges
RUN apt-get clean \
 && rm -rf /var/lib/apt/lists/*

ENTRYPOINT ["dotnet", "NecesseScraper.dll"]
