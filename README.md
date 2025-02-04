# RDR2 API

## 📌 Sobre o Projeto

O **RDR2_API** é uma API desenvolvida em **.NET 9** utilizando **Clean Architecture** e implementando o padrão **CQRS**. O projeto simula um sistema de gerenciamento de personagens e gangues no universo de Red Dead Redemption 2, permitindo operações como criação de personagens, associação a gangues e atribuição de armas.

---

## 🏗 Arquitetura e Design Patterns

### 🔹 Clean Architecture

A estrutura do projeto segue padrões **Clean Architecture**, separando responsabilidades em diferentes camadas:

- **Application**: Contém a lógica de negócios, incluindo handlers de comandos e validações.
- **Domain**: Define as entidades, interfaces de repositório e exceções.
- **Infrastructure**: Implementa os repositórios concretos e comunicação com o banco de dados.
- **API (Apresentação)**: Expõe endpoints para interação com o sistema.

### 🔹 CQRS

O projeto adota o padrão **CQRS**, separando comandos (**Commands**) de consultas (**Queries**):

- **Commands**: Responsáveis por modificar o estado do sistema (ex: adicionar um personagem a uma gangue).
- **Queries**: Responsáveis por recuperar dados sem alterar o estado.

### 🔹 Design Patterns Utilizados

- **Mediator Pattern**: Implementado via **MediatR** para coordenar a comunicação entre handlers e comandos.
- **Repository Pattern**: Abstrai a persistência dos dados, promovendo desacoplamento.
- **Validation Pattern**: Utilizado com **FluentValidation** para validar os comandos e consultas.

---

## 🛠 Tecnologias Utilizadas

- **.NET 9** - Framework principal
- **Entity Framework Core** - ORM para acesso ao banco de dados
- **FluentValidation** - Validação de comandos e requests
- **MediatR** - Implementação de CQRS
- **Moq & xUnit** - Testes unitários e mocks
- **SQL Server** - Banco de dados

## 🧪 Testes
O projeto inclui alguns testes unitários com **xUnit** e **Moq**:
Os testes cobrem:
- Handlers de Comandos (`AddCharacterToGangCommandHandlerTests`)
- Validação de Comandos (`AddCharacterToGangCommandValidatorTests`)
- Repositórios (`CharacterRepositoryTests`)
