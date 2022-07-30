FROM mcr.microsoft.com/dotnet/runtime:6.0-alpine AS base

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish "SampleConsumerApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SampleConsumerApp.dll"]