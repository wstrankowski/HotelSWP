using GUI.ViewModels;
using Microsoft.Speech.Recognition;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSWP.ASR
{
    public class DatesASR : BaseASR
    {
        private Grammar dataGrammar;
        private Grammar changeDateGrammar;
        readonly string[] days = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31"};
        readonly string[] months = new string[] { "stycznia", "lutego", "marca", "kwietnia", "maja", "czerwca", "lipca", "sierpnia", "września", "października", "listopada", "grudnia" };
        readonly string[] years = new string[] { "2020", "2021", "2022" };
        readonly string changeDate = "zmień datę";
        readonly string[] dateOptions = { "przyjazdu", "wyjazdu" };

        public void AddGetDateGrammar()
        {
            Choices dayChoices = GetChoices(days);
            Choices monthChoices = GetChoices(months);
            Choices yearChoices = GetChoices(years);
            GrammarBuilder grammarBuilder = new GrammarBuilder();
            grammarBuilder.Append(dayChoices);
            grammarBuilder.Append(monthChoices);
            grammarBuilder.Append(yearChoices);
            dataGrammar = new Grammar(grammarBuilder);
            _sre.LoadGrammarAsync(dataGrammar);
        }

        public void AddChangeDateGrammar()
        {
            Choices dateChoices = GetChoices(dateOptions);
            GrammarBuilder grammarBuilder = new GrammarBuilder();
            grammarBuilder.Append(changeDate);
            grammarBuilder.Append(dateChoices);
            changeDateGrammar = new Grammar(grammarBuilder);
            _sre.LoadGrammarAsync(changeDateGrammar);
        }

        public void UnloadDataGrammar()
        {
            _sre.UnloadGrammar(dataGrammar);
        }
    }
}
