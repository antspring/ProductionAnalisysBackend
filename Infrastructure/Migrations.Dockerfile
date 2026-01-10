FROM mcr.microsoft.com/dotnet/sdk:9.0 AS migrator
WORKDIR /src

RUN dotnet tool install --global dotnet-ef --version 9.0.11
ENV PATH="$PATH:/root/.dotnet/tools"

COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "Infrastructure/Infrastructure.csproj"

COPY . .

ENTRYPOINT ["dotnet", "ef", "database", "update", "--project", "Infrastructure", "--startup-project", "Infrastructure", "--verbose"]
