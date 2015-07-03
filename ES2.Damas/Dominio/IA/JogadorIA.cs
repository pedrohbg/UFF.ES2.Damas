using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES2.Damas.Dominio.IA
{
    using System.Collections.ObjectModel;

    using ES2.Damas.Dominio.Entidades;
    using ES2.Damas.Dominio.Regras;

    public class JogadorIA
    {
        private Jogador _jogadorIA;
        private ObservableCollection<PecaDama> _pecasTabuleiro;
        private List<PecaDama> _listaLugaresValidos;

        public JogadorIA(Jogador jogador, ObservableCollection<PecaDama> pecasTabuleiro, List<PecaDama> listaLugaresValidos)
        {
            _pecasTabuleiro = pecasTabuleiro;
            _listaLugaresValidos = listaLugaresValidos;
            _jogadorIA = jogador;
        }


        public List<PecaDama> PecasComPossibilidadeDeMovimento()
        {
            var lista = new List<PecaDama>();
            RegraDama regraDama = new RegraDama(_pecasTabuleiro, _listaLugaresValidos);
            RegraPeao regraPeao = new RegraPeao(_pecasTabuleiro, _listaLugaresValidos);


            lista.AddRange(regraPeao.PecasComPossibilidadeMovimento(_jogadorIA));
            lista.AddRange(regraDama.PecasComPossibilidadeMovimento(_jogadorIA));


            return lista;
        }

        public List<PecaDama> LugaresValidos()
        {
            return _pecasTabuleiro.Where(x => x.Tipo == TipoPeca.PosicaoValida).ToList();
        }

        public List<PecaDama> PecasComPossibilidadeAtaque(List<PecaDama> pecas)
        {
            var lista = new List<PecaDama>();


            RegraDama regraDama = new RegraDama(_pecasTabuleiro, _listaLugaresValidos);
            RegraPeao regraPeao = new RegraPeao(_pecasTabuleiro, _listaLugaresValidos);

            foreach (var pecaDama in pecas)
            {

                List<PecaDama> listaCandidatas = new List<PecaDama>();

                if (pecaDama.Tipo == TipoPeca.Dama)
                {
                    listaCandidatas = regraDama.PecasCandidatasEliminacao(pecaDama);
                }
                else if (pecaDama.Tipo == TipoPeca.Peao)
                {
                    listaCandidatas = regraPeao.PecasCandidatasEliminacao(pecaDama);
                }

                if (listaCandidatas.Count > 0)
                {
                    lista.Add(pecaDama);
                }
            }
            return lista;

        }
    }
}
