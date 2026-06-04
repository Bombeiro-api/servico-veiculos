# MICROSSERVICO DE FROTA DE VEICULOS E VIATURAS (MODULO 3)
## Sistema de Controle e Despacho de Ocorrencias (CAD - Corpo de Bombeiros)

Este microsservico e responsavel pela gestao e monitoramento em tempo real da frota operacional de viaturas especializadas, pelo controle de seus status operacionais, insumos criticos e pelo gerenciamento historico de escalas/guarnicoes por turnos de servico. Ele compoe o ecossistema do CAD (Computer-Aided Dispatch), fornecendo dados cruciais de disponibilidade de recursos para os modulos de despacho de ocorrencias.

-------------------------------------------------------------------------------
1. TECNOLOGIAS UTILIZADAS
-------------------------------------------------------------------------------
* Linguagem: C#
* Framework Web: ASP.NET Core Web API (.NET 8.0)
* Mapeamento Objeto-Relacional (ORM): Entity Framework Core (EF Core)
* Banco de Dados: SQLite (Armazenamento local leve via arquivo .db)
* Documentacao e Testes: Swagger / OpenAPI

-------------------------------------------------------------------------------
2. ARQUITETURA DO SISTEMA
-------------------------------------------------------------------------------
O projeto adota uma arquitetura em camadas bem definida, facilitando a manutencao, isolamento de regras de negocio e escalabilidade:

1. Models (Entidades): Representacao das tabelas e relacionamentos do banco de dados (Viatura, Bombeiro, EscalaTurno).
2. Services (Camada de Negocio): Onde residem as regras operacionais (ex: logica de encerramento automatico de turnos anteriores ao alocar uma nova guarnicao).
3. Controllers (API / Portas de Entrada): Exposicao dos endpoints HTTP RESTful para comunicacao interna e externa (ViaturaController, BombeiroController).
4. DTOs (Data Transfer Objects): Objetos otimizados para o transporte seguro de dados nas requisicoes (AlocarGuarnicaoDTO).
5. Infra / DataContext: Configuracoes estruturais e conexao direta com a engine do banco de dados.

-------------------------------------------------------------------------------
3. REQUISITOS E PRE-REQUISITOS PARA EXECUCAO
-------------------------------------------------------------------------------
Antes de iniciar o microsservico, certifique-se de possuir o seguinte ambiente configurado em sua maquina:

1. IDE: Visual Studio 2022 (ou superior) com a carga de trabalho "Desenvolvimento Web e ASP.NET" marcada no instalador.
2. SDK: .NET 8.0 SDK instalado.
3. Runtimes: .NET Runtime 8.0 e ASP.NET Core Runtime 8.0 instalados.
4. EF Core CLI: Ferramentas de linha de comando do Entity Framework instaladas globalmente. Caso nao possua, instale via prompt de comando:
   dotnet tool install --global dotnet-ef
5. (Opcional) DB Browser for SQLite: Excelente ferramenta visual para abrir o arquivo Nome_banco.db e auditar as tabelas localmente.

-------------------------------------------------------------------------------
4. COMO EXECUTAR O PROJETO
-------------------------------------------------------------------------------
### 4.1. Preparacao do Banco de Dados (Migrations)
Com o terminal (PowerShell ou CMD) aberto na raiz da pasta que comtem o arquivo Template.csproj, execute o comando abaixo para aplicar as migracoes estruturais e gerar o arquivo de banco de dados fisico local:

comando: dotnet ef database update

Nota: Isso criara o arquivo Nome_banco.db com as tabelas de Viaturas, Bombeiros e Escalas prontas.

### 4.2. Inicializacao pelo Visual Studio
1. Abra o arquivo de solucao .sln (AppTemplate.sln) no seu Visual Studio.
2. Garanta que a visualizacao esta no Modo de Solucao (Gerenciador de Solucoes) e nao no modo de exibicao de pastas soltas.
3. Na barra superior, ao lado do botao verde de Play, selecione o perfil de execucao "http".
4. Pressione F5 ou clique no botao Play.
5. O console subira em ambiente de desenvolvimento (ASPNETCORE_ENVIRONMENT: Development) ouvindo a porta local http://localhost:5089/swagger. O navegador abrira automaticamente a interface do Swagger.

-------------------------------------------------------------------------------
5. ENDPOINTS DA API (DOCUMENTACAO)
-------------------------------------------------------------------------------
### 5.1. Modulo de Bombeiros (BombeiroController)
* POST /api/Bombeiro
  Cadastra um militar no banco de dados (Nome, Funcao operacional e Matricula).
* GET /api/Bombeiro
  Lista todos os bombeiros cadastrados no sistema.

### 5.2. Modulo de Viaturas (ViaturaController)
* POST /api/Viatura
  Cadastra uma nova viatura na frota (Tipo ex: ABTR/ASU, Placa, Identificador de Radio, Especificacoes Tecnicas e Insumos iniciais). (RF08)
* GET /api/Viatura
  Lista a frota completa cadastrada e seus status.
* PATCH /api/Viatura/{id}/status
  Altera manualmente o status operacional da viatura (ex: 0 = Disponivel na Base, 1 = Em Deslocamento, 2 = No Local, 3 = Em Manutencao). (RF09)
* POST /api/Viatura/{id}/iniciar-turno
  Inicia formalmente um turno operacional para a viatura informada, vinculando a lista de IDs dos bombeiros alocados. (RF10)

-------------------------------------------------------------------------------
6. FLUXO DE UTILIZACAO RECOMENDADO (CENARIO DE TESTE COMPLETO)
-------------------------------------------------------------------------------
Para testar o comportamento correto do microsservico de ponta a ponta e garantir a integridade do historico auditavel de turnos, execute os seguintes passos no painel do Swagger:

1. Cadastrar os Militares:
   Acesse o POST /api/Bombeiro e envie o payload para criar o motorista e o comandante do turno:
   JSON de exemplo:
   {
     "nome": "Sargento Joao",
     "funcao": "Motorista",
     "matricula": "BM-12345"
   }
   Anote o ID gerado na resposta do servidor (ex: 1). Repita para criar outros militares.

2. Cadastrar a Viatura:
   Acesse o POST /api/Viatura e insira as informacoes de um veiculo (iniciando propositalmente com status 3 - Em Manutencao):
   JSON de exemplo:
   {
     "tipo": "ABTR",
     "placa": "XYZ-9876",
     "identificadorRadio": "Alfa-1",
     "especificacoesTecnicas": "Caminhao tanque com capacidade para 5000L",
     "status": 3,
     "nivelTanqueAgua": 100,
     "cilindrosOxigenioCheios": 4,
     "nivelCombustivel": 100
   }
   Anote o ID gerado para a viatura (ex: 1).

3. Alocacao de Guarnicao e Abertura de Turno:
   Acesse o endpoint POST /api/Viatura/{id}/iniciar-turno. No campo id insira o ID da viatura (1). No corpo da requisicao, envie a lista de IDs dos bombeiros que assumirao o veiculo:
   JSON de exemplo:
   {
     "bombeirosIds": [1, 2]
   }

O que o microsservico fara nos bastidores:
* Buscara a viatura correspondente.
* Localizara se havia algum bombeiro com escala aberta anteriormente nesta viatura e cravara a data/hora de termino (DataFim = DateTime.Now) encerrando o turno anterior de forma auditavel.
* Criara novos registros na tabela Escalas mapeando os novos bombeiros com data de inicio.
* Atualizara automaticamente o status da viatura para 0 - DisponivelNaBase, deixando-a pronta para ser despachada pela central de operacoes.