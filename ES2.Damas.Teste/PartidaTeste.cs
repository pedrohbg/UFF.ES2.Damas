using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES2.Damas.Teste
{
    using System.Windows;

    using NUnit.Framework;

    [TestFixture]
    class PartidaTeste
    {
        
        [Test, RequiresSTA]
        public void VerificarFimDePartida()
        {
            MainWindow main = new MainWindow("jogador_vs_jogador");
            main.VerificarFimDePartida();

        }

        [Test, RequiresSTA]
        public void VerificarMaquinaContraMaquina()
        {
            MainWindow main = new MainWindow("maquina_vs_maquina");
            main.CanvasClick(null,null);

        }
    }
}
