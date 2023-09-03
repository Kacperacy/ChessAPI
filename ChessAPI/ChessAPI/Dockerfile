FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ChessAPI/ChessAPI.csproj", "ChessAPI/"]
RUN dotnet restore "ChessAPI/ChessAPI.csproj"
COPY . .
WORKDIR "/src/ChessAPI"
RUN dotnet build "ChessAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChessAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChessAPI.dll"]
