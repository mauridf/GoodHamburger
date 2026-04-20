# Good Hamburger API 🍔

API REST desenvolvida em **.NET 8** para gerenciamento de pedidos de uma hamburgueria, criada como solução para desafio técnico.  
O projeto foi construído com foco em **boas práticas de engenharia**, **arquitetura escalável**, **código limpo** e **manutenibilidade**.

---

# Visão Geral

A aplicação permite:

- Gerenciamento completo do cardápio
- Criação e gerenciamento de pedidos
- Aplicação automática de descontos promocionais
- Consulta detalhada de pedidos
- Tratamento padronizado de erros
- Estrutura preparada para evolução futura

---

# Stack Tecnológica

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- Swagger / OpenAPI
- xUnit
- FluentAssertions

---

# Arquitetura Utilizada

O projeto segue princípios de **DDD (Domain-Driven Design)** e **SOLID**, organizado em camadas.

```text
src/
├── GoodHamburger.Api
├── GoodHamburger.Application
├── GoodHamburger.Domain
└── GoodHamburger.Infrastructure

tests/
└── GoodHamburger.UnitTests
```

## Responsabilidades

### GoodHamburger.Api
- Controllers
- Configuração da aplicação
- Swagger
- Middlewares
- Injeção de Dependência

### GoodHamburger.Application
- Casos de uso
- DTOs
- Services
- Orquestração entre camadas

### GoodHamburger.Domain
- Entidades
- Regras de negócio
- Exceptions
- Núcleo do sistema

### GoodHamburger.Infrastructure
- Entity Framework Core
- PostgreSQL
- Repositórios
- Persistência de dados

---

# Regras de Negócio

## Limite por Pedido

Cada pedido pode conter no máximo:

- 1 Sanduíche
- 1 Acompanhamento
- 1 Bebida

## Regras de Desconto

| Combinação | Desconto |
|-----------|----------|
| Sanduíche + Batata + Refrigerante | 20% |
| Sanduíche + Refrigerante | 15% |
| Sanduíche + Batata | 10% |

O desconto é calculado automaticamente no momento da criação ou atualização do pedido.

## Diferenciais Implementados (Evolução)

Além dos requisitos originais do desafio, foram adicionadas melhorias:

- Suporte a quantidade por item
- Imagens no cardápio
- Desconto inteligente com múltiplos combos
- Arquitetura DDD + SOLID
- Testes unitários

---

# Como Executar o Projeto

## 1. Clonar o repositório

```bash
git clone URL_DO_REPOSITORIO
```

## 2. Criar banco PostgreSQL

```sql
CREATE DATABASE goodhamburger_db;
```

## 3. Configurar connection string

Arquivo:

```text
src/GoodHamburger.Api/appsettings.json
```

Exemplo:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=goodhamburger_db;Username=postgres;Password=postgres"
  }
}
```

## 4. Aplicar migrations

```bash
dotnet ef database update --project src/GoodHamburger.Infrastructure --startup-project src/GoodHamburger.Api
```

## 5. Executar aplicação

```bash
dotnet run --project src/GoodHamburger.Api
```

---

# Swagger / Documentação

Após iniciar a aplicação:

```text
https://localhost:xxxx/swagger
```

---

# Endpoints Disponíveis

## Menu

```http
GET    /api/menu
GET    /api/menu/{id}
POST   /api/menu
PUT    /api/menu/{id}
DELETE /api/menu/{id}
```

## Orders

```http
GET    /api/orders
GET    /api/orders/{id}
POST   /api/orders
PUT    /api/orders/{id}
DELETE /api/orders/{id}
```

---

# Exemplo de Cadastro de Item no Menu

```json
{
  "name": "X Burger",
  "price": 5.00,
  "category": 1
}
```

## Categorias

```text
1 = Sandwich
2 = Side
3 = Drink
```

---

# Exemplo de Pedido

```json
{
  "menuItemIds": [
    "GUID_ITEM_1",
    "GUID_ITEM_2"
  ]
}
```

---

# Tratamento de Erros

A API retorna mensagens claras e padronizadas para:

- Itens duplicados no pedido
- Pedido inválido
- Recurso não encontrado
- Erros internos

---

# Decisões Técnicas

## Por que PostgreSQL?

- Gratuito
- Estável
- Excelente performance
- Amplamente utilizado no mercado

## Por que DDD?

Separação clara entre regras de negócio e infraestrutura.

## Por que SOLID?

Facilita manutenção, testes e evolução do sistema.

---

# Melhorias Futuras

- Autenticação JWT
- Docker Compose
- CI/CD Pipeline
- Cache Redis
- Logs estruturados
- Frontend em Blazor
- Testes de integração
- Observabilidade

---

# Autor

**Maurício Carvalho**
