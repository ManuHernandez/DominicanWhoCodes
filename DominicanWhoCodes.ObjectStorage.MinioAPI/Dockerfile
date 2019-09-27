FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["DominicanWhoCodes.ObjectStorage.MinioAPI/DominicanWhoCodes.ObjectStorage.MinioAPI.csproj", "DominicanWhoCodes.ObjectStorage.MinioAPI/"]
COPY ["DominicanWhoCodes.Shared/DominicanWhoCodes.Shared.csproj", "DominicanWhoCodes.Shared/"]
RUN dotnet restore "DominicanWhoCodes.ObjectStorage.MinioAPI/DominicanWhoCodes.ObjectStorage.MinioAPI.csproj"
COPY . .
WORKDIR "/src/DominicanWhoCodes.ObjectStorage.MinioAPI"
RUN dotnet build "DominicanWhoCodes.ObjectStorage.MinioAPI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DominicanWhoCodes.ObjectStorage.MinioAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DominicanWhoCodes.ObjectStorage.MinioAPI.dll"]