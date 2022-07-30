FROM mcr.microsoft.com/azure-cli:2.29.0 AS base

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish \
	-c release \
	-o result \
	--runtime alpine-x64 \
	--self-contained true \
	/p:PublishTrimmed=true \
	/p:TrimMode=Link \
	/p:PublishSingleFile=true \
	LocalDockerAzureTokenEndpoint.csproj

FROM base AS final
EXPOSE 80
WORKDIR /app
COPY --from=build /src/result .
CMD ["./LocalDockerAzureTokenEndpoint"]