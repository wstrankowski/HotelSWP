using Hotel.DAL;
using Hotel.Models;
using HotelSWP.ASR;
using HotelSWP.TTS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GUI.ViewModels
{
    public class FinalViewModel : BaseViewModel
    {
        public enum FinalInputOption
        {
            PESEL,
            PESEL_CONFIRMATION,
            RESERVATION_CONFIRMATION
        }

        public static FinalInputOption InputOption = FinalInputOption.PESEL;

        private Reservation _reservation;
        private readonly FinalASR _asr;
        private readonly FinalTTS _tts;
        private string _pesel { get; set; }
        public string Pesel
        {
            get
            {
                return _pesel;
            }
            set
            {
                _pesel = value;
                OnPropertyRaised(nameof(Pesel));
            }
        }
        public Reservation Reservation
        {
            get
            {
                return _reservation;
            }
            set
            {
                _reservation = value;
                OnPropertyRaised(nameof(Reservation));
            }
        }
        public FinalViewModel(FinalTTS finalTTS, FinalASR finalASR, MainWindow mainWindow) : base(finalTTS, finalASR, mainWindow)
        {
            _asr = finalASR;
            _tts = finalTTS;
        }
        
        public override bool CanChangeView()
        {
            return true;
        }

        protected override void Handle(string txt)
        {
            if (InputOption == FinalInputOption.PESEL)
            {
                Pesel = txt;
                _asr.UnloadPeselGrammar();
                GetPeselConfirmation();
            }
            else if (InputOption == FinalInputOption.PESEL_CONFIRMATION)
            {
                if (txt.Equals("zatwierdź"))
                {
                    InputOption = FinalInputOption.RESERVATION_CONFIRMATION;
                }
                else if (txt.Equals("zmień"))
                {
                    Pesel = "";
                    GetPesel();
                }
                _asr.UnloadPeselConfirmationGrammar();

            }
            else if (InputOption == FinalInputOption.RESERVATION_CONFIRMATION)
            {
                if (txt.Equals("żłóż rezerwację"))
                {
                    var pesel = Regex.Replace(Pesel, @"\s+", "");
                    var client = new Client(pesel);
                    Reservation.AddClientToReservation(client);
                    var repository = new Repository();
                    repository.AddReservation(Reservation);
                    _tts.GoodbyeMessage();
                    _mainWindow.Finish();
                }
            }
        }

        public override void Start()
        {
            base.Start();
            Reservation = _mainWindow.GetReservation();
            if (string.IsNullOrEmpty(Pesel))
            {
                GetPesel();
                while (InputOption != FinalInputOption.RESERVATION_CONFIRMATION) { }
            }
            GetReservationConfirmation();
        }

        private void GetPesel()
        {
            InputOption = FinalInputOption.PESEL;
            _tts.AskForPesel();
            _asr.AddPeselGrammar();
        }

        private void GetPeselConfirmation()
        {
            InputOption = FinalInputOption.PESEL_CONFIRMATION;
            _tts.AskForPeselConfirmation();
            _asr.AddPeselConfirmationGrammar();
        }

        private void GetReservationConfirmation()
        {
            _asr.AddReservationConfirmationGrammar();
            _tts.AskForReservationConfirmation();
        }
    }
}
