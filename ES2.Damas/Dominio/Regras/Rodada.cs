using System;

using ES2.Damas.Dominio.Entidades;

namespace ES2.Damas.Dominio.Regras
{
    public class Rodada
    {
        private int TotalRodadas;
        private int Turno;

        public static int JogadasAgil { get; private set; }
        public static int JogadasClassico { get; private set; }

        public static int NumeroJogadasRestantes { get; private set; }

        public static int PecasAgilEmJogo { get; private set; }

        public static int PecasClassicoEmJogo { get; private set; }
        
        public Rodada()
        {
            //NumeroJogadasRestantes = 20;
            //PecasAgilEmJogo = 12;
            //PecasClassicoEmJogo = 12;
            //TotalRodadas = 0;
            //Turno = 1;
        }

        
    }
}
