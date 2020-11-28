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
        protected readonly SpeechSynthesizer ss;

        public BaseTTS()
        {
            ss = new SpeechSynthesizer();
            ss.SetOutputToDefaultAudioDevice();
        }

        public void PleaseRepeat()
        {
            ss.Speak("Proszę powtórzyć");
        }

        public abstract void Help();

        public void StopSpeakig()
        {
            ss.SpeakAsyncCancelAll();
        }

        internal void CantChangeView()
        {
            ss.SpeakAsync("Nie można zmienić widoku przed uzupełnieniem wszystkich pól");
        }
    }
}
