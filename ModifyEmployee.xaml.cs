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
using CorManagementSystem.PMSClass;
using System.Data.SqlClient;
using System.Data;

namespace CorManagementSystem
{
    /// <summary>
    /// ModifyEmployee.xaml 的交互逻辑
    /// </summary>
    public partial class ModifyEmployee : Window
    {
        public ModifyEmployee()
        {
            InitializeComponent();
        }

        public string ImagPath
        {
            get;
            set;
        }
        public string ModifyEmployeeID
        {
            get;
            set;
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text != ""&&textBox1.Text!=ModifyEmployeeID)
            {
                SqlConnection cn = DBConnection.MyConnection();
                cn.Open();
                string sqlCmd = string.Format("select*from tb_Employee where EmployeeID='{0}'", textBox1.Text);
                SqlCommand sqlCommand = new SqlCommand(sqlCmd, cn);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    MessageBox.Show("员工编号冲突!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            string sql = string.Format("update tb_Employee set EmployeeID='{0}',EmployeeName='{1}', EmployeeSex='{2}',EmployeeDept='{3}' ,EmployeeBirthday='{4}' ,EmployeeNation='{5}' ,EmployeeMarriage='{6}', EmployeeDuty='{7}', EmployeePhone='{8}', EmployeeAccession='{9}', EmployeePhoto='{10}', EmployeePay='{11}'where EmployeeID='{12}'",
                textBox1.Text.Trim(), textBox2.Text.Trim(), comboBox1.Text, comboBox2.Text, datePicker1.SelectedDate.ToString(), comboBox3.Text, comboBox4.Text,
                comboBox5.Text, textBox3.Text.Trim(), datePicker2.SelectedDate.ToString(), ImagPath, float.Parse(textBox4.Text), ModifyEmployeeID);
            new DBOperate().OperateData(sql);
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
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
