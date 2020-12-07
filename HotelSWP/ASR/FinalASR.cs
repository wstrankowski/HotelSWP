using Microsoft.Speech.Recognition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSWP.ASR
{
    public class FinalASR : BaseASR
    {
        private Grammar peselGrammar;
        private Grammar peselConfirmationGrammar;
        private Grammar reservationConfirmationGrammar;
        private readonly string[] numberOptions = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        private readonly string[] peselConfirmOptions = { "zmień", "zatwierdź" };
        private readonly string FINISH = "żłóż rezerwację";

        public void AddPeselGrammar()
        {
            Choices numberChoices = GetChoices(numberOptions);
            GrammarBuilder grammarBuilder = new GrammarBuilder();
            for(int i=0;i<11;i++)
            {
                grammarBuilder.Append(numberChoices);
            }
            peselGrammar = new Grammar(grammarBuilder);
            _sre.LoadGrammarAsync(peselGrammar);
        }

        public void AddReservationConfirmationGrammar()
        {
            GrammarBuilder grammarBuilder = new GrammarBuilder();
            grammarBuilder.Append(FINISH);
            reservationConfirmationGrammar = new Grammar(grammarBuilder);
            _sre.LoadGrammarAsync(reservationConfirmationGrammar);
        }
        public void AddPeselConfirmationGrammar()
        {
            GrammarBuilder grammarBuilder = new GrammarBuilder();
            Choices peselConfirmChoices = GetChoices(peselConfirmOptions);
            grammarBuilder.Append(peselConfirmChoices);
            peselConfirmationGrammar = new Grammar(grammarBuilder);
            _sre.LoadGrammarAsync(peselConfirmationGrammar);
        }
        public void UnloadPeselGrammar()
        {
            _sre.UnloadGrammar(peselGrammar);
        }
        public void UnloadPeselConfirmationGrammar()
        {
            _sre.UnloadGrammar(peselConfirmationGrammar);
        }
    }
}
