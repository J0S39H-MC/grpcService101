#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 80
#EXPOSE 443
EXPOSE 9046
EXPOSE 5001



FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["gRPCService101/gRPCService101.csproj", "gRPCService101/"]
COPY ["gRPCService101.ServiceModel/gRPCService101.ServiceModel.csproj", "gRPCService101.ServiceModel/"]
RUN dotnet restore "gRPCService101/gRPCService101.csproj"
COPY . .
WORKDIR "/src/gRPCService101"
RUN dotnet build "gRPCService101.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "gRPCService101.csproj" -c Release -o /app/publish


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "gRPCService101.dll"]
