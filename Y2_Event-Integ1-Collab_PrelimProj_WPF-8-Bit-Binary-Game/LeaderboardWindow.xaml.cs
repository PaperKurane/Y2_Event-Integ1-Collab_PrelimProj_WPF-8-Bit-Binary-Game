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

namespace Y2_Event_Integ1_Collab_PrelimProj_WPF_8_Bit_Binary_Game
{
    /// <summary>
    /// Interaction logic for LeaderboardWindow.xaml
    /// 
    /// Read from a file, extract it to the leaderboard displayers split into three categories of difficulty. 
    /// 
    /// Screen will be blocked by the "login screen" 
    /// </summary>
    public partial class LeaderboardWindow : Window
    {
        public LeaderboardWindow()
        {
            InitializeComponent();
        }
    }
}
