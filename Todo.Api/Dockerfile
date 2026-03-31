# =============================================
# Etapa 1: Build (usando SDK compatível com .NET 8)
# =============================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar apenas os arquivos .csproj para cache de restore
COPY ["Todo.Api/Todo.Api.csproj", "Todo.Api/"]
COPY ["Todo.Aplication/Todo.Aplication.csproj", "Todo.Aplication/"]
COPY ["Todo.Domain/Todo.Domain.csproj", "Todo.Domain/"]
COPY ["Todo.Infrastructure/Todo.Infrastructure.csproj", "Todo.Infrastructure/"]
COPY ["Todo.Tests/Todo.Tests.csproj", "Todo.Tests/"]

# Restaurar dependências de todos os projetos
RUN dotnet restore "Todo.Api/Todo.Api.csproj"
RUN dotnet restore "Todo.Aplication/Todo.Aplication.csproj"
RUN dotnet restore "Todo.Domain/Todo.Domain.csproj"
RUN dotnet restore "Todo.Infrastructure/Todo.Infrastructure.csproj"

# Copiar todo o código fonte
COPY . .

# === LIMPEZA: Remove obj para forçar regeneração dentro do Docker ===
RUN find /src -type d -name "obj" -exec rm -rf {} + 2>/dev/null || true
RUN find /src -type d -name "bin" -exec rm -rf {} + 2>/dev/null || true

# Fazer novo restore para gerar project.assets.json com paths do Linux
RUN dotnet restore "Todo.Api/Todo.Api.csproj"

# Build do projeto principal a partir da raiz
RUN dotnet build "Todo.Api/Todo.Api.csproj" -c $BUILD_CONFIGURATION --no-restore

# Publish do projeto principal
RUN dotnet publish "Todo.Api/Todo.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false --no-restore

# =============================================
# Etapa 2: Runtime (imagem leve para produção)
# =============================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Segurança: usuário não-root
USER app

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Todo.Api.dll"]