# Good Hamburger API + Frontend 🍔

API REST desenvolvida em **.NET 8** com **Frontend em Blazor WebAssembly + MudBlazor**, criada como solução para desafio técnico.
O projeto foi construído com foco em **boas práticas de engenharia**, **arquitetura escalável**, **código limpo** e **manutenibilidade**.

---

# Visão Geral

A aplicação permite:

* Gerenciamento completo do cardápio
* Painel administrativo Web
* Criação e gerenciamento de pedidos
* Aplicação automática de descontos promocionais
* Consulta detalhada de pedidos
* Interface moderna e responsiva
* Tratamento padronizado de erros
* Estrutura preparada para evolução futura

---

# Stack Tecnológica

## Backend

* .NET 8
* ASP.NET Core Web API
* Entity Framework Core
* PostgreSQL
* Swagger / OpenAPI

## Frontend

* Blazor WebAssembly
* MudBlazor
* HttpClient
* Componentização Razor

## Qualidade

* xUnit
* FluentAssertions

---

# Arquitetura Utilizada

```text
src/
├── GoodHamburger.Api
├── GoodHamburger.Application
├── GoodHamburger.Domain
├── GoodHamburger.Infrastructure
└── GoodHamburger.Web

tests/
└── GoodHamburger.UnitTests
```

---

# Módulos do Sistema

## Backend

Responsável por:

* Regras de negócio
* Persistência de dados
* API REST
* Cálculo de descontos
* Endpoints administrativos

## Frontend Web

Responsável por:

* Exibição do cardápio
* Carrinho de compras
* Cadastro de pedidos
* Administração do menu
* Inclusão / edição / exclusão de itens
* Interface moderna via MudBlazor

---

# Regras de Negócio

## Limite por Pedido

Cada pedido pode conter no máximo:

* 1 Sanduíche
* 1 Acompanhamento
* 1 Bebida

## Descontos Automáticos

| Combinação                        | Desconto |
| --------------------------------- | -------- |
| Sanduíche + Batata + Refrigerante | 20%      |
| Sanduíche + Refrigerante          | 15%      |
| Sanduíche + Batata                | 10%      |

---

# Como Executar

## 1. Clonar repositório

```bash
git clone URL_DO_REPOSITORIO
cd GoodHamburger
```

---

## 2. Banco PostgreSQL

```sql
CREATE DATABASE goodhamburger_db;
```

---

## 3. Configurar API

Arquivo:

```text
src/GoodHamburger.Api/appsettings.json
```

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=goodhamburger_db;Username=postgres;Password=postgres"
  }
}
```

---

## 4. Rodar migrations

```bash
dotnet ef database update --project src/GoodHamburger.Infrastructure --startup-project src/GoodHamburger.Api
```

---

## 5. Executar Backend

```bash
dotnet run --project src/GoodHamburger.Api
```

Swagger:

```text
https://localhost:7100/swagger
```

---

## 6. Executar Frontend

```bash
dotnet run --project src/GoodHamburger.Web
```

Aplicação Web:

```text
https://localhost:7284
```

---

# Telas do Frontend

## Cardápio

* Lista produtos
* Adiciona ao carrinho

## Carrinho

* Resumo do pedido
* Total com desconto

## Pedidos

* Consulta pedidos realizados

## Admin

* CRUD completo do menu
* Modal para cadastro
* Busca rápida

---

# Endpoints

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

# Diferenciais Implementados

* Backend em camadas (DDD + SOLID)
* Frontend moderno em Blazor
* UI profissional com MudBlazor
* CRUD administrativo
* PostgreSQL real
* Código limpo
* Estrutura pronta para cloud
* Fácil manutenção

---

# Autor

**Maurício Oliveira**