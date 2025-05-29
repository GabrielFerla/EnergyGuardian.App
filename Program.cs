using System;
using System.Collections.Generic;
using EnergyGuardian.Models;
using EnergyGuardian.Services;

namespace EnergyGuardian
{
    class Program
    {
        static List<Infraestrutura> setoresCadastrados = new List<Infraestrutura>();
        static List<FalhaEnergia> falhasRegistradas = new List<FalhaEnergia>();
        
        static void Main(string[] args)
        {
            try
            {
                bool continuar = true;

                  while (continuar)
                {
                    ExibirMenu();
                    string? opcao = Console.ReadLine();
                      switch (opcao)
                    {
                        case "1":
                            CadastrarSetor();
                            break;
                        case "2":
                            RegistrarFalha();
                            break;
                        case "3":
                            VisualizarSetores();
                            break;
                        case "4":
                            VisualizarFalhas();
                            break;
                        case "5":
                            GerarRelatorio();
                            break;
                        case "6":
                            AcionarPlanoEmergencia();
                            break;
                        case "7":
                            DesativarTodosAlertas();
                            break;
                        case "0":
                            continuar = false;
                            break;
                        default:
                            Console.WriteLine("Opção inválida! Tente novamente.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao executar o sistema: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Execução finalizada.");
            }
        }        /// <summary>
        /// Exibe o menu principal do sistema com todas as opções disponíveis
        /// </summary>
        static void ExibirMenu()
        {
            Console.Clear();            Console.WriteLine("=== ENERGY GUARDIAN - SISTEMA DE MONITORAMENTO ===");            Console.WriteLine("1 - Cadastrar Setor");
            Console.WriteLine("2 - Registrar Falha de Energia");
            Console.WriteLine("3 - Visualizar Setores Cadastrados");
            Console.WriteLine("4 - Visualizar Falhas Registradas");
            Console.WriteLine("5 - Gerar Relatórios");
            Console.WriteLine("6 - Gerenciar Plano de Emergência");
            Console.WriteLine("7 - Desativar Todos os Alertas");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");
        }/// <summary>
        /// Cadastra um novo setor no sistema com validação de entrada
        /// </summary>
        static void CadastrarSetor()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== CADASTRO DE SETOR ===");
                
                // Validação do nome do setor
                string nome = ObterNomeSetorValido();
                
                // Validação do gerador
                bool temGerador = ObterRespostaSimNao("Possui Gerador? (S/N): ");
                
                // Validação se é setor crítico
                bool ehCritico = ObterRespostaSimNao("É um setor crítico? (S/N): ");
                
                if (ehCritico)
                {
                    string tipoRisco = ObterTipoRisco();
                    var setorCritico = new SetorCritico(nome, temGerador, tipoRisco);
                    setoresCadastrados.Add(setorCritico);
                    Console.WriteLine($"Setor crítico '{nome}' cadastrado com sucesso!");
                }
                else
                {
                    var setor = new Infraestrutura(nome, temGerador);
                    setoresCadastrados.Add(setor);
                    Console.WriteLine($"Setor '{nome}' cadastrado com sucesso!");
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Erro durante o cadastro: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Obtém um nome válido para o setor com validação
        /// </summary>
        /// <returns>Nome válido do setor</returns>
        static string ObterNomeSetorValido()
        {
            string nome;
            do
            {
                try
                {
                    Console.Write("Nome do Setor: ");
                    nome = Console.ReadLine()?.Trim() ?? "";
                    
                    if (string.IsNullOrWhiteSpace(nome))
                    {
                        throw new ArgumentException("O nome do setor não pode ser vazio.");
                    }
                    
                    if (nome.Length < 2)
                    {
                        throw new ArgumentException("O nome do setor deve ter pelo menos 2 caracteres.");
                    }
                    
                    if (nome.Length > 50)
                    {
                        throw new ArgumentException("O nome do setor deve ter no máximo 50 caracteres.");
                    }
                    
                    return nome;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }
            } while (true);
        }

        /// <summary>
        /// Obtém uma resposta Sim/Não válida do usuário
        /// </summary>
        /// <param name="pergunta">Pergunta a ser exibida</param>
        /// <returns>True para Sim, False para Não</returns>
        static bool ObterRespostaSimNao(string pergunta)
        {
            while (true)
            {
                try
                {
                    Console.Write(pergunta);
                    string resposta = Console.ReadLine()?.ToUpper().Trim() ?? "";
                    
                    if (resposta == "S" || resposta == "SIM")
                    {
                        return true;
                    }
                    else if (resposta == "N" || resposta == "NAO" || resposta == "NÃO")
                    {
                        return false;
                    }
                    else
                    {
                        throw new ArgumentException("Resposta inválida. Digite 'S' para Sim ou 'N' para Não.");
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Obtém o tipo de risco para setores críticos
        /// </summary>
        /// <returns>Tipo de risco válido</returns>
        static string ObterTipoRisco()
        {
            while (true)
            {
                try
                {
                    Console.Write("Tipo de risco associado: ");
                    string tipoRisco = Console.ReadLine()?.Trim() ?? "";
                    
                    if (string.IsNullOrWhiteSpace(tipoRisco))
                    {
                        throw new ArgumentException("O tipo de risco não pode ser vazio.");
                    }
                    
                    if (tipoRisco.Length < 3)
                    {
                        throw new ArgumentException("O tipo de risco deve ter pelo menos 3 caracteres.");
                    }
                    
                    return tipoRisco;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }
            }
        }        /// <summary>
        /// Exibe a lista de todos os setores cadastrados no sistema
        /// </summary>
        static void VisualizarSetores()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== SETORES CADASTRADOS ===");
                
                if (setoresCadastrados.Count == 0)
                {
                    Console.WriteLine("Nenhum setor cadastrado.");
                }
                else
                {
                    for (int i = 0; i < setoresCadastrados.Count; i++)
                    {
                        var setor = setoresCadastrados[i];
                        string tipo = setor is SetorCritico ? "Crítico" : "Normal";
                        string statusEmergencia = setor.PlanoEmergenciaAtivado ? "[EMERGÊNCIA]" : "[NORMAL]";
                        
                        Console.WriteLine($"{i + 1}. [{tipo}] {setor.NomeSetor} {statusEmergencia}");
                        Console.WriteLine($"   Gerador: {(setor.TemGerador ? "Sim" : "Não")}");
                        
                        if (setor is SetorCritico setorCritico)
                        {
                            Console.WriteLine($"   Risco: {setorCritico.TipoRisco}");
                        }
                        Console.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao exibir setores: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }/// <summary>
        /// Registra uma nova falha de energia no sistema com validação completa
        /// </summary>
        static void RegistrarFalha()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== REGISTRO DE FALHA DE ENERGIA ===");
                
                if (setoresCadastrados.Count == 0)
                {
                    Console.WriteLine("Não há setores cadastrados. Cadastre um setor primeiro.");
                    return;
                }
                
                Console.WriteLine("Selecione o setor afetado:");
                ExibirListaSetores();
                
                int indiceSetor = ObterIndiceSetorValido();
                var setorAfetado = setoresCadastrados[indiceSetor - 1];
                
                string descricao = ObterDescricaoFalhaValida();
                DateTime dataFalha = ObterDataFalhaValida();
                
                var falha = new FalhaEnergia(dataFalha, descricao, setorAfetado);
                falhasRegistradas.Add(falha);
                
                Logger.RegistrarEvento(falha);
                Console.WriteLine("Falha registrada com sucesso!");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Erro durante o registro: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }        /// <summary>
        /// Exibe a lista numerada de setores cadastrados para seleção
        /// </summary>
        static void ExibirListaSetores()
        {
            try
            {
                for (int i = 0; i < setoresCadastrados.Count; i++)
                {
                    var setor = setoresCadastrados[i];
                    string tipo = setor is SetorCritico ? "[CRÍTICO]" : "[NORMAL]";
                    Console.WriteLine($"{i + 1}. {setor.NomeSetor} {tipo}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao exibir lista de setores: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtém um índice válido de setor do usuário
        /// </summary>
        /// <returns>Índice válido do setor (1-based)</returns>
        static int ObterIndiceSetorValido()
        {
            while (true)
            {
                try
                {
                    Console.Write("Número do setor: ");
                    string entrada = Console.ReadLine()?.Trim() ?? "";
                    
                    if (string.IsNullOrWhiteSpace(entrada))
                    {
                        throw new ArgumentException("Entrada não pode ser vazia.");
                    }
                    
                    if (!int.TryParse(entrada, out int indice))
                    {
                        throw new FormatException("Por favor, digite apenas números.");
                    }
                    
                    if (indice < 1 || indice > setoresCadastrados.Count)
                    {
                        throw new ArgumentOutOfRangeException($"Número deve estar entre 1 e {setoresCadastrados.Count}.");
                    }
                      return indice;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Obtém uma descrição válida para a falha
        /// </summary>
        /// <returns>Descrição válida da falha</returns>
        static string ObterDescricaoFalhaValida()
        {
            while (true)
            {
                try
                {
                    Console.Write("Descrição da falha: ");
                    string descricao = Console.ReadLine()?.Trim() ?? "";
                    
                    if (string.IsNullOrWhiteSpace(descricao))
                    {
                        throw new ArgumentException("A descrição da falha não pode ser vazia.");
                    }
                    
                    if (descricao.Length < 5)
                    {
                        throw new ArgumentException("A descrição deve ter pelo menos 5 caracteres.");
                    }
                    
                    if (descricao.Length > 200)
                    {
                        throw new ArgumentException("A descrição deve ter no máximo 200 caracteres.");
                    }
                    
                    return descricao;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Obtém uma data válida para a falha (permite usar data atual ou específica)
        /// </summary>
        /// <returns>Data válida da falha</returns>
        static DateTime ObterDataFalhaValida()
        {
            while (true)
            {
                try
                {
                    Console.Write("Data da falha (dd/MM/yyyy HH:mm) ou ENTER para usar data atual: ");
                    string entrada = Console.ReadLine()?.Trim() ?? "";
                    
                    if (string.IsNullOrWhiteSpace(entrada))
                    {
                        return DateTime.Now;
                    }
                    
                    if (!DateTime.TryParseExact(entrada, "dd/MM/yyyy HH:mm", null, 
                        System.Globalization.DateTimeStyles.None, out DateTime dataFalha))
                    {
                        throw new FormatException("Formato de data inválido. Use: dd/MM/yyyy HH:mm (ex: 25/12/2024 14:30)");
                    }
                    
                    if (dataFalha > DateTime.Now)
                    {
                        throw new ArgumentException("A data da falha não pode ser no futuro.");
                    }
                    
                    if (dataFalha < DateTime.Now.AddYears(-1))
                    {
                        throw new ArgumentException("A data da falha não pode ser anterior a 1 ano.");
                    }
                    
                    return dataFalha;
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }
            }
        }        /// <summary>
        /// Exibe o histórico de todas as falhas de energia registradas no sistema
        /// </summary>
        static void VisualizarFalhas()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== FALHAS REGISTRADAS ===");
                
                if (falhasRegistradas.Count == 0)
                {
                    Console.WriteLine("Nenhuma falha registrada.");
                }
                else
                {
                    for (int i = 0; i < falhasRegistradas.Count; i++)
                    {
                        var falha = falhasRegistradas[i];
                        string tipoSetor = falha.SetorAfetado is SetorCritico ? "[CRÍTICO]" : "[NORMAL]";
                        
                        Console.WriteLine($"{i + 1}. Data: {falha.Data.ToString("dd/MM/yyyy HH:mm")}");
                        Console.WriteLine($"   Setor: {falha.SetorAfetado.NomeSetor} {tipoSetor}");
                        Console.WriteLine($"   Descrição: {falha.Descricao}");
                        
                        if (falha.SetorAfetado is SetorCritico setorCritico)
                        {
                            Console.WriteLine($"   Risco Associado: {setorCritico.TipoRisco}");
                        }
                        Console.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao exibir falhas: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }/// <summary>
        /// Gera relatórios do sistema com validação de entrada
        /// </summary>
        static void GerarRelatorio()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== GERAR RELATÓRIO ===");
                
                if (setoresCadastrados.Count == 0)
                {
                    Console.WriteLine("Não há setores cadastrados. Cadastre um setor primeiro.");
                    return;
                }
                
                int opcaoRelatorio = ObterOpcaoRelatorio();
                
                switch (opcaoRelatorio)
                {
                    case 1:
                        GerarRelatorioSetorEspecifico();
                        break;
                    case 2:
                        GerarRelatorioTodosSetores();
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao gerar relatório: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Obtém a opção de relatório válida do usuário
        /// </summary>
        /// <returns>Opção de relatório (1 ou 2)</returns>
        static int ObterOpcaoRelatorio()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Escolha uma opção:");
                    Console.WriteLine("1 - Relatório de um setor específico");
                    Console.WriteLine("2 - Relatório de todos os setores");
                    Console.Write("Opção: ");
                    
                    string entrada = Console.ReadLine()?.Trim() ?? "";
                    
                    if (string.IsNullOrWhiteSpace(entrada))
                    {
                        throw new ArgumentException("Entrada não pode ser vazia.");
                    }
                    
                    if (!int.TryParse(entrada, out int opcao))
                    {
                        throw new FormatException("Por favor, digite apenas números.");
                    }
                      if (opcao < 1 || opcao > 2)
                    {
                        throw new ArgumentOutOfRangeException("Opção deve ser 1 ou 2.");
                    }
                    
                    return opcao;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Gera relatório para um setor específico
        /// </summary>
        static void GerarRelatorioSetorEspecifico()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== RELATÓRIO DE SETOR ESPECÍFICO ===");
                Console.WriteLine("Selecione o setor para gerar o relatório:");
                
                ExibirListaSetores();
                
                int indiceSetor = ObterIndiceSetorValido();
                var setor = setoresCadastrados[indiceSetor - 1];
                var relatorio = RelatorioService.GerarRelatorio(setor);
                
                Console.WriteLine("\n" + relatorio);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao gerar relatório específico: {ex.Message}");
            }
        }

        /// <summary>
        /// Gera relatório para todos os setores
        /// </summary>
        static void GerarRelatorioTodosSetores()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== RELATÓRIO DE TODOS OS SETORES ===");
                
                var relatorioGeral = RelatorioService.GerarRelatorioTodosSetores(setoresCadastrados);
                Console.WriteLine(relatorioGeral);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao gerar relatório geral: {ex.Message}");
            }
        }        /// <summary>
        /// Gerencia planos de emergência dos setores com validação completa
        /// </summary>
        static void AcionarPlanoEmergencia()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== GERENCIAR PLANO DE EMERGÊNCIA ===");
                
                if (setoresCadastrados.Count == 0)
                {
                    Console.WriteLine("Não há setores cadastrados. Cadastre um setor primeiro.");
                    return;
                }
                
                Console.WriteLine("Selecione o setor para gerenciar o plano de emergência:");
                ExibirSetoresComStatus();
                
                int indiceSetor = ObterIndiceSetorValido();
                var setor = setoresCadastrados[indiceSetor - 1];
                
                ProcessarAcaoPlanoEmergencia(setor);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao gerenciar plano de emergência: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }        /// <summary>
        /// Exibe setores com status de emergência para gerenciamento
        /// </summary>
        static void ExibirSetoresComStatus()
        {
            try
            {
                for (int i = 0; i < setoresCadastrados.Count; i++)
                {
                    var setor = setoresCadastrados[i];
                    string statusEmergencia = setor.PlanoEmergenciaAtivado ? "[EMERGÊNCIA ATIVA]" : "[NORMAL]";
                    string tipoSetor = setor is SetorCritico ? "[CRÍTICO]" : "[NORMAL]";
                    Console.WriteLine($"{i + 1}. {setor.NomeSetor} {tipoSetor} {statusEmergencia}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao exibir setores com status: {ex.Message}");
            }
        }

        /// <summary>
        /// Processa a ação do plano de emergência baseado no status atual
        /// </summary>
        /// <param name="setor">Setor a ser processado</param>
        static void ProcessarAcaoPlanoEmergencia(Infraestrutura setor)
        {
            try
            {
                if (setor.PlanoEmergenciaAtivado)
                {
                    ProcessarDesativacaoEmergencia(setor);
                }
                else
                {
                    ProcessarAtivacaoEmergencia(setor);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar ação: {ex.Message}");
            }
        }

        /// <summary>
        /// Processa a desativação do plano de emergência
        /// </summary>
        /// <param name="setor">Setor para desativar emergência</param>
        static void ProcessarDesativacaoEmergencia(Infraestrutura setor)
        {
            Console.WriteLine($"\nO plano de emergência está ATIVO para {setor.NomeSetor}.");
            Console.WriteLine("1 - Desativar Plano de Emergência");
            Console.WriteLine("0 - Cancelar");
            
            int opcao = ObterOpcaoValidaEmergencia();
            
            if (opcao == 1)
            {
                setor.DesativarPlanoEmergencia();
                Console.WriteLine("Plano de emergência desativado com sucesso!");
            }
            else
            {
                Console.WriteLine("Operação cancelada.");
            }
        }

        /// <summary>
        /// Processa a ativação do plano de emergência
        /// </summary>
        /// <param name="setor">Setor para ativar emergência</param>
        static void ProcessarAtivacaoEmergencia(Infraestrutura setor)
        {
            Console.WriteLine($"\nO plano de emergência está DESATIVADO para {setor.NomeSetor}.");
            Console.WriteLine("1 - Acionar Plano de Emergência");
            Console.WriteLine("0 - Cancelar");
            
            int opcao = ObterOpcaoValidaEmergencia();
            
            if (opcao == 1)
            {
                setor.AcionarPlanoEmergencia();
                Console.WriteLine("Plano de emergência acionado com sucesso!");
            }
            else
            {
                Console.WriteLine("Operação cancelada.");
            }
        }

        /// <summary>
        /// Obtém opção válida para emergência (0 ou 1)
        /// </summary>
        /// <returns>Opção válida</returns>
        static int ObterOpcaoValidaEmergencia()
        {
            while (true)
            {
                try
                {
                    Console.Write("Opção: ");
                    string entrada = Console.ReadLine()?.Trim() ?? "";
                    
                    if (string.IsNullOrWhiteSpace(entrada))
                    {
                        throw new ArgumentException("Entrada não pode ser vazia.");
                    }
                    
                    if (!int.TryParse(entrada, out int opcao))
                    {
                        throw new FormatException("Por favor, digite apenas números.");
                    }
                      if (opcao < 0 || opcao > 1)
                    {
                        throw new ArgumentOutOfRangeException("Opção deve ser 0 ou 1.");
                    }
                    
                    return opcao;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }
            }
        }        /// <summary>
        /// Desativa todos os planos de emergência ativos no sistema
        /// </summary>
        static void DesativarTodosAlertas()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== DESATIVAR TODOS OS ALERTAS ===");
                
                if (setoresCadastrados.Count == 0)
                {
                    Console.WriteLine("Não há setores cadastrados.");
                    return;
                }
                
                int contadorDesativados = 0;
                
                Console.WriteLine("Verificando setores com planos de emergência ativos...\n");
                
                foreach (var setor in setoresCadastrados)
                {
                    if (setor.PlanoEmergenciaAtivado)
                    {
                        Console.WriteLine($"Desativando: {setor.NomeSetor}");
                        setor.DesativarPlanoEmergencia();
                        contadorDesativados++;
                    }
                }
                
                Console.WriteLine();
                
                if (contadorDesativados > 0)
                {
                    Console.WriteLine($"✓ {contadorDesativados} plano(s) de emergência desativado(s) com sucesso!");
                    Console.WriteLine("Todos os setores retornaram ao estado normal.");
                }
                else
                {
                    Console.WriteLine("ℹ Não havia setores com plano de emergência ativado.");
                    Console.WriteLine("Todos os setores já estavam em estado normal.");
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Erro durante a desativação: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }
    }
}