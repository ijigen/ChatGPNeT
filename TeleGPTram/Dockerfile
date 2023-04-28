FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TeleGPTram/TeleGPTram.csproj", "TeleGPTram/"]
RUN dotnet restore "TeleGPTram/TeleGPTram.csproj"
COPY . .
WORKDIR "/src/TeleGPTram"
RUN dotnet build "TeleGPTram.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TeleGPTram.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TeleGPTram.dll"]
