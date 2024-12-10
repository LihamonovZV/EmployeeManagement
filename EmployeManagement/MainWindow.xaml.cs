using EmployeManagement.ViewModel;
using System.Windows;
using EmployeManagement.View;
namespace EmployeManagement
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new HomePage());
            NavigationVM navVM = new NavigationVM(MainFrame);
            MainVM vM = new MainVM(navVM);

            DataContext = vM;
        }
    }
}
