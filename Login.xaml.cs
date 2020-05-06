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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using CorManagementSystem.PMSClass;


namespace CorManagementSystem
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (textBox.Text == "" || pwdBox.Password == "")
                {
                    MessageBox.Show("用户名或密码不能为空", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    string userName = textBox.Text.Trim();
                    string pwd = pwdBox.Password.Trim();
                    SqlConnection cn = DBConnection.MyConnection();
                    cn.Open();
                    string sql = string.Format("select*from tb_User where UserName='{0}'and password='{1}'", userName, pwd);
                    SqlCommand sc = new SqlCommand(sql, cn);
                    SqlDataReader reader = sc.ExecuteReader();
                    if (reader.HasRows)
                    {
                        cn.Close();
                        DateTime dateTime = DateTime.Now;
                        sql = string.Format("update tb_User set LoginTime='{0}' where UserName='{1}'",dateTime.ToString(),userName);
                        new DBOperate().OperateData(sql);
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        mainWindow.loginTimeLabel.Content = dateTime;
                        mainWindow.userNameLabel.Content = textBox.Text;
                        this.Close();
                        mainWindow.Show();
                    }
                    else
                    {
                        cn.Close();
                        MessageBox.Show("用户名或密码不正确", "错误提示", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch(Exception ex){ MessageBox.Show(ex.Message);}
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            textBox.Text = "";
            pwdBox.Password = "";
        }
    }
}
