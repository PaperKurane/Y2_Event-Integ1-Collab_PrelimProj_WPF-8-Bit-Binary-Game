using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Y2_Event_Integ1_Collab_PrelimProj_WPF_8_Bit_Binary_Game
{
    internal class LeaderboardManager
    {
        public List<KeyValuePair<string, string[]>> ReadLeaderBoard(string difficulty)
        {
            List<KeyValuePair<string, string[]>> leaderboardData = new List<KeyValuePair<string, string[]>>();

            using (StreamReader sr = new StreamReader("Rankings" + difficulty + ".csv"))
            {
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    string[] read = line.Split(',');
                    string key = read[0];
                    string[] value = {read[1], read[2]};
                    leaderboardData.Add(new KeyValuePair<string, string[]>(key, value));
                }
            }

            return leaderboardData;
        }

        public void EasyBoard(List<KeyValuePair<string, string[]>> sortedUserData)
        {
            LeaderBoardPlacer(sortedUserData);
        }

        public void MediumBoard(List<KeyValuePair<string, string[]>> sortedUserData)
        {
            LeaderBoardPlacer(sortedUserData);
        }

        public void HardBoard(List<KeyValuePair<string, string[]>> sortedUserData)
        {
            LeaderBoardPlacer(sortedUserData);
        }

        private void LeaderBoardPlacer(List<KeyValuePair<string, string[]>> sortedUserData)
        {
            int maxRecords = Math.Min(sortedUserData.Count, 10); // Display only top 10 records

            for (int i = 0; i < maxRecords; i++)
            {
                //TextBox tbPlaceNum = mainWindow.FindName($"tbPlaceNum{i + 1}") as TextBox;
                //TextBox tbPlace = mainWindow.FindName($"tbPlace{i + 1}") as TextBox;
                //TextBox tbPlaceTime = mainWindow.FindName($"tbPlaceTime{i + 1}") as TextBox;
                //TextBox tbPlaceScore = mainWindow.FindName($"tbPlaceScore{i + 1}") as TextBox;

                //tbPlaceNum.Text = (i + 1).ToString();
                //tbPlace.Text = sortedUserData[i].Key;
                //tbPlaceTime.Text = sortedUserData[i].Value[0];
                //tbPlaceScore.Text = sortedUserData[i].Value[1];
            }
        }

        private void WriteLeaderBoard()
        {
            using (StreamWriter sw = new StreamWriter(""))
            {

            }
        }
    }
}
