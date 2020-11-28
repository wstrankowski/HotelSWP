using GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HotelSWP
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Thread windowThread = new Thread(new ThreadStart(() => {
                var window = new MainWindow();
                window.Show();
                System.Windows.Threading.Dispatcher.Run();
            }));
            windowThread.SetApartmentState(ApartmentState.STA);
            windowThread.Start();
        }
    }
}
