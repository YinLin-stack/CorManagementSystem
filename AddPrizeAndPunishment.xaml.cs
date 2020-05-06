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
    /// AddPrizeAndPunishment.xaml 的交互逻辑
    /// </summary>
    public partial class AddPrizeAndPunishment : Window
    {
        public AddPrizeAndPunishment()
        {
            InitializeComponent();
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox1.Text == "" ||comboBox2.Text==""|| textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("请完善奖罚信息！","提示",MessageBoxButton.OK,MessageBoxImage.Information);
                return;
            }
            string sql = string.Format("insert into tb_PrizeAndPunishment (UserID,UserName,UserDept,UserJF,UserJFContent,UserJLMoney,UserFKMoney,UserJFDate,UserCXDate)values" +
                "('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",comboBox1.Text,textBox1.Text,textBox2.Text,comboBox2.Text,textBox3.Text.Trim(),textBox4.Text,textBox5.Text,datePicker1.DisplayDate.ToString(),
                datePicker2.DisplayDate.ToString());
            SqlConnection cn = DBConnection.MyConnection();
            cn.Open();
            SqlCommand sc = new SqlCommand(sql,cn);
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
                    textBox4.Text ="";
                    textBox5.Text ="0";
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
