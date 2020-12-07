using Hotel.DAL;
using Hotel.Models;
using HotelSWP.ASR;
using HotelSWP.GUI.Models;
using HotelSWP.Helpers;
using HotelSWP.TTS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        private readonly RoomsModel _model = new RoomsModel();
        public RoomsModel Model
        {
            get
            {
                return _model;
            }
        }
        private bool isChangeMode;

        public string GuestsNumber
        {
            get
            {
                return _model.GuestsNumber.ToString();
            }
            set
            {
                _model.GuestsNumber = int.Parse(value);
                OnPropertyRaised(nameof(GuestsNumber));
            }
        }
        private IEnumerable<Convenience> _notSelectedConveniences;
        public IEnumerable<Convenience> NotSelectedConveniences
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
        public IEnumerable<Convenience> SelectedConveniences
        {
            get
            {
                return _model.SelectedConveniences;
            }
            set
            {
                _model.SelectedConveniences = value;
                OnPropertyRaised(nameof(SelectedConveniences));
            }
        }
        public RoomsViewModel(RoomsTTS roomsTTS, RoomsASR roomsASR, MainWindow mainWindow) : base(roomsTTS, roomsASR, mainWindow)
        {
            _asr = roomsASR;
            _tts = roomsTTS;
            var repository = new Repository();
            var conveniences = repository.GetConveniences();
            NotSelectedConveniences = new List<Convenience>(conveniences);
            SelectedConveniences = new List<Convenience>();
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
                    string convName = words[1];
                    if (words[0] == "Dodaj")
                    {
                        var convenience = NotSelectedConveniences.FindConvenienceByName(convName);
                        if (convenience != null)
                        {
                            SelectedConveniences = SelectedConveniences.Append(convenience);
                            NotSelectedConveniences = NotSelectedConveniences.RemoveConvenienceByName(convName);
                        }
                    }
                    else
                    {
                        var convenience = SelectedConveniences.FindConvenienceByName(convName);
                        if (convenience != null)
                        {
                            NotSelectedConveniences = NotSelectedConveniences.Append(convenience);
                            SelectedConveniences = SelectedConveniences.RemoveConvenienceByName(convName);
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
            return _model.IsCompleted();
        }
    }
}
