# 🛒 Loja API

API REST completa para gerenciamento de produtos e categorias, construída com **C# .NET** e deployada em produção.

🔗 **[lojaapi-production.up.railway.app](https://lojaapi-production.up.railway.app)**  
👤 **[LinkedIn — Fabio Boa Sorte](https://www.linkedin.com/in/fabioboasorte/)**

---

## 🚀 Tecnologias

- **[.NET 10](https://dotnet.microsoft.com/)** — plataforma de desenvolvimento
- **[ASP.NET Core](https://learn.microsoft.com/aspnet/core)** — framework web
- **[Entity Framework Core](https://learn.microsoft.com/ef/core)** — ORM e migrations
- **[PostgreSQL](https://www.postgresql.org/)** — banco de dados em produção
- **[JWT Bearer](https://jwt.io/)** — autenticação e autorização
- **[xUnit](https://xunit.net/)** — testes automatizados
- **[Railway](https://railway.app/)** — deploy e infraestrutura

---

## 📦 Funcionalidades

- ✅ CRUD completo de **Produtos** (com SKU)
- ✅ CRUD completo de **Categorias**
- ✅ Relacionamento **ManyToMany** entre Produtos e Categorias
- ✅ **Paginação** nos resultados
- ✅ **Autenticação JWT** com roles (Admin)
- ✅ **Validação** de dados com Data Annotations
- ✅ **Tratamento de erros** centralizado via Middleware
- ✅ **DTOs** para controle do contrato da API
- ✅ **Migrations** automáticas no startup
- ✅ **Testes automatizados** com banco em memória

---

## 🗂️ Estrutura do Projeto

```
LojaApi/
├── Controllers/
│   ├── AuthController.cs
│   ├── ProdutosController.cs
│   └── CategoriasController.cs
├── Data/
│   └── AppDbContext.cs
├── Exceptions/
│   ├── ProdutoNotFoundException.cs
│   └── CategoriaNotFoundException.cs
├── Middleware/
│   └── ErrorHandlingMiddleware.cs
├── Models/
│   ├── Produto.cs
│   ├── Categoria.cs
│   ├── LoginRequest.cs
│   └── DTOs/
│       ├── CriarProdutoDto.cs
│       ├── AtualizarProdutoDto.cs
│       ├── AtualizarCategoriaDto.cs
│       └── PaginacaoDto.cs
├── Services/
│   ├── IProdutoService.cs
│   ├── ProdutoService.cs
│   ├── IAuthService.cs
│   └── AuthService.cs
├── GlobalUsings.cs
└── Program.cs

LojaApi.Tests/
└── Services/
    └── ProdutoServiceTests.cs
```

---

## ⚙️ Rodando localmente

### Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/) ou [Docker](https://www.docker.com/)

### Instalação

```bash
# clone o repositório
git clone https://github.com/seu-usuario/loja-api.git
cd loja-api

# restaura as dependências
dotnet restore

# configura a connection string no appsettings.json
# "DefaultConnection": "Host=localhost;Database=loja;Username=postgres;Password=postgres"

# sobe a API (migrations aplicadas automaticamente)
dotnet run --project LojaApi
```

Acesse `http://localhost:5209/swagger` para explorar a API.

### Rodando os testes

```bash
cd LojaApi.Tests
dotnet test
```

---

## 🔐 Autenticação

A API usa **JWT Bearer Token**. Endpoints de escrita exigem autenticação.

### Login

```http
POST /api/auth/login
Content-Type: application/json

{
  "usuario": "admin",
  "senha": "123456"
}
```

### Usando o token

```http
GET /api/produtos
Authorization: Bearer {seu_token}
```

---

## 📡 Endpoints

### Autenticação

| Método | Rota | Descrição | Auth |
|--------|------|-----------|------|
| POST | `/api/auth/login` | Gera token JWT | ❌ |

### Produtos

| Método | Rota | Descrição | Auth |
|--------|------|-----------|------|
| GET | `/api/produtos` | Lista produtos com paginação | ❌ |
| GET | `/api/produtos/{id}` | Busca produto por ID | ❌ |
| POST | `/api/produtos` | Cria um produto | ✅ |
| PUT | `/api/produtos/{id}` | Atualiza um produto | ✅ |
| DELETE | `/api/produtos/{id}` | Remove um produto | ✅ Admin |

### Categorias

| Método | Rota | Descrição | Auth |
|--------|------|-----------|------|
| GET | `/api/categorias` | Lista todas as categorias | ❌ |
| GET | `/api/categorias/{id}` | Busca categoria por ID | ❌ |
| POST | `/api/categorias` | Cria uma categoria | ✅ |
| PUT | `/api/categorias/{id}` | Atualiza uma categoria | ✅ |
| DELETE | `/api/categorias/{id}` | Remove uma categoria | ✅ Admin |

---

## 📋 Exemplos de uso

### Listar produtos com paginação

```http
GET /api/produtos?pagina=1&tamanhoPagina=10
```

```json
{
  "pagina": 1,
  "totalPaginas": 1,
  "totalItens": 3,
  "itens": [
    {
      "id": 1,
      "nome": "Notebook",
      "preco": 3500.0,
      "marca": "Dell",
      "sku": "SKNOTE11909",
      "categorias": [
        { "id": 1, "nome": "Informática" },
        { "id": 3, "nome": "Notebooks" }
      ]
    }
  ]
}
```

### Criar produto

```http
POST /api/produtos
Authorization: Bearer {token}
Content-Type: application/json

{
  "nome": "Notebook Gamer",
  "marca": "Dell",
  "sku": "SKNOTE11909",
  "preco": 5000.00,
  "categoriaIds": [1, 2]
}
```

### Atualizar produto

```http
PUT /api/produtos/1
Authorization: Bearer {token}
Content-Type: application/json

{
  "nome": "Notebook Gamer Pro",
  "marca": "Dell",
  "preco": 7000.00,
  "categoriaIds": [1]
}
```

---

## 🧪 Testes

11 testes cobrindo os cenários críticos do `ProdutoService`:

```
Total: 11 | Passou: 11 | Falhou: 0 | Duração: ~0.6s
```

| Teste | Cenário |
|-------|---------|
| `GetAll_DeveRetornarTodosProdutos` | Lista produtos corretamente |
| `GetAll_DevePaginarCorretamente` | Paginação retorna página e total corretos |
| `GetAll_SegundaPagina_DeveRetornarItemRestante` | Segunda página retorna itens restantes |
| `GetById_ProdutoExistente_DeveRetornarProduto` | Busca por ID existente incluindo SKU |
| `GetById_ProdutoInexistente_DeveLancarExcecao` | Lança exceção para ID inválido |
| `Create_DeveSalvarERetornarProduto` | Cria e persiste produto com SKU |
| `Update_ProdutoExistente_DeveAtualizarDados` | Atualiza dados corretamente |
| `Update_ProdutoInexistente_DeveLancarExcecao` | Lança exceção para ID inválido |
| `Delete_ProdutoExistente_DeveRemoverProduto` | Remove produto corretamente |
| `Delete_ProdutoInexistente_DeveLancarExcecao` | Lança exceção para ID inválido |

---

## 🏗️ Arquitetura

```
Request → Controller → Service → DbContext → PostgreSQL
                ↑
          Middleware (erros)
          JWT (autenticação)
```

- **Controllers** — recebem e retornam HTTP, sem lógica de negócio
- **Services** — toda a lógica de negócio e acesso ao banco
- **DTOs** — controlam o contrato da API separado do modelo do banco
- **Middleware** — tratamento centralizado de exceções
- **DbContext** — configuração do EF Core e seed de dados

---

## 📄 Licença

MIT
