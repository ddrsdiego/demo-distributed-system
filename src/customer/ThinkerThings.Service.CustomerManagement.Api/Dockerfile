FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/customer/ThinkerThings.Service.CustomerManagement.Api/ThinkerThings.Service.CustomerManagement.Api.csproj", "src/customer/ThinkerThings.Service.CustomerManagement.Api/"]
RUN dotnet restore "src/customer/ThinkerThings.Service.CustomerManagement.Api/ThinkerThings.Service.CustomerManagement.Api.csproj"
COPY . .
WORKDIR "/src/src/customer/ThinkerThings.Service.CustomerManagement.Api"
RUN dotnet build "ThinkerThings.Service.CustomerManagement.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ThinkerThings.Service.CustomerManagement.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ThinkerThings.Service.CustomerManagement.Api.dll"]