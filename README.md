Store API — Gerenciamento de Pedidos de Loja

📖 Descrição

A Store API é uma aplicação ASP.NET Core Web API desenvolvida para gerenciar pedidos de uma loja.
Ela permite criar, editar e fechar pedidos, adicionando e removendo produtos, com persistência via Entity Framework Core (InMemory).

O projeto segue os princípios de DDD (Domain-Driven Design), com separação clara de responsabilidades em camadas.

StoreTest/
 ├── Store.Domain/           # Entidades e regras de negócio
 ├── Store.Infrastructure/   # Persistência e repositórios (EF Core)
 ├── Store.Application/      # DTOs e serviços (lógica de aplicação)
 ├── Store.Api/              # API (Controllers, Startup, Swagger)

⚙️ Funcionalidades Principais
Funcionalidade	Descrição
🆕 Criar pedido	Inicia um novo pedido
➕ Adicionar produto	Adiciona itens a um pedido aberto
➖ Remover produto	Remove itens de um pedido
✅ Fechar pedido	Fecha um pedido (não pode mais ser alterado)
📋 Listar pedidos	Retorna todos os pedidos (com paginação e filtro por status)
🔍 Obter pedido	Busca um pedido pelo ID
🔒 Regras de Negócio

Produtos não podem ser adicionados ou removidos em pedidos fechados.

Um pedido só pode ser fechado se possuir ao menos um produto.

🧠 Conceitos Aplicados

DDD (Domain-Driven Design):

Camadas: Domain, Infrastructure, Application, API.

Regras de negócio isoladas na camada Domain.

Injeção de Dependência: serviços e repositórios injetados via construtor.

Repository Pattern para abstrair o acesso a dados.

DTOs (Data Transfer Objects) para comunicação entre camadas.

Swagger/OpenAPI para documentação e testes interativos.

🧰 Endpoints Principais
Método	Rota	Descrição
POST	/api/orders/start	Inicia um novo pedido
POST	/api/orders/{orderId}/items	Adiciona item ao pedido
DELETE	/api/orders/{orderId}/items/{itemId}	Remove item do pedido
PUT	/api/orders/{orderId}/close	Fecha o pedido
GET	/api/orders	Lista pedidos (com paginação e filtro por status)
GET	/api/orders/{id}	Obtém um pedido por ID

🧪 Testando a API (Swagger)

Rode o projeto:

dotnet run --project Store.Api


Acesse no navegador:

http://localhost:5167/swagger


Teste os endpoints diretamente pela interface Swagger.

🧾 Exemplo de Fluxo no Swagger

1️⃣ Iniciar Pedido

POST /api/orders/start


2️⃣ Adicionar Produto

POST /api/orders/{orderId}/items
{
  "productId": "a6cdd6c3-...-f9d3",
  "productName": "Teclado Gamer",
  "quantity": 2,
  "unitPrice": 250.00
}


3️⃣ Fechar Pedido

PUT /api/orders/{orderId}/close


4️⃣ Listar Todos os Pedidos

GET /api/orders

🧩 Filtros e Paginação

A rota GET /api/orders aceita parâmetros opcionais:

Parâmetro	Tipo	Exemplo	Descrição
status	string	open ou closed	Filtra pedidos por status
page	int	1	Página atual
pageSize	int	10	Quantidade de registros por página
🧱 Banco de Dados

O projeto utiliza o Entity Framework Core InMemory, o que dispensa instalação de banco externo.
Ao reiniciar a aplicação, os dados são recriados automaticamente.

🧰 Como Rodar o Projeto

1️⃣ Clone o repositório:

git clone https://github.com/seuusuario/store-api.git


2️⃣ Entre na pasta:

cd store-api


3️⃣ Restaure os pacotes:

dotnet restore


4️⃣ Rode o projeto:

dotnet run --project Store.Api


5️⃣ Acesse o Swagger:

http://localhost:5167/swagger

🧪 (Opcional) Rodando Testes Unitários

Se tiver criado a camada de testes:

dotnet test
