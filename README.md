# Lyra AI Management App

![Logo](/docs/images/logo-200px.png)

📌 *Nota: Projeto desenvolvido para fins acadêmicos na disciplina de Advanced Business Development with .NET*

O Lyra AI Management é o serviço em .NET 9 Minimal API responsável por **gerenciar a comunicação com a camada de Inteligência Artificial** utilizada pelo aplicativo móvel Lyra.

Ele centraliza o recebimento dos resumos enviados pelo backend Java, consulta o modelo de IA para gerar recomendações ao usuário e registra todos os eventos em banco de dados para posterior análise.

Este serviço foi desenvolvido para ser leve, escalável e facilmente integrável, permitindo que outras aplicações consumam as funcionalidades da IA de forma simples e organizada.

## Índice
- [Integrantes](#integrantes)
- [Justificativa da Arquitetura](#justificativa-da-arquitetura)
- [Funcionalidades](#funcionalidades)
- [Como Rodar o projeto](#como-rodar-o-projeto)
- [Efetuando Testes no Sistema](#efetuando-testes-no-sistema)

## Integrantes
| Turma |    RM    |     Nome Completo     |
|:------|:--------:|:---------------------:|
| 2TDSB | RM559123 | Caroline de Oliveira  |
| 2TDSB | RM554473 | Giulia Corrêa Camillo |
| 2TDSB | RM555679 | Lavinia Soo Hyun Park |

## Justificativa da Arquitetura

A aplicação Lyra AI Management foi construída utilizando uma estrutura inspirada na Clean Architecture, priorizando baixo acoplamento, alta coesão e separação clara de responsabilidades.

O objetivo principal é garantir que os serviços de IA, registro de logs e comunicação com sistemas externos possam evoluir de forma independente, mantendo simplicidade e escalabilidade.

A solução foi organizada nas seguintes camadas:

### 🔹 API Layer (Endpoints/)
Responsável por expor os endpoints HTTP, agrupados por recurso (IA, Logs, Health, etc.).

Nessa camada ficam as:
- Validações básicas de entrada
- Rotas divididas por versão (v1, v2)
- Composição das respostas (incluindo Paginaçao e HATEOAS nas respostas)

### 🔹 Domain Layer (Models/)
Define as entidades principais do serviço:
- AiLog
- DTOs de entrada e saída
- Estruturas simples utilizadas para transporte e padronização dos dados

Essa camada não depende de infraestrutura e mantém apenas regras mínimas de consistência.

### 🔹 Infrastructure Layer (Infrastructure/)
Centraliza tudo que é externo ou de baixo nível:
- Configurações do Entity Framework Core
- Connection string e injeção de dependência do DbContext
- Paginação, HATEOAS e utilitários auxiliares
- Configuração de health checks, versionamento e Scalar
- Configurações de integrações externas, como OpenTelemetry e o serviço de IA (Gemini)

Essa camada mantém o `Program.cs` limpo e organizado, delegando responsabilidades.

### 🔹 Test Layer (Tests/)
Projeto separado utilizando xUnit + WebApplicationFactory, garantindo:
- Testes reais dos endpoints
- Validação do fluxo da IA
- Verificação da estrutura JSON retornada

> 🔍 Observação Importante
> A aplicação adota um design minimalista, apropriado para serviços de backend que fazem mediação entre sistemas.
>
> Validações e regras simples são tratadas diretamente nos endpoints, enquanto a infraestrutura concentra capacidades transversais como logs, versionamento e documentação.
> 
> Essa estrutura reduz complexidade, evita sobrecarga desnecessária e mantém o sistema fácil de evoluir.

O diagrama abaixo complementa essa estrutura, apresentando como a API .NET se integra ao fluxo completo da solução e interage com o backend Java, o serviço de IA e o banco de dados:

![Diagrama](/docs/images/diagrama-dotnet.png)

## Funcionalidades

### 🔹 1. Endpoint de IA
- Recebe resumo enviado pelo backend Java
- Consulta o modelo Gemini (```/api/v1/ai/solicitar```)
- Gera recomendação personalizada
- Devolve resposta em JSON para o Java
### 🔹 2. Registro de Logs (AI Logs)
- Salva o histórico no Oracle (resumo, recomendação, nível, sucesso)
- Auditoria completa de cada chamada
- Suporte a paginação
- HATEOAS para navegação entre páginas
### 🔹 3. Versionamento de API
- Suporte às versões v1 e v2
- A v2 foi mantida para futuras melhorias e compatibilidade
- Permite evoluir sem quebrar integrações
### 🔹 4. Health Checks
- Verifica conexão com o Oracle
- Atualiza seu estado a cada 60 segundos
- Interface visual via HealthChecks UI (```/health-ui```)
### 🔹 5. Scalar
- Endpoints documentados automaticamente via Scalar
- Inclui exemplos de requisição e resposta
- Exibe tipos de retorno, parâmetros e detalhes adicionais de cada método
### 🔹 6. Logging & Tracing
- Uso do ILogger para registrar eventos importantes durante o fluxo das requisições
- Integração com OpenTelemetry, permitindo rastreamento detalhado e visibilidade do comportamento da aplicação

## Deploy da API

# Lyra AI Management App

<p align="center">
  <img src="/docs/images/Logo-Com-Nome.png" width="200"/>
</p>

📌 *Nota: Projeto desenvolvido para fins acadêmicos na disciplina de Advanced Business Development with .NET*

O Lyra AI Management é o serviço em .NET 9 Minimal API responsável por **gerenciar a comunicação com a camada de Inteligência Artificial** utilizada pelo aplicativo móvel Lyra.

Ele centraliza o recebimento dos resumos enviados pelo backend Java, consulta o modelo de IA para gerar recomendações ao usuário e registra todos os eventos em banco de dados para posterior análise.

Este serviço foi desenvolvido para ser leve, escalável e facilmente integrável, permitindo que outras aplicações consumam as funcionalidades da IA de forma simples e organizada.

## Índice
- [Integrantes](#integrantes)
- [Justificativa da Arquitetura](#justificativa-da-arquitetura)
- [Funcionalidades](#funcionalidades)
- [Deploy da API](#deploy-da-api)
- [Como Rodar o projeto](#como-rodar-o-projeto)
- [Efetuando Testes no Sistema](#efetuando-testes-no-sistema)

## Integrantes
| Turma |    RM    |     Nome Completo     |
|:------|:--------:|:---------------------:|
| 2TDSB | RM559123 | Caroline de Oliveira  |
| 2TDSB | RM554473 | Giulia Corrêa Camillo |
| 2TDSB | RM555679 | Lavinia Soo Hyun Park |

## Justificativa da Arquitetura

A aplicação Lyra AI Management foi construída utilizando uma estrutura inspirada na Clean Architecture, priorizando baixo acoplamento, alta coesão e separação clara de responsabilidades.

O objetivo principal é garantir que os serviços de IA, registro de logs e comunicação com sistemas externos possam evoluir de forma independente, mantendo simplicidade e escalabilidade.

A solução foi organizada nas seguintes camadas:

### 🔹 API Layer (Endpoints/)
Responsável por expor os endpoints HTTP, agrupados por recurso (IA, Logs, Health, etc.).

Nessa camada ficam as:
- Validações básicas de entrada
- Rotas divididas por versão (v1, v2)
- Composição das respostas (incluindo Paginaçao e HATEOAS nas respostas)

### 🔹 Domain Layer (Models/)
Define as entidades principais do serviço:
- AiLog
- DTOs de entrada e saída
- Estruturas simples utilizadas para transporte e padronização dos dados

Essa camada não depende de infraestrutura e mantém apenas regras mínimas de consistência.

### 🔹 Infrastructure Layer (Infrastructure/)
Centraliza tudo que é externo ou de baixo nível:
- Configurações do Entity Framework Core
- Connection string e injeção de dependência do DbContext
- Paginação, HATEOAS e utilitários auxiliares
- Configuração de health checks, versionamento e Scalar
- Configurações de integrações externas, como OpenTelemetry e o serviço de IA (Gemini)

Essa camada mantém o `Program.cs` limpo e organizado, delegando responsabilidades.

### 🔹 Test Layer (Tests/)
Projeto separado utilizando xUnit + WebApplicationFactory, garantindo:
- Testes reais dos endpoints
- Validação do fluxo da IA
- Verificação da estrutura JSON retornada

> 🔍 Observação Importante
> A aplicação adota um design minimalista, apropriado para serviços de backend que fazem mediação entre sistemas.
>
> Validações e regras simples são tratadas diretamente nos endpoints, enquanto a infraestrutura concentra capacidades transversais como logs, versionamento e documentação.
>
> Essa estrutura reduz complexidade, evita sobrecarga desnecessária e mantém o sistema fácil de evoluir.

O diagrama abaixo complementa essa estrutura, apresentando como a API .NET se integra ao fluxo completo da solução e interage com o backend Java, o serviço de IA e o banco de dados:

![Diagrama](/docs/images/diagrama-dotnet.png)

## Funcionalidades

### 🔹 1. Endpoint de IA
- Recebe resumo enviado pelo backend Java
- Consulta o modelo Gemini (```/api/v1/ai/solicitar```)
- Gera recomendação personalizada
- Devolve resposta em JSON para o Java
### 🔹 2. Registro de Logs (AI Logs)
- Salva o histórico no Oracle (resumo, recomendação, nível, sucesso)
- Auditoria completa de cada chamada
- Suporte a paginação
- HATEOAS para navegação entre páginas
### 🔹 3. Versionamento de API
- Suporte às versões v1 e v2
- A v2 foi mantida para futuras melhorias e compatibilidade
- Permite evoluir sem quebrar integrações
### 🔹 4. Health Checks
- Verifica conexão com o Oracle
- Atualiza seu estado a cada 60 segundos
- Interface visual via HealthChecks UI (```/health-ui```)
### 🔹 5. Scalar
- Endpoints documentados automaticamente via Scalar
- Inclui exemplos de requisição e resposta
- Exibe tipos de retorno, parâmetros e detalhes adicionais de cada método
### 🔹 6. Logging & Tracing
- Uso do ILogger para registrar eventos importantes durante o fluxo das requisições
- Integração com OpenTelemetry, permitindo rastreamento detalhado e visibilidade do comportamento da aplicação

## Deploy da API

A aplicação foi deployada com sucesso na Azure App Service e está disponível para consulta dos endpoints principais — como prova de integração entre os serviços do projeto.

| Tipo                   |                    Endereço                     |
|:-----------------------|:-----------------------------------------------:|
| WebApp (.NET)          |    https://cglgs2025webapp.azurewebsites.net    |
| Health Simples (ativo) |https://cglgs2025webapp.azurewebsites.net/health |

> Caso queira subir a aplicação manualmente no Azure Portal via CLI, acesse a pasta scripts e siga o passo a passo descrito no arquivo

📌 Nota:
O deploy foi utilizado para demonstrar a viabilidade real de hospedagem da API e sua capacidade de integração com o backend Java.

Todos os testes funcionais do sistema — incluindo IA, Oracle e testes unitários — foram executados em ambiente local, garantindo controle total das dependências.

## Como Rodar o Projeto

A aplicação está pronta para deploy, e um WebApp foi criado na Azure para fins de integração com outros serviços.

No entanto, optamos por não manter o deploy ativo nesta fase, garantindo custo zero em cloud e priorizando os testes locais de integração com o backend Java e a IA.

> ⚠️ **Importante:**  
> Clone este repositório antes de tudo!
> ```bash
> git clone https://github.com/laviniapark/FIAP-2025-GS-CGL-ABD.git
> ```
> Escolha a pasta desejada e abra o projeto na sua IDE de preferência
---
### 📜 1. Requisitos

| Ferramenta | Descrição | Download |
|-------------|------------|-----------|
|**.NET SDK 9.0** | Framework necessário para compilar e executar o projeto | [Baixar .NET SDK](https://dotnet.microsoft.com/en-us/download) |
|**Oracle XE** | Banco de dados local (ou utilize o da instituição) | [Baixar Oracle XE](https://www.oracle.com/database/technologies/appdev/xe.html) |
|**IDE** | Recomendado: Visual Studio, Rider ou VS Code | — |
|**API Client** | Testes realizados com **Insomnia**, mas funciona também no **Postman** ou outro de sua preferência | — |
---
### 🗄️ 2. Configuração da conexão com o Banco de Dados

No arquivo `appsettings.json`, configure sua conexão Oracle:

```
"ConnectionStrings": {
    "DefaultConnection": "Data Source=[ORACLE-URL]:1521/[ORACLE-HOST];User Id=[ORACLE-USER];Password=[ORACLE-PASSWORD]"
  }
```

> Substitua os valores entre colchetes `[ ]` conforme suas credenciais Oracle

### 🤖 3. Configuração do Gemini AI

1. Acesse https://aistudio.google.com/api-keys
2. Clique em **Criar chave de API**
3. Dar um Nome a Chave: AiManagementApp (ou outro de sua preferência)
4. Select a Cloud Project > Criar Projeto
5. Escolha um nome para seu projeto: LyraAi (ou outro de sua preferência) > Criar Projeto
6. Clicar em Criar Chave

Copie a API Key e cole dentro do arquivo `appsettings.json`:

```
"GeminiAPIKey": "[API_KEY]"
```

### 🧠 **4. Executando o Projeto (CLI Mode)**

> 🖥️ Execute os comandos abaixo na raiz do projeto:

```bash
# 1. Restaurar dependências
dotnet restore

# 2. Aplicar migrations (cria as tabelas no Oracle)
 dotnet ef database update --project AiManagementApp

# 3. Iniciar o servidor
dotnet run --environment Development --project AiManagementApp
```

> 🔗 **URL gerada:** copie a exibida no console (exemplo: `http://localhost:5107`)

## Efetuando Testes no Sistema

### Visão Geral dos Endpoints

Abra a URL gerada pelo .NET e acesse o Scalar: `http://localhost:5107/scalar`

> 💡 **Dica:** No Scalar você pode visualizar todos os endpoints, métodos disponíveis e exemplos de requisição/retorno

![Scalar](/docs/images/scalar.png)

---

### 1. Verificar Saúde do Sistema

O endpoint de Health Check confirma se a aplicação e o banco Oracle estão ativos: `http://localhost:5107/health-ui`

![Health Resposta](/docs/images/health-ui.png)

> Tanto ✅ quanto o "Healthy" mostram que a comunicaçao com o banco está saudável

---

### 2. Executar Testes Unitários

Após verificar que o servidor está saudável, você pode rodar os **testes unitários** para validar a lógica principal da aplicação.

> 💡 **Os testes foram desenvolvidos utilizando o framework xUnit**, cobrindo os principais fluxos de CRUD e validações internas.

#### ▶️ Rodando os testes
Na raiz do projeto, execute o comando abaixo:

```bash
dotnet test
```

Isso irá:
- Restaurar automaticamente os pacotes necessários;
- Compilar o projeto e os testes;
- Executar todos os casos de teste definidos na pasta `AiManagementApp.Tests`.

#### 📊 Resultados esperados
Ao final da execução, o terminal exibirá um resumo semelhante a:

```
Resumo do teste: total: 4; falhou: 0; bem-sucedido: 4; ignorado: 0; duração: 9,8s
```
> ✅ Se todos os testes passarem, significa que a lógica principal da API está funcionando conforme o esperado

---

### (Opcional) Executar Testes Manuais (API Client)

Nesta etapa, você pode testar os endpoints manualmente utilizando **Insomnia** ou **Postman**, seguindo o mesmo comportamento dos testes automatizados.

#### 1. **Verificar Registros Cadastrados (GET ALL)**

Assim que a Migration for executada, o sistema já cria **6 registros iniciais** na tabela de Logs.
Para listar, use:

```
GET http://localhost:5107/api/v1/ai-logs?PageNumber=1&PageSize=2
```

> ⚙️ **Parâmetros:**
> - `PageNumber` = número da página (use `1` por padrão)
> - `PageSize` = quantidade de registros exibidos por página

📸 *Exemplo de resposta:*

![GET ALL RESPONSE](/docs/images/getall.png)

> 📑 As respostas são paginadas e estruturadas conforme o padrão HATEOAS, fornecendo links de navegação e ações relacionadas ao recurso consultado

---

#### 2. Buscar Registro Específico (GET by ID)
```
GET http://localhost:5107/api/v1/ai-logs/{id}
```
> 🔎 Substitua `{id}` por um ID retornado no GET anterior

📸 *Exemplo de resposta:*

![GETBYID RESPONSE](/docs/images/getbyid.png)

---

#### 3. Cadastrar Novo Registro (POST)
```
POST http://localhost:5107/api/v1/ai-logs
```
Insira o seguinte JSON de exemplo no body da requisição:

```json
{
  "dhRequisicao": "2025-11-14T12:30:00",
  "resumoRecebido": "Usuário comentou que acordou cansado e teve dificuldade para se concentrar nas primeiras tarefas do dia.",
  "recomendacaoGerada": "Tente fazer uma pausa curta para recarregar a mente. Respirar profundamente por alguns minutos ou caminhar um pouco pode ajudar a recuperar o foco. Lembre-se de avançar aos poucos, sem se cobrar além do necessário.",
  "nivel": 0,
  "sucessoEnvio": true
}
```
📸 *Exemplo de resposta 201 (criação bem-sucedida):*

![POST RESPONSE](/docs/images/post.png)

---

#### 4. Atualizar Registro Existente (PUT)
```
PUT http://localhost:5107/api/v1/ai-logs/{id}
```
> ✏️ Copie o JSON atual e altere apenas os campos desejados (**não inclua o id na requisição**)

📸 *Exemplo de resposta:*

![PUT RESPONSE](/docs/images/put.png)

---

#### 5. Deletar Registro (DELETE)
```
DELETE http://localhost:5107/api/v1/ai-logs/{id}
```

📸 *Exemplo de resposta:*

![DELETE RESPONSE](/docs/images/delete.png)

---

#### **Bônus** - Visualizar retorno da IA
```
GET http://localhost:5107/api/v1/ai/teste
```
> Esse método utiliza um dado mockado em seu próprio método para receber um exemplo de resposta da IA, sendo utilizado para testes locais.

*Dado Mockado*
```
resumoRecebido: "Estou meio cansada hoje"
nivel: 0
```

📸 *Resposta recebida:*

![TEST GET RESPONSE](/docs/images/getteste.png)


**✅ Resumo:**

O Lyra AI Management consolida práticas modernas de desenvolvimento com .NET, aplicando conceitos de Clean Architecture, versionamento de APIs, observabilidade e documentação inteligente.

Integrando IA generativa ao fluxo principal da aplicação, o serviço entrega uma experiência confiável, modular e de fácil manutenção, refletindo um design maduro e pronto para ambientes reais.

## Como Rodar o Projeto

> ⚠️ **Importante:**  
> Clone este repositório antes de tudo!
> ```bash
> git clone https://github.com/laviniapark/FIAP-2025-GS-CGL-ABD.git
> ```
> Escolha a pasta desejada e abra o projeto na sua IDE de preferência
---
### 📜 1. Requisitos

| Ferramenta | Descrição | Download |
|-------------|------------|-----------|
|**.NET SDK 9.0** | Framework necessário para compilar e executar o projeto | [Baixar .NET SDK](https://dotnet.microsoft.com/en-us/download) |
|**Oracle XE** | Banco de dados local (ou utilize o da instituição) | [Baixar Oracle XE](https://www.oracle.com/database/technologies/appdev/xe.html) |
|**IDE** | Recomendado: Visual Studio, Rider ou VS Code | — |
|**API Client** | Testes realizados com **Insomnia**, mas funciona também no **Postman** ou outro de sua preferência | — |
---
### 🗄️ 2. Configuração da conexão com o Banco de Dados

No arquivo `appsettings.json`, configure sua conexão Oracle:

```
"ConnectionStrings": {
    "DefaultConnection": "Data Source=[ORACLE-URL]:1521/[ORACLE-HOST];User Id=[ORACLE-USER];Password=[ORACLE-PASSWORD]"
  }
```

> Substitua os valores entre colchetes `[ ]` conforme suas credenciais Oracle

### 🤖 3. Configuração do Gemini AI

1. Acesse https://aistudio.google.com/api-keys
2. Clique em **Criar chave de API**
3. Dar um Nome a Chave: AiManagementApp (ou outro de sua preferência)
4. Select a Cloud Project > Criar Projeto
5. Escolha um nome para seu projeto: LyraAi (ou outro de sua preferência) > Criar Projeto
6. Clicar em Criar Chave

Copie a API Key e cole dentro do arquivo `appsettings.json`:

```
"GeminiAPIKey": "[API_KEY]"
```

### 🧠 **4. Executando o Projeto (CLI Mode)**

> 🖥️ Execute os comandos abaixo na raiz do projeto:

```bash
# 1. Restaurar dependências
dotnet restore

# 2. Aplicar migrations (cria as tabelas no Oracle)
 dotnet ef database update --project AiManagementApp

# 3. Iniciar o servidor
dotnet run --environment Development --project AiManagementApp
```

> 🔗 **URL gerada:** copie a exibida no console (exemplo: `http://localhost:5107`)

## Efetuando Testes no Sistema

### Visão Geral dos Endpoints

Abra a URL gerada pelo .NET e acesse o Scalar: `http://localhost:5107/scalar`

> 💡 **Dica:** No Scalar você pode visualizar todos os endpoints, métodos disponíveis e exemplos de requisição/retorno

![Scalar](/docs/images/scalar.png)

---

### 1. Verificar Saúde do Sistema

O endpoint de Health Check confirma se a aplicação e o banco Oracle estão ativos: `http://localhost:5107/health-ui`

![Health Resposta](/docs/images/health-ui.png)

> Tanto ✅ quanto o "Healthy" mostram que a comunicaçao com o banco está saudável 

---

### 2. Executar Testes Unitários

Após verificar que o servidor está saudável, você pode rodar os **testes unitários** para validar a lógica principal da aplicação.

> 💡 **Os testes foram desenvolvidos utilizando o framework xUnit**, cobrindo os principais fluxos de CRUD e validações internas.

#### ▶️ Rodando os testes
Na raiz do projeto, execute o comando abaixo:

```bash
dotnet test
```

Isso irá:
- Restaurar automaticamente os pacotes necessários;
- Compilar o projeto e os testes;
- Executar todos os casos de teste definidos na pasta `AiManagementApp.Tests`.

#### 📊 Resultados esperados
Ao final da execução, o terminal exibirá um resumo semelhante a:

```
Resumo do teste: total: 4; falhou: 0; bem-sucedido: 4; ignorado: 0; duração: 9,8s
```
> ✅ Se todos os testes passarem, significa que a lógica principal da API está funcionando conforme o esperado

---

### (Opcional) Executar Testes Manuais (API Client)

Nesta etapa, você pode testar os endpoints manualmente utilizando **Insomnia** ou **Postman**, seguindo o mesmo comportamento dos testes automatizados.

#### 1. **Verificar Registros Cadastrados (GET ALL)**

Assim que a Migration for executada, o sistema já cria **6 registros iniciais** na tabela de Logs.
Para listar, use:

```
GET http://localhost:5107/api/v1/ai-logs?PageNumber=1&PageSize=2
```

> ⚙️ **Parâmetros:**
> - `PageNumber` = número da página (use `1` por padrão)
> - `PageSize` = quantidade de registros exibidos por página

📸 *Exemplo de resposta:*

![GET ALL RESPONSE](/docs/images/getall.png)

> 📑 As respostas são paginadas e estruturadas conforme o padrão HATEOAS, fornecendo links de navegação e ações relacionadas ao recurso consultado

---

#### 2. Buscar Registro Específico (GET by ID)
```
GET http://localhost:5107/api/v1/ai-logs/{id}
```
> 🔎 Substitua `{id}` por um ID retornado no GET anterior

📸 *Exemplo de resposta:*

![GETBYID RESPONSE](/docs/images/getbyid.png)

---

#### 3. Cadastrar Novo Registro (POST)
```
POST http://localhost:5107/api/v1/ai-logs
```
Insira o seguinte JSON de exemplo no body da requisição:

```json
{
  "dhRequisicao": "2025-11-14T12:30:00",
  "resumoRecebido": "Usuário comentou que acordou cansado e teve dificuldade para se concentrar nas primeiras tarefas do dia.",
  "recomendacaoGerada": "Tente fazer uma pausa curta para recarregar a mente. Respirar profundamente por alguns minutos ou caminhar um pouco pode ajudar a recuperar o foco. Lembre-se de avançar aos poucos, sem se cobrar além do necessário.",
  "nivel": 0,
  "sucessoEnvio": true
}
```
📸 *Exemplo de resposta 201 (criação bem-sucedida):*

![POST RESPONSE](/docs/images/post.png)

---

#### 4. Atualizar Registro Existente (PUT)
```
PUT http://localhost:5107/api/v1/ai-logs/{id}
```
> ✏️ Copie o JSON atual e altere apenas os campos desejados (**não inclua o id na requisição**)

📸 *Exemplo de resposta:*

![PUT RESPONSE](/docs/images/put.png)

---

#### 5. Deletar Registro (DELETE)
```
DELETE http://localhost:5107/api/v1/ai-logs/{id}
```

📸 *Exemplo de resposta:*

![DELETE RESPONSE](/docs/images/delete.png)

---

#### **Bônus** - Visualizar retorno da IA
```
GET http://localhost:5107/api/v1/ai/teste
```
> Esse método utiliza um dado mockado em seu próprio método para receber um exemplo de resposta da IA, sendo utilizado para testes locais.

*Dado Mockado*
```
resumoRecebido: "Estou meio cansada hoje"
nivel: 0
```

📸 *Resposta recebida:*

![TEST GET RESPONSE](/docs/images/getteste.png)


**✅ Resumo:**

O Lyra AI Management consolida práticas modernas de desenvolvimento com .NET, aplicando conceitos de Clean Architecture, versionamento de APIs, observabilidade e documentação inteligente.

Integrando IA generativa ao fluxo principal da aplicação, o serviço entrega uma experiência confiável, modular e de fácil manutenção, refletindo um design maduro e pronto para ambientes reais.