#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore ./Mkt.Domain.Order.API/Mkt.Domain.Order.API.csproj
# Build and publish a release
RUN dotnet publish ./Mkt.Domain.Order.API/Mkt.Domain.Order.API.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0

ENV ASPNETCORE_URLS=http://+:5011
EXPOSE 5011

WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "Mkt.Domain.Order.API.dll"]