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
        public static RoomsInputOption InputOption = RoomsInputOption.GUESTS;

        public enum RoomsInputOption
        {
            GUESTS,
            CONVENIENCES,
            FINISHED
        }

        private readonly RoomsASR _asr;
        private readonly RoomsTTS _tts;
        private readonly RoomsModel model = new RoomsModel();
        private bool isChangeMode;

        public string GuestsNumber
        {
            get
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
        public IEnumerable<string> NotSelectedConveniences
        {
            get
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
            NotSelectedConveniences = new List<string>(_asr.conveniences);
            SelectedConveniences = new List<string>();
        }

        public override void Start()
        {
            base.Start();
            if (InputOption == RoomsInputOption.GUESTS)
            {
                GetGuestsNumber();
                while (InputOption == RoomsInputOption.GUESTS) { }
                GetConveniences();
                while (InputOption == RoomsInputOption.CONVENIENCES) { }
                _asr.AddChangeGrammar();
            }
            _tts.Finished();
        }

        private void GetConveniences()
        {
            _tts.AskForConveniences();
            _asr.AddConveniencesGrammar();
        }

        private void GetGuestsNumber()
        {
            _tts.AskForGuestsNumber();
            _asr.AddGuestsNumberGrammar();
        }

        protected override void Handle(string txt)
        {
            if(InputOption == RoomsInputOption.GUESTS)
            {
                GuestsNumber = txt;
                if (isChangeMode)
                {
                    InputOption = RoomsInputOption.FINISHED;
                    isChangeMode = false;
                }
                else
                {
                    InputOption = RoomsInputOption.CONVENIENCES;
                }
                _asr.UnloadGuestsNumberGrammar();
            }
            else if(InputOption == RoomsInputOption.CONVENIENCES)
            {
                if (txt.Equals("Zakończ edycję"))
                {
                    InputOption = RoomsInputOption.FINISHED;
                    _asr.UnloadConveniencesGrammar();
                    if(isChangeMode)
                    {
                        isChangeMode = false;
                    }
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
            else
            {
                string[] words = txt.Split(' ');
                if (IsChangeRequest(words))
                {
                    if (words[1] == "liczbę" && words[2]=="gości")
                    {
                        InputOption = RoomsInputOption.GUESTS;
                        GetGuestsNumber();
                    }
                    else
                    {
                        InputOption = RoomsInputOption.CONVENIENCES;
                        GetConveniences();
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
