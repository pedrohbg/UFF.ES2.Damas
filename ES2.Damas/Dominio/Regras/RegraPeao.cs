using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using ES2.Damas.Dominio.Entidades;
using System.Windows;

namespace ES2.Damas.Dominio.Regras
{
   
    public class RegraPeao : Base
    {

        public RegraPeao(ObservableCollection<PecaDama> pecasTabuleiro, List<PecaDama> listaLugaresValidos)
            : base(pecasTabuleiro, listaLugaresValidos)
        {
        }

        public void ExibirPossibilidadeMovimento(PecaDama peca)
        {
            List<PecaDama> pecasCandidatasEliminacao = PecasCandidatasEliminacao(peca);

            //movimento de eliminacao
            if (pecasCandidatasEliminacao.Any())
            {
                ExibirMovimentoAtaque(peca, pecasCandidatasEliminacao);
            }
            else//movimento normal
            {
                ExibirMovimentoParaFrente(peca);
            }


        }

        public List<PecaDama> PecasEmAtaque(Jogador jogador)
        {
            List<PecaDama> pecasEmAtaque = new List<PecaDama>();
            List<PecaDama> listaDePecaDoJogador = new List<PecaDama>();
            listaDePecaDoJogador = PecasTabuleiro.Where(x => x.Jogador == jogador).ToList();

            foreach (var pecaDama in listaDePecaDoJogador)
            {

                List<PecaDama> pecasVizinhasInimigas = RetornaPecasVizinhasInimigas(pecaDama);

                List<PecaDama> pecasCandidatasEliminacao = RetornaPecasComPossibilidadeEliminacao(
                    pecaDama, pecasVizinhasInimigas);

                if (pecasCandidatasEliminacao.Any())
                {
                    pecasEmAtaque.Add(pecaDama);
                }

            }

            return pecasEmAtaque;
        }

        public void ExibirMovimentoAtaque(PecaDama peca, List<PecaDama> pecasCandidatasEliminacao)
        {

            foreach (var pecaInimigaCandidata in pecasCandidatasEliminacao)
            {
                //nordeste
                if (peca.Pos.X + 1 == pecaInimigaCandidata.Pos.X
                    && peca.Pos.Y + 1 == pecaInimigaCandidata.Pos.Y)
                {

                    CriarPosicaoEliminacao(pecaInimigaCandidata, new Point(peca.Pos.X + 2, peca.Pos.Y + 2));
                }
                //sudeste
                if (peca.Pos.X + 1 == pecaInimigaCandidata.Pos.X
                    && peca.Pos.Y - 1 == pecaInimigaCandidata.Pos.Y)
                {

                    CriarPosicaoEliminacao(pecaInimigaCandidata, new Point(peca.Pos.X + 2, peca.Pos.Y - 2));
                }

                //noroeste
                if (peca.Pos.X - 1 == pecaInimigaCandidata.Pos.X
                    && peca.Pos.Y + 1 == pecaInimigaCandidata.Pos.Y)
                {

                    CriarPosicaoEliminacao(pecaInimigaCandidata, new Point(peca.Pos.X - 2, peca.Pos.Y + 2));

                }

                //sudoeste
                if (peca.Pos.X - 1 == pecaInimigaCandidata.Pos.X
                    && peca.Pos.Y - 1 == pecaInimigaCandidata.Pos.Y)
                {

                    CriarPosicaoEliminacao(pecaInimigaCandidata, new Point(peca.Pos.X - 2, peca.Pos.Y - 2));
                }
            }
        }

        private void ExibirMovimentoParaFrente(PecaDama peca)
        {
            double xValido;
            double yValido;

            if (peca.Jogador == Jogador.Agil)
            {
                yValido = peca.Pos.Y + 1;
            }
            else
            {
                yValido = peca.Pos.Y - 1;
            }
            xValido = peca.Pos.X - 1;
            if (!TemPeca(peca, xValido, yValido))
            {
                CriarPosicaoValida(new Point(xValido, yValido));

            }
            xValido = peca.Pos.X + 1;
            if (!TemPeca(peca, xValido, yValido))
            {
                CriarPosicaoValida(new Point(xValido, yValido));

            }
        }

        public List<PecaDama> RetornaPecasVizinhasInimigas(PecaDama peca)
        {

            List<PecaDama> pecasVizinhasInimigas = new List<PecaDama>();

            IEnumerable<PecaDama> pecas = PecasTabuleiro.Where(x => x.Pos.X == peca.Pos.X - 1 && x.Pos.Y == peca.Pos.Y + 1 && x.Jogador == ObterTipoJogadorInimigo(peca.Jogador)).ToList();
            if (pecas.Any())
                pecasVizinhasInimigas.Add(pecas.First());

            pecas = PecasTabuleiro.Where(x => x.Pos.X == peca.Pos.X + 1 && x.Pos.Y == peca.Pos.Y - 1 && x.Jogador == ObterTipoJogadorInimigo(peca.Jogador)).ToList();
            if (pecas.Any())
                pecasVizinhasInimigas.Add(pecas.First());

            pecas = PecasTabuleiro.Where(x => x.Pos.X == peca.Pos.X + 1 && x.Pos.Y == peca.Pos.Y + 1 && x.Jogador == ObterTipoJogadorInimigo(peca.Jogador)).ToList();
            if (pecas.Any())
                pecasVizinhasInimigas.Add(pecas.First());

            pecas = PecasTabuleiro.Where(x => x.Pos.X == peca.Pos.X - 1 && x.Pos.Y == peca.Pos.Y - 1 && x.Jogador == ObterTipoJogadorInimigo(peca.Jogador)).ToList();
            if (pecas.Any())
                pecasVizinhasInimigas.Add(pecas.First());


            return pecasVizinhasInimigas;
        }

