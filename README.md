# Dev Links API

![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-8.0-blue)
![JWT](https://img.shields.io/badge/JWT-Authentication-yellow)
![License](https://img.shields.io/badge/license-MIT-green.svg)

![Dev Links - Tela Inicial](https://drive.google.com/uc?export=view&id=17ko4Nypv84BEXmt5cy7vg5tSMGPeuO9E)

## 📌 Descrição do Projeto

A **Dev Links API** fornece a funcionalidade de gerenciamento de links para a aplicação [Dev Links](https://github.com/gabrielfelipeee/devlinks), permitindo que usuários se registrem, façam login e editem/excluam seus links personalizados.

### Funcionalidades

- **Autenticação JWT**: Protege as rotas da API com autenticação baseada em token JWT.
- **Gerenciamento de Links**: Os usuários podem adicionar, editar e remover links associados às suas contas.
- **Cadastro de Usuário**: Os usuários podem criar contas para gerenciar seu portfólio de links.
- **Login de Usuário**: Autenticação via email e senha, com retorno de um token JWT para acesso à API.
- **Perfil de Usuário**: Cada usuário possui um perfil único onde pode personalizar seus links.

## 🚀 Tecnologias Utilizadas

Esta API foi desenvolvida com as seguintes tecnologias:

- **ASP.NET Core 8**  
- **Entity Framework Core**  
- **JWT Authentication**  
- **MySQL** (Banco de Dados)

## 🌍 Acesso ao Projeto

Para rodar localmente, siga os passos abaixo:

1️⃣ Clone o repositório da API:
```bash
git clone https://github.com/gabrielfelipeee/Devlinks.Api.git
cd Devlinks.Api/src
```
2️⃣ Instale as dependências:
```bash
dotnet restore
```
3️⃣ Configure a string de conexão no arquivo appsettings.json:
```bash
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Port=3306;Database=[node_do_banco];Uid=root;Pwd=[sua_senha]"
}
```
4️⃣ Execute a API:
```bash
cd Api.Application
dotnet run
```

## Desenvolvedor

- [Gabriel Felipe](https://www.linkedin.com/in/gabrielfelipeee/)

## Pessoas Contribuidoras

💡 Quer contribuir? Fique à vontade para abrir um Pull Request!

## Licença

Este projeto está licenciado sob a **MIT License** - veja o arquivo [LICENSE](LICENSE) para mais detalhes.
