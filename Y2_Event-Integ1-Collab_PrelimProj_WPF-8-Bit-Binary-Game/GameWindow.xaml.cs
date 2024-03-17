using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// 
    /// dOES SIR CONSIDER MUTING AN OPTION
    /// </summary>
    public partial class GameWindow : Window
    {
        DateTime _startTime;
        DateTime _sessionStartTime;
        TimeSpan _totalTimeSpent;
        DispatcherTimer _dt = null;
        DispatcherTimer _sessionTimer = null;
        Random _rnd = new Random();
        double _timeLeftInMilliseconds = 0;

        string _difficulty = null;
        int _currentLevel = 1;
        int _userScore = 0;

        bool _gameRestart = false;

        SoundSystem sound = new SoundSystem();

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

            _difficulty = difficulty;

            if (!WindowManager._mainWin)
            {
                WindowManager._gameWindow = this;
                WindowManager._gameWin = true;
            }

            StartGame();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            WindowManager._gameWin = false;

            if (!WindowManager._mainWin)
            {
                UserDetails();
                gMainGame.Visibility = Visibility.Collapsed;
            }
            else
            {
                WindowManager._mainWindow.Show();
                WindowManager._gameWindow.Close();
            }
        }
        
        private void UserDetails()
        {
            lbTime.Content = "Time: " + _totalTimeSpent.ToString(@"mm\:ss\.fff");
            lbScore.Content = "Score: " + _userScore;
        }

        private void StartGame()
        {
            btnSubmit.Visibility = Visibility.Collapsed;
            pbTimer1.Visibility = Visibility.Collapsed;
            pbTimer2.Visibility = Visibility.Collapsed;

            DifficultySetter();
        }

        private void DifficultySetter()
        {
            switch (_difficulty)
            {
                case "Easy":
                    _timeLeftInMilliseconds = 1000 * 60;
                    DecimalChances();
                    break;
                case "Medium":
                    _timeLeftInMilliseconds = 1000 * 45;
                    DecimalChances();
                    break;
                case "Hard":
                    _timeLeftInMilliseconds = 1000 * 30;
                    imgCautionBlocker.Visibility = Visibility.Visible;
                    DecimalChances();
                    break;
            }
        }

        private void DecimalChances()
        {
            switch (_difficulty)
            {
                case "Easy":
                    List<int> easyNums = new List<int> {15, 25, 35, 45, 55, 65, 75, 85, 96};

                    for (int x = 1; x < 129; x++)
                    {
                        if (x % 2 == 0)
                            easyNums.Add(x);
                    }

                    if (_rnd.Next(1, 3) == 1) // 50% chance for one of these numbers to appear
                        tbDecimalDisplay.Text = easyNums[_rnd.Next(1, easyNums.Count)].ToString();
                    else
                        tbDecimalDisplay.Text = _rnd.Next(1, 129).ToString();
                    break;

                case "Hard":
                    List<int> hardNums = new List<int> { 47, 91, 167, 189, 201, 226, 249 };

                    if (_rnd.Next(1, 3) == 1) // 50% chance for one of these numbers to appear
                        tbDecimalDisplay.Text = hardNums[_rnd.Next(1, hardNums.Count)].ToString();
                    else
                        tbDecimalDisplay.Text = _rnd.Next(1, 256).ToString();
                    break;

                default:
                    tbDecimalDisplay.Text = _rnd.Next(1, 256).ToString();
                    break;
            }
        }

        private void ResetGame()
        {
            imgCautionBlocker.Visibility = Visibility.Collapsed;

            if (_gameRestart == true)
                DifficultySetter();

            lbStatusHandler(3);

            sound.Resume("Pull The Trigger - 8-Bit VRC6.wav");

            DisableAllBinaryButtons(false);
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

        private void SessionTimer()
        {
            _sessionStartTime = DateTime.Now;

            _sessionTimer = new DispatcherTimer();
            _sessionTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            _sessionTimer.Tick += SessionTimer_Tick;
            _sessionTimer.Start();
        }

        private void SessionTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsedTime = DateTime.Now - _sessionStartTime;
            _totalTimeSpent += elapsedTime; // Accumulate the total time spent
            _sessionStartTime = DateTime.Now; // Reset the start time for the next interval
        }

        private async void btnMega_Click(object sender, RoutedEventArgs e)
        {
            btnMega.FontSize = 84; // Default is 48
            for (int x = 3; x >= 1; x--)
            {
                btnMega.Content = x.ToString();
                sound.Initialize(x + ".wav", 5, false);
                await Task.Delay(1000);
            }
            btnMega.FontSize = 48;

            btnMega.Visibility = Visibility.Hidden;
            btnSubmit.Visibility = Visibility.Visible;
            pbTimer1.Visibility = Visibility.Visible;
            pbTimer2.Visibility = Visibility.Visible;

            TimerStart();
            SessionTimer();
        }

        private void TimerStart()
        {
            _startTime = DateTime.Now;

            _dt = new DispatcherTimer();
            _dt.Interval = new TimeSpan(0, 0, 0, 0, 50);
            _dt.Tick += _dt_Tick;
            _dt.Start();
        }

        private void _dt_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsedtime = DateTime.Now - _startTime;
            double remainingMilliseconds = _timeLeftInMilliseconds - elapsedtime.TotalMilliseconds;

            tbTimerDisplay.Text = ((int)(remainingMilliseconds / 1000)).ToString("F0");

            pbTimer1.Value = (remainingMilliseconds / _timeLeftInMilliseconds) * 100;
            pbTimer2.Value = 100 - pbTimer1.Value;

            if (remainingMilliseconds <= 0)
            {
                _dt.Stop();
                AnswerParser();
            }
        }

        private void TimeReductionLogic()
        {
            if (_currentLevel < 12)
            {
                if (_currentLevel != 1)
                {
                    double currSeconds = _timeLeftInMilliseconds / 1000;
                    double newSeconds = Math.Round(currSeconds * 0.06);
                    newSeconds = currSeconds - newSeconds;
                    _timeLeftInMilliseconds = 1000 * newSeconds;
                }
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (_gameRestart)
                ResetGame();
            else
                AnswerParser();
        }

        private void AnswerParser()
        {
            if (checkWin())
            {
                _dt.Stop();

                lbStatusHandler(1);

                TimeReductionLogic();
                pbTimer1.Value = 0;
                pbTimer2.Value = 0;

                _currentLevel++;
                tbLevelDisplay.Text = "Level: " + _currentLevel;
                _userScore++;
                tbScoreDisplay.Text = "Score: " + _userScore;

                DecimalChances();

                ResetAllBinaryButtons();
                TimerStart();
            }
            else
            {
                _dt.Stop();
                _sessionTimer.Stop();

                sound.Pause("Pull The Trigger - 8-Bit VRC6.wav");
                sound.Initialize("yikes.wav", 5, false);

                DisableAllBinaryButtons(true);

                lbStatusHandler(2);

                btnSubmit.Margin = new Thickness(395, 342, 0, 0);
                btnSubmit.Width = 255;
                btnSubmit.HorizontalAlignment = HorizontalAlignment.Left;
                btnSubmit.Content = "Try Again";

                btnExit.Visibility = Visibility.Visible;

                _gameRestart = true;
            }
        }

        private void lbStatusHandler(int mode)
        {
            string[] winMsg = new string[] { "Wow. Great Job!", "Masterfully done.", "Good Work!", "Your intelligence scares me.", "You did better than I thought!", "Wow that's crazy!", "Lets GOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO" };
            string[] loseMsg = new string[] { "Terrible. I overestimated you.", "What was that?", "You're actually throwing", "Nah aint no way you did that", "Try being better", "Skill Issue", "Are you even trying?" };
            string[] gameMsg = new string[] { "I believe in you.", "You got this!", "Keep going!" };

            if (mode == 1)
                lbStatus.Content = winMsg[_rnd.Next(1, winMsg.Length)];
            else if (mode == 2)
                lbStatus.Content = loseMsg[_rnd.Next(1, loseMsg.Length)];
            else if (mode == 3)
                lbStatus.Content = gameMsg[_rnd.Next(1, gameMsg.Length)];
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
            else
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
            else
                DefaultButtonAttributes(button);
        }

        private void ResetAllBinaryButtons()
        {
            Button[] binaryButtons = new Button[] {btnBinary128, btnBinary64, btnBinary32, btnBinary16, btnBinary8, btnBinary4, btnBinary2, btnBinary1};

            foreach (Button button in binaryButtons)
                DefaultButtonAttributes(button);
        }

        private void DisableAllBinaryButtons(bool state)
        {
            Button[] binaryButtons = new Button[] { btnBinary128, btnBinary64, btnBinary32, btnBinary16, btnBinary8, btnBinary4, btnBinary2, btnBinary1 };

            foreach (Button button in binaryButtons)
            {
                if (state == true)
                    button.IsEnabled = false;
                else
                    button.IsEnabled = true;
            }
        }

        private void DefaultButtonAttributes(Button button)
        {
            button.Content = "0";
            button.Background = Brushes.White;
            button.Foreground = Brushes.Black;
            button.FontWeight = FontWeights.Normal;
        }

        private void tbUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbUsername.Text.Length > 24)
            {
                btnResultSubmit.IsEnabled = false;
                lbCharLimit.Content = "Name has to be less than 24 characters!";
                lbCharLimit.Foreground = Brushes.Red;
            }
            else
            {
                btnResultSubmit.IsEnabled = true;
                lbCharLimit.Content = "(Character Limit is 24 Characters)";
                lbCharLimit.Foreground = Brushes.White;
            }
        }

        private void btnResultSubmit_Click(object sender, RoutedEventArgs e)
        {
            string userName = tbUsername.Text.ToString();
            TimeSpan totalTime = GetTimeWithMilliseconds(_totalTimeSpent.ToString());
            string userTime = totalTime.ToString(@"dd\:mm\:ss\.fff");
            int userScore = _userScore;

            WindowManager._mainWin = true;
            WindowManager._mainWindow = new MainWindow(userName, userTime, userScore, _difficulty);
            WindowManager._mainWindow.Show();
            WindowManager._gameWindow.Close();
        }

        private TimeSpan GetTimeWithMilliseconds(string totalTimeSpent)
        {
            string[] parts = totalTimeSpent.Split(':', '.');
            int hours = int.Parse(parts[0]);
            int minutes = int.Parse(parts[1]);
            int seconds = int.Parse(parts[2]);
            int milliseconds = int.Parse(parts[3].Substring(0, 3));

            return new TimeSpan(0, hours, minutes, seconds, milliseconds);
        }

        private void btnResultCancel_Click(object sender, RoutedEventArgs e)
        {
            WindowManager._mainWin = true;
            WindowManager._mainWindow = new MainWindow();
            WindowManager._mainWindow.Show();
            WindowManager._gameWindow.Close();
        }

        private void btnResultSubmit_MouseEnter(object sender, MouseEventArgs e)
        {
            lbStats.Content = "Pressing this button will record your session to the leaderboard if eligible!";
        }

        private void btnResultCancel_MouseEnter(object sender, MouseEventArgs e)
        {
            lbStats.Content = "Pressing this button will not record your current session to the leaderboard.";
        }

        private void btnResultCancelSubmit_MouseLeave(object sender, MouseEventArgs e)
        {
            lbStats.Content = "Final Stats";
        }
    }
}
