build:
	docker run -v $(shell pwd):/app -w /app -it microsoft/dotnet:2.0-sdk dotnet build src/OAuth.DotNetCore/OAuth.DotNetCore.csproj

.PHONY: build
