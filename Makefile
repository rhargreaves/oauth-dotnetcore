DOTNET=docker run \
	-v $(shell pwd):/app \
	-w /app \
	-it \
	mcr.microsoft.com/dotnet/core/sdk:2.2-alpine \
	dotnet
CSPROJ=src/OAuth.DotNetCore/OAuth.DotNetCore.csproj
VERSION=2.2.0

build:
	$(DOTNET) build $(CSPROJ)

pack: build
	$(DOTNET) pack $(CSPROJ) -c Release

test:
	$(DOTNET) test \
		-f netcoreapp2.2 \
		tests/OAuth.UnitTests/OAuth.UnitTests.csproj 

nuget-push: pack
	$(DOTNET) nuget push \
		src/OAuth.DotNetCore/bin/Release/OAuth.DotNetCore.$(VERSION).nupkg \
		-k $(NUGET_KEY) \
		-s nuget.org

.PHONY: build pack nuget-push
