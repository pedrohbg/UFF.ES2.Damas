using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using ES2.Damas.Dominio.Entidades;
using ES2.Damas.Dominio.Regras;

namespace ES2.Damas
{
    using System.Linq;
    using System.Threading;
    using System.Windows.Media.Animation;
    using System.Windows.Media.Imaging;
    using System.Windows.Threading;

    using ES2.Damas.Dominio.IA;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        private ObservableCollection<PecaDama> _pecasTabuleiro;
        private PecaDama _pecaSelecionada;
        private List<PecaDama> _listaLugaresValidos = new List<PecaDama>();
        private Grid _gridPecaSelecionada;
        

        private Turno _turnoTeste;

        private string _tipoJogo;

        private bool jogoEncerrado;


        //public MainWindow()
        //{
        //    InitializeComponent();
        //    _turnoTeste = new Turno();
        //    TrocarImagemTurno(_turnoTeste.ObterTurno());
        //    NovoJogo();
        //    ContarTempoJogada();

        //}
        
        public MainWindow(string tipoJogo)
        {
            InitializeComponent();


            _turnoTeste = new Turno();

            TrocarImagemTurno(_turnoTeste.ObterTurno());

            NovoJogo();

            ContarTempoJogada();

            _tipoJogo = tipoJogo;
        }

        public void MaquinaContraMaquinaIa()
        {
         
            while (!jogoEncerrado)
            {


                JogadorDamaIA(Jogador.Agil);
                TabuleiroDama.Refresh();
                System.Threading.Thread.Sleep(400);
                JogadorDamaIA(Jogador.Classico);
                TabuleiroDama.Refresh();
                System.Threading.Thread.Sleep(400);
            }

        }

        public void JogadorContraMaquinaIa()
        {

            if (_turnoTeste.ObterTurno() == Jogador.Agil)
            {
                TabuleiroDama.Refresh();
                System.Threading.Thread.Sleep(400);
                JogadorDamaIA(Jogador.Agil);
            }
            

        }

        private void ContarTempoJogada()
        {
            //_time = new TimeSpan();
            //_time = TimeSpan.FromSeconds(1);

            //_timer = new DispatcherTimer(
            //    new TimeSpan(0, 0, 1),
            //    DispatcherPriority.Normal,
            //    delegate
            //    {
            //        TempoJogada.Content = _time.ToString("c");
            //        if (_time == TimeSpan.Zero)
            //        {
            //            _timer.Stop();
            //        }
            //        _time = _time.Add(TimeSpan.FromSeconds(+1));
            //    },
            //    Application.Current.Dispatcher);

            //_timer.Start();
        }

        public void CanvasClick(object sender, MouseButtonEventArgs e)
        {
            switch (_tipoJogo)
            {
                case "jogador_vs_jogador":
                    break;
                case "jogador_vs_maquina":
                    JogadorContraMaquinaIa();
                    break;
                case "maquina_vs_maquina":
                    MaquinaContraMaquinaIa();
                    break;

            }

            
            return;

            Canvas canvas = (Canvas)sender;

            Point pontoEscolhido = Mouse.GetPosition(canvas);
            double pontoX = Math.Truncate(pontoEscolhido.X);
            double pontoY = Math.Truncate(pontoEscolhido.Y);
        }

        private void GridClick(object sender, MouseButtonEventArgs e)
        {

            Grid grid = (Grid)sender;
            PecaDama peca = (PecaDama)grid.DataContext;

            PecaClick2(peca, grid);

        }

