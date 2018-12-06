using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB
{
    class MYsql
    {
        private MySqlConnection conn;
        public bool status;

        public MYsql()
        {
            status = Connection();
        }

        private bool Connection()
        {
            string host = "myDB";
            string user = "root";
            string password = "1234";
            string db = "gdc";

            string connStr = string.Format("server={0};uid={1};password={2};database={3}", host, user, password, db);
            MySqlConnection conn = new MySqlConnection(connStr);

            try
            {
                conn.Open();
                this.conn = conn;
                //MessageBox.Show("MS-SQL 연결 성공!");
                return true;
            }
            catch
            {
                conn.Close();
                this.conn = null;
                //MessageBox.Show("MS-SQL 연결 실패!");
                return false;
            }
        }

        public bool ConnectionClose()
        {
            try
            {
                conn.Close();
                //MessageBox.Show("MS-SQL 연결끊기 성공!");
                return true;
            }
            catch
            {
                //MessageBox.Show("MS-SQL 연결끊기 실패!");
                return false;
            }
        }

        public bool NonQuery(string sql)
        {
            try
            {
                if (status)
                {
                    MySqlCommand comm = new MySqlCommand(sql, conn);
                    comm.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    return false;
                }                
            }
            catch
            {
                return false;
            }
        }

        public MySqlDataReader Reader(string sql)
        {
            try
            {
                if (status)
                {
                    MySqlCommand comm = new MySqlCommand(sql, conn);
                    return comm.ExecuteReader();
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public void ReaderClose(MySqlDataReader reader)
        {
            reader.Close();
        }
    }
}