        public List<PecaDama> RetornaPecasComPossibilidadeEliminacao(PecaDama peca, List<PecaDama> pecasVizinhasInimigas)
        {
            List<PecaDama> pecasInimigasCandidatas = new List<PecaDama>();
            if (pecasVizinhasInimigas.Count == 0)
                return pecasInimigasCandidatas;

            IEnumerable<PecaDama> pecas;

            foreach (var pecaInimigaCandidata in pecasVizinhasInimigas)
            {
                //nordeste
                if (peca.Pos.X + 1 == pecaInimigaCandidata.Pos.X
                    && peca.Pos.Y + 1 == pecaInimigaCandidata.Pos.Y)
                {

                    pecas = PecasTabuleiro.Where(x => x.Pos.X == peca.Pos.X + 2 && x.Pos.Y == peca.Pos.Y + 2).ToList();
                    if ((!pecas.Any() && PosicaoDentroDoTabuleiro(new Point(peca.Pos.X + 2, peca.Pos.Y + 2))) || (pecas.Any() && pecas.First().Tipo == TipoPeca.PosicaoValida))
                        if (!pecaInimigaCandidata.IsDead)
                            pecasInimigasCandidatas.Add(pecaInimigaCandidata);

                }
                //sudeste
                if (peca.Pos.X + 1 == pecaInimigaCandidata.Pos.X
                    && peca.Pos.Y - 1 == pecaInimigaCandidata.Pos.Y)
                {
                    pecas = PecasTabuleiro.Where(x => x.Pos.X == peca.Pos.X + 2 && x.Pos.Y == peca.Pos.Y - 2).ToList();
                    if ((!pecas.Any() && PosicaoDentroDoTabuleiro(new Point(peca.Pos.X + 2, peca.Pos.Y - 2))) || (pecas.Any() && pecas.First().Tipo == TipoPeca.PosicaoValida))
                        if (!pecaInimigaCandidata.IsDead)
                            pecasInimigasCandidatas.Add(pecaInimigaCandidata);

                }

                //noroeste
                if (peca.Pos.X - 1 == pecaInimigaCandidata.Pos.X
                    && peca.Pos.Y + 1 == pecaInimigaCandidata.Pos.Y)
                {

                    pecas = PecasTabuleiro.Where(x => x.Pos.X == peca.Pos.X - 2 && x.Pos.Y == peca.Pos.Y + 2).ToList();
                    if ((!pecas.Any() && PosicaoDentroDoTabuleiro(new Point(peca.Pos.X - 2, peca.Pos.Y + 2))) || (pecas.Any() && pecas.First().Tipo == TipoPeca.PosicaoValida))
                        if (!pecaInimigaCandidata.IsDead)
                            pecasInimigasCandidatas.Add(pecaInimigaCandidata);

                }

                //sudoeste
                if (peca.Pos.X - 1 == pecaInimigaCandidata.Pos.X
                    && peca.Pos.Y - 1 == pecaInimigaCandidata.Pos.Y)
                {

                    pecas = PecasTabuleiro.Where(x => x.Pos.X == peca.Pos.X - 2 && x.Pos.Y == peca.Pos.Y - 2).ToList();
                    if ((!pecas.Any() && PosicaoDentroDoTabuleiro(new Point(peca.Pos.X - 2, peca.Pos.Y - 2))) || (pecas.Any() && pecas.First().Tipo == TipoPeca.PosicaoValida))
                        if (!pecaInimigaCandidata.IsDead)
                            pecasInimigasCandidatas.Add(pecaInimigaCandidata);

                }

            }


            return pecasInimigasCandidatas;
        }

        public List<PecaDama> PecasCandidatasEliminacao(PecaDama peca)
        {
            List<PecaDama> pecasVizinhasInimigas = RetornaPecasVizinhasInimigas(peca);
            List<PecaDama> pecasCandidatasEliminacao = RetornaPecasComPossibilidadeEliminacao(peca, pecasVizinhasInimigas);

            return pecasCandidatasEliminacao;
        }

        public List<PecaDama> PecasComPossibilidadeMovimento(Jogador jogador)
        {
            List<PecaDama> pecas = new List<PecaDama>();
            double xValido;
            double yValido;

            foreach (var peca in PecasTabuleiro.Where(x => x.Jogador == jogador).ToList())
            {

                if (peca.Jogador == Jogador.Agil)
                {
                    yValido = peca.Pos.Y + 1;
                }
                else
                {
                    yValido = peca.Pos.Y - 1;
                }

                if ((!TemPeca(peca, peca.Pos.X - 1, yValido) && this.PosicaoDentroDoTabuleiro(new Point(peca.Pos.X - 1, yValido))) || (!TemPeca(peca, peca.Pos.X + 1, yValido) && this.PosicaoDentroDoTabuleiro(new Point(peca.Pos.X + 1, yValido))))
                {
                    pecas.Add(peca);

                }

            }
            return pecas;
        }


    }
}
