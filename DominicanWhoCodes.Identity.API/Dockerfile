FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["../DominicanWhoCodes.Identity.API/DominicanWhoCodes.Identity.API.csproj", "../DominicanWhoCodes.Identity.API/"]
RUN dotnet restore "../DominicanWhoCodes.Identity.API/DominicanWhoCodes.Identity.API.csproj"
COPY . .
WORKDIR "/src/../DominicanWhoCodes.Identity.API"
RUN dotnet build "DominicanWhoCodes.Identity.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DominicanWhoCodes.Identity.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DominicanWhoCodes.Identity.API.dll"]