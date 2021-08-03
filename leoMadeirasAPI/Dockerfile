FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /api

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /api
COPY --from=build-env /api/out .
ENTRYPOINT ["dotnet", "leoMadeirasAPI.dll"]