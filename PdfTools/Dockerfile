FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . ./

RUN dotnet restore

RUN apt-get update && apt-get install -y libgdiplus \
    && cp /PdfTools/libwkhtmltox.dll /app/libwkhtmltox.dll \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/*

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "PdfTools.dll"]