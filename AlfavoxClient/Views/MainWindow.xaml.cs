using System.Windows;
using System.Windows.Input;

namespace AlfavoxClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var vm = new ViewModel();
            DataContext = vm;
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Find, vm.FindKeysCmd, vm.CanFindKeysCmd));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Close, vm.CloseCmd, vm.CanCloseCmd));
        }
    }
}