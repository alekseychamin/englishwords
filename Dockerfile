FROM mcr.microsoft.com/dotnet/aspnet:6.0
COPY src/PublicApi/bin/Release/net6.0/ App/
WORKDIR /App
ENTRYPOINT ["dotnet", "PublicApi.dll"]