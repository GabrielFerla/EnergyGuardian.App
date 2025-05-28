# Energy Guardian - Sistema de Monitoramento de Energia

![Version](https://img.shields.io/badge/version-1.0.0-blue.svg)
![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)

## Sumário
- [Visão Geral](#visão-geral)
- [Funcionalidades Principais](#funcionalidades-principais)
- [Tipos de Setores](#tipos-de-setores)
- [Como Usar o Sistema](#como-usar-o-sistema)
- [Status dos Planos de Emergência](#status-dos-planos-de-emergência)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Requisitos do Sistema](#requisitos-do-sistema)
- [Desenvolvimento e Contribuição](#desenvolvimento-e-contribuição)

## Visão Geral

Energy Guardian é um sistema de gerenciamento e monitoramento de falhas de energia em diversos setores de uma organização. O sistema permite cadastrar diferentes tipos de setores (normais e críticos), registrar falhas de energia, acionar planos de emergência e gerar relatórios para análise.

## Funcionalidades Principais

- **Cadastro de Setores**: Permite cadastrar diferentes áreas da organização, incluindo informações sobre a existência de geradores e se o setor é considerado crítico.
- **Registro de Falhas**: Documenta ocorrências de falhas de energia em setores específicos, incluindo data, hora e descrição do problema.
- **Gerenciamento de Planos de Emergência**: Permite acionar ou desativar planos de emergência para setores específicos.
- **Geração de Relatórios**: Disponibiliza relatórios individuais por setor ou um relatório geral com o status de todos os setores.
- **Desativação de Alertas**: Funcionalidade para desativar todos os alertas ativos de uma só vez em situações de normalização.

## Tipos de Setores

### Setor Normal
Setores de operação padrão que podem ou não possuir geradores de backup.

### Setor Crítico
Setores que necessitam de atenção especial durante falhas de energia, incluindo informações sobre o tipo de risco associado (ex: risco à vida, risco a equipamentos sensíveis, etc.).

## Como Usar o Sistema

### Menu Principal

Ao iniciar o sistema, você terá acesso ao seguinte menu:

```
=== ENERGY GUARDIAN - SISTEMA DE MONITORAMENTO ===
1 - Cadastrar Setor
2 - Registrar Falha de Energia
3 - Visualizar Setores Cadastrados
4 - Visualizar Falhas Registradas
5 - Gerar Relatórios
6 - Gerenciar Plano de Emergência
7 - Desativar Todos os Alertas
0 - Sair
```

### 1. Cadastrar Setor

Esta opção permite adicionar um novo setor ao sistema:
1. Digite o nome do setor
2. Informe se o setor possui gerador (S/N)
3. Indique se é um setor crítico (S/N)
4. Se for crítico, especifique o tipo de risco associado

### 2. Registrar Falha de Energia

Permite documentar uma nova ocorrência de falha de energia:
1. Selecione o setor afetado da lista apresentada
2. Forneça uma descrição da falha
3. A data e hora são registradas automaticamente

### 3. Visualizar Setores Cadastrados

Exibe a lista de todos os setores cadastrados no sistema, incluindo:
- Nome do setor
- Tipo (Normal/Crítico)
- Presença de gerador
- Tipo de risco (apenas para setores críticos)

### 4. Visualizar Falhas Registradas

Mostra o histórico de todas as falhas de energia documentadas:
- Data e hora da ocorrência
- Setor afetado
- Descrição da falha

### 5. Gerar Relatórios

Oferece opções para geração de relatórios:
1. Relatório de um setor específico
   - Selecione o setor desejado
   - O sistema apresentará um resumo das condições do setor e falhas relacionadas
2. Relatório de todos os setores
   - Apresenta um panorama geral de todos os setores e suas condições

### 6. Gerenciar Plano de Emergência

Permite acionar ou desativar planos de emergência para setores específicos:
1. Selecione o setor na lista apresentada
2. O sistema verificará automaticamente o status atual do plano de emergência:
   - Se estiver desativado, oferecerá a opção para acioná-lo
   - Se estiver ativado, oferecerá a opção para desativá-lo

### 7. Desativar Todos os Alertas

Desativa simultaneamente todos os planos de emergência ativos no sistema, útil durante a normalização após uma situação crítica.

## Status dos Planos de Emergência

Os planos de emergência podem estar em dois estados:
- **Desativado**: Estado normal de operação
- **Ativado**: Estado de alerta que indica que o setor está em situação de emergência

## Observações Importantes

1. Um plano de emergência só pode ser acionado se estiver atualmente desativado
2. Um plano só pode ser desativado se estiver atualmente acionado
3. Setores críticos possuem mensagens específicas durante o acionamento do plano de emergência, destacando o tipo de risco envolvido
4. A interface do sistema sempre mostra apenas as opções válidas com base no estado atual dos setores

## Estrutura do Projeto

O projeto está organizado nas seguintes pastas:

- **Models/**: Contém as classes de modelo do sistema
  - `Infraestrutura.cs`: Classe base para todos os setores
  - `SetorCritico.cs`: Classe especializada para setores com riscos especiais
  - `FalhaEnergia.cs`: Representação de uma ocorrência de falha de energia

- **Services/**: Serviços utilizados pelo sistema
  - `Logger.cs`: Sistema de registro de eventos
  - `RelatorioService.cs`: Geração de relatórios

- **Program.cs**: Ponto de entrada do programa e interface com o usuário

## Requisitos do Sistema

- .NET 8.0 ou superior
- Console compatível com caracteres Unicode para exibição adequada das mensagens

## Desenvolvimento e Contribuição

### Ambiente de Desenvolvimento

Para desenvolver neste projeto, você precisará:

1. Visual Studio 2022 ou VS Code com extensões C#
2. SDK .NET 8.0 ou superior

### Controle de Versão

Este projeto utiliza Git para controle de versão. Um arquivo `.gitignore` está incluído para evitar o commit de arquivos desnecessários como:
- Arquivos de configuração do Visual Studio (`.vs/`)
- Arquivos temporários e de cache


### Compilando o Projeto

Para compilar sem executar:

```powershell
dotnet build
```


### Executando o Projeto

Para executar o sistema, abra um terminal no diretório do projeto e execute:

```powershell
dotnet run
```



