# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY bruno_backend.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish bruno_backend.csproj -c Release -o /app/publish --no-restore

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

RUN mkdir -p Logs

ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:5111

EXPOSE 5111

ENTRYPOINT ["dotnet", "bruno_backend.dll"]
