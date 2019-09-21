FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["../DominicanWhoCodes.Identity.Gateway/DominicanWhoCodes.Identity.Gateway.csproj", "../DominicanWhoCodes.Identity.Gateway/"]
RUN dotnet restore "../DominicanWhoCodes.Identity.Gateway/DominicanWhoCodes.Identity.Gateway.csproj"
COPY . .
WORKDIR "/src/../DominicanWhoCodes.Identity.Gateway"
RUN dotnet build "DominicanWhoCodes.Identity.Gateway.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DominicanWhoCodes.Identity.Gateway.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DominicanWhoCodes.Identity.Gateway.dll"]