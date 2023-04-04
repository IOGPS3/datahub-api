#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 7283
EXPOSE 5283

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Data-hub.csproj", "."]
RUN dotnet restore "./Data-hub.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Data-hub.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Data-hub.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Data-hub.dll"]