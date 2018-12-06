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
    class RuleView
    {
        private MSsql db;
        private Commons comm;
        private Panel head, contents;
        private ListView listView;
        private TextBox textBox1, textBox2, textBox3, textBox4, textBox5, textBox6;
        private Button btn1, btn2, btn3, btn4;
        private Form parentForm;
        private Hashtable hashtable;

        public RuleView(Form parentForm, Object oDB)
        {
            this.parentForm = parentForm;
            this.db = (MSsql)oDB;
            comm = new Commons();
            getView();
        }

        private void getView()
        {

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(1000, 500));
            hashtable.Add("point", new Point(0, 0));
            hashtable.Add("color", Color.White);
            hashtable.Add("name", "head");
            head = comm.getPanel(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(1000, 200));
            hashtable.Add("point", new Point(0, 500));
            hashtable.Add("color", Color.Yellow);
            hashtable.Add("name", "contents");
            contents = comm.getPanel(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("width", 45);
            hashtable.Add("point", new Point(0, 0));
            hashtable.Add("color", Color.Silver);
            hashtable.Add("name", "textBox1");
            hashtable.Add("enabled", false);
            textBox1 = comm.getTextBox(hashtable, contents);

            hashtable = new Hashtable();
            hashtable.Add("width", 100);
            hashtable.Add("point", new Point(45, 0));
            hashtable.Add("color", Color.White);
            hashtable.Add("name", "textBox2");
            hashtable.Add("enabled", true);
            textBox2 = comm.getTextBox(hashtable, contents);

            hashtable = new Hashtable();
            hashtable.Add("width", 200);
            hashtable.Add("point", new Point(145, 0));
            hashtable.Add("color", Color.White);
            hashtable.Add("name", "textBox3");
            hashtable.Add("enabled", true);
            textBox3 = comm.getTextBox(hashtable, contents);

            hashtable = new Hashtable();
            hashtable.Add("width", 50);
            hashtable.Add("point", new Point(345, 0));
            hashtable.Add("color", Color.Silver);
            hashtable.Add("name", "textBox4");
            hashtable.Add("enabled", false);
            textBox4 = comm.getTextBox(hashtable, contents);

            hashtable = new Hashtable();
            hashtable.Add("width", 300);
            hashtable.Add("point", new Point(395, 0));
            hashtable.Add("color", Color.Silver);
            hashtable.Add("name", "textBox5");
            hashtable.Add("enabled", false);
            textBox5 = comm.getTextBox(hashtable, contents);

            hashtable = new Hashtable();
            hashtable.Add("width", 300);
            hashtable.Add("point", new Point(695, 0));
            hashtable.Add("color", Color.Silver);
            hashtable.Add("name", "textBox6");
            hashtable.Add("enabled", false);
            textBox6 = comm.getTextBox(hashtable, contents);

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(100, 80));
            hashtable.Add("point", new Point(100, 50));
            hashtable.Add("color", Color.White);
            hashtable.Add("name", "btn1");
            hashtable.Add("text", "저장");
            hashtable.Add("click", (EventHandler)btn_click);
            btn1 = comm.getButton(hashtable, contents);

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(100, 80));
            hashtable.Add("point", new Point(300, 50));
            hashtable.Add("color", Color.White);
            hashtable.Add("name", "btn2");
            hashtable.Add("text", "수정");
            hashtable.Add("click", (EventHandler)btn_click);
            btn2 = comm.getButton(hashtable, contents);

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(100, 80));
            hashtable.Add("point", new Point(600, 50));
            hashtable.Add("color", Color.White);
            hashtable.Add("name", "btn3");
            hashtable.Add("text", "삭제");
            hashtable.Add("click", (EventHandler)btn_click);
            btn3 = comm.getButton(hashtable, contents);

            hashtable = new Hashtable();
            hashtable.Add("size", new Size(100, 80));
            hashtable.Add("point", new Point(800, 50));
            hashtable.Add("color", Color.White);
            hashtable.Add("name", "btn4");
            hashtable.Add("text", "초기화");
            hashtable.Add("click", (EventHandler)btn_click);
            btn4 = comm.getButton(hashtable, contents);

            hashtable = new Hashtable();
            hashtable.Add("color", Color.White);
            hashtable.Add("name", "listView");
            hashtable.Add("click", (MouseEventHandler)listView_click);
            listView = comm.getListView(hashtable, head);

            SelectData();
        }


        private void listView_click(object o, EventArgs a)
        {
            ListView lv = (ListView)o;
            ListView.SelectedListViewItemCollection itemGroup = lv.SelectedItems;
            ListViewItem item = itemGroup[0];
            textBox1.Text = item.SubItems[0].Text;
            textBox2.Text = item.SubItems[1].Text;
            textBox3.Text = item.SubItems[2].Text;
            textBox4.Text = item.SubItems[3].Text;
            textBox5.Text = item.SubItems[4].Text;
            textBox6.Text = item.SubItems[5].Text;
        }

        private void btn_click(object o, EventArgs a)
        {
            Button btn = (Button)o;
            switch (btn.Name)
            {
                case "btn1": // 저장
                    InsertData();
                    break;
                case "btn2": // 수정
                    UpdateData();
                    break;
                case "btn3": // 삭제
                    DeleteData();
                    break;
                case "btn4": // 초기화
                    SelectData();
                    break;
                default:
                    break;
            }
        }

        private void SelectData()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            listView.Clear();

            listView.Columns.Add("rNo", 45, HorizontalAlignment.Center);
            listView.Columns.Add("rName", 100, HorizontalAlignment.Left);
            listView.Columns.Add("rDesc", 200, HorizontalAlignment.Left);
            listView.Columns.Add("delYn", 50, HorizontalAlignment.Center);
            listView.Columns.Add("regDate", 300, HorizontalAlignment.Left);
            listView.Columns.Add("modDate", 300, HorizontalAlignment.Left);
            string sql = "select rNo, rName, rDesc, delYn, regDate, modDate from Rule;";
            MySqlDataReader sdr = db.Reader(sql);
            while (sdr.Read())
            {
                string[] arr = new string[sdr.FieldCount];
                for (int i = 0; i < sdr.FieldCount; i++)
                {
                    arr[i] = sdr.GetValue(i).ToString();
                }
                listView.Items.Add(new ListViewItem(arr));
            }
            db.ReaderClose(sdr);
        }

        private void InsertData()
        {
            if (textBox1.Text != "")
            {
                MessageBox.Show("권한이 선택 되어 있습니다. \n초기화 후 진행해주세요.");
                return;
            }

            if (TextBoxCheck())
            {
                string sql = string.Format("insert into Rule (rName, rDesc) values ('{0}','{1}');", textBox2.Text, textBox3.Text);
                if (db.NonQuery(sql)) SelectData();
            }
        }

        private void UpdateData()
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("권한를 선택해주세요.");
                return;
            }

            if (TextBoxCheck())
            {
                string sql = string.Format("update Rule set rName = '{1}', rDesc = '{2}', modDate = getDate() where rNo = {0}", textBox1.Text, textBox2.Text, textBox3.Text);
                if (db.NonQuery(sql)) SelectData();
            }
        }

        private void DeleteData()
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("권한를 선택해주세요.");
                return;
            }
            string sql = string.Format("update Rule set delYn = 'Y' where rNo = {0}", textBox1.Text);
            if (db.NonQuery(sql)) SelectData();
        }

        private bool TextBoxCheck()
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("권한이름을 입력하세요.");
                textBox2.Focus();
                return false;
            }
            return true;
        }
    }
}
