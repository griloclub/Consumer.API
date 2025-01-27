# Consumer API

API REST em .NET 8 para gestão de comandas e mesas de restaurante.

# Requisitos de Instalação
- Docker Desktop
- Cliente Firebird (para gerenciamento do banco) ou Dbeaver (Foi oque utilizei para o gerenciamento de banco de dados)

# Instalação rápida:
1. Clone o repositório
2. Acesse a pasta `.docker`
3. Execute `docker-compose up -d`
4. Configure a conexão Firebird:
```
Host: localhost
Banco: DB.Consumer
Usuário: SYSDBA
Senha: ifoodKey#2025
```
5. Atualize o banco / rodar update na migration:
```bash
cd src
dotnet ef database update --project .\Consumer.Infrastructure\ --startup-project .\Consumer.API\
```
6. Execute a aplicação no Visual Studio ou via `dotnet run`
7. Pronto! A aplicação já está funcionando.

# Passo a passo da instalação:
1. Abrir o CMD ou qualquer outro TERMINAL de sua preferencia.
2. Navegue até a pasta aonde fez o clone do projeto.
3. Entre nas camadas do projeto.

```
PS C:\SuaPasta> cd .\Consumer\
PS C:\SuaPasta\Consumer> dir

Directory: C:\SuaPasta\Consumer

Mode                 LastWriteTime         Length Name
----                 -------------         ------ ----
d-----         00/00/00   0:00 PM                .docker
d-----         00/00/00   0:00 PM                .github
d-----         00/00/00   0:00 PM                src
d-----         00/00/00   0:00 PM                tests
-a----         00/00/00   0:00 PM           2581 .gitattributes
-a----         00/00/00   0:00 PM           6585 .gitignore
-a----         00/00/00   0:00 PM           6459 Consumer.sln
-a----         00/00/00   0:00 PM            656 README.md
```

4. Navegue até a pasta **.docker**
5. Dentro da pasta .docker existira um arquivo do docker-compose.yml para criação do banco de dados em container

```
PS C:\SuaPasta\Consumer\> cd .docker
PS C:\SuaPasta\Consumer\.docker> dir

Directory: C:\SuaPasta\Consumer\.docker>

Mode                 LastWriteTime         Length Name
----                 -------------         ------ ----
-a----         00/00/00   0:00 PM            314 docker-compose.yml
```

6. Ao chegar nessa parte, você deverá verificar se o docker desktop está aberto (ou inicializado em 2° PLano.)
7. Caso esteja tudo certo rode o comando no proximo passo:
8. ```docker-compose up -d```
9. O docker irá fazer as configurações corretamente e criar um container.
10. Inicializar / rodar o container que foi gerado no docker. (Linha de comando ou manualmente).
11. Abrir o **FIREBIRD**, cria uma nova conexão e colocar as configurações do arquivo docker-compose.yml:

```
Password      - ISC_PASSWORD=ifoodKey#2025
Path /Db      - FIREBIRD_DATABASE=DB.Consumer
Username      - FIREBIRD_USER=SYSDBA
```

10. Configurar connection string em `appsettings.json` - **Caso seja necessário!**
11. Executar as **Migrations** da seguinte forma pelo CMD / Terminal:

```
PS C:\SuaPasta\Consumer\> cd src
PS C:\SuaPasta\Consumer\src> dotnet ef database Update --project .\Consumer.Infrastructure\ --startup-project .\Consumer.API\
```

```
dotnet ef database Update --project .\Consumer.Infrastructure\ --startup-project .\Consumer.API\
```

12. Se ocorreu tudo certo até esse passo, então a aplicação já estará pronta para ser executa.
13. No Visual Studio, Aperte a tecla **F5** ou vai até ao botão de Play - https e execute.


## Endpoints da API

## Headers
- Para erros em português utilizar:
{ key: Accept-Language; Value : pt-BR}

- Para erros em inglês não precisa utilizar nada (A aplicação reconhece EN de forma automatica), porém caso queira passar utilizar:
{ key: Accept-Language; Value : en}

### Autenticação
```http
POST /api/login
{
  "username": "string",
  "password": "string"
}
```
Usuários para teste:
```json
{
  "username": "user01", "password": "user123"
  "username": "user02", "password": "user456"
  "username": "user03", "password": "user789"
}
```

### Mesas
```http
GET /api/tables
GET /api/tables/{id}
```

## Funcionalidades
- O sistema fornece endpoints para gerenciar mesas, pedidos e acesso de usuários. 
- Autenticação baseada em JWT
- Gerenciamento de mesas/comandas
- Informações detalhadas dos pedidos

## Endpoints da API

### Autenticação
- POST /api/auth/login - Autenticação de usuário
### Mesas
- GET /api/tables - Listar mesas/comandas abertas
- GET /api/tables/{id} - Obter informações detalhadas da mesa

## Responsabilidades
- **Consumer.API**: Controllers, middlewares e configuração
- **Consumer.Application**: Casos de uso, DTOs e validadores
- **Consumer.Communication**: DTOs de request/response
- **Consumer.Domain**: Entidades e interfaces
- **Consumer.Exception**: Classes de exceção customizadas
- **Consumer.Infrastructure**: Implementações EF Core, migrations
- **Tests**: Testes unitários e de integração

## Status Codes de retorno
- 200: Sucesso
- 400: Erro de validação
- 401: Não autorizado 
- 404: Mesa não encontrada
- 500: Erro interno
