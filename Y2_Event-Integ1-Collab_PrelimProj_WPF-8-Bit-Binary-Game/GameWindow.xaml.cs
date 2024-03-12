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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Y2_Event_Integ1_Collab_PrelimProj_WPF_8_Bit_Binary_Game
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        DateTime startTime;
        DispatcherTimer _dt = null;
        Random _rnd = new Random();
        double _timeLeftInMilliseconds = 1000 * 60; // 1000 * Time in seconds
        bool _timer = true; // TRUE TEMPORARILY, CHANGE LATER

        int _currentLevel = 1;
        int _userScore = 0;

        bool _gameRestart = false;

        public GameWindow()
        {
            InitializeComponent();

            if (!WindowManager._mainWin)
            {
                WindowManager._gameWindow = this;
                WindowManager._gameWin = true;
            }

            StartGame();
        }

        public GameWindow(string difficulty)
        {
            InitializeComponent();

            if (!WindowManager._mainWin)
            {
                WindowManager._gameWindow = this;
                WindowManager._gameWin = true;
            }

            StartGame();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) // Do these even do anything
        {
            MessageBox.Show("Game Window is Closed");
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            WindowManager._gameWin = false;

            if (!WindowManager._mainWin)
            {
                WindowManager._mainWin = true;
                WindowManager._mainWindow = new MainWindow();
                WindowManager._mainWindow.Show();
            }
            else
            {
                WindowManager._mainWindow = new MainWindow();
                WindowManager._mainWindow.Show();
                WindowManager._gameWindow.Close();
            }

            this.Close();
        }

        private void StartGame()
        {
            btnSubmit.Visibility = Visibility.Collapsed;
            pbTimer1.Visibility = Visibility.Collapsed;
            pbTimer2.Visibility = Visibility.Collapsed;

            tbDecimalDisplay.Text = _rnd.Next(0, 256) + "";
        }

        private void ResetGame()
        {
            lbStatusHandler(3);
            tbDecimalDisplay.Text = _rnd.Next(0, 256) + "";

            ResetAllBinaryButtons();

            btnSubmit.Content = "Submit";
            btnSubmit.Margin = new Thickness(142, 342, 0, 0);
            btnSubmit.Width = 516;
            btnSubmit.HorizontalAlignment = HorizontalAlignment.Left;

            _currentLevel = 1;
            tbLevelDisplay.Text = "Level: " + _currentLevel;
            _userScore = 0;
            tbScoreDisplay.Text = "Score: " + _userScore;

            btnMega.Visibility = Visibility.Visible;
            btnMega.Content = "Click anywhere to start...";
            btnMega.FontSize = 48;

            btnExit.Visibility = Visibility.Collapsed;
            pbTimer1.Value = 0;
            pbTimer2.Value = 0;

            _gameRestart = false;
        }

        private async void btnMega_Click(object sender, RoutedEventArgs e)
        {
            btnMega.FontSize = 84; // Default is 48
            for (int x = 1; x >= 1; x--)
            {
                btnMega.Content = x.ToString();
                await Task.Delay(1000);
            }
            btnMega.FontSize = 48;

            btnMega.Visibility = Visibility.Hidden;
            btnSubmit.Visibility = Visibility.Visible;
            pbTimer1.Visibility = Visibility.Visible;
            pbTimer2.Visibility = Visibility.Visible;

            TimerStart();
        }

        private void _dt_Tick(object sender, EventArgs e)
        {
            if (_timer == true)
            {
                TimeSpan elapsedtime = DateTime.Now - startTime;
                double remainingMilliseconds = _timeLeftInMilliseconds - elapsedtime.TotalMilliseconds;

                pbTimer1.Value = (remainingMilliseconds / _timeLeftInMilliseconds) * 100;
                pbTimer2.Value = 100 - pbTimer1.Value;

                if (remainingMilliseconds <= 0)
                {
                    _dt.Stop();
                    checkWin();
                }
            }
        }

        private void TimerStart()
        {
            startTime = DateTime.Now;

            _dt = new DispatcherTimer();
            _dt.Interval = new TimeSpan(0, 0, 0, 0, 50);
            _dt.Tick += _dt_Tick;
            _dt.Start();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (_gameRestart)
                ResetGame();
            else
            {
                if (checkWin())
                {
                    _dt.Stop();

                    lbStatusHandler(1);

                    _timeLeftInMilliseconds = 1000 * 60;
                    pbTimer1.Value = 0;
                    pbTimer2.Value = 0;

                    _currentLevel++;
                    tbLevelDisplay.Text = "Level: " + _currentLevel;
                    _userScore++;
                    tbScoreDisplay.Text = "Score: " + _userScore;

                    tbDecimalDisplay.Text = _rnd.Next(0, 255) + "";

                    TimerStart();
                    ResetAllBinaryButtons();
                }
                else
                {
                    _dt.Stop();

                    lbStatusHandler(2);

                    _userScore = 0;
                    tbScoreDisplay.Text = "Score: " + _userScore;

                    btnSubmit.Margin = new Thickness(403, 342, 0, 0);
                    btnSubmit.Width = 255;
                    btnSubmit.HorizontalAlignment = HorizontalAlignment.Left;
                    btnSubmit.Content = "Try Again";

                    btnExit.Visibility = Visibility.Visible;

                    _gameRestart = true;
                }
            }
        }

        private void lbStatusHandler(int mode)
        {
            // Input 1, 2, 3 for mode, int mode, switch case for numbers

            string[] winMsg = new string[] { "Wow. Great Job!", "Masterfully done.", "Good Work!", "Your intelligence scares me.", "You did better than I thought!", "Wow that's crazy!", "Lets GOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO" };
            string[] loseMsg = new string[] { "Terrible. I overestimated you.", "What was that?", "You're actually throwing", "Nah aint no way you did that", "Try being better", "Skill Issue", "Are you even trying?" };
            string[] gameMsg = new string[] { "I believe in you.", "You got this!", "Keep going!" };

            if (mode == 1)
                lbStatus.Content = winMsg[_rnd.Next(0, winMsg.Length)];
            else if (mode == 2)
                lbStatus.Content = loseMsg[_rnd.Next(0, loseMsg.Length)];
            else if (mode == 3)
                lbStatus.Content = gameMsg[_rnd.Next(0, gameMsg.Length)];
        }

        private bool checkWin()
        {
            int[] answer = new int[8];
            int total = 0;

            #region Yanderedev Coding
            if ((String)btnBinary128.Content == "1")
                answer[0] = 128;
            if ((String)btnBinary64.Content == "1")
                answer[1] = 64;
            if ((String)btnBinary32.Content == "1")
                answer[2] = 32;
            if ((String)btnBinary16.Content == "1")
                answer[3] = 16;
            if ((String)btnBinary8.Content == "1")
                answer[4] = 8;
            if ((String)btnBinary4.Content == "1")
                answer[5] = 4;
            if ((String)btnBinary2.Content == "1")
                answer[6] = 2;
            if ((String)btnBinary1.Content == "1")
                answer[7] = 1;
            #endregion

            foreach (int a in answer)
                total += a;

            int goal = int.Parse(tbDecimalDisplay.Text);

            if (total == goal)
                return true;
            else // If wrong answer: Margin="403,342,0,0" Width="255" HorizontalAlignment="Left"
                 // If right answer: Margin="142,342,0,0" Width="516" HorizontalAlignment="Left"
                return false;
        }

        private void btnBinaryClick(object sender, RoutedEventArgs e)
        {
            string btnName = ((Button)sender).Name.ToString();

            switch (btnName)
            {
                case "btnBinary128":
                    BtnChangeAppearance(sender);
                    break;
                case "btnBinary64":
                    BtnChangeAppearance(sender);
                    break;
                case "btnBinary32":
                    BtnChangeAppearance(sender);
                    break;
                case "btnBinary16":
                    BtnChangeAppearance(sender);
                    break;
                case "btnBinary8":
                    BtnChangeAppearance(sender);
                    break;
                case "btnBinary4":
                    BtnChangeAppearance(sender);
                    break;
                case "btnBinary2":
                    BtnChangeAppearance(sender);
                    break;
                case "btnBinary1":
                    BtnChangeAppearance(sender);
                    break;
            }
        }

        private void BtnChangeAppearance(object sender)
        {
            Button button = (Button)sender;

            if (button.Content.ToString() == "0")
            {
                button.Content = "1";
                button.Background = Brushes.Orange;
                button.Foreground = Brushes.White;
                button.FontWeight = FontWeights.Bold;
            }
            else // Default values
                DefaultButtonAttributes(button);
        }

        private void ResetAllBinaryButtons()
        {
            Button[] binaryButtons = new Button[] { btnBinary128, btnBinary64, btnBinary32, btnBinary16, btnBinary8, btnBinary4, btnBinary2, btnBinary1 };

            foreach (Button button in binaryButtons)
                DefaultButtonAttributes(button);
        }

        private void DefaultButtonAttributes(Button button)
        {
            button.Content = "0";
            button.Background = Brushes.White;
            button.Foreground = Brushes.Black;
            button.FontWeight = FontWeights.Normal;
        }
    }
}
