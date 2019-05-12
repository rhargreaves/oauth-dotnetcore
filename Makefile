DOTNET=docker run \
	-v $(shell pwd):/app \
	-w /app \
	-it \
	mcr.microsoft.com/dotnet/core/sdk:2.2-alpine \
	dotnet
CSPROJ=src/OAuth/OAuth.csproj
VERSION=2.2.0

build:
	$(DOTNET) build \
		-f netstandard2.0 \
		$(CSPROJ)

pack: build
	$(DOTNET) pack \
		-c Release \
		$(CSPROJ)

test: build
	$(DOTNET) test \
		-f netcoreapp2.2 \
		tests/OAuth.UnitTests/OAuth.UnitTests.csproj 

nuget-push: pack
	$(DOTNET) nuget push \
		src/OAuth.DotNetCore/bin/Release/OAuth.DotNetCore.$(VERSION).nupkg \
		-k $(NUGET_KEY) \
		-s nuget.org

.PHONY: build pack nuget-push
