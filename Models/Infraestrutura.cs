using System;

namespace EnergyGuardian.Models
{
    public class Infraestrutura
    {
        public string NomeSetor { get; set; }
        public bool TemGerador { get; set; }
        public bool PlanoEmergenciaAtivado { get; private set; }        public Infraestrutura(string nomeSetor, bool temGerador)
        {
            NomeSetor = nomeSetor;
            TemGerador = temGerador;
            PlanoEmergenciaAtivado = false;
        }
          public virtual void AcionarPlanoEmergencia()
        {
            if (!PlanoEmergenciaAtivado)
            {
                PlanoEmergenciaAtivado = true;
                Console.WriteLine($"Plano de emergência acionado para {NomeSetor}.");
            }
            else
            {
                Console.WriteLine($"AVISO: O plano de emergência já está ativo para {NomeSetor}.");
            }
        }
        
        public virtual void DesativarPlanoEmergencia()
        {
            if (PlanoEmergenciaAtivado)
            {
                PlanoEmergenciaAtivado = false;
                Console.WriteLine($"Plano de emergência desativado para {NomeSetor}.");
            }
            else
            {
                Console.WriteLine($"AVISO: O plano de emergência já está desativado para {NomeSetor}.");
            }
        }
    }
}