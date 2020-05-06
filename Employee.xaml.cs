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
using System.Collections;
using CorManagementSystem.PMSClass;

namespace CorManagementSystem
{
    /// <summary>
    /// Employee.xaml 的交互逻辑
    /// </summary>
    public partial class Employee : Window
    {
        public Employee()
        {
            InitializeComponent();
        }
        private SqlDataAdapter sda;
        public DataSet DS
        {
            get;
            set;
        }
        public string HeaderName
        {
            get;
            set;
        }
        public DataRowView DRV
        {
            set;
            get;
        }
        public TreeViewItem TreeVItem
        {
            get;
            set;
        }
        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            TreeVItem = sender as TreeViewItem;
            if (TreeVItem != null)
            {
                HeaderName = TreeVItem.Header.ToString();
                string sql;
                switch (HeaderName)
                {
                    case "所有部门":
                        sql = "select*from tb_Employee";
                        break;
                    default:
                        sql = "select*from tb_Employee where EmployeeDept='" + HeaderName + "'";
                        break;
                }
                DS = new DBOperate().GetDataSet(sql, HeaderName, ref sda);
                dataGrid.DataContext = DS.Tables[HeaderName];
                label.Content = HeaderName + "员工：" + DS.Tables[HeaderName].Rows.Count.ToString() + "人";
                //此处终于让我对于路由事件有了深刻的认识！这里也可以选用PreGotFocus事件，效果等效。
                e.Handled = true;

            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddEmployee addEmployee = new AddEmployee();
            addEmployee.Show();
        }
        private void ModifyButton_Click(object sender, RoutedEventArgs e)
        {
            ModifyEmployee modifyEmployee = new ModifyEmployee();
            if (DRV != null)
            {
                modifyEmployee.ModifyEmployeeID = (string)DRV["员工编号"];
                string sql = "select*from tb_Employee where EmployeeID='" + (string)DRV["员工编号"] + "'";
                SqlDataAdapter sda = new SqlDataAdapter(sql, DBConnection.MyConnection());
                DataSet ds = new DataSet();
                sda.Fill(ds, (string)DRV["员工编号"]);
                //SqlCommandBuilder scb = new SqlCommandBuilder(sda);
                DataTable dt = ds.Tables[0];
                DataRow dr = dt.Rows[0];
                if ((string)dr["EmployeeID"] != "")
                { modifyEmployee.textBox1.Text = (string)dr["EmployeeID"]; }
                if ((string)dr["EmployeeName"] != "")
                { modifyEmployee.textBox2.Text = (string)dr["EmployeeName"]; }
                if ((string)dr["EmployeeSex"] != "")
                { modifyEmployee.comboBox1.Text = (string)dr["EmployeeSex"]; }
                if ((string)dr["EmployeeDept"] != "")
                { modifyEmployee.comboBox2.Text = (string)dr["EmployeeDept"]; }
                if ((string)dr["EmployeeBirthday"] != "")
                { modifyEmployee.datePicker1.DisplayDate = Convert.ToDateTime((string)dr["EmployeeBirthday"]); }
                if ((string)dr["EmployeeNation"] != "")
                { modifyEmployee.comboBox3.Text = (string)dr["EmployeeNation"]; }
                if (dr["EmployeeMarriage"] != null)
                { modifyEmployee.comboBox4.Text = (string)dr["EmployeeMarriage"]; }
                if ((string)dr["EmployeeDuty"] != "")
                { modifyEmployee.comboBox5.Text = (string)dr["EmployeeDuty"]; }
                if ((string)dr["EmployeePhone"] != "")
                { modifyEmployee.textBox3.Text = (string)dr["EmployeePhone"]; }
                if ((string)dr["EmployeeAccession"] != "")
                { modifyEmployee.datePicker2.DisplayDate = Convert.ToDateTime((string)dr["EmployeeAccession"]); }
                if ((string)dr["EmployeePay"] != "")
                { modifyEmployee.textBox4.Text = (string)dr["EmployeePay"]; }
                if ((string)dr["EmployeePhoto"] != "")
                {
                    modifyEmployee.ImagPath = (string)dr["EmployeePhoto"];
                    BitmapImage bmi = new BitmapImage();
                    bmi.BeginInit();
                    bmi.UriSource = new Uri((string)dr["EmployeePhoto"]);
                    bmi.CacheOption = BitmapCacheOption.None;
                    bmi.EndInit();
                    modifyEmployee.img.Source = bmi;
                }
                modifyEmployee.Show();
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (DRV != null)
            {
                MessageBoxResult result=MessageBox.Show("确定删除整行吗？", "警告", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK)
                {
                    string sql = string.Format("delete from tb_Employee where EmployeeID='{0}'", (string)DRV["员工编号"]);
                    SqlConnection cn = DBConnection.MyConnection();
                    cn.Open();
                    SqlCommand sc = new SqlCommand(sql, cn);
                    sc.ExecuteNonQuery();
                    cn.Close();
                    //我打算在这里主动触发一下Selected事件，可是不知道怎么实例化RoutedEvent对象封装进RoutedEventArgs里面
                    //而我又必须在事件处理程序里面用e.handled没办法避开e.这里先搁置直接手动触发一次Selected事件,采用原始方法来刷新吧！
                    if (TreeVItem != null)
                    {
                        HeaderName = TreeVItem.Header.ToString();
                        switch (HeaderName)
                        {
                            case "所有部门":
                                sql = "select*from tb_Employee";
                                break;
                            default:
                                sql = "select*from tb_Employee where EmployeeDept='" + HeaderName + "'";
                                break;
                        }
                        //这里sda声明后没有初始化为什么也能用ref修饰？？
                        DS = new DBOperate().GetDataSet(sql, HeaderName, ref sda);
                        dataGrid.DataContext = DS.Tables[HeaderName];
                    }
                }
            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = textBox.Text;
            if (text != "")
            {
                string sql = "select*from tb_Employee where EmployeeName='" + text + "'";
                DS = new DBOperate().GetDataSet(sql, text, ref sda);
                dataGrid.DataContext = DS.Tables[text];
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                IList list = e.AddedItems;
                DRV = list[0] as DataRowView;
                string empId = (string)DRV["员工编号"];//获取被选中行的员工编号列的数据
                SqlConnection cn = DBConnection.MyConnection();
                string sql = "select*from tb_Employee where EmployeeID='" + empId + "'";//构造sql查询语句
                cn.Open();
                SqlCommand sc = new SqlCommand(sql, cn);
                SqlDataReader sda = sc.ExecuteReader(CommandBehavior.CloseConnection);
                sda.Read();
                string photoPath = "";
                for (int i = 0; i < sda.FieldCount; i++)
                {
                    if (sda.GetName(i) == "EmployeePhoto")
                    {
                        photoPath = (string)sda.GetValue(i);//获取员工照片存储路径
                        break;
                    }
                }
                sda.Close();
                //不熟悉的知识点，为image对象的source属性赋值
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(photoPath, UriKind.RelativeOrAbsolute);
                bi.EndInit();
                groupBox.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                img.Source = bi;


            }
        }
        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Button modifyButton = new Button();
            RoutedEventArgs routedEventArgs = new RoutedEventArgs();
            ModifyButton_Click(modifyButton,routedEventArgs);
        }
        private void EmployeeWindow_Loaded(object sender, RoutedEventArgs e)
        {
            string sql = "select*from tb_Department";
            SqlDataAdapter sda = null;
            DataSet ds = new DBOperate().GetDataSet_Department(sql, "tb_Department",ref sda);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                TreeViewItem tvi = new TreeViewItem();
                tvi.Header =(string)dr["部门名称"];
                tvi.Selected += TreeViewItem_Selected;
                treeViewItem.Items.Add(tvi);
            }
        }
    }
}
