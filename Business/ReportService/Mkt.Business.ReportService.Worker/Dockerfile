#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Business/ReportService/Mkt.Business.ReportService.Worker/Mkt.Business.ReportService.Worker.csproj", "Business/ReportService/Mkt.Business.ReportService.Worker/"]
RUN dotnet restore "Business/ReportService/Mkt.Business.ReportService.Worker/Mkt.Business.ReportService.Worker.csproj"
COPY . .
WORKDIR "/src/Business/ReportService/Mkt.Business.ReportService.Worker"
RUN dotnet build "Mkt.Business.ReportService.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mkt.Business.ReportService.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mkt.Business.ReportService.Worker.dll"]