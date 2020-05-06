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
using CorManagementSystem.PMSClass;

namespace CorManagementSystem
{
    /// <summary>
    /// AddDepartment.xaml 的交互逻辑
    /// </summary>
    public partial class AddDepartment : Window
    {
        public AddDepartment()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text == "") return;
            string sql = "select*from tb_Department where DepartmentName='" + textBox.Text + "'";
            SqlConnection cn = DBConnection.MyConnection();
            cn.Open();
            SqlCommand sc = new SqlCommand(sql,cn);
            SqlDataReader sdr = sc.ExecuteReader(CommandBehavior.CloseConnection);
            if (sdr.HasRows)
            {
                MessageBox.Show("部门名重复!","提示",MessageBoxButton.OK,MessageBoxImage.Warning);
                return;
            }
            sdr.Close();
            sql = string.Format("insert into tb_Department (DepartmentName)values('{0}')", textBox.Text);
            cn = DBConnection.MyConnection();
            cn.Open();
            sc = new SqlCommand(sql,cn);
            sc.ExecuteNonQuery();
            cn.Close();
        }
    }
}
