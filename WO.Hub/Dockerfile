#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WO.Hub/WO.Hub.csproj", "WO.Hub/"]
RUN dotnet restore "WO.Hub/WO.Hub.csproj"
COPY . .
WORKDIR "/src/WO.Hub"
RUN dotnet build "WO.Hub.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WO.Hub.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WO.Hub.dll"]