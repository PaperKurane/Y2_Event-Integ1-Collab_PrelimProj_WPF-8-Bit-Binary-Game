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
        private string _difficulty = null;

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

            rbEasy.Visibility = Visibility.Visible;
            rbMedium.Visibility = Visibility.Visible;
            rbHard.Visibility = Visibility.Visible;

            lbDifficultyLabel.Visibility = Visibility.Visible;
            tbDifficultyDesc.Visibility = Visibility.Visible;

            btnContinue.Visibility = Visibility.Visible;
            btnBack.Visibility = Visibility.Visible;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            string rbName = ((RadioButton)sender).Name.ToString();
            
            switch (rbName)
            {
                case "rbEasy":
                    tbDifficultyDesc.Text = "Easy Mode\n- Chill, easy gameplay\n- Timer: 60 Seconds\n- Higher chance for easier numbers to appear.";
                    _difficulty = "Easy";
                    break;
                case "rbMedium":
                    tbDifficultyDesc.Text = "Medium Mode\n- The intended experience.\n- Timer: 45 Seconds";
                    _difficulty = "Medium";
                    break;
                case "rbHard":
                    tbDifficultyDesc.Text = "Hard Mode\n- May or may not be fun.\n- Timer: 30 Seconds\n- Binary labels are removed.\n- Harder numbers.";
                    _difficulty = "Hard";
                    break;
            }
        }

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            WindowManager._mainWin = false;

            WindowManager._gameWindow = new GameWindow(_difficulty);
            WindowManager._gameWindow.Show();

            WindowManager._mainWindow.Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            btnBack.Visibility = Visibility.Collapsed;

            btnStart.Visibility = Visibility.Visible;
            btnInstructions.Visibility = Visibility.Visible;
            btnQuitGame.Visibility = Visibility.Visible;

            rbEasy.Visibility = Visibility.Collapsed;
            rbMedium.Visibility = Visibility.Collapsed;
            rbHard.Visibility = Visibility.Collapsed;

            lbDifficultyLabel.Visibility = Visibility.Collapsed;
            tbDifficultyDesc.Visibility = Visibility.Collapsed;

            btnContinue.Visibility = Visibility.Collapsed;
        }

        private void btnQuitGame_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
