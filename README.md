# Microsserviço de Frota de Veículos e Corporações (CAD - Corpo de Bombeiros)
Este microsserviço é responsável pela gestão e monitoramento em tempo real da
frota operacional de viaturas especializadas, pelo controle de seus status
operacionais, insumos críticos e pelo gerenciamento histórico de
escalas/guarnições por turnos de serviço. Ele compõe o ecossistema do CAD
(Computer-Aided Dispatch), fornecendo dados cruciais de disponibilidade de
recursos para os módulos de despacho de ocorrências.
---
## Tecnologias Utilizadas
* **Linguagem:** C#
* **Framework Web:** ASP.NET Core Web API (.NET 8.0)
* **Mapeamento Objeto-Relacional (ORM):** Entity Framework Core (EF Core)
* **Banco de Dados:** SQLite (Armazenamento local leve via arquivo .db)
* **Documentação e Testes:** Scalar API Reference (Interface moderna)
---
## Arquitetura do Sistema
O projeto adota uma arquitetura em camadas bem definida, facilitando a
manutenção, isolamento de regras de negócio e escalabilidade:
1. **Models (Entidades):** Representação das tabelas e relacionamentos do banco
de dados (Corporação, Viatura, Bombeiro, EscalaTurno).
2. **Services (Camada de Negócio):** Onde residem as regras operacionais (ex:
lógica de encerramento automático de turnos anteriores ao alocar uma nova
guarnição).
3. **Controllers (API / Portas de Entrada):** Exposição dos endpoints HTTP
RESTful para comunicação interna e externa.
4. **DTOs (Data Transfer Objects):** Objetos otimizados para o transporte seguro
de dados nas requisições.
5. **Infra / DataContext:** Configurações estruturais e conexão direta com a
engine do banco de dados.
---
## Requisitos para Execução

Antes de iniciar o microsserviço, certifique-se de possuir o ambiente configurado
em sua máquina:
* **IDE:** Visual Studio 2022 (ou superior) com a carga de trabalho
"Desenvolvimento Web e ASP.NET" marcada no instalador.
* **SDK e Runtimes:** .NET 8.0 SDK, .NET Runtime 8.0 e ASP.NET Core Runtime 8.0
instalados.
* **EF Core CLI:** Ferramentas de linha de comando do Entity Framework instaladas
globalmente. Caso não possua, instale via prompt de comando: `dotnet tool install
--global dotnet-ef`.
* *(Opcional)* **DB Browser for SQLite:** Excelente ferramenta visual para
auditar as tabelas localmente.
---
## Como Executar o Projeto
### 1. Preparação do Banco de Dados
Com o terminal aberto na raiz da pasta que contém o arquivo `Template.csproj`,
execute o comando abaixo para aplicar as migrações estruturais e gerar o arquivo
de banco de dados físico local:
dotnet ef database update
**Nota:** Isso criará o arquivo `.db` e acionará o **Data Seeding**, populando
automaticamente o banco com uma corporação base localizada em Içara, uma equipe
inicial de bombeiros e viaturas prontas para uso.
### 2. Inicialização
1. Abra o arquivo de solução `.sln` no seu Visual Studio.
2. Garanta que a visualização está no Modo de Solução (Gerenciador de Soluções).
3. Na barra superior, selecione o perfil de execução "http".
4. Pressione **F5** ou clique no botão Play.
5. O console subirá em ambiente de desenvolvimento ouvindo a porta local
`http://localhost:5089`. O navegador abrirá automaticamente a interface do
Scalar.
---
## Principais Endpoints da API
### Módulo de Corporações (Integração com Mapas)
* `GET /api/Corporacao`: Retorna os quartéis, suas coordenadas geográficas e a
lista aninhada da frota e guarnição atual.
* `POST /api/Corporacao`: Cadastra novas bases físicas.

### Módulo de Bombeiros
* `POST /api/Bombeiro`: Cadastra um militar no banco de dados (Nome, Função
operacional e Matrícula).
* `GET /api/Bombeiro`: Lista todos os bombeiros cadastrados no sistema.
### Módulo de Viaturas
* `POST /api/Viatura`: Cadastra uma nova viatura na frota (Tipo, Placa,
Identificador, Especificações e Insumos).
* `GET /api/Viatura`: Lista a frota completa cadastrada e seus status
operacionais.
* `PATCH /api/Viatura/{id}/status`: Altera manualmente o status da viatura (0 =
Disponível, 1 = Em Deslocamento, etc).
* `POST /api/Viatura/{id}/iniciar-turno`: Inicia formalmente um turno, vinculando
a lista de IDs dos bombeiros alocados.
---
## Fluxo de Teste (Motor de Regras de Negócio)
Para testar a inteligência do microsserviço e a integridade do histórico
auditável de turnos:
1. Acesse `POST /api/Viatura/{id}/iniciar-turno`.
2. Insira o ID da viatura na URL e envie os IDs dos bombeiros no corpo da
requisição.
**O que o microsserviço fará nos bastidores:**
* Buscará a viatura correspondente.
* Localizará se havia algum bombeiro com escala aberta anteriormente nesta
viatura e cravará a data/hora de término, encerrando o turno anterior.
* Criará novos registros na tabela mapeando os novos bombeiros.
* Atualizará automaticamente o status da viatura para `0 - DisponivelNaBase`,
deixando-a pronta para ser despachada.
---
*Autor: Renan Matias Zanini*
