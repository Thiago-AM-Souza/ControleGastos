# Sistema de controle de gastos

## Visão geral (Backend)
O projeto é uma Web API em .NET 9, tendo sua arquitetura baseada em Clean Arch com CQRS e banco de dados PostgreSQL. 

Projetos principais separados por camadas:
- `WebApi` — endpoints HTTP.
- `Application` — commands, queries e handlers.
- `Infrastructure` — repositórios EF e Dapper.
- `Domain` - entidades do negócio.
- `BuildingBlocks` — recursos de uso comum.

Principais bibliotecas:
- MediatR
- Dapper
- Entity Framework Core
- FluentValidation
- Swashbuckle Swagger

## Fluxo resumido
1. Requisição chega ao controller.
2. Controller cria `Command` / `Query` via MediatR.
3. `QueryHandler` / `CommandHandler` realiza validações e usa os repositórios para obter dados.
4. Repositório executa query SQL com Dapper ou EF Core e retorna os DTOs.
5. Handlers mapeiam resultados para `Result` e retornam ao controller.

## Como compilar e executar (local)
- Abra a solução no Visual Studio 2022.
- Certifique-se de ter o .NET 9 instalado.
- Para compilar: use __Build > Build Solution__.
- Para executar: use __Debug > Start Debugging__ ou __Debug > Start Without Debugging__.
- Alternativamente com CLI:
  - `dotnet build`
  - `dotnet run --project WebApi/ControleGastos.WebApi`


## Visão geral (Frontend)

Aplicação frontend implementada com TypeScript + React (Vite) e Bootstrap. Fornece interfaces para gerenciar categorias, pessoas e transações, e visualizar relatórios.

## Tecnologias
- TypeScript
- React (Vite)
- Bootstrap
- Axios

## Estrutura do projeto
- `src/` — código-fonte principal
  - `components/` — componentes reutilizáveis (ex.: `PaginatedTable`, `Toast`, `Modal`)
  - `pages/` — páginas e telas agrupadas por domínio:
    - `categorias/` — cadastro, listagem e relatórios
    - `pessoas/` — cadastro, listagem e relatórios
    - `transacoes/` — cadastro e listagem
  - `services/` — chamadas HTTP
  - `models/` e `types/` — modelos, requests/responses e enums

 ## Como executar (local)
1. Instalar dependências:

```bash
npm install
```

2. Rodar em modo desenvolvimento:

```bash
npm run dev
```

(abra o endereço fornecido pelo Vite.)
