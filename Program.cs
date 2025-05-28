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
                    string opcao = Console.ReadLine();
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
        }

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
        }        static void CadastrarSetor()
        {
            Console.Clear();
            Console.WriteLine("=== CADASTRO DE SETOR ===");
            
            string nome;
            do {
                Console.Write("Nome do Setor: ");
                nome = Console.ReadLine()?.Trim() ?? "";
                if (string.IsNullOrWhiteSpace(nome))
                {
                    Console.WriteLine("O nome do setor não pode ser vazio. Por favor, informe um nome válido.");
                }
            } while (string.IsNullOrWhiteSpace(nome));
            
            Console.Write("Possui Gerador? (S/N): ");
            bool temGerador = (Console.ReadLine()?.ToUpper() ?? "") == "S";
            
            Console.Write("É um setor crítico? (S/N): ");
            bool ehCritico = (Console.ReadLine()?.ToUpper() ?? "") == "S";
            
            if (ehCritico)
            {
                Console.Write("Tipo de risco associado: ");
                string tipoRisco = Console.ReadLine() ?? "Não especificado";
                
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
            
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        static void VisualizarSetores()
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
                    
                    Console.WriteLine($"{i + 1}. [{tipo}] {setor.NomeSetor} - Gerador: {(setor.TemGerador ? "Sim" : "Não")}");
                    
                    if (setor is SetorCritico setorCritico)
                    {
                        Console.WriteLine($"   Risco: {setorCritico.TipoRisco}");
                    }
                }
            }
            
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        static void RegistrarFalha()
        {
            Console.Clear();
            Console.WriteLine("=== REGISTRO DE FALHA DE ENERGIA ===");
            
            if (setoresCadastrados.Count == 0)
            {
                Console.WriteLine("Não há setores cadastrados. Cadastre um setor primeiro.");
                Console.ReadKey();
                return;
            }
            
            Console.WriteLine("Selecione o setor afetado:");
            
            for (int i = 0; i < setoresCadastrados.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {setoresCadastrados[i].NomeSetor}");
            }
            
            Console.Write("Número do setor: ");
            if (int.TryParse(Console.ReadLine(), out int indice) && indice > 0 && indice <= setoresCadastrados.Count)
            {
                var setorAfetado = setoresCadastrados[indice - 1];
                  Console.Write("Descrição da falha: ");
                string descricao = Console.ReadLine() ?? "Falha não especificada";
                
                var falha = new FalhaEnergia(DateTime.Now, descricao, setorAfetado);
                falhasRegistradas.Add(falha);
                
                Logger.RegistrarEvento(falha);
                Console.WriteLine("Falha registrada com sucesso!");
            }
            else
            {
                Console.WriteLine("Seleção inválida.");
            }
            
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        static void VisualizarFalhas()
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
                    Console.WriteLine($"{i + 1}. Data: {falha.Data.ToString("dd/MM/yyyy HH:mm")}");
                    Console.WriteLine($"   Setor: {falha.SetorAfetado.NomeSetor}");
                    Console.WriteLine($"   Descrição: {falha.Descricao}");
                    Console.WriteLine();
                }
            }
            
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }        static void GerarRelatorio()
        {
            Console.Clear();
            Console.WriteLine("=== GERAR RELATÓRIO ===");
            
            if (setoresCadastrados.Count == 0)
            {
                Console.WriteLine("Não há setores cadastrados. Cadastre um setor primeiro.");
                Console.ReadKey();
                return;
            }
            
            Console.WriteLine("Escolha uma opção:");
            Console.WriteLine("1 - Relatório de um setor específico");
            Console.WriteLine("2 - Relatório de todos os setores");
            Console.Write("Opção: ");
            
            string opcao = Console.ReadLine() ?? "";
            
            if (opcao == "1") 
            {
                // Relatório para um setor específico
                Console.Clear();
                Console.WriteLine("=== RELATÓRIO DE SETOR ESPECÍFICO ===");
                Console.WriteLine("Selecione o setor para gerar o relatório:");
                
                for (int i = 0; i < setoresCadastrados.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {setoresCadastrados[i].NomeSetor}");
                }
                
                Console.Write("Número do setor: ");
                if (int.TryParse(Console.ReadLine(), out int indice) && indice > 0 && indice <= setoresCadastrados.Count)
                {
                    var setor = setoresCadastrados[indice - 1];
                    var relatorio = RelatorioService.GerarRelatorio(setor);
                    
                    Console.WriteLine("\n" + relatorio);
                }
                else
                {
                    Console.WriteLine("Seleção inválida.");
                }
            }
            else if (opcao == "2") 
            {
                // Relatório para todos os setores
                Console.Clear();
                Console.WriteLine("=== RELATÓRIO DE TODOS OS SETORES ===");
                
                var relatorioGeral = RelatorioService.GerarRelatorioTodosSetores(setoresCadastrados);
                Console.WriteLine(relatorioGeral);
            }
            else
            {
                Console.WriteLine("Opção inválida.");
            }
            
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }        static void AcionarPlanoEmergencia()
        {
            Console.Clear();
            Console.WriteLine("=== GERENCIAR PLANO DE EMERGÊNCIA ===");
            
            if (setoresCadastrados.Count == 0)
            {
                Console.WriteLine("Não há setores cadastrados. Cadastre um setor primeiro.");
                Console.ReadKey();
                return;
            }
            
            Console.WriteLine("Selecione o setor para gerenciar o plano de emergência:");
            
            for (int i = 0; i < setoresCadastrados.Count; i++)
            {
                var setor = setoresCadastrados[i];
                string statusEmergencia = setor.PlanoEmergenciaAtivado ? "[EMERGÊNCIA ATIVA]" : "";
                Console.WriteLine($"{i + 1}. {setor.NomeSetor} {statusEmergencia}");
            }
            
            Console.Write("Número do setor: ");
            if (int.TryParse(Console.ReadLine(), out int indice) && indice > 0 && indice <= setoresCadastrados.Count)
            {
                var setor = setoresCadastrados[indice - 1];
                  // Verifica o status atual e mostra apenas a opção válida
                if (setor.PlanoEmergenciaAtivado)
                {
                    Console.WriteLine("\nO plano de emergência está ativo.");
                    Console.WriteLine("1 - Desativar Plano de Emergência");
                    Console.Write("Opção: ");
                    
                    string opcao = Console.ReadLine() ?? "";
                    
                    if (opcao == "1")
                    {
                        setor.DesativarPlanoEmergencia();
                    }
                    else
                    {
                        Console.WriteLine("Opção inválida.");
                    }
                }
                else
                {
                    Console.WriteLine("\nO plano de emergência está desativado.");
                    Console.WriteLine("1 - Acionar Plano de Emergência");
                    Console.Write("Opção: ");
                    
                    string opcao = Console.ReadLine() ?? "";
                    
                    if (opcao == "1")
                    {
                        setor.AcionarPlanoEmergencia();
                    }
                    else
                    {
                        Console.WriteLine("Opção inválida.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Seleção inválida.");
            }
            
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }        static void DesativarTodosAlertas()
        {
            Console.Clear();
            Console.WriteLine("=== DESATIVAR TODOS OS ALERTAS ===");
            
            if (setoresCadastrados.Count == 0)
            {
                Console.WriteLine("Não há setores cadastrados.");
                Console.ReadKey();
                return;
            }
            
            int contadorDesativados = 0;
            
            foreach (var setor in setoresCadastrados)
            {
                if (setor.PlanoEmergenciaAtivado)
                {
                    setor.DesativarPlanoEmergencia();
                    contadorDesativados++;
                }
            }
            
            if (contadorDesativados > 0)
            {
                Console.WriteLine($"\n{contadorDesativados} plano(s) de emergência desativado(s) com sucesso!");
            }
            else
            {
                Console.WriteLine("\nNão havia setores com plano de emergência ativado.");
            }
            
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}