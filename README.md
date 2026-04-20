# Good Hamburger API

API REST desenvolvida em **.NET 8** para gerenciamento de pedidos de uma hamburgueria, criada como solução para desafio técnico.

---

# Objetivo

Permitir:

- Cadastro e gerenciamento do cardápio
- Criação e gerenciamento de pedidos
- Aplicação automática de descontos
- Consulta de pedidos
- Estrutura escalável e manutenível

---

# Tecnologias Utilizadas

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- Swagger / OpenAPI
- Arquitetura em Camadas
- DDD
- SOLID
- xUnit (testes)

---

# Arquitetura

```text
src/

GoodHamburger.Api
GoodHamburger.Application
GoodHamburger.Domain
GoodHamburger.Infrastructure
Camadas
Api

Responsável por:

Controllers
Swagger
Configuração da aplicação
Middlewares
Application

Responsável por:

Casos de uso
DTOs
Services
Domain

Responsável por:

Entidades
Regras de negócio
Exceptions
Infrastructure

Responsável por:

EF Core
PostgreSQL
Repositories
Regras de Negócio
Limite por pedido

Cada pedido pode conter no máximo:

1 Sanduíche
1 Batata
1 Refrigerante
Descontos
Combinação	Desconto
Sanduíche + Batata + Refrigerante	20%
Sanduíche + Refrigerante	15%
Sanduíche + Batata	10%
Como Executar
1. Clonar repositório
git clone URL_DO_REPOSITORIO
2. Configurar banco PostgreSQL

Criar database:

CREATE DATABASE goodhamburger_db;
3. Ajustar connection string

Arquivo:

src/GoodHamburger.Api/appsettings.json

Exemplo:

{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=goodhamburger_db;Username=postgres;Password=postgres"
  }
}
4. Rodar migration
dotnet ef database update --project src/GoodHamburger.Infrastructure --startup-project src/GoodHamburger.Api
5. Executar aplicação
dotnet run --project src/GoodHamburger.Api
Swagger

Ao executar:

https://localhost:xxxx/swagger
Endpoints
Menu
GET    /api/menu
GET    /api/menu/{id}
POST   /api/menu
PUT    /api/menu/{id}
DELETE /api/menu/{id}
Orders
GET    /api/orders
GET    /api/orders/{id}
POST   /api/orders
PUT    /api/orders/{id}
DELETE /api/orders/{id}
Exemplo Cadastro Menu
{
  "name": "X Burger",
  "price": 5.00,
  "category": 1
}
Categorias
1 = Sandwich
2 = Side
3 = Drink
Exemplo Pedido
{
  "menuItemIds": [
    "GUID_ITEM_1",
    "GUID_ITEM_2"
  ]
}
Tratamento de Erros

A API retorna mensagens claras para:

Itens duplicados
Pedido inválido
Recurso não encontrado
Erros internos
Decisões Técnicas
Por que PostgreSQL?
Gratuito
Robusto
Excelente performance
Muito utilizado no mercado
Por que DDD?

Separação clara entre domínio e infraestrutura.

Por que SOLID?

Maior manutenção e testabilidade.

Melhorias Futuras
Autenticação JWT
Cache Redis
Docker Compose
CI/CD
Logs estruturados
Frontend em Blazor
Testes de integração
Autor

Maurício Carvalho


---

# Commit final

```bash id="w5m1rn"
docs: add complete project documentation
```