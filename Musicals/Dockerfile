#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#FROM ubuntu:20
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Musicals/Musicals.csproj", "Musicals/"]
RUN dotnet restore "Musicals/Musicals.csproj"
COPY . .
WORKDIR "/src/Musicals"
RUN dotnet build "Musicals.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "Musicals.csproj" -c Debug -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Musicals.dll"]