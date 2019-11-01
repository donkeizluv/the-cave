FROM node:lts-alpine as node-build
WORKDIR /app
COPY /src/client/package*.json ./
RUN npm install
COPY /src/client/. ./    
RUN npm run build

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS dotnet-build
WORKDIR /app

COPY *.sln .
COPY /src/server/*.csproj ./src/server/
COPY /src/core/*.csproj ./src/core/
RUN dotnet restore

COPY /src/server/. ./src/server/
COPY /src/core/. ./src/core/
RUN dotnet publish -c Release -o publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
WORKDIR /app
COPY --from=dotnet-build /app/publish .
COPY --from=node-build /app/dist ./wwwroot
# ENTRYPOINT dotnet cave-server.dll
CMD ["dotnet", "cave-server.dll"]
