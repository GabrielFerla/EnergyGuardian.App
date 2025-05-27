using System;
using System.Collections.Generic;
using System.Text;
using EnergyGuardian.Models;

namespace EnergyGuardian.Services
{    public static class RelatorioService
    {
        public static string GerarRelatorio(Infraestrutura setor)
        {
            string status;
            if (setor.PlanoEmergenciaAtivado)
            {
                status = "EMERGÊNCIA";
            }
            else if (setor.TemGerador)
            {
                status = "Estável";
            }
            else
            {
                status = "Alerta";
            }

            return $"Relatório de Status:\n" +
                   $"Setor: {setor.NomeSetor}\n" +
                   $"Tem Gerador: {(setor.TemGerador ? "Sim" : "Não")}\n" +
                   $"Plano de Emergência: {(setor.PlanoEmergenciaAtivado ? "ATIVADO" : "Desativado")}\n" +
                   $"Status: {status}";
        }
        
        public static string GerarRelatorioTodosSetores(List<Infraestrutura> setores)
        {
            if (setores == null || setores.Count == 0)
            {
                return "Não há setores cadastrados para gerar relatório.";
            }
            
            StringBuilder relatorio = new StringBuilder();
            relatorio.AppendLine($"=== RELATÓRIO GERAL DE SETORES - {DateTime.Now.ToString("dd/MM/yyyy HH:mm")} ===");
            relatorio.AppendLine($"Total de Setores: {setores.Count}");
            
            int setoresCriticos = 0;
            int setoresComGerador = 0;
            int setoresEmEmergencia = 0;
            int setoresEmAlerta = 0;
            int setoresEstaveis = 0;
            
            foreach (var setor in setores)
            {
                if (setor is SetorCritico)
                {
                    setoresCriticos++;
                }
                
                if (setor.TemGerador)
                {
                    setoresComGerador++;
                }
                
                if (setor.PlanoEmergenciaAtivado)
                {
                    setoresEmEmergencia++;
                }
                else if (!setor.TemGerador)
                {
                    setoresEmAlerta++;
                }
                else
                {
                    setoresEstaveis++;
                }
            }
            
            relatorio.AppendLine($"Setores Críticos: {setoresCriticos}");
            relatorio.AppendLine($"Setores com Gerador: {setoresComGerador}");
            relatorio.AppendLine($"Setores em Estado de EMERGÊNCIA: {setoresEmEmergencia}");
            relatorio.AppendLine($"Setores em Estado de ALERTA: {setoresEmAlerta}");
            relatorio.AppendLine($"Setores ESTÁVEIS: {setoresEstaveis}");
            relatorio.AppendLine("\n=== DETALHES DOS SETORES ===");
            
            for (int i = 0; i < setores.Count; i++)
            {
                var setor = setores[i];
                string tipo = setor is SetorCritico ? "CRÍTICO" : "Normal";
                string status;
                
                if (setor.PlanoEmergenciaAtivado)
                {
                    status = "EMERGÊNCIA";
                }
                else if (setor.TemGerador)
                {
                    status = "Estável";
                }
                else
                {
                    status = "Alerta";
                }
                
                relatorio.AppendLine($"\n{i + 1}. {setor.NomeSetor} - Tipo: {tipo} - Status: {status}");
                relatorio.AppendLine($"   Gerador: {(setor.TemGerador ? "Sim" : "Não")}");
                relatorio.AppendLine($"   Plano de Emergência: {(setor.PlanoEmergenciaAtivado ? "ATIVADO" : "Desativado")}");
                
                if (setor is SetorCritico setorCritico)
                {
                    relatorio.AppendLine($"   Risco: {setorCritico.TipoRisco}");
                }
            }
            
            return relatorio.ToString();
        }
    }
}
