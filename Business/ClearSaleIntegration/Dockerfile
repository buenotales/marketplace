#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore ./Mkt.Business.ClearSaleIntegration.Worker/Mkt.Business.ClearSaleIntegration.Worker.csproj
# Build and publish a release
RUN dotnet publish ./Mkt.Business.ClearSaleIntegration.Worker/Mkt.Business.ClearSaleIntegration.Worker.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0

# Declare environment variables
ENV Kafka__Server=kafka1:19091

WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "Mkt.Business.ClearSaleIntegration.Worker.dll"]