using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Y2_Event_Integ1_Collab_PrelimProj_WPF_8_Bit_Binary_Game
{
    internal class SoundSystem
    {
        private MediaPlayer media;
        public void Initialize(string fileName, double volume)
        {
            string path = System.IO.Path.Combine(Environment.CurrentDirectory, @"Sounds\", fileName);

            media = new MediaPlayer();
            media.Open(new Uri(path));
            media.Play();

            Volume(volume);
        }

        public void Volume(double volume)
        {
            if (media != null)
                media.Volume = Math.Max(0.0, Math.Min(5.0, volume));
        }
    }
}