        private void PecaClick2(PecaDama peca, Grid grid)
        {


            if (!TurnoCorreto(peca))
                return;

            if (EstaMovimentando(peca))
            {

                bool atacou = false;
                if (EstaAtacando(peca))
                {
                    RemoverPecaInimiga(peca.PecaAEliminar);
                    atacou = true;
                }
                MovimentarPeca(peca);

                LimparLugaresValidos();

                //verificando se ainda tem possibilidade de ataque
                if (atacou)
                {
                    List<PecaDama> pecasCandidatasEliminacao = ObterPecasCandidatasEliminacao(_pecaSelecionada);
                    if (pecasCandidatasEliminacao.Count > 0)
                    {
                        PecaClick2(_pecaSelecionada, grid);
                        return;
                    }
                    NumeroJogadasRestantes.Content = 20;
                }
                else
                {
                    NumeroJogadasRestantes.Content = Convert.ToInt32(NumeroJogadasRestantes.Content) - 1;
                }
                //-----------------------------

                RetirarDoTabuleiroPecasMortas();
                VerificarTransformacaoDama(_pecaSelecionada);
                TerminarJogadaTurnoAtual();
                SomarNumeroDeJogadas();
                ContarTempoJogada();

                VerificarFimDePartida();


            }
            else
            {
                if (!PecaAtacanteEJogoEmAtaque(peca))
                {
                    return;
                }

                SelecionarPeca(peca);
                LimparPinturaAreaSelecinada();
                PintarAreaSelecinada(grid);
                LimparLugaresValidos();
                CriarPossibilidadeMovimento(_pecaSelecionada, grid);
            }

        }



        public void JogadorDamaIA(Jogador jogador)
        {

            JogadorIA jogadorIa = new JogadorIA(jogador, _pecasTabuleiro, _listaLugaresValidos);
            List<PecaDama> pecas = jogadorIa.PecasComPossibilidadeDeMovimento();
            List<PecaDama> pecasAtaque = jogadorIa.PecasComPossibilidadeAtaque(_pecasTabuleiro.Where(x => x.Jogador == jogador).ToList());
            if (pecas.Count == 0 && pecasAtaque.Count == 0)
            {
                this.TerminarJogadaTurnoAtual();
                return;
            }

            Random rnd = new Random();


            PecaDama peca;
            int indice;
            if (pecasAtaque.Count > 0)
            {
                indice = rnd.Next(pecasAtaque.Count);
                peca = pecasAtaque[indice];
            }
            else
            {
                indice = rnd.Next(pecas.Count);
                peca = pecas[indice];
            }
            

            this.PecaClickIA(peca);
            if (_turnoTeste.ObterTurno() == jogador)
            {


                List<PecaDama> posicoesValidas = jogadorIa.LugaresValidos();
                if (posicoesValidas.Count == 0)
                {
                    return;
                }
                rnd = new Random();
                indice = rnd.Next(posicoesValidas.Count);
                PecaDama posicaoValida = posicoesValidas[indice];

                this.PecaClickIA(posicaoValida);
            }



        }

        private void PecaClickIA(PecaDama peca)
        {


            if (!TurnoCorreto(peca))
                return;

            if (EstaMovimentando(peca))
            {

                bool atacou = false;
                if (EstaAtacando(peca))
                {
                    RemoverPecaInimiga(peca.PecaAEliminar);
                    atacou = true;
                }
                MovimentarPecaIA(peca);

                LimparLugaresValidos();

                //verificando se ainda tem possibilidade de ataque
                if (atacou)
                {
                    List<PecaDama> pecasCandidatasEliminacao = ObterPecasCandidatasEliminacao(_pecaSelecionada);
                    if (pecasCandidatasEliminacao.Count > 0)
                    {
                        PecaClickIA(_pecaSelecionada);
                        return;
                    }
                    NumeroJogadasRestantes.Content = 20;
                }
                else
                {
                    NumeroJogadasRestantes.Content = Convert.ToInt32(NumeroJogadasRestantes.Content) - 1;
                }
                //-----------------------------

                RetirarDoTabuleiroPecasMortas();
                VerificarTransformacaoDama(_pecaSelecionada);
                TerminarJogadaTurnoAtual();
                SomarNumeroDeJogadas();
                ContarTempoJogada();

                VerificarFimDePartida();


            }
            else
            {
                if (!PecaAtacanteEJogoEmAtaque(peca))
                {
                    return;
                }

                SelecionarPeca(peca);
                LimparPinturaAreaSelecinada();
                //PintarAreaSelecinada(grid);
                LimparLugaresValidos();
                CriarPossibilidadeMovimento(_pecaSelecionada, null);

            }

        }

