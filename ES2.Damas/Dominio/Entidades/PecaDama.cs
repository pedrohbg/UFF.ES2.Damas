using System.Windows;
using GalaSoft.MvvmLight;


namespace ES2.Damas.Dominio.Entidades
{
    public class PecaDama : ViewModelBase
    {
        private Point _Pos;
        public Point Pos
        {
            get { return this._Pos; }
            set { this._Pos = value; RaisePropertyChanged(() => this.Pos); }
        }

        private TipoPeca _Tipo;
        public TipoPeca Tipo
        {
            get { return this._Tipo; }
            set { this._Tipo = value; RaisePropertyChanged(() => this.Tipo); }
        }

        private Jogador _Jogador;
        public Jogador Jogador
        {
            get { return this._Jogador; }
            set { this._Jogador = value; RaisePropertyChanged(() => this.Jogador); }
        }


        public PecaDama PecaAEliminar { get; set; }

        public bool IsDead { get; set; }
    }
}
