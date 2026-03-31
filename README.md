# 📝 Todo API

API de Gerenciamento de Tarefas (To-Do List) construída com **ASP.NET Core 8**, **Entity Framework Core**, **SQL Server** e **Docker**.

## 📋 Tabela de Conteúdos

- [Sobre](#sobre)
- [Tecnologias](#tecnologias)
- [Pré-requisitos](#pré-requisitos)
- [Instalação](#instalação)
- [Configuração](#configuração)
- [Como Executar](#como-executar)
- [Endpoints da API](#endpoints-da-api)
- [Testes](#testes)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Docker](#docker)
- [Troubleshooting](#troubleshooting)

## 🎯 Sobre

Esta é uma API RESTful para gerenciar tarefas com funcionalidades como:

✅ Criar tarefas com título, descrição e data de vencimento
✅ Listar todas as tarefas ou filtrar por status e datas
✅ Atualizar tarefas existentes
✅ Deletar tarefas (soft delete)
✅ Alterar status (Pendente → Em Andamento → Concluído)
✅ Reabrir tarefas concluídas
✅ Validação de dados com FluentValidation
✅ Documentação com Swagger/OpenAPI

## 🛠️ Tecnologias

- .NET 8
- ASP.NET Core 8
- SQL Server 2022
- Entity Framework Core 8
- FluentValidation
- Swagger/OpenAPI
- Docker & Docker Compose
- xUnit, Moq, FluentAssertions

## 📦 Pré-requisitos

### Local (Desenvolvimento)
- .NET SDK 8.0+ → https://dotnet.microsoft.com/en-us/download/dotnet/8.0
- SQL Server 2022+ → https://www.microsoft.com/pt-br/sql-server
- Git → https://git-scm.com/

### Docker (Recomendado)
- Docker Desktop → https://www.docker.com/products/docker-desktop

## 🚀 Instalação

### 1. Clonar o Repositório

```bash
git clone https://github.com/rogerch93/Todo.Api.git
cd Todo.Api
2. Restaurar Dependências
dotnet restore
3. Verificar Versão do .NET
dotnet --version
⚙️ Configuração
Desenvolvimento (Local)
Edite appsettings.Development.json:
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=ToDoBD;Trusted_Connection=True;..."
  }
}
Produção (Docker)
O arquivo appsettings.Production.json já está configurado para Docker.
🏃 Como Executar
Opção 1: Localmente
cd Todo.Api
dotnet ef database update --project ../Todo.Infrastructure
dotnet run
API em: http://localhost:5001 Swagger em: http://localhost:5001/swagger
Opção 2: Docker (Recomendado)
cd Todo.Api
docker-compose up -d --build
Start-Sleep -Seconds 15
docker ps
API em: http://localhost:8080 Swagger em: http://localhost:8080/swagger
Parar os Containers
docker-compose down -v
🔌 Endpoints da API
Base URL: http://localhost:8080/api/Tarefas
Criar Tarefa
POST /api/Tarefas
Listar Tarefas
GET /api/Tarefas
Filtrar Tarefas
GET /api/Tarefas?status=Pendente&dataInicio=2026-01-01&dataFim=2026-12-31
Obter Tarefa por ID
GET /api/Tarefas/{id}
Atualizar Tarefa
PUT /api/Tarefas/{id}
Iniciar Tarefa
POST /api/Tarefas/{id}/iniciar
Concluir Tarefa
POST /api/Tarefas/{id}/concluir
Reabrir Tarefa
POST /api/Tarefas/{id}/reabrir
Deletar Tarefa
DELETE /api/Tarefas/{id}
🧪 Testes
dotnet test
📁 Estrutura do Projeto
Todo.Api/
├── Todo.Api/              # Projeto Web
├── Todo.Aplication/       # Lógica de Aplicação
├── Todo.Domain/           # Entidades de Domínio
├── Todo.Infrastructure/   # Acesso a Dados
├── Todo.Tests/            # Testes Unitários
└── docker-compose.yml     # Orquestração Docker
🐳 Docker
Serviços
1.	todo-sqlserver: SQL Server 2022 (porta 1433)
•	Usuário: SA
•	Senha: Your@Password123
2.	todo-api-container: API (porta 8080)
Comandos Úteis
# Status dos containers
docker ps

# Logs da API
docker logs todo-api-container

# Logs do SQL Server
docker logs todo-sqlserver

# Entrar no bash
docker exec -it todo-api-container /bin/bash
🔍 Troubleshooting
Erro: "Cannot connect to devweb"
Verifique qual appsettings.json está sendo usado:
$env:ASPNETCORE_ENVIRONMENT = "Development"
dotnet run
Erro: "Port 8080 is already in use"
docker-compose down
SQL Server não inicializa
docker-compose down -v
docker-compose up -d
Start-Sleep -Seconds 30
Swagger não aparece
•	Verifique: http://localhost:8080/swagger
•	Confira logs: docker logs todo-api-container
🤝 Contribuições
1.	Fork o repositório
2.	Crie uma branch (git checkout -b feature/AmazingFeature)
3.	Commit as mudanças (git commit -m 'Add AmazingFeature')
4.	Push (git push origin feature/AmazingFeature)
5.	Abra um Pull Request
📄 Licença
MIT License
👤 Autor
Rogerio Chaves
•	GitHub: @rogerch93
•	Repositório: Todo.Api
📞 Suporte
Se encontrar problemas:
1.	Verifique o Troubleshooting
2.	Abra uma Issue no GitHub
3.	Confira os logs: docker logs todo-api-container
