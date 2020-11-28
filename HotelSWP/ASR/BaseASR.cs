using GUI.ViewModels;
using Microsoft.Speech.Recognition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSWP.ASR
{
    public class BaseASR
    {
        protected SpeechRecognitionEngine _sre = new SpeechRecognitionEngine();

        public BaseASR()
        {
            _sre.SetInputToDefaultAudioDevice();
            Choices ch_stop = new Choices();
            ch_stop.Add("Pomoc");
            ch_stop.Add("Dalej");
            ch_stop.Add("Cofnij");
            GrammarBuilder grammar_stop_builder = new GrammarBuilder();
            grammar_stop_builder.Append(ch_stop);
            Grammar stop_grammar = new Grammar(grammar_stop_builder);
            _sre.LoadGrammarAsync(stop_grammar);
        }

        protected Choices GetChoices(string[] choiceOptions)
        {
            Choices choices = new Choices();
            choices.Add(choiceOptions);
            return choices;
        }

        public void SetHandler(EventHandler<SpeechRecognizedEventArgs> handler)
        {
            _sre.SpeechRecognized += handler;
        }
        public void StartRecognizing()
        {
            _sre.RecognizeAsync(RecognizeMode.Multiple);
            Console.WriteLine("START " + GetType() + " " + _sre.AudioState);

        }
        public void StopRecognizing()
        {
            _sre.RecognizeAsyncCancel();
            Console.WriteLine("STOP " + GetType() + " " + _sre.AudioState);
        }
    }
}
