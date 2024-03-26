# Setup base image
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base

# Setup build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Setup workspace
WORKDIR /src
COPY /src .
WORKDIR /src/NecesseScraper

# Restore dependencies
RUN dotnet restore "NecesseScraper.csproj"

# Publish the application
RUN dotnet publish "NecesseScraper.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore

# Setup final runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

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
