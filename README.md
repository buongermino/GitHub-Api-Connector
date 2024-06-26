# GitHub-Api-Connector API

## Overview
Esse repositorio cont�m o projeto "GitHub-Api-Connector", uma API desenvolvida em C# e .NET 8, que possui as seguintes funcionalidades:

- Faz a busca na API do Github dos reposit�rios mais relevantes de 5 linguagens pr�-selecionadas:  "csharp", "java", "javascript", "golang", "kotlin" e em seguida as armazena no banco de dados, caso ainda nao tenham sido salvas
- � capaz de listar todos os reposit�rios salvos no banco de dados, junto com seus respectivos donos
- � capaz de listar um �nico reposit�rio por pelo Id gerado pela aplica��o e exibir seus detalhes

## Executando o projeto
A solu��o depende de uma inst�ncia de banco de dados Micrososft SQL Server para executar em localhost.
Com um servi�o de banco de dados executando, substitua o valor de **DefaultConnection** dentro do arquivo **appsettings.json** pela sua string de conex�o SQL Server:

     "ConnectionStrings": {
       "DefaultConnection": "Data Source=<seu_servidor>;Database=github_repos_db;User ID=<seu_usuario>;Password=<seu_Password>;Trust Server Certificate=True"
     },

Se estiver tudo certo com o banco de dados, ao iniciar, a aplica��o ir� executar e aplicar a migration, criando o banco de dados.

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

O endpoint `GET api/repositories/fetch-and-save` aciona o m�todo action **FetchAndSaveRepositories**:

- n�o recebe nenhum par�metro (excluindo a injec�o de depend�ncia **IFetchAndSaveRepositoriesUseCase**)
- aciona o caso de uso **fetchAndSaveRepositoriesUseCase**, invocando o seu  m�todo **Execute()**
	- este caso de uso declara uma lista das 5 linguagens pr�-selecionadas a serem buscadas na API do GitHub
	- em seguida, invoca o m�todo que faz a chamada a API do GitHub, passando as linguagens como argumento
		- o m�todo itera sobre essa lista de linguagens atrav�s da instru��o foreach, e para cada linguagem, � feita uma requisi��o a API do GitHub, onde foi definido por query param retornar apenas 10 reposit�rios mais relevantes para cada chamada e mapeando a reposta do GitHub para uma lista que ser� utilizada pelo caso de uso. Nesse momento s�o feitas 5 chamadas a API n Github, onde em cada chamada retornam 10 reposit�rios da linguagem indexada. O motivo de fazer esse n�mero de requisi��es � porque o Github n�o aceita mais de uma linguagem como **query param** , apenas uma por vez. O resultado final ser�o 5 json's, contendo 10 linguagens cada.
	- em posse da resposta da API, aqui s�o feitas 2 intera��es encadeadas, pois cada resposta da API retorna um json complexo que possui uma lista de objetos (que s�o os reposit�rios, denominados na API como "*items*"). A primeira intera��o atua na lista da resposta da API, e a segunda itera sobre os items, onde s�o extra�das as informa��es a serem salvas no banco.
		-	na itera��o mais interna, as repostas da API s�o mapeadas para a Entidade GitHubRepo e s�o adicionadas a uma lista de reposit�rios
	-	na sequencia o m�todo ir� iterar sobre essa lista e salvar� no banco de dados os reposit�rios que ainda n�o existem no banco de dados

- O endpoint retorna HTTP Status code 204 - No content

#### 2. ListRepositories

O endpoint `GET api/repositories/all` aciona o m�todo action **FetchAndSaveRepositories**:

- n�o recebe nenhum par�metro (excluindo a injec�o de depend�ncia **IFetchAndSaveRepositoriesUseCase**)
- aciona o caso de uso **fetchAndSaveRepositoriesUseCase**, invocando o seu  m�todo **Execute()**
	- O caso de uso acessa o reposit�rio e traz todas as ocorr�ncias encontradas
	- Em seguida, caso o usu�rio tenha passado um filtro, ser� filtrada pela linguagem
	- Logo ap�s � aplicada a pagina��o
	- e na sequencia o mapeamento da entidade para o DTO atrav�s do AutoMapper
-  O endpoint retorna HTTP Status code 200 - OK com a lista de ocorr�ncias que correspondem ao crit�rio ou HTTP Status code 204 - No content caso n�o tenha ocorr�ncias.

#### 3. ListRepositoryById

O endpoint `GET api/repositories/{id}` aciona o m�todo action **ListRepositoryById**:

- recebe um Guid Id como par�metro (excluindo a injec�o de depend�ncia **IGetGitHubRepositoryByIdUseCase**)
- � verificado se o Id n�o � inv�lido (Empty)
- caso seja, � retornado HTTP Status Code 400 - Bad Request
- aciona o caso de uso **getGitHubRepositoryByIdUseCase**, invocando o seu  m�todo **Execute()**, passando como par�metro o Id recebido 
	- este caso de uso acessa o reposit�rio de reposit�rios do Github e retorna a ocorr�ncia que corresponde ao Id
	- faz um mapeamento para o DTO atrav�s do AutoMapper
- se o reposit�rio n�o for encontrado (null), � retornado um status HTTP Status 204 - no content
- caso seja encontrado, o reposit�rio retorna HTTP Status Code 200 - Ok e o reposit�rio encontrado

## Testes automatizados
A API cont�m alguns testes unit�rios b�sicos que cobrem os cen�rios dos casos de uso 2 e 3