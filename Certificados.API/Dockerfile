#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Certificados.API/Certificados.API.csproj", "Certificados.API/"]
RUN dotnet restore "Certificados.API/Certificados.API.csproj"
COPY . .
WORKDIR "/src/Certificados.API"
RUN dotnet build "Certificados.API.csproj" -c Release -o /app/build
RUN dotnet tool install --tool-path /tools dotnet-trace
RUN dotnet tool install --tool-path /tools dotnet-counters
RUN dotnet tool install --tool-path /tools dotnet-dump

FROM build AS publish
ARG PRODUCT_VERSION
RUN dotnet publish "Certificados.API.csproj" -c Release -o /app/publish /p:InformationalVersion=$PRODUCT_VERSION

FROM base AS final
WORKDIR /tools
COPY --from=build /tools .
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Certificados.API.dll"]

