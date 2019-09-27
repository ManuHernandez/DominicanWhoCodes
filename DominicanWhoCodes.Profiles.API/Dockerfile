FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["DominicanWhoCodes.Profiles.API/DominicanWhoCodes.Profiles.API.csproj", "DominicanWhoCodes.Profiles.API/"]
COPY ["DominicanWhoCodes.Profiles.Infrastructure/DominicanWhoCodes.Profiles.Infrastructure.csproj", "DominicanWhoCodes.Profiles.Infrastructure/"]
COPY ["DominicanWhoCodes.Profiles.Domain/DominicanWhoCodes.Profiles.Domain.csproj", "DominicanWhoCodes.Profiles.Domain/"]
COPY ["DominicanWhoCodes.Shared/DominicanWhoCodes.Shared.csproj", "DominicanWhoCodes.Shared/"]
RUN dotnet restore "DominicanWhoCodes.Profiles.API/DominicanWhoCodes.Profiles.API.csproj"
COPY . .
WORKDIR "/src/DominicanWhoCodes.Profiles.API"
RUN dotnet build "DominicanWhoCodes.Profiles.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DominicanWhoCodes.Profiles.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DominicanWhoCodes.Profiles.API.dll"]