using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Data.Common;
using Microsoft.Win32;
using System.Windows.Controls;

namespace CorManagementSystem.PMSClass
{
    class DBOperate
    {
        public int OperateData(string strSql)
        {
            SqlConnection cn = DBConnection.MyConnection();
            cn.Open();
            SqlCommand cmd = new SqlCommand(strSql,cn);
            int i = cmd.ExecuteNonQuery();
            cn.Close();
            return i;
        }
        //注意下面第三个参数的ref，忘了哈！很重要的的知识点！
        public DataSet GetDataSet(string sql,string tableName, ref SqlDataAdapter sda)
        {
            sda = new SqlDataAdapter(sql,DBConnection.MyConnection());
            DataSet ds = new DataSet();
            DataTableMapping dataTableMapping = sda.TableMappings.Add("tb_Employee",tableName);
            dataTableMapping.ColumnMappings.Add("ID", "编号");
            dataTableMapping.ColumnMappings.Add("EmployeeID", "员工编号");
            dataTableMapping.ColumnMappings.Add("EmployeeName", "员工姓名");
            dataTableMapping.ColumnMappings.Add("EmployeeSex", "员工性别");
            dataTableMapping.ColumnMappings.Add("EmployeeDept", "所属部门");
            dataTableMapping.ColumnMappings.Add("EmployeeBirthday", "员工生日");
            dataTableMapping.ColumnMappings.Add("EmployeeNation", "民族");
            dataTableMapping.ColumnMappings.Add("EmployeeMarriage","婚姻状况");
            dataTableMapping.ColumnMappings.Add("EmployeeDuty", "担任职务");
            dataTableMapping.ColumnMappings.Add("EmployeePhone", "联系电话");
            dataTableMapping.ColumnMappings.Add("EmployeePhoto", "");
            dataTableMapping.ColumnMappings.Add("EmployeeAccession", "就职日期");
            dataTableMapping.ColumnMappings.Add("EmployeePay", "工资待遇");
            //注意这里Fill方法的第二个参数的写法！一般情况下是写临时表的表明，有数据表映射时必须写数据库表名，否则不能映射出友好名称！
            sda.Fill(ds,"tb_Employee");
            ds.Dispose();
            return ds;
        }
        public DataSet GetDataSet_PrizeAndPunishment(string sql, string tableName, out SqlDataAdapter sda)
        {
            sda = new SqlDataAdapter(sql, DBConnection.MyConnection());
            DataSet ds = new DataSet();
            DataTableMapping dataTableMapping = sda.TableMappings.Add("tb_PrizeAndPunishment", tableName);
            dataTableMapping.ColumnMappings.Add("Id", "编号");
            dataTableMapping.ColumnMappings.Add("UserID", "员工编号");
            dataTableMapping.ColumnMappings.Add("UserName", "员工姓名");
            dataTableMapping.ColumnMappings.Add("UserDept", "所属部门");
            dataTableMapping.ColumnMappings.Add("UserJF", "奖/罚");
            dataTableMapping.ColumnMappings.Add("UserJFContent", "奖罚内容");
            dataTableMapping.ColumnMappings.Add("UserJLMoney", "奖励金额");
            dataTableMapping.ColumnMappings.Add("UserFKMoney", "罚款金额");
            dataTableMapping.ColumnMappings.Add("UserJFDate", "奖罚日期");
            dataTableMapping.ColumnMappings.Add("UserCXDate", "撤销日期");
            //注意这里Fill方法的第二个参数的写法！一般情况下是写临时表的表明，有数据表映射时必须写数据库表名，否则不能映射出友好名称！
            sda.Fill(ds, "tb_PrizeAndPunishment");
            ds.Dispose();
            return ds;
        }
        public DataSet GetDataSet_Department(string sql, string tableName, ref SqlDataAdapter sda)
        {
            sda = new SqlDataAdapter(sql, DBConnection.MyConnection());
            DataSet ds = new DataSet();
            DataTableMapping dataTableMapping = sda.TableMappings.Add("tb_Department", tableName);
            dataTableMapping.ColumnMappings.Add("Id", "编号");
            dataTableMapping.ColumnMappings.Add("DepartmentName", "部门名称");
            //注意这里Fill方法的第二个参数的写法！一般情况下是写临时表的表明，有数据表映射时必须写数据库表名，否则不能映射出友好名称！
            sda.Fill(ds, "tb_Department");
            ds.Dispose();
            return ds;
        }

        public string GetImagePath()
        {
            OpenFileDialog openF = new OpenFileDialog();
            openF.DefaultExt = ".png";
            openF.Filter = "*.jpg|*.jpg|*.png|*.png|*.jif|*.jif|*.jpeg|*.jpeg|*.bmp|*.bmp";
            string path = "";
            if(openF.ShowDialog()==true)
            {
                return openF.FileName;        
            }
            return path;
        }
        //刷新DataGrid专用函数      
    }
}
