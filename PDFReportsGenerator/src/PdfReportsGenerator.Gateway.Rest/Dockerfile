FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/PdfReportsGenerator.Gateway.Rest/PdfReportsGenerator.Gateway.Rest.csproj", "src/PdfReportsGenerator.Gateway.Rest/"]
COPY ["src/PdfReportsGenerator.Application/PdfReportsGenerator.Application.csproj", "src/PdfReportsGenerator.Application/"]
COPY ["src/PdfReportsGenerator.Domain/PdfReportsGenerator.Domain.csproj", "src/PdfReportsGenerator.Domain/"]
COPY ["src/PdfReportsGenerator.Infrastructure/PdfReportsGenerator.Infrastructure.csproj", "src/PdfReportsGenerator.Infrastructure/"]
RUN dotnet restore "src/PdfReportsGenerator.Gateway.Rest/PdfReportsGenerator.Gateway.Rest.csproj"
COPY . .
WORKDIR "/src/src/PdfReportsGenerator.Gateway.Rest"
RUN dotnet build "PdfReportsGenerator.Gateway.Rest.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PdfReportsGenerator.Gateway.Rest.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PdfReportsGenerator.Gateway.Rest.dll"]