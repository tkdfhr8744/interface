using DB;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _20181123
{
    class MappingView
    {
        private MSsql db;
        private Commons comm;
        private Panel member, rule, mapping;
        private Label label1, label2, label3, label4;
        private ComboBox comboBox1, comboBox2;
        private Button btn1, btn2;
        private ListView listView;
        private Form parentForm;
        private Hashtable hashtable;
        private BindingSource bs;
        private int result1, result2;

        public MappingView(Form parentForm, Object oDB)
        {
            this.parentForm = parentForm;
            this.db = (MSsql)oDB;
            comm = new Commons();
            getView();
        }
        private void getView()
        {
            hashtable = new Hashtable();
            hashtable.Add("size", new Size(500, 45));
            hashtable.Add("point", new Point(0, 0));
            hashtable.Add("color", Color.Red);
            hashtable.Add("name", "member");
            member = comm.getPanel(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(500, 45));
            hashtable.Add("point", new Point(500, 0));
            hashtable.Add("color", Color.Yellow);
            hashtable.Add("name", "rule");
            rule = comm.getPanel(hashtable, parentForm);
            
            hashtable = new Hashtable();
            hashtable.Add("size", new Size(1000, 655));
            hashtable.Add("point", new Point(0, 45));
            hashtable.Add("color", Color.White);
            hashtable.Add("name", "mapping");
            mapping = comm.getPanel(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(500, 20));
            hashtable.Add("point", new Point(0, 5));
            hashtable.Add("color", Color.Red);
            hashtable.Add("name", "label1");
            hashtable.Add("text", "Member");
            label1 = comm.getLabel(hashtable, member);
            
            hashtable = new Hashtable();
            hashtable.Add("size", new Size(400, 20));
            hashtable.Add("point", new Point(0, 5));
            hashtable.Add("color", Color.Yellow);
            hashtable.Add("name", "label2");
            hashtable.Add("text", "Rule");
            label2 = comm.getLabel(hashtable, rule);

            hashtable = new Hashtable();
            hashtable.Add("width", 500);
            hashtable.Add("point", new Point(0, 25));
            hashtable.Add("color", Color.White);
            hashtable.Add("name", "comboBox1");
            hashtable.Add("click", (EventHandler) Member_click);
            comboBox1 = comm.getComboBox(hashtable, member);

            hashtable = new Hashtable();
            hashtable.Add("width", 485);
            hashtable.Add("point", new Point(0, 25));
            hashtable.Add("color", Color.White);
            hashtable.Add("name", "comboBox2");
            hashtable.Add("click", (EventHandler) Rule_click);
            comboBox2 = comm.getComboBox(hashtable, rule);

            hashtable = new Hashtable();
            hashtable.Add("color", Color.White);
            hashtable.Add("name", "listView");
            //hashtable.Add("click", (MouseEventHandler)listView_click);
            listView = comm.getListView(hashtable, mapping);

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(50, 20));
            hashtable.Add("point", new Point(400, 5));
            hashtable.Add("color", Color.Yellow);
            hashtable.Add("name", "추가");
            hashtable.Add("text", "추가");
            hashtable.Add("click", (EventHandler)Btn1_Click);
            btn1 = comm.getButton(hashtable, rule);
            
            GetSelect();
        }

        private void GetSelect()
        {
            SelectMember();
            SelectRule();
            listset();
        }

        private void listset()
        {
            listView.Clear();
            listView.Columns.Add("rNo", 45, HorizontalAlignment.Center);
            listView.Columns.Add("rName", 100, HorizontalAlignment.Left);
            listView.Columns.Add("mNo", 45, HorizontalAlignment.Center);
            listView.Columns.Add("mName", 100, HorizontalAlignment.Center);
            string sql = "select Mapping.mNo,Member.mName,Mapping.rNo ,Rule.rName from Mapping,Member,Rule where Mapping.rNo=Rule.rNo and Member.mNo= Mapping.mNo;";
            MySqlDataReader sdr = db.Reader(sql);
            while (sdr.Read())//행
            {
                string[] arr = new string[sdr.FieldCount];
                for (int i = 0; i < sdr.FieldCount; i++)
                {
                    arr[i] = sdr.GetValue(i).ToString();
                }
                listView.Items.Add(new ListViewItem(arr));
            }
            db.ReaderClose(sdr);
        }//나중에 조인해서 이름까지
        
        private void SelectMember()
        {
            string sql = "select mNo, mName from Member where delYn = 'N';";
            MySqlDataReader sdr = db.Reader(sql);
            bs = new BindingSource();
            hashtable = new Hashtable();
            hashtable.Add("0", "선택하세요.");

            while (sdr.Read())
            {
                hashtable.Add(sdr.GetInt32(0), sdr.GetString(1));
            }
            db.ReaderClose(sdr);
            bs.DataSource = hashtable;
            comboBox1.DataSource = bs;
            comboBox1.SelectedIndexChanged += Member_click;
        }

        private void SelectRule()
        {
            string sql = "select rNo, rName from Rule where delYn = 'N';";
            MySqlDataReader sdr = db.Reader(sql);
            bs = new BindingSource();
            hashtable = new Hashtable();
            hashtable.Add("0", "선택하세요.");
            while (sdr.Read())
            {
                hashtable.Add(sdr.GetInt32(0), sdr.GetString(1));
            }
            db.ReaderClose(sdr);
            bs.DataSource = hashtable;
            comboBox2.DataSource = bs;
            comboBox2.SelectedIndexChanged += Rule_click;
        }

        private void Member_click(object o, EventArgs a)
        {
           
            switch (comboBox1.SelectedValue.ToString()) {
                case "0":
                    return;
                default:
                    //MessageBox.Show(comboBox1.Text);
                    result1=Convert.ToInt32(comboBox1.SelectedValue);
                  
                    break;

            }
        }

        private void Rule_click(object o, EventArgs a)
        {
            switch (comboBox2.SelectedValue.ToString())
            {
                case "0":
                    return;
                default:
                    result2=Convert.ToInt32(comboBox2.SelectedValue);
                    //MessageBox.Show(comboBox2.Text);
                    break;
            }
        }

        private void Btn1_Click(object sender, EventArgs e)
        {
            bool equl= true;

            string sql1 = string.Format("select * from Mapping where rNo ={1} and mNo = {0};", result1, result2);
            MySqlDataReader sdr = db.Reader(sql1);
            while(sdr.Read())
            {
                equl = false;
            }
            db.ReaderClose(sdr);
            if (equl)
            {
                string sql2 = string.Format("insert into Mapping (rNo, mNo) values ({1},{0});", result1, result2);
                db.NonQuery(sql2);
            }
            else
            {
                MessageBox.Show("중복된 관계입니다.");
            }
            listset();
        }
    }
}
