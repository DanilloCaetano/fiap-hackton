FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["UserConsumer/UserConsumer.csproj", "UserConsumer/"]
COPY ["Infraestructure/Infraestructure.csproj", "Infraestructure/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Common/Common.csproj", "Common/"]
RUN dotnet restore "UserConsumer/UserConsumer.csproj"
COPY . .
WORKDIR "/src/UserConsumer"
RUN dotnet build "UserConsumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserConsumer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 6060
ENTRYPOINT ["dotnet", "UserConsumer.dll"]