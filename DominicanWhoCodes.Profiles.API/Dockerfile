FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["../DominicanWhoCodes.Profiles.API/DominicanWhoCodes.Profiles.API.csproj", "../DominicanWhoCodes.Profiles.API/"]
RUN dotnet restore "../DominicanWhoCodes.Profiles.API/DominicanWhoCodes.Profiles.API.csproj"
COPY . .
WORKDIR "/src/../DominicanWhoCodes.Profiles.API"
RUN dotnet build "DominicanWhoCodes.Profiles.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DominicanWhoCodes.Profiles.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DominicanWhoCodes.Profiles.API.dll"]