using GUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GUI.ViewModels.RoomsViewModel;

namespace HotelSWP.TTS
{
    public class RoomsTTS : BaseTTS
    {
        public void AskForGuestsNumber()
        {
            SpeakAsync("Proszę podać liczbę gości.");
        }

        public void AskForConveniences()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Proszę wybrać udogodnienia. ");
            sb.Append("Aby dodać na przykład internet proszę powiedzieć");
            sb.Append("\"Dodaj internet\". Aby usunąć proszę powiedzieć ");
            sb.Append("\"Usuń internet\". Aby zakończyć edycję proszę powiedzieć ");
            sb.Append("\"Zakończ edycję\" ");
            string text = sb.ToString();
            SpeakAsync(text);
        }

        public override void Help()
        {
            switch(InputOption)
            {
                case RoomsInputOption.GUESTS:
                    AskForGuestsNumber();
                    break;
                case RoomsInputOption.CONVENIENCES:
                    AskForConveniences();
                    break;
                case RoomsInputOption.FINISHED:
                    Finished();
                    break;
            }
        }

        public void Finished()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Aby przejść dalej powiedz \"DALEJ\", ");
            sb.Append("aby zmienić liczbę gości powiedz \"zmień liczbę gości\", ");
            sb.Append("aby zmienić udogodnienia powiedz \"zmień udogodnienia\".");
            string text = sb.ToString();
            SpeakAsync(text);
        }
    }
}
