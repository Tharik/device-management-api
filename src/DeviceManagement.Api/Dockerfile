FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["src/DeviceManagement.Api/DeviceManagement.Api.csproj", "src/DeviceManagement.Api/"]
RUN dotnet restore "src/DeviceManagement.Api/DeviceManagement.Api.csproj"

COPY . .
WORKDIR "/src/src/DeviceManagement.Api"
RUN dotnet publish "DeviceManagement.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "DeviceManagement.Api.dll"]