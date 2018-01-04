DOTNET=docker run -v $(shell pwd):/app -w /app -it microsoft/dotnet:2.0-sdk dotnet

build:
	$(DOTNET) build src/OAuth.DotNetCore/OAuth.DotNetCore.csproj

pack:
	$(DOTNET) pack src/OAuth.DotNetCore/OAuth.DotNetCore.csproj -c Release

nuget-push:
	$(DOTNET) nuget push src/OAuth.DotNetCore/bin/Release/OAuth.DotNetCore.2.0.0.nupkg -k $(NUGET_KEY) -s nuget.org

.PHONY: build pack
