FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY src/FinFlow.sln ./
COPY src/FinFlow/FinFlow.Api.csproj FinFlow/
COPY src/FinFlow.Application/FinFlow.Application.csproj FinFlow.Application/
COPY src/FinFlow.Domain/FinFlow.Domain.csproj FinFlow.Domain/
COPY src/FinFlow.Infrastructure/FinFlow.Infrastructure.csproj FinFlow.Infrastructure/

RUN dotnet restore FinFlow/FinFlow.Api.csproj

COPY src/FinFlow.Application/ FinFlow.Application/
COPY src/FinFlow.Domain/ FinFlow.Domain/
COPY src/FinFlow.Infrastructure/ FinFlow.Infrastructure/
COPY src/FinFlow/ FinFlow/

WORKDIR /src/FinFlow
RUN dotnet publish FinFlow.Api.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 8080
ENTRYPOINT ["dotnet", "FinFlow.Api.dll"]
