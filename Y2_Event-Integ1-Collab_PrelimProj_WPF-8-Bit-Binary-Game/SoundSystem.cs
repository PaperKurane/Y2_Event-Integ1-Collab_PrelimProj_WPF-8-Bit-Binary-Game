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
        private static Dictionary<string, MediaPlayer> _CurrAudio = new Dictionary<string, MediaPlayer>();

        public void Initialize(string fileName, double volume, bool loop)
        {
            string path = System.IO.Path.Combine(Environment.CurrentDirectory, @"Sounds\", fileName);

            MediaPlayer media = new MediaPlayer();
            media.Open(new Uri(path));

            if (loop)
            {
                media.MediaEnded += (sender, e) =>
                {
                    ((MediaPlayer)sender).Position = TimeSpan.Zero; //loops the song lmao
                    ((MediaPlayer)sender).Play();
                };
            }

            media.Play();

            media.Volume = Math.Max(0.0, Math.Min(5.0, volume));

            _CurrAudio[fileName] = media;
        }

        public void Resume(string fileName)
        {
            if (_CurrAudio.ContainsKey(fileName))
            {
                _CurrAudio[fileName].Play();
            }
        }

        public void Pause(string fileName)
        {
            if (_CurrAudio.ContainsKey(fileName))
            {
                _CurrAudio[fileName].Pause();
            }
        }

        public void Stop(string fileName)
        {
            if (_CurrAudio.ContainsKey(fileName))
            {
                _CurrAudio[fileName].Stop();
            }
        }
    }
}
