#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/BuildingBlocks/ThinkerThings.BuildingBlocks.Cache/ThinkerThings.BuildingBlocks.Cache.csproj", "src/BuildingBlocks/ThinkerThings.BuildingBlocks.Cache/"]
COPY ["src/BuildingBlocks/ThinkerThings.BuildingBlocks.Application/ThinkerThings.BuildingBlocks.Application.csproj", "src/BuildingBlocks/ThinkerThings.BuildingBlocks.Application/"]
COPY ["src/services/orders/ThinkerThings.Service.Orders.Api/ThinkerThings.Services.Orders.Api.csproj", "src/services/orders/ThinkerThings.Service.Orders.Api/"]
RUN dotnet restore "src/services/orders/ThinkerThings.Service.Orders.Api/ThinkerThings.Services.Orders.Api.csproj"
COPY . .
WORKDIR "/src/src/services/orders/ThinkerThings.Service.Orders.Api"
RUN dotnet build "ThinkerThings.Services.Orders.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ThinkerThings.Services.Orders.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "ThinkerThings.Services.Orders.Api.dll"]