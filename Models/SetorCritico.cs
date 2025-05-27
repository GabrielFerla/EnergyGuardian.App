namespace EnergyGuardian.Models
{
    public class SetorCritico : Infraestrutura
    {
        public string TipoRisco { get; set; }

        public SetorCritico(string nomeSetor, bool temGerador, string tipoRisco)
            : base(nomeSetor, temGerador)
        {
            TipoRisco = tipoRisco;
        }

        public override void AcionarPlanoEmergencia()
        {
            Console.WriteLine($"[CRÍTICO] Risco de {TipoRisco} no setor {NomeSetor}!");
        }
    }
}
