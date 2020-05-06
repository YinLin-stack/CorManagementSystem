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
    /// ModifyPrizeAndPunishment.xaml 的交互逻辑
    /// </summary>
    public partial class ModifyPrizeAndPunishment : Window
    {
        public ModifyPrizeAndPunishment()
        {
            InitializeComponent();
        }
        public int ID
        {
            set;
            get;
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox1.Text == "" || comboBox2.Text == "" || textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("请完善奖罚信息！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            string sql = string.Format("update tb_PrizeAndPunishment set UserID='{0}',UserName='{1}' ,UserDept='{2}', UserJF='{3}' ,UserJFContent='{4}' ,UserJLMoney='{5}' ,UserFKMoney='{6}' ,UserJFDate='{7}' ,UserCXDate='{8}' where ID={9}",
                comboBox1.Text,textBox1.Text, textBox2.Text, comboBox2.Text, textBox3.Text.Trim(), textBox4.Text.Trim(), textBox5.Text.Trim(), datePicker1.SelectedDate.ToString(),
                datePicker2.SelectedDate.ToString(),ID);
            SqlConnection cn = DBConnection.MyConnection();
            cn.Open();
            SqlCommand sc = new SqlCommand(sql, cn);
            sc.ExecuteNonQuery();
            this.Close();
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void comboBox2_Selected(object sender, RoutedEventArgs e)
        {
            ComboBoxItem cbi = sender as ComboBoxItem;
            if (cbi != null)
            {
                string jfType = (string)cbi.Content;
                if (jfType == "奖励")
                {
                    textBox4.IsReadOnly = false;
                    textBox4.Text = "";
                    textBox5.Text = "0";
                    textBox5.IsReadOnly = true;
                }
                else//罚款
                {
                    textBox5.IsReadOnly = false;
                    textBox5.Text = "";
                    textBox4.Text = "0";
                    textBox4.IsReadOnly = true;
                }
            }
        }
    }
}
