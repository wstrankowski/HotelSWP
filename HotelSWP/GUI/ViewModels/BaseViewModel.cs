using HotelSWP.ASR;
using HotelSWP.TTS;
using Microsoft.Speech.Recognition;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GUI.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        private readonly BaseTTS _baseTTS;
        private readonly BaseASR _baseASR;
        private readonly MainWindow _mainWindow;

        public BaseViewModel(BaseTTS baseTTS, BaseASR baseASR, MainWindow mainWindow)
        {
            _baseTTS = baseTTS;
            _baseASR = baseASR;
            _baseASR.SetHandler(Sre_SpeechRecognized);
            _mainWindow = mainWindow;
        }

        public void Sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string txt = e.Result.Text;
            float confidence = e.Result.Confidence;
            Console.WriteLine("Rozpoznano: " + txt + " z pewnoscia: " + confidence);
            Console.WriteLine(GetType());
            if (confidence < 0.60)
            {
                _baseTTS.PleaseRepeat();
                return;
            }
            else
            {
                if (txt.IndexOf("Pomoc") >= 0)
                {
                    _baseTTS.Help();
                } else if(txt.IndexOf("Dalej") >=0)
                {
                    _mainWindow.Next();
                } else if(txt.IndexOf("Cofnij") >=0)
                {
                    _mainWindow.Previous();
                }
                else
                {
                    Handle(txt);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyRaised(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        public virtual void Start()
        {
            _baseASR.StartRecognizing();
        }
        public virtual void Stop()
        {
            _baseTTS.StopSpeakig();
            _baseASR.StopRecognizing();
        }
        public void InformThatCantChangeView()
        {
            _baseTTS.StopSpeakig();
            _baseTTS.CantChangeView();
        }
        public abstract bool CanChangeView();
        protected abstract void Handle(string txt);

        protected bool IsChangeRequest(string[] words)
        {
            return words.Length > 1 && words[0].Equals("zmień");
        }

    }
}
