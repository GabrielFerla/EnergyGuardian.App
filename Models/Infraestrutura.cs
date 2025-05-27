using System;

namespace EnergyGuardian.Models
{
    public class Infraestrutura
    {
        public string NomeSetor { get; set; }
        public bool TemGerador { get; set; }
        public bool PlanoEmergenciaAtivado { get; private set; }

        public Infraestrutura(string nomeSetor, bool temGerador)
        {
            NomeSetor = nomeSetor;
            TemGerador = temGerador;
            PlanoEmergenciaAtivado = false;
        }        public virtual void AcionarPlanoEmergencia()
        {
            PlanoEmergenciaAtivado = true;
            Console.WriteLine($"Plano de emergência acionado para {NomeSetor}.");
        }
        
        public virtual void DesativarPlanoEmergencia()
        {
            PlanoEmergenciaAtivado = false;
            Console.WriteLine($"Plano de emergência desativado para {NomeSetor}.");
        }
    }
}