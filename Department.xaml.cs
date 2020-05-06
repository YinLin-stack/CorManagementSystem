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
    /// Department.xaml 的交互逻辑
    /// </summary>
    public partial class Department : Window
    {
        public Department()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string sql = "select*from tb_Department";
            SqlConnection cn = DBConnection.MyConnection();
            SqlDataAdapter sda = null;//外面保留一个sqlDataAdapter对象好像没什么用
            DataSet ds = new DBOperate().GetDataSet_Department(sql,"Department",ref sda);
            dataGrid.DataContext = ds.Tables[0];
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs routedEventArgs = new RoutedEventArgs();
            Window_Loaded(this,routedEventArgs);
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddDepartment addDepartment = new AddDepartment();
            addDepartment.Show();
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null) return;
            MessageBoxResult result= MessageBox.Show("你确定要删除本行吗？","提示",MessageBoxButton.OKCancel,MessageBoxImage.Warning);
            if (result == MessageBoxResult.Cancel) return;
            DataRowView drv = dataGrid.SelectedItem as DataRowView;
            if (drv != null)
            {
                string deptName = (string)drv.Row["部门名称"];
                string sql = "delete from tb_Department where DepartmentName='" + deptName + "'";
                SqlConnection cn = DBConnection.MyConnection();
                cn.Open();
                SqlCommand sc = new SqlCommand(sql, cn);
                sc.ExecuteNonQuery();
                cn.Close();
            }

        }
    }
}