        public void VerificarFimDePartida()
        {
            //empate
            if (Convert.ToInt32(NumeroJogadasRestantes.Content) == 0)
            {
                MessageBox.Show("Jogo em Empate!");
                jogoEncerrado = true;
                this.Close();
            }
            else if (Convert.ToInt32(NumeroPecasAgil.Content) == 0)
            {
                MessageBox.Show("Clássico venceu!");
                jogoEncerrado = true;
                this.Close();
            }
            else if (Convert.ToInt32(NumeroPecasClassico.Content) == 0)
            {
                MessageBox.Show("Ágil venceu!");
                jogoEncerrado = true;
                this.Close();
            }

        }

        private void SomarNumeroDeJogadas()
        {
            NumeroJogadasTotal.Content = Convert.ToInt32(NumeroJogadasTotal.Content) + 1;
        }



        private void RetirarDoTabuleiroPecasMortas()
        {
            var pecasTabuleiroAux = new ObservableCollection<PecaDama>();
            foreach (var pecaDama in _pecasTabuleiro)
            {
                if (pecaDama.IsDead)
                {
                    pecasTabuleiroAux.Add(pecaDama);
                }
            }
            foreach (var pecaDama in pecasTabuleiroAux)
            {
                _pecasTabuleiro.Remove(pecaDama);
            }
        }

        private List<PecaDama> ObterPecasCandidatasEliminacao(PecaDama peca)
        {
            List<PecaDama> pecas = new List<PecaDama>();
            RegraPeao regraPeao = new RegraPeao(_pecasTabuleiro, _listaLugaresValidos);
            RegraDama regraDama = new RegraDama(_pecasTabuleiro, _listaLugaresValidos);

            switch (peca.Tipo)
            {
                case TipoPeca.Peao:
                    pecas = regraPeao.PecasCandidatasEliminacao(peca);
                    break;
                case TipoPeca.Dama:
                    pecas = regraDama.PecasCandidatasEliminacao(peca);
                    break;
            }
            return pecas;
        }

        private void TerminarJogadaTurnoAtual()
        {
            _turnoTeste.TerminarJogadaTurnoAtual();

            TrocarImagemTurno(_turnoTeste.ObterTurno());

            ExibirPecasEmJogo();

        }

        private void ExibirPecasEmJogo()
        {
            int pecasAgil = _pecasTabuleiro.Where(x => x.Jogador == Jogador.Agil).Count();
            int pecasClassico = _pecasTabuleiro.Where(x => x.Jogador == Jogador.Classico).Count();
            NumeroPecasAgil.Content = pecasAgil;
            NumeroPecasClassico.Content = pecasClassico;

            NumeroEliminadasAgil.Content = 12 - pecasAgil;
            NumeroEliminadasClassico.Content = 12 - pecasClassico;

        }

        private bool PecaAtacanteEJogoEmAtaque(PecaDama peca)
        {
            List<PecaDama> pecasEmAtaque = new RegraPeao(_pecasTabuleiro, _listaLugaresValidos).PecasEmAtaque(peca.Jogador);
            if (pecasEmAtaque.Count > 0 && !pecasEmAtaque.Exists(x => x == peca))
            {
                return false;
            }
            return true;
        }

        private void RemoverPecaInimiga(PecaDama pecaAEliminar)
        {
            pecaAEliminar.IsDead = true;
        }

        private bool EstaAtacando(PecaDama peca)
        {
            if (peca.PecaAEliminar != null)
            {
                return true;
            }
            return false;
        }

        private void LimparPinturaAreaSelecinada()
        {
            if (_gridPecaSelecionada != null)
            {
                _gridPecaSelecionada.Background = null;
            }

        }

