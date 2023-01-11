#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore ./Mkt.Domain.Product.API/Mkt.Domain.Product.API.csproj
# Build and publish a release
RUN dotnet publish ./Mkt.Domain.Product.API/Mkt.Domain.Product.API.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
ENV ASPNETCORE_URLS=http://+:5010
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "Mkt.Domain.Product.API.dll"]