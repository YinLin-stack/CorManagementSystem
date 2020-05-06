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
using System.Data;
using System.Data.SqlClient;
using Microsoft.Win32;
using CorManagementSystem.PMSClass;
using System.Data;
using System.Data.SqlClient;

namespace CorManagementSystem
{
    /// <summary>
    /// AddEmployee.xaml 的交互逻辑
    /// </summary>
    public partial class AddEmployee : Window
    {
        public AddEmployee()
        {
            InitializeComponent();
        }
        public string ImagPath
        {
            set;
            get;
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text != "")
            {
                SqlConnection cn = DBConnection.MyConnection();
                cn.Open();
                string sqlCmd = string.Format("select*from tb_Employee where EmployeeID='{0}'",textBox1.Text);
                SqlCommand sqlCommand = new SqlCommand(sqlCmd,cn);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    MessageBox.Show("员工编号冲突!","提示",MessageBoxButton.OK,MessageBoxImage.Warning);
                    return;
                }
            }
            if (textBox3.Text.Length != 11) { MessageBox.Show("电话号码格式不正确！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning); }
            string sql = string.Format("insert into tb_Employee (EmployeeID,EmployeeName,EmployeeSex,EmployeeDept,EmployeeBirthday,EmployeeNation,EmployeeMarriage,EmployeeDuty,EmployeePhone,EmployeeAccession,EmployeePhoto,EmployeePay)values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}') ",
                textBox1.Text.Trim(),textBox2.Text.Trim(),comboBox1.Text ,comboBox2.Text, datePicker1.SelectedDate.ToString(), comboBox3.Text, comboBox4.Text,
                comboBox5.Text, textBox3.Text.Trim(),datePicker2.SelectedDate.ToString(), ImagPath, float.Parse(textBox4.Text));
            new DBOperate().OperateData(sql);        
            this.Close();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SelectImgButton_Click(object sender, RoutedEventArgs e)
        {
            ImagPath = new DBOperate().GetImagePath();
            if (ImagPath != "")
            {
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(ImagPath, UriKind.RelativeOrAbsolute);
                bi.EndInit();
                img.Source = bi;
            }

        }
    }
}
