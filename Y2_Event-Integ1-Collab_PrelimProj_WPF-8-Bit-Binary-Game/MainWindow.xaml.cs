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

namespace Y2_Event_Integ1_Collab_PrelimProj_WPF_8_Bit_Binary_Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Window1 w1 = new Window1();
        //Window1 w2 = new Window1("Hello world");
        public MainWindow()
        {
            InitializeComponent();
            if (!WindowManager._mainWin)
            {
                WindowManager._mainWindow = this;
                WindowManager._mainWin = true;
            }
        }

        private void btnButton_Click(object sender, RoutedEventArgs e)
        {
            //w1.Show();
            //w2.Show();

            if (!WindowManager._gameWin)
            {
                WindowManager._gameWindow = new GameWindow();
                WindowManager._gameWin = true;
                WindowManager._gameWindow.Show();
                //this.Hide();
            }
            else
            {
                WindowManager._gameWindow.Focus();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Window1 w1 = new Window1();
            //w1.Show();

            WindowManager._mainWin = false;
        }
    }
}
