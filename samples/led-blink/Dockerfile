FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim-arm32v7 AS build
WORKDIR /app

# Install Debugger
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
       unzip procps \
    && rm -rf /var/lib/apt/lists/* \
    && curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /remote_debugger

# copy csproj and restore as distinct layers
# COPY *.config .
COPY *.csproj .
RUN dotnet restore

# copy and build app
COPY . .
RUN dotnet publish -c debug -o out

FROM mcr.microsoft.com/dotnet/runtime:5.0-buster-slim-arm32v7 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "led-blink.dll"]
