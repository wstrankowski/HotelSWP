using HotelSWP.ASR;
using HotelSWP.TTS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.ViewModels
{
    public class FinalViewModel : BaseViewModel
    {
        public FinalViewModel(BaseTTS baseTTS, BaseASR baseASR, MainWindow mainWindow) : base(baseTTS, baseASR, mainWindow)
        {
        }

        public override bool CanChangeView()
        {
            throw new NotImplementedException();
        }

        protected override void Handle(string txt)
        {
            throw new NotImplementedException();
        }
    }
}
