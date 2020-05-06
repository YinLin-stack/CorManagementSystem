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
    /// PrizeAndPunishment.xaml 的交互逻辑
    /// </summary>
    public partial class PrizeAndPunishment : Window
    {
       
        public PrizeAndPunishment()
        {
            InitializeComponent();
        }
        public AddPrizeAndPunishment AddPrizeAndPunishment
        {
            get;
            set;
        }
        public ModifyPrizeAndPunishment ModifyPrizeAndPunishment
        {
            set;
            get;
        }
        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            string sql = "select*from tb_PrizeAndPunishment";
            //这个适配器别的地方用得着吗，犯得着放在外面？
            SqlDataAdapter sda;
            DataSet ds = new DBOperate().GetDataSet_PrizeAndPunishment(sql,"tb_PrizeAndPunishment",out sda);
            dataGrid.DataContext = ds.Tables[0];
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string sql = "select*from tb_Employee";
            SqlDataAdapter sda = null;
            DataSet ds = new DBOperate().GetDataSet(sql,"tb_Employee",ref sda);
            AddPrizeAndPunishment = new AddPrizeAndPunishment();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                ComboBoxItem cbi = new ComboBoxItem();
                cbi.Content = (string)dr["员工编号"];
                cbi.Selected += Cbi_Selected;
                AddPrizeAndPunishment.comboBox1.Items.Add(cbi);
            }
            AddPrizeAndPunishment.Show();
        }

        private void Cbi_Selected(object sender, RoutedEventArgs e)
        {
            ComboBoxItem cbi = sender as ComboBoxItem;
            if (cbi != null)
            {
                string employeeID = (string)cbi.Content;
                string sql = "select*from tb_Employee where EmployeeID='" + employeeID + "'";
                SqlConnection cn = DBConnection.MyConnection();
                cn.Open();
                SqlCommand sc = new SqlCommand(sql,cn);
                SqlDataReader sdr = sc.ExecuteReader(CommandBehavior.CloseConnection);
                sdr.Read();
                for (int i = 0; i < sdr.FieldCount; i++)
                {
                    if (sdr.GetName(i) == "EmployeeName")
                    {
                        AddPrizeAndPunishment.textBox1.Text = (string)sdr.GetValue(i);
                    }
                    if (sdr.GetName(i) == "EmployeeDept")
                    {
                        AddPrizeAndPunishment.textBox2.Text = (string)sdr.GetValue(i);
                    }
                }             
            }
        }
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            string sql = "select*from tb_PrizeAndPunishment";
            //这个适配器别的地方用得着吗，犯得着放在外面？
            SqlDataAdapter sda;
            DataSet ds = new DBOperate().GetDataSet_PrizeAndPunishment(sql, "tb_PrizeAndPunishment", out sda);
            dataGrid.DataContext = ds.Tables[0];
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void ModifyButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null) return;
            ModifyPrizeAndPunishment = new ModifyPrizeAndPunishment();
            DataRowView drv = dataGrid.SelectedItem as DataRowView;
            if (drv != null)
            {
                ModifyPrizeAndPunishment.ID= (int)drv.Row["编号"];
                string userID = (string)drv.Row["员工编号"];
                ModifyPrizeAndPunishment.comboBox2.Text = (string)drv["奖/罚"];
                ModifyPrizeAndPunishment.textBox1.Text = (string)drv["员工姓名"];
                ModifyPrizeAndPunishment.textBox2.Text = (string)drv["所属部门"];
                ModifyPrizeAndPunishment.textBox3.Text = (string)drv["奖罚内容"];
                ModifyPrizeAndPunishment.textBox4.Text = (string)drv["奖励金额"];
                ModifyPrizeAndPunishment.textBox5.Text = (string)drv["罚款金额"];
                ModifyPrizeAndPunishment.datePicker1.DisplayDate = Convert.ToDateTime((string)drv["奖罚日期"]);
                ModifyPrizeAndPunishment.datePicker2.DisplayDate = Convert.ToDateTime((string)drv["撤销日期"]);
                string sql = "select*from tb_Employee";
                SqlDataAdapter sda = new SqlDataAdapter();
                DataSet ds = new DBOperate().GetDataSet(sql, "tb_Employee", ref sda);
                foreach (DataRow dr1 in ds.Tables[0].Rows)
                {
                    ComboBoxItem cbi = new ComboBoxItem();
                    cbi.Content = (string)dr1["员工编号"];
                    cbi.Selected += Cbi_Selected1;
                    if ((string)cbi.Content == userID)
                    {
                        cbi.IsSelected = true;
                    }
                    ModifyPrizeAndPunishment.comboBox1.Items.Add(cbi);
                }
            }
             
            ModifyPrizeAndPunishment.Show();          
        }
        private void Cbi_Selected1(object sender, RoutedEventArgs e)
        {
            ComboBoxItem cbi = sender as ComboBoxItem;
            if (cbi != null)
            {
                string employeeID = (string)cbi.Content;
                string sql = "select*from tb_Employee where EmployeeID='" + employeeID + "'";
                SqlConnection cn = DBConnection.MyConnection();
                cn.Open();
                SqlCommand sc = new SqlCommand(sql, cn);
                SqlDataReader sdr = sc.ExecuteReader(CommandBehavior.CloseConnection);
                sdr.Read();
                for (int i = 0; i < sdr.FieldCount; i++)
                {
                    if (sdr.GetName(i) == "EmployeeName")
                    {
                         ModifyPrizeAndPunishment.textBox1.Text = (string)sdr.GetValue(i);
                    }
                    if (sdr.GetName(i) == "EmployeeDept")
                    {
                         ModifyPrizeAndPunishment.textBox2.Text = (string)sdr.GetValue(i);
                    }
                }
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null) return;
            MessageBoxResult result= MessageBox.Show("确定要删除一行吗？","警告",MessageBoxButton.OKCancel,MessageBoxImage.Warning);
            if (result == MessageBoxResult.Cancel) return;
            else
            {
                DataRowView drv = dataGrid.SelectedItem as DataRowView;
                int id = (int)drv.Row["编号"];
                string sql =string.Format("delete from tb_PrizeAndPunishment where ID={0}",id);
                SqlConnection cn = DBConnection.MyConnection();
                cn.Open();
                SqlCommand sc = new SqlCommand(sql, cn);
                sc.ExecuteNonQuery();
                cn.Close();
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text != "")
            {              
                string sql = "select*from tb_PrizeAndPunishment where UserName='" + textBox.Text + "'";
                SqlDataAdapter sda;
                DataSet ds = new DBOperate().GetDataSet_PrizeAndPunishment(sql,textBox.Text,out sda);
                dataGrid.DataContext = ds.Tables[0];
            }
        }
    }
}
