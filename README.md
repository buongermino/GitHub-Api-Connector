# GitHub-Api-Connector API

## Overview
Esse repositorio contém o projeto "GitHub-Api-Connector", uma API desenvolvida em C# e .NET 8, que possui as seguintes funcionalidades:

- Faz a busca na API do Github dos repositórios mais relevantes de 5 linguagens pré-selecionadas:  "csharp", "java", "javascript", "golang", "kotlin" e em seguida as armazena no banco de dados, caso ainda nao tenham sido salvas
- É capaz de listar todos os repositórios salvos no banco de dados, junto com seus respectivos donos
- É capaz de listar um único repositório por pelo Id gerado pela aplicação e exibir seus detalhes

## Executando o projeto
A solução depende de uma instância de banco de dados Micrososft SQL Server para executar em localhost.
Com um serviço de banco de dados executando, substitua o valor de **DefaultConnection** dentro do arquivo **appsettings.json** pela sua string de conexão SQL Server:

     "ConnectionStrings": {
       "DefaultConnection": "Data Source=<seu_servidor>;Database=github_repos_db;User ID=<seu_usuario>;Password=<seu_Password>;Trust Server Certificate=True"
     },

Se estiver tudo certo com o banco de dados, ao iniciar, a aplicação irá executar e aplicar a migration, criando o banco de dados.

# Tecnologias utilizadas

- C# e .NET 8
- Banco de dados: Microsoft SQL Server junto com o ORM Entity Framework Core 8
- Testes automatizados: xUnit, Fluent Assertions, Entity Framework In Memory Database
- Demais ferramentas: Swashbuckle, Automapper

## Endpoints

- GET `api/repositories/fetch-and-save`
- GET `api/repositories/all`
- GET `api/repositories/{id}`

### Controller Action Methods:
#### 1. FetchAndSaveRepositories

O endpoint `GET api/repositories/fetch-and-save` aciona o método action **FetchAndSaveRepositories**:

- não recebe nenhum parâmetro (excluindo a injecão de dependência **IFetchAndSaveRepositoriesUseCase**)
- aciona o caso de uso **fetchAndSaveRepositoriesUseCase**, invocando o seu  método **Execute()**
	- este caso de uso declara uma lista das 5 linguagens pré-selecionadas a serem buscadas na API do GitHub
	- em seguida, invoca o método que faz a chamada a API do GitHub, passando as linguagens como argumento
		- o método itera sobre essa lista de linguagens através da instrução foreach, e para cada linguagem, é feita uma requisição a API do GitHub, onde foi definido por query param retornar apenas 10 repositórios mais relevantes para cada chamada e mapeando a reposta do GitHub para uma lista que será utilizada pelo caso de uso. Nesse momento são feitas 5 chamadas a API n Github, onde em cada chamada retornam 10 repositórios da linguagem indexada. O motivo de fazer esse número de requisições é porque o Github não aceita mais de uma linguagem como **query param** , apenas uma por vez. O resultado final serão 5 json's, contendo 10 linguagens cada.
	- em posse da resposta da API, aqui são feitas 2 interações encadeadas, pois cada resposta da API retorna um json complexo que possui uma lista de objetos (que são os repositórios, denominados na API como "*items*"). A primeira interação atua na lista da resposta da API, e a segunda itera sobre os items, onde são extraídas as informações a serem salvas no banco.
		-	na iteração mais interna, as repostas da API são mapeadas para a Entidade GitHubRepo e são adicionadas a uma lista de repositórios
	-	na sequencia o método irá iterar sobre essa lista e salvará no banco de dados os repositórios que ainda não existem no banco de dados

- O endpoint retorna HTTP Status code 204 - No content

#### 2. ListRepositories

O endpoint `GET api/repositories/all` aciona o método action **FetchAndSaveRepositories**:

- não recebe nenhum parâmetro (excluindo a injecão de dependência **IFetchAndSaveRepositoriesUseCase**)
- aciona o caso de uso **fetchAndSaveRepositoriesUseCase**, invocando o seu  método **Execute()**
	- O caso de uso acessa o repositório e traz todas as ocorrências encontradas
	- Em seguida, caso o usuário tenha passado um filtro, será filtrada pela linguagem
	- Logo após é aplicada a paginação
	- e na sequencia o mapeamento da entidade para o DTO através do AutoMapper
-  O endpoint retorna HTTP Status code 200 - OK com a lista de ocorrências que correspondem ao critério ou HTTP Status code 204 - No content caso não tenha ocorrências.

#### 3. ListRepositoryById

O endpoint `GET api/repositories/{id}` aciona o método action **ListRepositoryById**:

- recebe um Guid Id como parâmetro (excluindo a injecão de dependência **IGetGitHubRepositoryByIdUseCase**)
- é verificado se o Id não é inválido (Empty)
- caso seja, é retornado HTTP Status Code 400 - Bad Request
- aciona o caso de uso **getGitHubRepositoryByIdUseCase**, invocando o seu  método **Execute()**, passando como parâmetro o Id recebido 
	- este caso de uso acessa o repositório de repositórios do Github e retorna a ocorrência que corresponde ao Id
	- faz um mapeamento para o DTO através do AutoMapper
- se o repositório não for encontrado (null), é retornado um status HTTP Status 204 - no content
- caso seja encontrado, o repositório retorna HTTP Status Code 200 - Ok e o repositório encontrado

## Testes automatizados
A API contém alguns testes unitários básicos que cobrem os cenários dos casos de uso 2 e 3