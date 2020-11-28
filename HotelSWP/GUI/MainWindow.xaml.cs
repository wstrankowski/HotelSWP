using GUI.ViewModels;
using HotelSWP.ASR;
using HotelSWP.TTS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LinkedList<BaseViewModel> _viewModels;
        private Thread viewModelThread;
        public MainWindow()
        {
            InitializeComponent();
            InitializeViewModels();
            var viewModel = _viewModels.First.Value;
            SetView(viewModel);
        }

        private void InitializeViewModels()
        {
            _viewModels = new LinkedList<BaseViewModel>();
            var dvm = new DatesViewModel(new DatesTTS(), new DatesASR(), this);
            var rvm = new RoomsViewModel(new RoomsTTS(), new RoomsASR(), this);
            var fvm = new FinalViewModel(new FinalTTS(), new BaseASR(), this);
            _viewModels.AddLast(dvm);
            _viewModels.AddLast(rvm);
            _viewModels.AddLast(fvm);
        }

        private void Next_Clicked(object sender, RoutedEventArgs e)
        {
            Next();
        }

        public void Next()
        {
            Dispatcher.Invoke(() =>
            {
                var current = GetCurrentNode();
                var next = current.Next;
                if (next != null)
                {
                    SwitchView(next.Value);
                }
            });
        }

        private void Previous_Clicked(object sender, RoutedEventArgs e)
        {
            Previous();
        }
        
        public void Previous()
        {
            Dispatcher.Invoke(() =>
            {
                var current = GetCurrentNode();
                var previous = current.Previous;
                if (previous != null)
                {
                    SwitchView(previous.Value);
                }
            });            
        }

        private void SwitchView(BaseViewModel viewModel)
        {
            var currentViewModel = GetCurrentNode().Value;
            if (currentViewModel.CanChangeView())
            {
                StopView(currentViewModel);
                SetView(viewModel);
            }
            else
            {
                currentViewModel.InformThatCantChangeView();
            }
        }

        private void SetView(BaseViewModel viewModel)
        {
            viewModelThread = new Thread(viewModel.Start);
            viewModelThread.Start();
            DataContext = viewModel;
        }
        private void StopView(BaseViewModel currentViewModel)
        {
            currentViewModel.Stop();
            viewModelThread.Abort();
        }

        private LinkedListNode<BaseViewModel> GetCurrentNode()
        {
            return _viewModels.Find((BaseViewModel)DataContext);
        }
    }
}
