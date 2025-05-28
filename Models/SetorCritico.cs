using System;

namespace EnergyGuardian.Models
{
    public class SetorCritico : Infraestrutura
    {
        public string TipoRisco { get; set; }

        public SetorCritico(string nomeSetor, bool temGerador, string tipoRisco)
            : base(nomeSetor, temGerador)
        {
            TipoRisco = tipoRisco;
        }        public override void AcionarPlanoEmergencia()
        {
            if (!PlanoEmergenciaAtivado)
            {
                base.AcionarPlanoEmergencia();
                Console.WriteLine($"[CRÍTICO] Risco de {TipoRisco} no setor {NomeSetor}!");
            }
            else
            {
                Console.WriteLine($"AVISO: O plano de emergência já está ativo para o setor crítico {NomeSetor}.");
            }
        }        public override void DesativarPlanoEmergencia()
        {
            if (PlanoEmergenciaAtivado)
            {
                base.DesativarPlanoEmergencia();
                Console.WriteLine($"[CRÍTICO] Situação de risco de {TipoRisco} controlada no setor {NomeSetor}.");
            }
            else
            {
                Console.WriteLine($"AVISO: O plano de emergência já está desativado para o setor crítico {NomeSetor}.");
            }
        }
    }
}
