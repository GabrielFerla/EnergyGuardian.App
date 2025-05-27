using System;
using EnergyGuardian.Models;
using EnergyGuardian.Services;

namespace EnergyGuardian
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var setor1 = new SetorCritico("UTI", true, "Falha em equipamentos vitais");
                var setor2 = new Infraestrutura("Sala de Servidores", false);

                setor1.AcionarPlanoEmergencia();
                setor2.AcionarPlanoEmergencia();

                var falha = new FalhaEnergia(DateTime.Now, "Queda total", setor1);
                Logger.RegistrarEvento(falha);

                var relatorio = RelatorioService.GerarRelatorio(setor1);
                Console.WriteLine(relatorio);
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
    }
}