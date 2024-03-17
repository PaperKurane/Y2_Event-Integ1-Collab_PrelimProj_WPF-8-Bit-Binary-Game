using System;
using System.Collections.Generic;
using System.IO;
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

        private List<KeyValuePair<string, string[]>> _userDataEasy = new List<KeyValuePair<string, string[]>>();
        private List<KeyValuePair<string, string[]>> _userDataMedium = new List<KeyValuePair<string, string[]>>();
        private List<KeyValuePair<string, string[]>> _userDataHard = new List<KeyValuePair<string, string[]>>();

        private List<KeyValuePair<string, string[]>> _sortedUserDataEasy = new List<KeyValuePair<string, string[]>>();
        private List<KeyValuePair<string, string[]>> _sortedUserDataMedium = new List<KeyValuePair<string, string[]>>();
        private List<KeyValuePair<string, string[]>> _sortedUserDataHard = new List<KeyValuePair<string, string[]>>();

        SoundSystem sound = new SoundSystem();

        public MainWindow()
        {
            InitializeComponent();

            sound.Initialize("Pull The Trigger - 8-Bit VRC6.wav", 0.05);

            string[] difficultiesforReading = new string[] {"Easy", "Medium", "Hard"};
            _userDataEasy = ReadLeaderboard(difficultiesforReading[0]);
            _userDataMedium = ReadLeaderboard(difficultiesforReading[1]);
            _userDataHard = ReadLeaderboard(difficultiesforReading[2]);

            _sortedUserDataEasy = SortUserData(_userDataEasy);
            _sortedUserDataMedium = SortUserData(_userDataMedium);
            _sortedUserDataHard = SortUserData(_userDataHard);

            DisplayLeaderboard(_sortedUserDataEasy); // Easy because default

            if (!WindowManager._mainWin)
            {
                WindowManager._mainWindow = this;
                WindowManager._mainWin = true;
            }
        }

        public MainWindow(string userName, string userTime, int userScore, string difficulty)
        {
            InitializeComponent();

            string[] difficultiesforReading = new string[] { "Easy", "Medium", "Hard" };
            _userDataEasy = ReadLeaderboard(difficultiesforReading[0]);
            _userDataMedium = ReadLeaderboard(difficultiesforReading[1]);
            _userDataHard = ReadLeaderboard(difficultiesforReading[2]);

            _sortedUserDataEasy = SortUserData(_userDataEasy);
            _sortedUserDataMedium = SortUserData(_userDataMedium);
            _sortedUserDataHard = SortUserData(_userDataHard);

            DisplayLeaderboard(_sortedUserDataEasy); // Easy because default

            if (!WindowManager._mainWin)
            {
                WindowManager._mainWindow = this;
                WindowManager._mainWin = true;
            }

            LeaderBoardHandler(userName, userTime, userScore, difficulty);
        }

        public List<KeyValuePair<string, string[]>> ReadLeaderboard(string difficulty)
        {
            List<KeyValuePair<string, string[]>> leaderboardData = new List<KeyValuePair<string, string[]>>();

            using (StreamReader sr = new StreamReader("Rankings" + difficulty + ".csv"))
            {
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    string[] read = line.Split(',');
                    if (read.Length == 3)
                    {
                        string key = read[0];
                        string[] value = { read[1], read[2] };
                        leaderboardData.Add(new KeyValuePair<string, string[]>(key, value));
                    }
                }
            }

            return leaderboardData;
        }

        public void WriteLeaderboard(string difficulty)
        {
            List<KeyValuePair<string, string[]>> writableUserData = null;

            switch (difficulty)
            {
                case "Easy":
                    writableUserData = _sortedUserDataEasy;
                    break;
                case "Medium":
                    writableUserData = _sortedUserDataMedium;
                    break;
                case "Hard":
                    writableUserData = _sortedUserDataHard;
                    break;
            }

            using (StreamWriter sw = new StreamWriter("Rankings" + difficulty + ".csv"))
            {
                foreach (KeyValuePair<string,string[]> userData in writableUserData)
                {
                    string line = $"{userData.Key},{userData.Value[0]},{userData.Value[1]}";
                    sw.WriteLine(line);
                }
            }
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
                    tbDifficultyDesc.Text = "Easy Mode\n- Chill, easy gameplay\n- Timer: 60 Seconds\n- No numbers higher than 128.";
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
        private void btnLeaderboard_MouseEnter(object sender, MouseEventArgs e)
        {
            lbLdrBrdName.Visibility = Visibility.Visible;
        }

        private void btnLeaderboard_MouseLeave(object sender, MouseEventArgs e)
        {
            lbLdrBrdName.Visibility = Visibility.Collapsed;
        }

        private void btnLeaderboard_Click(object sender, RoutedEventArgs e)
        {
            gMain.Visibility = Visibility.Collapsed;
        }

        private void btnLdrBrdBack_Click(object sender, RoutedEventArgs e)
        {
            gMain.Visibility = Visibility.Visible;
        }

        private void LeaderBoardHandler(string userName, string userTime, int userScore, string difficulty)
        {
            string[] userData = {userTime, userScore.ToString()};

            switch (difficulty)
            {
                case "Easy":
                    _userDataEasy.Add(new KeyValuePair<string, string[]>(userName, userData));
                    _sortedUserDataEasy = SortUserData(_userDataEasy);
                    DisplayLeaderboard(_sortedUserDataEasy);
                    WriteLeaderboard(difficulty);
                    break;
                case "Medium":
                    _userDataMedium.Add(new KeyValuePair<string, string[]>(userName, userData));
                    _sortedUserDataMedium = SortUserData(_userDataMedium);
                    DisplayLeaderboard(_sortedUserDataMedium);
                    WriteLeaderboard(difficulty);
                    break;
                case "Hard":
                    _userDataHard.Add(new KeyValuePair<string, string[]>(userName, userData));
                    _sortedUserDataHard = SortUserData(_userDataHard);
                    DisplayLeaderboard(_sortedUserDataHard);
                    WriteLeaderboard(difficulty);
                    break;
            }
        }

        private List<KeyValuePair<string, string[]>> SortUserData(List<KeyValuePair<string, string[]>> userData)
        {
            List<KeyValuePair<string, string[]>> sortedUserData = userData.ToList();

            // Sirs Selection Sort Code thing
            for (int x = 0; x < sortedUserData.Count - 1; x++)
            {
                int minIndex = x;
                for (int y = x + 1; y < sortedUserData.Count; y++)
                {
                    int xScore = int.Parse(sortedUserData[y].Value[1]);
                    int yScore = int.Parse(sortedUserData[minIndex].Value[1]);
                    TimeSpan xTime = TimeSpan.Parse(sortedUserData[y].Value[0]);
                    TimeSpan yTime = TimeSpan.Parse(sortedUserData[minIndex].Value[0]);

                    if (xScore > yScore || (xScore == yScore && xTime < yTime))
                        minIndex = y;
                }

                KeyValuePair<string, string[]> temp = sortedUserData[x];
                sortedUserData[x] = sortedUserData[minIndex];
                sortedUserData[minIndex] = temp;
            }

            return sortedUserData;
        }

        private void RadioButtonLeaderboard_Checked(object sender, RoutedEventArgs e)
        {
            string difficulty = (sender as RadioButton)?.Name.Replace("rbLdrBrd", "");

            switch (difficulty)
            {
                case "Easy":
                    DisplayLeaderboard(_sortedUserDataEasy);
                    break;
                case "Medium":
                    DisplayLeaderboard(_sortedUserDataMedium);
                    break;
                case "Hard":
                    DisplayLeaderboard(_sortedUserDataHard);
                    break;
            }
        }

        private void DisplayLeaderboard(List<KeyValuePair<string, string[]>> sortedUserData)
        {
            ClearLeaderboard();

            for (int x = 0; x < sortedUserData.Count && x < 10; x++)
            {
                KeyValuePair<string, string[]> userData = sortedUserData[x];

                TextBox textBoxName = FindName($"tbPlace{x + 1}") as TextBox;
                TextBox textBoxTime = FindName($"tbPlaceTime{x + 1}") as TextBox;
                TextBox textBoxScore = FindName($"tbPlaceScore{x + 1}") as TextBox;

                if (textBoxName != null && textBoxTime != null && textBoxScore != null)
                {
                    textBoxName.Text = " " + userData.Key;
                    textBoxTime.Text = TimeSpan.Parse(userData.Value[0]).ToString(@"mm\:ss\.fff");
                    textBoxScore.Text = userData.Value[1];
                }
            }
        }

        private void ClearLeaderboard()
        {
            for (int x = 0; x < 10; x++)
            {
                TextBox textBoxName = FindName($"tbPlace{x + 1}") as TextBox;
                TextBox textBoxTime = FindName($"tbPlaceTime{x + 1}") as TextBox;
                TextBox textBoxScore = FindName($"tbPlaceScore{x + 1}") as TextBox;

                if (textBoxName != null && textBoxTime != null && textBoxScore != null)
                {
                    textBoxName.Text = "  ---";
                    textBoxTime.Text = "0";
                    textBoxScore.Text = "0";
                }
            }
        }
        #endregion
    }
}
