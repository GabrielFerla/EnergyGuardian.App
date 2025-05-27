using System;
using EnergyGuardian.Models;

namespace EnergyGuardian.Services
{
    public static class Logger
    {
        public static void RegistrarEvento(FalhaEnergia falha)
        {
            Console.WriteLine($"[LOG] {falha.Data} - Falha registrada no setor {falha.SetorAfetado.NomeSetor}: {falha.Descricao}");
        }
    }
}