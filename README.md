# Products Management API

Uma API RESTful em ASP.NET Core 10.0 demonstrando operações CRUD de gerenciamento de produtos com banco de dados in-memory, seguindo princípios de Clean Architecture.

## Sumário

- [Visão Geral](#visão-geral)
- [Arquitetura](#arquitetura)
- [Tecnologias](#tecnologias)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Como Executar](#como-executar)
- [Endpoints da API](#endpoints-da-api)
- [Validações](#validações)
- [Tratamento de Erros](#tratamento-de-erros)
- [Testes](#testes)
- [Próximos Passos](#próximos-passos)

## Visão Geral

Projeto de estudo de uma API RESTful construída com ASP.NET Core 10.0 para gerenciamento de produtos. Utiliza banco de dados in-memory, tornando-o ideal para desenvolvimento, aprendizado e execução de testes sem dependências externas.

## Arquitetura

A solução segue Clean Architecture com separação clara de responsabilidades em camadas:

- **Api.Products** — Camada de apresentação: controllers, filtros e configuração do pipeline
- **Domain.Products** — Camada de domínio: serviços, DTOs, interfaces e validadores
- **Infrastructure.Products** — Camada de infraestrutura: contexto do EF Core, entidades e configurações
- **Api.Products.Tests** — Testes unitários da camada de API

## Tecnologias

| Categoria | Tecnologia |
|-----------|------------|
| Framework | .NET 10.0 / ASP.NET Core Web API |
| ORM | Entity Framework Core 10.0 (In-Memory) |
| Validação | FluentValidation 12.1 |
| Documentação | Swagger / Swashbuckle 6.6 |
| Testes | xUnit 2.9, Moq 4.20, FluentAssertions 8.8 |
| Cobertura | Coverlet |

## Estrutura do Projeto

```
managingproducts/
├── Api.Products/                        # Camada de Apresentação
│   ├── Controllers/
│   │   └── ProductsController.cs        # Endpoints CRUD de produtos
│   ├── Global/
│   │   └── GlobalExceptionFilter.cs     # Filtro global de exceções com logging
│   ├── Program.cs                       # Entry point e configuração de DI
│   └── appsettings.json
│
├── Domain.Products/                     # Camada de Domínio
│   ├── Contracts/
│   │   └── IProductService.cs           # Interface do serviço
│   ├── Dto/
│   │   └── ProductDto.cs                # Data Transfer Object
│   ├── Services/
│   │   └── ProductService.cs            # Lógica de negócio
│   └── Validators/
│       └── ProductValidator.cs          # Regras de validação (FluentValidation)
│
├── Infrastructure.Products/             # Camada de Infraestrutura
│   ├── Context/
│   │   └── ProductsContext.cs           # DbContext do EF Core
│   ├── Entities/
│   │   └── Product.cs                   # Entidade de produto
│   └── Configurations/
│       └── ProductConfiguration.cs      # Configuração de tipo de entidade
│
└── Tests/
    └── Api.Products.Tests/              # Testes Unitários
        └── Controllers/
            └── ProductsControllerTests.cs
```

## Como Executar

### Pré-requisitos

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) ou superior
- Visual Studio 2022, VS Code ou qualquer IDE compatível com .NET

### Passos

1. **Clone o repositório**
   ```bash
   git clone <repository-url>
   cd managingproducts
   ```

2. **Restaure as dependências**
   ```bash
   dotnet restore
   ```

3. **Execute a API**
   ```bash
   cd Api.Products
   dotnet run
   ```

4. **Acesse o Swagger UI**

   Abra o navegador e navegue para `http://localhost:5230/swagger`

## Endpoints da API

| Método | Rota | Descrição | Status de Sucesso |
|--------|------|-----------|-------------------|
| GET | `/api/products` | Lista todos os produtos | `200 OK` |
| GET | `/api/products/{id}` | Busca produto por ID | `200 OK` |
| POST | `/api/products` | Cria um novo produto | `200 OK` |
| PUT | `/api/products/{id}` | Atualiza produto existente | `200 OK` |
| DELETE | `/api/products/{id}` | Remove um produto | `204 No Content` |

### Exemplos de Requisição/Resposta

#### POST /api/products

**Body:**
```json
{
  "name": "Laptop",
  "description": "Notebook de alta performance",
  "price": 999.99
}
```

**Resposta:**
```json
{
  "id": 1,
  "name": "Laptop",
  "description": "Notebook de alta performance",
  "price": 999.99,
  "createdDate": "2026-05-16T10:30:00Z"
}
```

#### PUT /api/products/{id}

**Body:**
```json
{
  "name": "Laptop Pro",
  "description": "Notebook profissional",
  "price": 1299.99
}
```

#### Resposta de Erro (400 Bad Request)

```json
{
  "error": "Product does not exist"
}
```

## Validações

As validações são implementadas com FluentValidation e aplicadas automaticamente via middleware:

| Campo | Regras |
|-------|--------|
| `name` | Obrigatório, não vazio, máximo 100 caracteres |
| `description` | Opcional, máximo 150 caracteres |
| `price` | Maior ou igual a 0 |

## Tratamento de Erros

O `GlobalExceptionFilter` intercepta todas as exceções não tratadas e retorna respostas estruturadas com logging:

| Exceção | Status HTTP | Log Level |
|---------|------------|-----------|
| `InvalidOperationException` | `400 Bad Request` | Warning |
| Qualquer outra | `500 Internal Server Error` | Error |

Todas as respostas de erro seguem o formato:
```json
{ "error": "mensagem descritiva" }
```

## Testes

O projeto inclui testes unitários do controller com xUnit, Moq e FluentAssertions.

### Executar os testes

```bash
cd Tests/Api.Products.Tests
dotnet test
```

### Casos cobertos

| Teste | Status |
|-------|--------|
| Listar todos os produtos | OK |
| Buscar produto por ID válido | OK |
| Criar produto com dados válidos | OK |
| Criar produto com model state inválido | OK |
| Atualizar produto com dados válidos | OK |
| Atualizar produto com model state inválido | OK |
| Deletar produto por ID | OK |
| Exceção ao deletar produto inexistente | OK |

## Próximos Passos

Melhorias sugeridas para evoluir o projeto em direção a um cenário de produção:

**Arquitetura**
- [ ] Abstrair registro de DI em extension methods por camada (`AddInfrastructure()`, `AddDomain()`)
- [ ] Implementar padrão Repository explícito para separar o acesso a dados do serviço
- [ ] Adicionar AutoMapper para mapeamento entre entidade e DTO

**Dados**
- [ ] Migrar para banco persistente (SQL Server, PostgreSQL) com migrations do EF Core
- [ ] Configurar precisão decimal para o campo `Price` (`decimal(18,2)`)

**Segurança & Qualidade**
- [ ] Implementar autenticação e autorização (JWT)
- [ ] Configurar CORS
- [ ] Adicionar rate limiting

**Testes**
- [ ] Adicionar testes unitários para `ProductService` e `ProductValidator`
- [ ] Adicionar testes de integração com banco in-memory real
- [ ] Adicionar testes de integração com banco in-memory real

**Observabilidade**
- [ ] Configurar structured logging (Serilog ou OpenTelemetry)
- [ ] Adicionar health checks (`/health`)

---

> Projeto educacional demonstrando Clean Architecture, validação com FluentValidation e testes com mocks em ASP.NET Core 10.0.
