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
    }
}
