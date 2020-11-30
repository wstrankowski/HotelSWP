using Microsoft.Speech.Recognition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSWP.ASR
{
    public class RoomsASR : BaseASR
    {
        private Grammar guestsNumberGrammar;
        private Grammar conveniencesGrammar;
        private Grammar convStopGrammar;
        private Grammar changeGrammar;
        readonly string[] numbers = new string[] { "1", "2", "3" };
        readonly string convStop = "Zakończ edycję";
        readonly string[] convOption = new string[] { "Dodaj", "Usuń" };
        public readonly string[] conveniences = new string[] { "Telewizor", "Internet", "Żelazko", "Czajnik", "Suszarka do włosów", "SPA", "Śniadanie" };
        readonly string change = "zmień";
        readonly string[] changeOptions = new string[] { "liczbę gości", "udogodnienia" };

        public void AddGuestsNumberGrammar()
        {
            try
            {
                Choices dayChoices = GetChoices(numbers);
                GrammarBuilder grammarBuilder = new GrammarBuilder();
                grammarBuilder.Append(dayChoices);
                guestsNumberGrammar = new Grammar(grammarBuilder);
                _sre.LoadGrammarAsync(guestsNumberGrammar);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
        }

        public void AddConveniencesGrammar()
        {
            try
            {
                Choices conveniencesChoices = GetChoices(conveniences);
                Choices convOptionChoices = GetChoices(convOption);
                GrammarBuilder grammarBuilder = new GrammarBuilder();
                grammarBuilder.Append(convOptionChoices);
                grammarBuilder.Append(conveniencesChoices);
                conveniencesGrammar = new Grammar(grammarBuilder);

                grammarBuilder = new GrammarBuilder();
                grammarBuilder.Append(convStop);
                convStopGrammar = new Grammar(grammarBuilder);
                _sre.LoadGrammarAsync(convStopGrammar);
                _sre.LoadGrammarAsync(conveniencesGrammar);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void AddChangeGrammar()
        {
            Choices choices = GetChoices(changeOptions);
            GrammarBuilder grammarBuilder = new GrammarBuilder();
            grammarBuilder.Append(change);
            grammarBuilder.Append(choices);
            changeGrammar = new Grammar(grammarBuilder);
            _sre.LoadGrammarAsync(changeGrammar);
        }

        public void UnloadGuestsNumberGrammar()
        {
            _sre.UnloadGrammar(guestsNumberGrammar);
        }

        public void UnloadConveniencesGrammar()
        {
            _sre.UnloadGrammar(convStopGrammar);
            _sre.UnloadGrammar(conveniencesGrammar);
        }
    }
}
