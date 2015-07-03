using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Collections.ObjectModel;

using ES2.Damas.Dominio.Entidades;
using ES2.Damas.Dominio.Regras;

namespace ES2.Damas.Teste
{
    using System.Windows;

    [TestFixture]
    public class MovimentoPeaoTeste
    {
        private ObservableCollection<PecaDama> _pecasTabuleiro;
        private PecaDama _pecaSelecionada;
        private List<PecaDama> _listaLugaresValidos = new List<PecaDama>();

        [Test]
        public void retorna_pecas_vizinhas_inimigas_a_partir_da_peca ()
        {
            
            this.CriarTabuleiroFake();

            PecaDama pecaTeste = new PecaDama { Pos = new Point(6, 6), Tipo = TipoPeca.Peao, Jogador = Jogador.Agil };

            RegraPeao regraPeao = new RegraPeao(_pecasTabuleiro, _listaLugaresValidos);

            List<PecaDama> pecasInimigas = regraPeao.RetornaPecasVizinhasInimigas(pecaTeste);

            List<PecaDama> pecasInimigasCandidatas = regraPeao.RetornaPecasComPossibilidadeEliminacao(pecaTeste, pecasInimigas);

            regraPeao.ExibirMovimentoAtaque(pecaTeste, pecasInimigasCandidatas);

        }


        public void CriarTabuleiroFake()
        {

            _pecasTabuleiro = new ObservableCollection<PecaDama>()
                            {
                                new PecaDama {Pos = new Point(0, 7), Tipo = TipoPeca.Peao, Jogador = Jogador.Classico},
                                new PecaDama {Pos = new Point(5, 5), Tipo = TipoPeca.Peao, Jogador = Jogador.Classico},
                             
                                new PecaDama {Pos = new Point(6, 6), Tipo = TipoPeca.Peao, Jogador = Jogador.Agil},

                            };

            
        }


    }
}
