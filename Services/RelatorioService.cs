using System;
using EnergyGuardian.Models;

namespace EnergyGuardian.Services
{
    public static class RelatorioService
    {
        public static string GerarRelatorio(Infraestrutura setor)
        {
            return $"Relatório de Status:\nSetor: {setor.NomeSetor}\nTem Gerador: {(setor.TemGerador ? "Sim" : "Não")}\nStatus: {(setor.TemGerador ? "Estável" : "Alerta")}";
        }
    }
}
