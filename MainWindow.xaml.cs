using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Threading;
using System.Data.SqlClient;
using CorManagementSystem.PMSClass;
using System.Data;
using System.Data.SqlClient;

namespace CorManagementSystem
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            showCurrentTime();
        }
        private SqlDataAdapter sda;
        private void showCurrentTime()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0,0,1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            currentTimeLabel.Content = DateTime.Now.ToString();
        }
        private void Item1_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = new Employee();
            employee.Show();
        }
        private void Item2_Click(object sender, RoutedEventArgs e)
        {          
            PrizeAndPunishment prizeAndPunishment = new PrizeAndPunishment();     
            prizeAndPunishment.Show();
        }
        private void Item7_Click(object sender, RoutedEventArgs e)
        {
            Department department = new Department();
            department.Show();
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
           
        }
    }
}
