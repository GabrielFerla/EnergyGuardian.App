using System;

namespace EnergyGuardian.Models
{
    public class FalhaEnergia
    {
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public Infraestrutura SetorAfetado { get; set; }

        public FalhaEnergia(DateTime data, string descricao, Infraestrutura setorAfetado)
        {
            Data = data;
            Descricao = descricao;
            SetorAfetado = setorAfetado;
        }
    }
}