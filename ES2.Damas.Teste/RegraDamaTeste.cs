using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES2.Damas.Teste
{
    using System.Collections.ObjectModel;
    using System.Windows;

    using ES2.Damas.Dominio.Entidades;
    using ES2.Damas.Dominio.Regras;

    using NUnit.Framework;

    [TestFixture]
    class RegraDamaTeste
    {

        [Test]
        public void ExibirMovimentoAtaque()
        {
            RegraDama regraDama = new RegraDama(GetPecasTabuleiro(), new List<PecaDama>());
            PecaDama peca = new PecaDama()
                {
                    Jogador = Jogador.Classico,
                    Pos = new Point(1, 0),
                    Tipo = TipoPeca.Dama,
                    IsDead = false,
                    PecaAEliminar = null,
                };


            List<PecaDama> pecasCandidatasEliminacao = new List<PecaDama>();

            PecaDama pecaNaLista = new PecaDama()
            {
                IsDead = false,
                Jogador = Jogador.Agil,
                PecaAEliminar = null,
                Pos = new Point(4, 3),
                Tipo = TipoPeca.Peao
            };
            pecasCandidatasEliminacao.Add(pecaNaLista);

            regraDama.ExibirMovimentoAtaque(peca, pecasCandidatasEliminacao);


        }
        [Test]
        public void RetornaPecasComPossibilidadeEliminacao()
        {
            RegraDama regraDama = new RegraDama(GetPecasTabuleiro(), new List<PecaDama>());
            PecaDama peca = new PecaDama()
            {
                IsDead = false,
                Jogador = Jogador.Agil,
                PecaAEliminar = null,
                Pos = new Point(2, 7),
                Tipo = TipoPeca.Dama
            };

            List<PecaDama> pecasCandidatasEliminacao = new List<PecaDama>();

            PecaDama pecaNaLista = new PecaDama()
            {
                IsDead = false,
                Jogador = Jogador.Classico,
                PecaAEliminar = null,
                Pos = new Point(4, 5),
                Tipo = TipoPeca.Peao

            };

            pecasCandidatasEliminacao.Add(pecaNaLista);

            regraDama.RetornaPecasComPossibilidadeEliminacao(peca, pecasCandidatasEliminacao);


        }

        public ObservableCollection<PecaDama> GetPecasTabuleiro()
        {

            return new ObservableCollection<PecaDama>()
                            {
                                new PecaDama {Pos = new Point(0, 7), Tipo = TipoPeca.Peao, Jogador = Jogador.Classico},
                                new PecaDama {Pos = new Point(2, 7), Tipo = TipoPeca.Peao, Jogador = Jogador.Classico},
                                
                                new PecaDama {Pos = new Point(4, 7), Tipo = TipoPeca.Peao, Jogador = Jogador.Classico},
                                new PecaDama {Pos = new Point(6, 7), Tipo = TipoPeca.Peao, Jogador = Jogador.Classico},

                                new PecaDama {Pos = new Point(1, 6), Tipo = TipoPeca.Peao, Jogador = Jogador.Classico},
                                new PecaDama {Pos = new Point(3, 6), Tipo = TipoPeca.Peao, Jogador = Jogador.Classico},
                                new PecaDama {Pos = new Point(5, 6), Tipo = TipoPeca.Peao, Jogador = Jogador.Classico},
                                new PecaDama {Pos = new Point(7, 6), Tipo = TipoPeca.Peao, Jogador = Jogador.Classico},

                                new PecaDama {Pos = new Point(0, 5), Tipo = TipoPeca.Peao, Jogador = Jogador.Classico},
                                new PecaDama {Pos = new Point(2, 5), Tipo = TipoPeca.Peao, Jogador = Jogador.Classico},
                                new PecaDama {Pos = new Point(4, 5), Tipo = TipoPeca.Peao, Jogador = Jogador.Classico},
                                new PecaDama {Pos = new Point(6, 5), Tipo = TipoPeca.Peao, Jogador = Jogador.Classico},

                                new PecaDama {Pos = new Point(1, 2), Tipo = TipoPeca.Peao, Jogador = Jogador.Agil},
                                new PecaDama {Pos = new Point(3, 2), Tipo = TipoPeca.Peao, Jogador = Jogador.Agil},
                                new PecaDama {Pos = new Point(5, 2), Tipo = TipoPeca.Peao, Jogador = Jogador.Agil},
                                new PecaDama {Pos = new Point(7, 2), Tipo = TipoPeca.Peao, Jogador = Jogador.Agil},

                                new PecaDama {Pos = new Point(0, 1), Tipo = TipoPeca.Peao, Jogador = Jogador.Agil},
                                new PecaDama {Pos = new Point(2, 1), Tipo = TipoPeca.Peao, Jogador = Jogador.Agil},
                                new PecaDama {Pos = new Point(4, 1), Tipo = TipoPeca.Peao, Jogador = Jogador.Agil},
                                new PecaDama {Pos = new Point(6, 1), Tipo = TipoPeca.Peao, Jogador = Jogador.Agil},
                                
                                new PecaDama {Pos = new Point(1, 0), Tipo = TipoPeca.Peao, Jogador = Jogador.Agil},
                                new PecaDama {Pos = new Point(3, 0), Tipo = TipoPeca.Peao, Jogador = Jogador.Agil},
                                new PecaDama {Pos = new Point(5, 0), Tipo = TipoPeca.Peao, Jogador = Jogador.Agil},
                                new PecaDama {Pos = new Point(7, 0), Tipo = TipoPeca.Peao, Jogador = Jogador.Agil},

                            };
        }
    }
}
