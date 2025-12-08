FROM mcr.microsoft.com/dotnet/sdk:9.0 AS migrator
WORKDIR /src

RUN dotnet tool install --global dotnet-ef --version 9.0.11
ENV PATH="$PATH:/root/.dotnet/tools"

COPY ["ProductionAnalisysAPI/ProductionAnalisysAPI.csproj", "ProductionAnalisysAPI/"]
COPY ["DataAccess/DataAccess.csproj", "DataAccess/"]
RUN dotnet restore "ProductionAnalisysAPI/ProductionAnalisysAPI.csproj"

COPY . .

ENTRYPOINT ["dotnet", "ef", "database", "update", "--project", "DataAccess", "--startup-project", "ProductionAnalisysAPI", "--verbose"]
