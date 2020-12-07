using Microsoft.Speech.Synthesis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSWP.TTS
{
    public class DatesTTS : BaseTTS
    {
        public void AskForArrivalDate()
        {
            SpeakAsync("Proszę podać datę przyjazdu");
        }

        public void AskForDepartureDate()
        {
            SpeakAsync("Proszę podać datę wyjazdu");
        }

        public override void Help()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Proszę podać datę w formacie dzień, miesiąc, rok. ");
            sb.Append("Przykładowo \"osiemnasty stycznia dwa tysiące dwadzieścia\"");
            string help = sb.ToString();
            SpeakAsync(help);
        }

        public void Finished()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Aby przejść dalej powiedz \"DALEJ\", ");
            sb.Append("aby zmienić datę przyjazdu powiedz \"zmień datę przyjazdu\", ");
            sb.Append("aby zmienić datę wyjazdu powiedz \"zmień datę wyjazdu\".");
            string help = sb.ToString();
            SpeakAsync(help);
        }
    }
}
