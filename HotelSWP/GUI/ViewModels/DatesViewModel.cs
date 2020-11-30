using HotelSWP.ASR;
using HotelSWP.GUI.Models;
using HotelSWP.TTS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GUI.ViewModels
{
    

    public class DatesViewModel : BaseViewModel
    {
        private enum InputOption
        {
            ARRIVAL,
            DEPARTURE,
            FINISHED
        }

        private readonly DatesTTS _tts;
        private readonly DatesASR _asr;
        private InputOption inputOption = InputOption.ARRIVAL;
        private bool isChangeMode;
        private readonly DatesModel model = new DatesModel();
        public string ArrivalDay
        {
            get
            {
                return model.arrivalDay;
            }
            set
            {
                model.arrivalDay = value;
                OnPropertyRaised("ArrivalDay");
            }
        }
        public string ArrivalMonth
        {
            get
            {
                return model.arrivalMonth;
            }
            set
            {
                model.arrivalMonth = value;
                OnPropertyRaised("ArrivalMonth");
            }
        }
        public string ArrivalYear
        {
            get
            {
                return model.arrivalYear;
            }
            set
            {
                model.arrivalYear = value;
                OnPropertyRaised("ArrivalYear");
            }
        }
        public string DepartureDay
        {
            get
            {
                return model.departureDay;
            }

            set
            {
                model.departureDay = value;
                OnPropertyRaised("DepartureDay");
            }
        }
        public string DepartureMonth
        {
            get
            {
                return model.departureMonth;
            }
            set
            {
                model.departureMonth = value;
                OnPropertyRaised("DepartureMonth");
            }
        }
        public string DepartureYear
        {
            get
            {
                return model.departureYear;
            }
            set
            {
                model.departureYear = value;
                OnPropertyRaised("DepartureYear");
            }
        }

        public DatesViewModel(DatesTTS datesTTS, DatesASR datesASR, MainWindow mainWindow) : base(datesTTS, datesASR, mainWindow)
        {
            _tts = datesTTS;
            _asr = datesASR;
        }

        public override void Start()
        {
            base.Start();
            if (inputOption == InputOption.ARRIVAL)
            {
                _tts.AskForArrivalDate();
                _asr.AddGetDateGrammar();
                while (inputOption == InputOption.ARRIVAL) { }
                _tts.AskForDepartureDate();
                while (inputOption == InputOption.DEPARTURE) { }
                _asr.AddChangeDateGrammar();
            }
            _tts.Finished();
        }

        protected override void Handle(string txt)
        {
            if (inputOption == InputOption.ARRIVAL)
            {
                string[] words = txt.Split(' ');
                ArrivalDay = words[0];
                ArrivalMonth = words[1];
                ArrivalYear = words[2];
                if (isChangeMode)
                {
                    inputOption = InputOption.FINISHED;
                    isChangeMode = false;
                }
                else
                {
                    inputOption = InputOption.DEPARTURE;
                }
            }
            else if (inputOption == InputOption.DEPARTURE)
            {
                string[] words = txt.Split(' ');
                DepartureDay = words[0];
                DepartureMonth = words[1];
                DepartureYear = words[2];
                inputOption = InputOption.FINISHED;
                if(isChangeMode)
                {
                    isChangeMode = false;
                }
            }
            else
            {
                string[] words = txt.Split(' ');
                if (IsChangeRequest(words))
                {
                    if (words[2] == "przyjazdu")
                    {
                        _tts.AskForArrivalDate();
                        inputOption = InputOption.ARRIVAL;
                    }
                    else
                    {
                        _tts.AskForDepartureDate();
                        inputOption = InputOption.DEPARTURE;
                    }
                    isChangeMode = true;
                }
            }
        }

        public override bool CanChangeView()
        {
            return model.IsCompleted();
        }
    }
}
