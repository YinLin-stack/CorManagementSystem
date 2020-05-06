using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CorManagementSystem.PMSClass
{
    class DBConnection
    {
        public static SqlConnection MyConnection()
        {
            return new SqlConnection(@"Integrated Security=true;Initial Catalog=db_PMS;Data Source=localhost\SQLEXPRESS");
        }
    }
}
