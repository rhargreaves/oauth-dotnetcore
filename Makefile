DOTNET=docker run -v $(shell pwd):/app -w /app -it microsoft/dotnet:2.0-sdk dotnet
CSPROJ=src/OAuth.DotNetCore/OAuth.DotNetCore.csproj
VERSION=2.2.0

build:
	$(DOTNET) build $(CSPROJ)

pack: build
	$(DOTNET) pack $(CSPROJ) -c Release

nuget-push: pack
	$(DOTNET) nuget push \
		src/OAuth.DotNetCore/bin/Release/OAuth.DotNetCore.$(VERSION).nupkg \
		-k $(NUGET_KEY) \
		-s nuget.org

.PHONY: build pack nuget-push
