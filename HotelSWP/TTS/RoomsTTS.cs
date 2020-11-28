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
        private RoomsInputOption _inputOption;

        public void AskForGuestsNumber()
        {
            ss.SpeakAsync("Proszę podać liczbę gości.");
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
            ss.SpeakAsync(text);
        }

        public override void Help()
        {
            switch(_inputOption)
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
            sb.Append("aby zmienić datę przyjazdu powiedz \"zmień datę przyjazdu\", ");
            sb.Append("aby zmienić datę wyjazdu powiedz \"zmień datę wyjazdu\".");
            string text = sb.ToString();
            ss.SpeakAsync(text);
        }

        internal void SetInputOption(RoomsInputOption inputOption)
        {
            _inputOption = inputOption;
        }
    }
}
