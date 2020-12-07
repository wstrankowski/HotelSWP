using GUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GUI.ViewModels.FinalViewModel;

namespace HotelSWP.TTS
{
    public class FinalTTS : BaseTTS
    {
        public override void Help()
        {
            switch(InputOption)
            {
                case FinalInputOption.PESEL:
                    AskForPesel();
                    break;
                case FinalInputOption.PESEL_CONFIRMATION:
                    AskForPeselConfirmation();
                    break;
                case FinalInputOption.RESERVATION_CONFIRMATION:
                    AskForReservationConfirmation();
                    break;
            }
        }

        public void AskForPesel()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Proszę podyktować numer PESEL");
            string text = sb.ToString();
            SpeakAsync(text);
        }

        public void AskForReservationConfirmation()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Aby zakończyć i złożyć rezerwację, ");
            sb.Append("proszę powiedzieć \"żłóż rezerwację\"");
            string text = sb.ToString();
            SpeakAsync(text);
        }

        public void GoodbyeMessage()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Dziękujemy za złożenie rezerwacji.");
            sb.Append("Do zobaczenia na miejscu!");
            string text = sb.ToString();
            Speak(text);
        }

        internal void AskForPeselConfirmation()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Jeśli numer PESEL się zgadza, ");
            sb.Append("proszę powiedzieć \"zatwierdź\", ");
            sb.Append("jeśli nie, proszę powiedzieć \"zmień\"");
            string text = sb.ToString();
            SpeakAsync(text);
        }
    }
}
