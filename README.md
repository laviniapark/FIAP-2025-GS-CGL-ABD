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

### 🧠 **3. Executando o Projeto (CLI Mode)**

> 🖥️ Execute os comandos abaixo na raiz do projeto:

```bash
# 1. Restaurar dependências
dotnet restore

# 2. Aplicar migrations (cria as tabelas no Oracle)
 dotnet ef database update --project AiManagementApp

# 3. Iniciar o servidor
dotnet run --project AiManagementApp
```

> 🔗 **URL gerada:** copie a exibida no console (exemplo: `http://localhost:5017`)

## Efetuando Testes no Sistema

