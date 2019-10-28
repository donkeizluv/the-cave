FROM node:lts-alpine as node-build
WORKDIR /app
COPY /src/client/package*.json ./
RUN npm install
COPY /src/client/. ./    
RUN npm run build

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS dotnet-build
WORKDIR /app
RUN mkdir core
RUN mkdir server
COPY /src/server/*.csproj ./server
COPY /src/core/*.csproj ./core
WORKDIR /app/server
RUN dotnet restore
WORKDIR /app
COPY /src/server/. ./server
COPY /src/core/. ./core
WORKDIR /app/server
RUN dotnet publish -c Release -o publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
WORKDIR /app
COPY --from=dotnet-build /app/server/publish .
COPY --from=node-build /app/dist ./wwwroot
# ENTRYPOINT dotnet cave-server.dll
CMD ["dotnet", "cave-server.dll"]
