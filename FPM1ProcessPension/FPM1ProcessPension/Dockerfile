#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["FPM1ProcessPension/FPM1ProcessPension.csproj", "FPM1ProcessPension/"]
RUN dotnet restore "FPM1ProcessPension/FPM1ProcessPension.csproj"
COPY . .
WORKDIR "/src/FPM1ProcessPension"
RUN dotnet build "FPM1ProcessPension.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FPM1ProcessPension.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FPM1ProcessPension.dll"]