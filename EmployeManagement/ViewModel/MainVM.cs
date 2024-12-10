using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using EmployeManagement.Model;
using EmployeManagement.View;

namespace EmployeManagement.ViewModel
{
    internal class MainVM : ObservableObject
    {
        public ICommand Delete {  get; private set; }       

        private bool _isButtonEnable;
        public bool IsButtonEnable
        {
            get => _isButtonEnable;
            set
            {
                if (_isButtonEnable != value)
                {
                    _isButtonEnable = value;
                    OnPropertyChanged(nameof(IsButtonEnable));
                }
                
            }
        }
        private NavigationVM _navVM;
        public ObservableCollection<Employee> Employees { get; set; }
        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                if (_selectedEmployee != value)
                {
                    _selectedEmployee = value;
                    OnPropertyChanged(nameof(SelectedEmployee));
                    IsEditButtonEnable = true;
                }
                else
                {
                    IsEditButtonEnable = false;
                }
            }
        }
        public ICommand Navigate { get; private set; }
        public MainVM(NavigationVM vm)
        {
            Delete = new RelayCommand(param => DeleteEmployee());
            Employees = new ObservableCollection<Employee>()
            {
                new Employee {FIO = "Казарин Илья Игоревич", Age = 18, Position = "Гений"},
                new Employee {FIO = "Лихамонов Захар Витальевич", Age = 18, Position = "Простак"}
            };
            _navVM = vm;
            Navigate = new RelayCommand(param => _navVM.NavigationTo(param, this));
        }
        private void DeleteEmployee()
        {
            Employees.Remove(_selectedEmployee);
            SelectedEmployee = null;
        }
    }
    
    public class NavigationVM
    {
        private readonly Frame _frame;
        public NavigationVM(Frame frame) 
        { 
            _frame = frame;
        }
        public void NavigationTo(object param, object dataContext)
        {
            string pageName = param.ToString();
            Page page;
            switch (pageName)
            {
                case "HomePage":
                    page = new HomePage();
                    break;
                case "EmployeeListPage":
                    page = new EmployeeListPage();
                    break;
                case "EmployeeEditPage":
                    page = new EmployeeEditPage();
                    break;
                default:
                    throw new ArgumentException("Неизвестная страница ", nameof(pageName));
            }
            page.DataContext = dataContext;
            _frame.Navigate(page);
        }
    }
}
