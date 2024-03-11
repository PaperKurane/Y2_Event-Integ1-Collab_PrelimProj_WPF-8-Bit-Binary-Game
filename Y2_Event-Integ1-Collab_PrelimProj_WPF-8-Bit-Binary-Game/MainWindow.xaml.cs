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

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (!WindowManager._gameWin)
            {
                DifficultySelect();
            }
            else
            {
                WindowManager._gameWindow.Focus();
            }
        }

        private void DifficultySelect()
        {
            btnStart.Visibility = Visibility.Collapsed;
            btnInstructions.Visibility = Visibility.Collapsed;
            btnQuitGame.Visibility = Visibility.Collapsed;



            WindowManager._gameWindow = new GameWindow();
            WindowManager._gameWin = true;
            WindowManager._mainWin = false;
            WindowManager._gameWindow.Show();
            this.Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            btnBack.Visibility = Visibility.Collapsed;

            btnStart.Visibility = Visibility.Visible;
            btnInstructions.Visibility = Visibility.Visible;
            btnQuitGame.Visibility = Visibility.Visible;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) // Do these even do anything
        {
            //Window1 w1 = new Window1();
            //w1.Show();

            WindowManager._mainWin = false;
        }

        private void btnQuitGame_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
