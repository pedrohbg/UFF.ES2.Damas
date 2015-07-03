using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ES2.Damas.Dominio.Entidades;

namespace ES2.Damas.Dominio.Regras
{
    
    public class Turno
    {
        private Jogador _turnoAtual;

        public Turno()
        {
            _turnoAtual = Jogador.Agil;
        }


        public Jogador ObterTurno()
        {
            return _turnoAtual;
        }

        public void TerminarJogadaTurnoAtual()
        {
            _turnoAtual = this.ObterJogadorContrario(_turnoAtual);
        }

        protected Jogador ObterJogadorContrario(Jogador jogador)
        {
            if (jogador == Jogador.Agil)
            {
                return Jogador.Classico;
            }
            else
            {
                return Jogador.Agil;
            }
        }



    }
}