        private bool TurnoCorreto(PecaDama peca)
        {
            if (peca.Jogador == Jogador.GM)
            {
                return true;
            }
            if (_turnoTeste.ObterTurno() != peca.Jogador)
            {
                return false;
            }
            return true;
        }

        private void MovimentarPeca(PecaDama peca)
        {
            _pecaSelecionada.Pos = peca.Pos;
            _gridPecaSelecionada.Background = null;
        }

        private void MovimentarPecaIA(PecaDama peca)
        {
            _pecaSelecionada.Pos = peca.Pos;
        }

        private void SelecionarPeca(PecaDama peca)
        {
            _pecaSelecionada = peca;
        }

        public bool EstaMovimentando(PecaDama peca)
        {
            if (peca.Tipo == TipoPeca.PosicaoValida)
            {
                return true;
            }
            return false;
        }


        private void CriarPossibilidadeMovimento(PecaDama peca, Grid grid)
        {
            RegraPeao regraPeao = new RegraPeao(_pecasTabuleiro, _listaLugaresValidos);
            RegraDama regraDama = new RegraDama(_pecasTabuleiro, _listaLugaresValidos);

            switch (peca.Tipo)
            {
                case TipoPeca.Peao:
                    regraPeao.ExibirPossibilidadeMovimento(peca);
                    break;
                case TipoPeca.Dama:
                    regraDama.ExibirPossibilidadeMovimento(peca);
                    break;
            }
        }

        private void PintarAreaSelecinada(Grid area)
        {
            area.Background = Brushes.Yellow;
            _gridPecaSelecionada = area;
        }

        private void TrocarImagemTurno(Jogador jogador)
        {
            if (jogador == Jogador.Agil)
            {
                BitmapImage image = new BitmapImage(new Uri("/Layout/Imagens/damaClara2.png", UriKind.Relative));
                ImagemTurno.Source = image;
            }
            else
            {
                BitmapImage image = new BitmapImage(new Uri("/Layout/Imagens/damaEscura2.png", UriKind.Relative));
                ImagemTurno.Source = image;

            }
        }

        private PecaDama VerificarTransformacaoDama(PecaDama pecaSelecionada)
        {
            if (pecaSelecionada.Jogador == Jogador.Classico && pecaSelecionada.Pos.Y.Equals(0) && pecaSelecionada.Tipo != TipoPeca.Dama)
            {
                pecaSelecionada = VirarDama(pecaSelecionada);
            }
            if (pecaSelecionada.Jogador == Jogador.Agil && pecaSelecionada.Pos.Y.Equals(7) && pecaSelecionada.Tipo != TipoPeca.Dama)
            {
                pecaSelecionada = VirarDama(pecaSelecionada);
            }


            return pecaSelecionada;

        }

        public PecaDama VirarDama(PecaDama peca)
        {

            peca.Tipo = TipoPeca.Dama;

            //var rotateAnimation = new DoubleAnimation(0, 360, TimeSpan.FromSeconds(1));
            //var rt = (RotateTransform)TabuleiroDama.RenderTransform;


            //rt.CenterX = 0.5;
            //rt.CenterY = 0.5;
            //rt.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);

            return peca;
        }

        private void LimparLugaresValidos()
        {
            foreach (PecaDama pd in _listaLugaresValidos)
            {
                _pecasTabuleiro.Remove(pd);
            }
            _listaLugaresValidos = new List<PecaDama>();
        }

        public void NovoJogo()
        {
            IniciarTabuleiro();
            
            ExibirPecasEmJogo();
            NumeroJogadasRestantes.Content = 20;
            NumeroJogadasTotal.Content = 0;
        }

        public void IniciarTabuleiro()
        {

            _pecasTabuleiro = new ObservableCollection<PecaDama>()
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

            TabuleiroDama.ItemsSource = _pecasTabuleiro;
        }


       

    }

    public static class ExtensionMethods
    {

        private static Action EmptyDelegate = delegate() { };


        public static void Refresh(this UIElement uiElement)
        {
            uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
        }
    }
}
