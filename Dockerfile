FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

# Gerekli paketleri yükle
RUN apt-get update && \
    apt-get install -y --no-install-recommends \
    gnupg ca-certificates && \
    rm -rf /var/lib/apt/lists/*

# Mono deposunu ekleyin
RUN apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF && \
    echo "deb https://download.mono-project.com/repo/ubuntu stable-bionic main" | tee /etc/apt/sources.list.d/mono-official-stable.list && \
    apt-get update

# libgdiplus kurulumu
RUN apt-get install -y --no-install-recommends libgdiplus

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PdfTools/PdfTools.csproj", "PdfTools/"]
RUN dotnet restore "./PdfTools/PdfTools.csproj"
COPY . .
WORKDIR "/src/PdfTools"
RUN dotnet build "./PdfTools.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PdfTools.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PdfTools.dll"]
