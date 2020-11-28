using HotelSWP.ASR;
using HotelSWP.GUI.Models;
using HotelSWP.TTS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GUI.ViewModels
{
    public class RoomsViewModel : BaseViewModel
    {
        public enum RoomsInputOption
        {
            GUESTS,
            CONVENIENCES,
            FINISHED
        }

        private readonly RoomsASR _asr;
        private readonly RoomsTTS _tts;
        private RoomsInputOption inputOption;
        private readonly RoomsModel model = new RoomsModel();
        public string GuestsNumber { get
            {
                return model.GuestsNumber.ToString();
            }
            set
            {
                model.GuestsNumber = int.Parse(value);
                OnPropertyRaised(nameof(GuestsNumber));
            }
        }
        private IEnumerable<string> _notSelectedConveniences;
        public IEnumerable<string> NotSelectedConveniences { get
            {
                return _notSelectedConveniences;
            }
            set
            {
                _notSelectedConveniences = value;
                OnPropertyRaised(nameof(NotSelectedConveniences));
            }
        }
        public IEnumerable<string> SelectedConveniences
        {
            get
            {
                return model.SelectedConveniences;
            }
            set
            {
                model.SelectedConveniences = value;
                OnPropertyRaised(nameof(SelectedConveniences));
            }
        }
        public RoomsViewModel(RoomsTTS roomsTTS, RoomsASR roomsASR, MainWindow mainWindow) : base(roomsTTS, roomsASR, mainWindow)
        {
            _asr = roomsASR;
            _tts = roomsTTS;
            _tts.SetInputOption(inputOption);
            NotSelectedConveniences = new List<string>(_asr.conveniences);
            SelectedConveniences = new List<string>();
        }

        public override void Start()
        {
            base.Start();
            inputOption = RoomsInputOption.GUESTS;
            _tts.AskForGuestsNumber();
            _asr.GetGuestsNumber();
            while (inputOption == RoomsInputOption.GUESTS) { }
            _asr.UnloadGuestsNumberGrammar();
            _tts.AskForConveniences();
            _asr.GetConveniences();
            while (inputOption == RoomsInputOption.CONVENIENCES) { }
            _asr.UnloadConveniencesGrammar();
            _tts.Finished();
        }

        protected override void Handle(string txt)
        {
            if(inputOption == RoomsInputOption.GUESTS)
            {
                GuestsNumber = txt;
                inputOption = RoomsInputOption.CONVENIENCES;
            }
            else if(inputOption == RoomsInputOption.CONVENIENCES)
            {
                if (txt.Equals("Zakończ edycję"))
                {
                    inputOption = RoomsInputOption.FINISHED;
                }
                else
                {
                    string[] words = txt.Split(' ');
                    if (words[0] == "Dodaj")
                    {
                        if (!SelectedConveniences.Contains(words[1]))
                        {
                            SelectedConveniences = SelectedConveniences.Append(words[1]);
                            var list = NotSelectedConveniences.ToList();
                            list.Remove(words[1]);
                            NotSelectedConveniences = list;
                        }
                    }
                    else
                    {
                        if (SelectedConveniences.Contains(words[1]))
                        {
                            NotSelectedConveniences = NotSelectedConveniences.Append(words[1]);
                            var list = SelectedConveniences.ToList();
                            list.Remove(words[1]);
                            SelectedConveniences = list;
                        }
                    }
                }
            }
            
        }

        public override bool CanChangeView()
        {
            return model.IsCompleted();
        }
    }
}
