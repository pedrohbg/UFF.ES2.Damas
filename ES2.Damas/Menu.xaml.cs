using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ES2.Damas
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void JogadorVsJogador_Click(object sender, RoutedEventArgs e)
        {
            
            MainWindow janelaJogoDama = new MainWindow("jogador_vs_jogador");
            janelaJogoDama.Show();
            
        }

        private void JogadorVsMaquina_Click(object sender, RoutedEventArgs e)
        {
            
            MainWindow janelaJogoDama = new MainWindow("jogador_vs_maquina");
            janelaJogoDama.Show();

        }

        private void MaquinaVsMaquina_Click(object sender, RoutedEventArgs e)
        {
            
            MainWindow janelaJogoDama = new MainWindow("maquina_vs_maquina");
            janelaJogoDama.Show();

        }

        private void Regras_Click(object sender, RoutedEventArgs e)
        {
            //todo navegação por dentro

            RegrasWindow regras = new RegrasWindow();
            regras.Show();

        }

        private void Creditos_Click(object sender, RoutedEventArgs e)
        {
            //todo navegação por dentro

            Creditos creditos = new Creditos();
            creditos.Show();

        }
    }
}
