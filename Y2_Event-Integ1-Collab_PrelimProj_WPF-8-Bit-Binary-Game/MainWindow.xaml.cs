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
        private Dictionary<string, string[]> _userData = new Dictionary<string, string[]>();
        private List<KeyValuePair<string, string[]>> _sortedUserData = new List<KeyValuePair<string, string[]>>();

        private Dictionary<string, string[]> _userDataEasy = new Dictionary<string, string[]>();
        private Dictionary<string, string[]> _userDataMedium = new Dictionary<string, string[]>();
        private Dictionary<string, string[]> _userDataHard = new Dictionary<string, string[]>();

        private LeaderboardManager _lm = new LeaderboardManager();

        public MainWindow()
        {
            InitializeComponent();

            if (!WindowManager._mainWin)
            {
                WindowManager._mainWindow = this;
                WindowManager._mainWin = true;
            }
        }

        public MainWindow(string userName, string userTime, int userScore)
        {
            InitializeComponent();

            if (!WindowManager._mainWin)
            {
                WindowManager._mainWindow = this;
                WindowManager._mainWin = true;
            }

            LeaderBoardHandler(userName, userTime, userScore);
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (!WindowManager._gameWin)
                DifficultySelect();
            else
                WindowManager._gameWindow.Focus();
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

            lbVersionNum.Visibility = Visibility.Collapsed;
            lbCopyright.Visibility = Visibility.Collapsed;
            btnLeaderboard.Visibility = Visibility.Collapsed;
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

            lbVersionNum.Visibility = Visibility.Visible;
            lbCopyright.Visibility = Visibility.Visible;
            btnLeaderboard.Visibility = Visibility.Visible;
        }

        private void btnQuitGame_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #region Leaderboard Section
        //private void btnLeaderboard_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    lbLdrBrdName.Visibility = Visibility.Visible;
        //}

        //private void btnLeaderboard_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    lbLdrBrdName.Visibility = Visibility.Collapsed;
        //}

        private void btnLeaderboard_Click(object sender, RoutedEventArgs e)
        {
            gMain.Visibility = Visibility.Collapsed;
        }

        private void btnLdrBrdBack_Click(object sender, RoutedEventArgs e)
        {
            gMain.Visibility = Visibility.Visible;
        }

        private void LeaderBoardHandler(string userName, string userTime, int userScore)
        {
            string[] userData = { userTime, userScore.ToString() };

            _userDataEasy["Claude Vincent Gallardo"] = new string[] { "00:01:21", "8" };
            _userDataEasy["Orange Jungle Juice"] = new string[] { "00:00:59", "6" };
            _userDataEasy["The Stingray on Desk"] = new string[] { "00:01:20", "8" };
            _userDataEasy["Pipi"] = new string[] { "00:01:25", "8" };

            switch (_difficulty)
            {
                case "Easy":
                    _userDataEasy[userName] = userData;
                    SortUserData(_userDataEasy);
                    break;
                case "Medium":
                    _userDataMedium[userName] = userData;
                    SortUserData(_userDataMedium);
                    break;
                case "Hard":
                    _userDataHard[userName] = userData;
                    SortUserData(_userDataHard);
                    break;
            }
        }

        private void SortUserData(Dictionary<string, string[]> userData)
        {
            _sortedUserData = userData.ToList();

            // Sirs Selection Sort Code thing
            for (int x = 0; x < _sortedUserData.Count - 1; x++)
            {
                int minIndex = x;
                for (int y = x + 1; y < _sortedUserData.Count; y++)
                {
                    int xScore = int.Parse(_sortedUserData[y].Value[1]);
                    int yScore = int.Parse(_sortedUserData[minIndex].Value[1]);
                    TimeSpan xTime = TimeSpan.Parse(_sortedUserData[y].Value[0]);
                    TimeSpan yTime = TimeSpan.Parse(_sortedUserData[minIndex].Value[0]);

                    if (xScore > yScore || (xScore == yScore && xTime < yTime))
                        minIndex = y;
                }

                KeyValuePair<string, string[]> temp = _sortedUserData[x];
                _sortedUserData[x] = _sortedUserData[minIndex];
                _sortedUserData[minIndex] = temp;
            }
        }

        private void RadioButtonLeaderboard_Checked(object sender, RoutedEventArgs e)
        {
            string difficulty = (sender as RadioButton)?.Name.Replace("rbLdrBrd", "");

            switch (difficulty)
            {
                case "Easy":
                    _lm.EasyBoard(_sortedUserData);
                    break;
                case "Medium":
                    _lm.MediumBoard(_sortedUserData);
                    break;
                case "Hard":
                    _lm.HardBoard(_sortedUserData);
                    break;
            }
        }
        #endregion
    }
}
