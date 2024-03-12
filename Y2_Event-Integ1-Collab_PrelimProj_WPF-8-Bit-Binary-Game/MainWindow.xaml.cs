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
                    tbDifficultyDesc.Text = "Easy Mode&#x0a;- The easiest mode.&#x0a;- Timer: 60 Seconds&#x0a;- Chill, easy gameplay.";
                    _difficulty = "Easy";
                    break;
                case "rbMedium":
                    tbDifficultyDesc.Text = "Medium Mode&#x0a;- The standard mode.&#x0a;- Timer: 45 Seconds&#x0a;- The intended experience.";
                    _difficulty = "Medium";
                    break;
                case "rbHard":
                    tbDifficultyDesc.Text = "Hard Mode&#x0a;- May or may not be fun.&#x0a;- Timer: 30 Seconds&#x0a;- Binary labels are removed.";
                    _difficulty = "Hard";
                    break;
            }
        }

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            WindowManager._gameWindow = new GameWindow(_difficulty); // Make this a constructor
            WindowManager._gameWin = true;
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) // Do these even do anything
        {
            MessageBox.Show("Main Manu Window is Closed");
        }

        private void btnQuitGame_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
