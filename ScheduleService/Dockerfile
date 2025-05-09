FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TechChallenge/RegistrationService.csproj", "TechChallenge/"]
COPY ["Infraestructure/Infraestructure.csproj", "Infraestructure/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Common/Common.csproj", "Common/"]
COPY ["Integration/Integration.csproj", "Integration/"]

RUN dotnet restore "TechChallenge/RegistrationService.csproj"
COPY . .
WORKDIR "/src/TechChallenge"
RUN dotnet build "RegistrationService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RegistrationService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "RegistrationService.dll"]