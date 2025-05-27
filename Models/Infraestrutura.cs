namespace EnergyGuardian.Models
{
    public class Infraestrutura
    {
        public string NomeSetor { get; set; }
        public bool TemGerador { get; set; }

        public Infraestrutura(string nomeSetor, bool temGerador)
        {
            NomeSetor = nomeSetor;
            TemGerador = temGerador;
        }

        public virtual void AcionarPlanoEmergencia()
        {
            Console.WriteLine($"Plano de emergência acionado para {NomeSetor}.");
        }
    }
}