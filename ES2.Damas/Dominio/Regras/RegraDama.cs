using System.Collections.Generic;
using System.Collections.ObjectModel;
using ES2.Damas.Dominio.Entidades;
using System.Linq;
using System.Windows;


namespace ES2.Damas.Dominio.Regras
{


    public class RegraDama : Base
    {
        public RegraDama(ObservableCollection<PecaDama> pecasTabuleiro, List<PecaDama> listaLugaresValidos)
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
                ExibirMovimento(peca);
            }
        }

        public void ExibirMovimentoAtaque(PecaDama peca, List<PecaDama> pecasCandidatasEliminacao)
        {
            foreach (var pecaInimigaCandidata in pecasCandidatasEliminacao)
            {
                //nordeste
                if (peca.Pos.X > pecaInimigaCandidata.Pos.X
                    && peca.Pos.Y > pecaInimigaCandidata.Pos.Y)
                {

                    CriarPosicaoEliminacao(pecaInimigaCandidata, new Point(pecaInimigaCandidata.Pos.X - 1, pecaInimigaCandidata.Pos.Y - 1));
                }
                //sudeste
                if (peca.Pos.X > pecaInimigaCandidata.Pos.X
                    && peca.Pos.Y < pecaInimigaCandidata.Pos.Y)
                {

                    CriarPosicaoEliminacao(pecaInimigaCandidata, new Point(pecaInimigaCandidata.Pos.X - 1, pecaInimigaCandidata.Pos.Y + 1));
                }

                //noroeste
                if (peca.Pos.X < pecaInimigaCandidata.Pos.X
                    && peca.Pos.Y > pecaInimigaCandidata.Pos.Y)
                {

                    CriarPosicaoEliminacao(pecaInimigaCandidata, new Point(pecaInimigaCandidata.Pos.X + 1, pecaInimigaCandidata.Pos.Y - 1));

                }

                //sudoeste
                if (peca.Pos.X < pecaInimigaCandidata.Pos.X
                    && peca.Pos.Y < pecaInimigaCandidata.Pos.Y)
                {

                    CriarPosicaoEliminacao(pecaInimigaCandidata, new Point(pecaInimigaCandidata.Pos.X + 1, pecaInimigaCandidata.Pos.Y + 1));
                }
            }
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
                if (peca.Pos.X < pecaInimigaCandidata.Pos.X
                    && peca.Pos.Y < pecaInimigaCandidata.Pos.Y)
                {

                    pecas = PecasTabuleiro.Where(x => x.Pos.X == pecaInimigaCandidata.Pos.X + 1 && x.Pos.Y == pecaInimigaCandidata.Pos.Y + 1).ToList();
                    if ((!pecas.Any() && PosicaoDentroDoTabuleiro(new Point(pecaInimigaCandidata.Pos.X + 1, pecaInimigaCandidata.Pos.Y + 1))) || (pecas.Any() && pecas.First().Tipo == TipoPeca.PosicaoValida))
                        if (!pecaInimigaCandidata.IsDead)
                            pecasInimigasCandidatas.Add(pecaInimigaCandidata);

                }
                //sudeste
                if (peca.Pos.X < pecaInimigaCandidata.Pos.X
                    && peca.Pos.Y > pecaInimigaCandidata.Pos.Y)
                {
                    pecas = PecasTabuleiro.Where(x => x.Pos.X == pecaInimigaCandidata.Pos.X + 1 && x.Pos.Y == pecaInimigaCandidata.Pos.Y - 1).ToList();
                    if ((!pecas.Any() && PosicaoDentroDoTabuleiro(new Point(pecaInimigaCandidata.Pos.X + 1, pecaInimigaCandidata.Pos.Y - 1))) || (pecas.Any() && pecas.First().Tipo == TipoPeca.PosicaoValida))
                        if(!pecaInimigaCandidata.IsDead)
                        pecasInimigasCandidatas.Add(pecaInimigaCandidata);

                }

                //noroeste
                if (peca.Pos.X > pecaInimigaCandidata.Pos.X
                    && peca.Pos.Y < pecaInimigaCandidata.Pos.Y)
                {

                    pecas = PecasTabuleiro.Where(x => x.Pos.X == pecaInimigaCandidata.Pos.X - 1 && x.Pos.Y == pecaInimigaCandidata.Pos.Y + 1).ToList();
                    if ((!pecas.Any() && PosicaoDentroDoTabuleiro(new Point(pecaInimigaCandidata.Pos.X - 1, pecaInimigaCandidata.Pos.Y + 1))) || (pecas.Any() && pecas.First().Tipo == TipoPeca.PosicaoValida))
                        if (!pecaInimigaCandidata.IsDead)
                            pecasInimigasCandidatas.Add(pecaInimigaCandidata);

                }

                //sudoeste
                if (peca.Pos.X > pecaInimigaCandidata.Pos.X
                    && peca.Pos.Y > pecaInimigaCandidata.Pos.Y)
                {

                    pecas = PecasTabuleiro.Where(x => x.Pos.X == pecaInimigaCandidata.Pos.X - 1 && x.Pos.Y == pecaInimigaCandidata.Pos.Y - 1).ToList();
                    if ((!pecas.Any() && PosicaoDentroDoTabuleiro(new Point(pecaInimigaCandidata.Pos.X - 1, pecaInimigaCandidata.Pos.Y - 1)) ) || (pecas.Any() && pecas.First().Tipo == TipoPeca.PosicaoValida))
                        if  (!pecaInimigaCandidata.IsDead)
                            pecasInimigasCandidatas.Add(pecaInimigaCandidata);

                }

            }


            return pecasInimigasCandidatas;
        }

        private List<PecaDama> RetornaPecasVizinhasInimigas(PecaDama peca)
        {
            List<PecaDama> pecasVizinhasInimigas = new List<PecaDama>();

            IEnumerable<PecaDama> pecas;

            //nordeste
            for (int i = 1; i < 8; i++)
            {
                pecas = PecasTabuleiro.Where(x => x.Pos.X == peca.Pos.X + i && x.Pos.Y == peca.Pos.Y + i
                                                    && x.Jogador == ObterTipoJogadorInimigo(peca.Jogador)).ToList();
                if (pecas.Any())
                {
                    pecasVizinhasInimigas.Add(pecas.First());
                    break;
                }
                
            }

            //noroeste
            for (int i = 1; i < 8; i++)
            {
                pecas = PecasTabuleiro.Where(x => x.Pos.X == peca.Pos.X - i && x.Pos.Y == peca.Pos.Y + i
                                            && x.Jogador == ObterTipoJogadorInimigo(peca.Jogador)).ToList();
                if (pecas.Any())
                {
                    pecasVizinhasInimigas.Add(pecas.First());
                    break;
                }
                    
            }

            //sudeste
            for (int i = 1; i < 8; i++)
            {
                pecas = PecasTabuleiro.Where(x => x.Pos.X == peca.Pos.X + i
                                            && x.Pos.Y == peca.Pos.Y - i && x.Jogador == ObterTipoJogadorInimigo(peca.Jogador)).ToList();
                if (pecas.Any())
                {
                    pecasVizinhasInimigas.Add(pecas.First());
                    break;
                }
                    
            }

            //sudoeste
            for (int i = 1; i < 8; i++)
            {
                pecas = PecasTabuleiro.Where(x => x.Pos.X == peca.Pos.X - i && x.Pos.Y == peca.Pos.Y - i
                                            && x.Jogador == ObterTipoJogadorInimigo(peca.Jogador)).ToList();
                if (pecas.Any())
                {
                    pecasVizinhasInimigas.Add(pecas.First());
                    break;
                }
                    

            }

            return pecasVizinhasInimigas;
        }

        private void ExibirMovimento(PecaDama peca)
        {
            double xValido;
            double yValido;


            //nordeste
            for (int i = 1; i < 8; i++)
            {
                xValido = peca.Pos.X + i;
                yValido = peca.Pos.Y + i;

                if (!TemPeca(peca, xValido, yValido))
                {
                    CriarPosicaoValida(new Point(xValido, yValido));
                }
                else
                {
                    break;
                }

            }

            //noroeste
            for (int i = 1; i < 8; i++)
            {
                xValido = peca.Pos.X - i;
                yValido = peca.Pos.Y + i;

                if (!TemPeca(peca, xValido, yValido))
                {
                    CriarPosicaoValida(new Point(xValido, yValido));
                }
                else
                {
                    break;
                }
            }

            //sudeste
            for (int i = 1; i < 8; i++)
            {
                xValido = peca.Pos.X + i;
                yValido = peca.Pos.Y - i;

                if (!TemPeca(peca, xValido, yValido))
                {
                    CriarPosicaoValida(new Point(xValido, yValido));
                }
                else
                {
                    break;
                }
            }

            //sudoeste
            for (int i = 1; i < 8; i++)
            {
                xValido = peca.Pos.X - i;
                yValido = peca.Pos.Y - i;

                if (!TemPeca(peca, xValido, yValido))
                {
                    CriarPosicaoValida(new Point(xValido, yValido));
                }
                else
                {
                    break;
                }
            }


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
                if (peca.Tipo != TipoPeca.Dama)
                {
                    continue;
                }

                //nordeste
                for (int i = 1; i < 8; i++)
                {
                    xValido = peca.Pos.X + i;
                    yValido = peca.Pos.Y + i;

                    if (!TemPeca(peca, xValido, yValido))
                    {
                        pecas.Add(peca);
                    }
                    else
                    {
                        break;
                    }

                }

                //noroeste
                for (int i = 1; i < 8; i++)
                {
                    xValido = peca.Pos.X - i;
                    yValido = peca.Pos.Y + i;

                    if (!TemPeca(peca, xValido, yValido))
                    {
                        pecas.Add(peca);
                    }
                    else
                    {
                        break;
                    }
                }

                //sudeste
                for (int i = 1; i < 8; i++)
                {
                    xValido = peca.Pos.X + i;
                    yValido = peca.Pos.Y - i;

                    if (!TemPeca(peca, xValido, yValido))
                    {
                        pecas.Add(peca);
                    }
                    else
                    {
                        break;
                    }
                }

                //sudoeste
                for (int i = 1; i < 8; i++)
                {
                    xValido = peca.Pos.X - i;
                    yValido = peca.Pos.Y - i;

                    if (!TemPeca(peca, xValido, yValido))
                    {
                        pecas.Add(peca);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return pecas;

        }
    }
}
