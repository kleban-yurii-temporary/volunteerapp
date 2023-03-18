#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["VolunteerRequestApp/Server/VolunteerRequestApp.Server.csproj", "VolunteerRequestApp/Server/"]
COPY ["VolunteerRequestApp.Server.Core/VolunteerRequestApp.Server.Core.csproj", "VolunteerRequestApp.Server.Core/"]
COPY ["VolunteerRequestApp.Server.Infrastructure/VolunteerRequestApp.Server.Infrastructure.csproj", "VolunteerRequestApp.Server.Infrastructure/"]
COPY ["VolunteerRequestApp/Shared/VolunteerRequestApp.Shared.csproj", "VolunteerRequestApp/Shared/"]
COPY ["VolunteerRequestApp/Client/VolunteerRequestApp.Client.csproj", "VolunteerRequestApp/Client/"]
COPY ["VolunteerRequestApp.Client.Infrastructure/VolunteerRequestApp.Client.Infrastructure.csproj", "VolunteerRequestApp.Client.Infrastructure/"]
RUN dotnet restore "VolunteerRequestApp/Server/VolunteerRequestApp.Server.csproj"
COPY . .
WORKDIR "/src/VolunteerRequestApp/Server"
RUN dotnet build "VolunteerRequestApp.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VolunteerRequestApp.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VolunteerRequestApp.Server.dll"]