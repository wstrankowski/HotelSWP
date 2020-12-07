using Microsoft.Speech.Synthesis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSWP.TTS
{
    public abstract class BaseTTS
    {
        private readonly SpeechSynthesizer ss;

        public BaseTTS()
        {
            ss = new SpeechSynthesizer();
            ss.SetOutputToDefaultAudioDevice();
        }

        public void PleaseRepeat()
        {
            SpeakAsync("Proszę powtórzyć");
        }

        public abstract void Help();

        public void StopSpeakig()
        {
            ss.SpeakAsyncCancelAll();
        }

        protected void SpeakAsync(string txt)
        {
            StopSpeakig();
            ss.SpeakAsync(txt);
        }

        protected void Speak(string text)
        {
            StopSpeakig();
            ss.Speak(text);
        }
        internal void CantChangeView()
        {
            SpeakAsync("Nie można zmienić widoku przed uzupełnieniem wszystkich pól");
        }

        
    }
}
