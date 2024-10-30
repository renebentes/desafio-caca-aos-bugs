![balta](https://baltaio.blob.core.windows.net/static/images/dark/balta-logo.svg) <!-- markdownlint-disable-line first-line-heading-->

## 🎖️ Desafio

**Caça aos Bugs 2024** é a sexta edição dos **Desafios .NET** realizados pelo [balta.io](https://balta.io). Durante esta jornada, fizemos parte da equipe **NOME_DA_BANDA** onde resolvemos todos os bugs de uma aplicação e aplicamos testes de unidade no projeto.

## 📱 Projeto

Depuração e solução de bugs, pensamento crítico e analítico, segurança e qualidade de software aplicando testes de unidade.

## Participantes

### 🚀 Líder Técnico

Rodolfo de Jesus Silva - [lrodolfol](https://github.com/lrodolfol)

### 👻 Caçadores de Bugs

- Fabricio Carvalho - [FabricioCarvalho348](https://github.com/FabricioCarvalho348)
- Rene Bentes Pinto - [renebentes](https://github.com/renebentes)
- Rodolfo de Jesus Silva - [lrodolfol](https://github.com/lrodolfol)

## ⚙️ Tecnologias

- C# 12
- .NET 8
- ASP.NET
- Minimal APIs
- Blazor Web Assembly
- xUnit

## 🥋 Skills Desenvolvidas

- Comunicação
- Trabalho em Equipe
- Networking
- Muito conhecimento técnico

## 🧪 Como testar o projeto

Neste repositório existem duas soluções de projetos .NET 8, desta forma você precisa ter instalado essa versão previamente. Caso não a possua instalada, siga os passos descritos na [documentação](https://dot.net).

A seguir, abra um terminal e clone o repositório:

```bash
git clone https://github.com/renebentes/desafio-caca-aos-bugs.git
```

### Bugs

Neste projeto, precisamos do [Microsoft Sql Server](https://www.microsoft.com/sql-server/sql-server-downloads) instalado e rodando. Você pode instalá-lo diretamente ou utilizar via [Docker](https://www.docker.com/get-started/).

A partir deste momento, assumiremos que você optou por usar Docker.

- Levante o serviço do banco de dados:

```bash
docker run --name sqlserver -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=1q2w3e4r@#$" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
```

- Instale globalmente a ferramenta de [linha de comando](https://learn.microsoft.com/ef/core/cli/dotnet) do [Entity Framework Core](https://learn.microsoft.com/ef/core/):

```bash
dotnet tool install --global dotnet-ef
```

- Acesse o diretório raiz da solução:

```bash
cd desafio-caca-aos-bugs/bugs
```

- Aplique as migrações:

```bash
dotnet ef database update --project ./Dima.Api/Dima.Api.csproj
```

- A partir de um cliente para gerenciar o banco de dado, como o [Azure Data Studio](https://learn.microsoft.com/azure-data-studio/download-azure-data-studio), execute os scripts disponíveis nos diretórios **Dima.Api/Data/Views** e **Dima.Api/Data/Scripts**.

- Execute os projetos:

> **NOTE**
>
> Abra um terminal para cada comando

```bash
dotnet run --project Dima.Api
```

```bash
dotnet run --project Dima.Web
```

Por fim, acesse o site no endereço http://localhost:5028 no seu navegador preferido. Se desejar, no endereço http://localhost:5164/swagger está disposta a documentação da API.

### Unit Tests

- Volte a raiz do repositório clonado anteriormente e acesse a pasta da solução:

```bash
cd unit-tests
```

- Para rodar os teste, execute:

```bash
dotnet test Balta.sln
```

# 💜 Participe

Quer participar dos próximos desafios? Junte-se a [maior comunidade .NET do Brasil 🇧🇷 💜](https://balta.io/discord)
