using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ES2.Damas.Dominio.Entidades;

namespace ES2.Damas.Dominio.Regras
{
    using System.Windows.Documents;

    public class Base
    {
        protected ObservableCollection<PecaDama> PecasTabuleiro;
        protected List<PecaDama> ListaLugaresValidos;

        public Base(ObservableCollection<PecaDama> pecasTabuleiro, List<PecaDama> listaLugaresValidos)
        {
            PecasTabuleiro = pecasTabuleiro;
            ListaLugaresValidos = listaLugaresValidos;
        }

        public static Jogador ObterTipoJogadorInimigo(Jogador jogador)
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

        
        public void CriarPosicaoValida(Point point)
        {
            if (PosicaoDentroDoTabuleiro(point))
            {
                PecaDama vPeca = new PecaDama();
                vPeca.Tipo = TipoPeca.PosicaoValida;
                vPeca.Jogador = Jogador.GM;
                vPeca.Pos = point;
                PecasTabuleiro.Add(vPeca);
                ListaLugaresValidos.Add(vPeca);
            }
        }

        public bool PosicaoDentroDoTabuleiro(Point point)
        {

            if ((point.X >= 0 && point.X < 8) && (point.Y >= 0 && point.Y < 8))
            {
                return true;
            }

            return false;
        }

        public void CriarPosicaoEliminacao(PecaDama pecaAEliminar, Point point)
        {
            if (PosicaoDentroDoTabuleiro(point))
            {
                PecaDama vPeca = new PecaDama();
                vPeca.Tipo = TipoPeca.PosicaoValida;
                vPeca.Jogador = Jogador.GM;
                vPeca.Pos = point;
                vPeca.PecaAEliminar = pecaAEliminar;
                PecasTabuleiro.Add(vPeca);
                ListaLugaresValidos.Add(vPeca);
            }
        }

        
        protected bool TemPeca(PecaDama peca, double posicaoXaValidar, double posicaoYaValidar)
        {
            bool temPeca =
                    PecasTabuleiro.Any(item => item.Pos.Equals(new Point(posicaoXaValidar, posicaoYaValidar)));
            return temPeca;
        }



    }
}
