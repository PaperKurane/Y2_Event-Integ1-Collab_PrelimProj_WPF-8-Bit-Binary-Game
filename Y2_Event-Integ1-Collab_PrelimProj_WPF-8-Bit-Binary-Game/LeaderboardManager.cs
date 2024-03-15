using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Y2_Event_Integ1_Collab_PrelimProj_WPF_8_Bit_Binary_Game
{
    internal class LeaderboardManager
    {
        private MainWindow mainWindow;

        public void EasyBoard(Dictionary<string, string[]> userData)
        {
            if (userData.Count == 0)
                return;

            List<KeyValuePair<string, string[]>> sortedUserData = userData.ToList();

            sortedUserData.Sort((x, y) =>
            {
                int xScore = int.Parse(x.Value[1]);
                int yScore = int.Parse(y.Value[1]);
                TimeSpan xTime = TimeSpan.Parse(x.Value[0]);
                TimeSpan yTime = TimeSpan.Parse(y.Value[0]);

                if (xScore != yScore)
                    return yScore.CompareTo(xScore); // Sort by score in descending order
                else
                    return xTime.CompareTo(yTime); // If scores are equal, sort by time in ascending order
            });

            LeaderBoardPlacer(sortedUserData);
        }

        public void MediumBoard(Dictionary<string, string[]> userData)
        {

        }

        public void HardBoard(Dictionary<string, string[]> userData)
        {

        }

        private void LeaderBoardPlacer(List<KeyValuePair<string, string[]>> sortedUserData)
        {
            int maxRecords = Math.Min(sortedUserData.Count, 10); // Display only top 10 records

            for (int i = 0; i < maxRecords; i++)
            {
                TextBox tbPlaceNum = mainWindow.FindName($"tbPlaceNum{i + 1}") as TextBox;
                TextBox tbPlace = mainWindow.FindName($"tbPlace{i + 1}") as TextBox;
                TextBox tbPlaceTime = mainWindow.FindName($"tbPlaceTime{i + 1}") as TextBox;
                TextBox tbPlaceScore = mainWindow.FindName($"tbPlaceScore{i + 1}") as TextBox;

                tbPlaceNum.Text = (i + 1).ToString();
                tbPlace.Text = sortedUserData[i].Key;
                tbPlaceTime.Text = sortedUserData[i].Value[0];
                tbPlaceScore.Text = sortedUserData[i].Value[1];
            }
        }
    }
}
