using DB;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace _20181123
{
    public class MainView
    {
        private MSsql db;
        private Commons comm;
        private Panel head, contents;
        private Button btn1, btn2, btn3;
        private Form parentForm, tagetForm;
        private Hashtable hashtable;

        public MainView(Form parentForm)
        {
            this.parentForm = parentForm;
            db = new MSsql();
            comm = new Commons();
            getView();
        }

        private void getView()
        {

            string sql = "select * from ViewControl where vNo=1;";
            MySqlDataReader sdr = db.Reader(sql);
            while (sdr.Read())
            {
                string[] arr = new string[sdr.FieldCount];
                for (int i = 0; i < sdr.FieldCount; i++)
                {
                    arr[i] = sdr.GetValue(i).ToString();
                }
                
                hashtable = new Hashtable();
                if (arr[2] == "head")
                {
                    hashtable.Add("size", new Size(Convert.ToInt32(arr[6]), Convert.ToInt32(arr[7])));
                    hashtable.Add("point", new Point(Convert.ToInt32(arr[8]), Convert.ToInt32(arr[9])));
                    hashtable.Add("color", color_set(Convert.ToInt32(arr[10])));
                    hashtable.Add("name", arr[2]);
                    head = comm.getPanel(hashtable, parentForm);
                }
                
                else if (arr[2] == "contents")
                {
                    hashtable.Add("size", new Size(Convert.ToInt32(arr[6]), Convert.ToInt32(arr[7])));
                    hashtable.Add("point", new Point(Convert.ToInt32(arr[8]), Convert.ToInt32(arr[9])));
                    hashtable.Add("color", color_set(Convert.ToInt32(arr[10])));
                    hashtable.Add("name", arr[2]);
                    contents = comm.getPanel(hashtable, parentForm);
                }
                
                else if (arr[2] == "btn1")
                {
                    hashtable.Add("size", new Size(Convert.ToInt32(arr[6]), Convert.ToInt32(arr[7])));
                    hashtable.Add("point", new Point(Convert.ToInt32(arr[8]), Convert.ToInt32(arr[9])));
                    hashtable.Add("color", color_set(Convert.ToInt32(arr[10])));
                    hashtable.Add("name", arr[2]);
                    hashtable.Add("text", arr[3]);
                    hashtable.Add("click", click_fc(Convert.ToInt32(arr[10])));
                    btn1 = comm.getButton(hashtable, head);
                }

                else if (arr[2] == "btn2")
                {
                    hashtable.Add("size", new Size(Convert.ToInt32(arr[6]), Convert.ToInt32(arr[7])));
                    hashtable.Add("point", new Point(Convert.ToInt32(arr[8]), Convert.ToInt32(arr[9])));
                    hashtable.Add("color", color_set(Convert.ToInt32(arr[10])));
                    hashtable.Add("name", arr[2]);
                    hashtable.Add("text", arr[3]);
                    hashtable.Add("click", click_fc(Convert.ToInt32(arr[10])));
                    btn2 = comm.getButton(hashtable, head);
                }

                else if (arr[2] == "btn3")
                {
                    hashtable.Add("size", new Size(Convert.ToInt32(arr[6]), Convert.ToInt32(arr[7])));
                    hashtable.Add("point", new Point(Convert.ToInt32(arr[8]), Convert.ToInt32(arr[9])));
                    hashtable.Add("color", color_set(Convert.ToInt32(arr[10])));
                    hashtable.Add("name", arr[2]);
                    hashtable.Add("text", arr[3]);
                    hashtable.Add("click", click_fc(Convert.ToInt32(arr[10])));
                    btn3 = comm.getButton(hashtable, head);
                }

            }
            db.ReaderClose(sdr);

        }
        //color1=흰2=회3=빨4=초5=파

        private Color color_set(int num)
        {
            if (num == 1) return Color.White;
            else if (num == 2) return Color.Gray;
            else if (num == 3) return Color.Red;
            else if (num == 4) return Color.Green;
            else if (num == 5) return Color.Blue;
            else return Color.White;
        }

        private EventHandler click_fc (int num)
        {
            if (num == 1) return btn1_click;
            else if (num == 2) return btn2_click;
            else  return btn3_click;
        }


        private void btn1_click(object o, EventArgs a)
        {
            // form 초기화
            if (tagetForm != null) tagetForm.Dispose();
            // form 호출
            tagetForm = comm.getMdiForm(parentForm, new UserForm(db), contents);
            tagetForm.Show();
        }

        private void btn2_click(object o, EventArgs a)
        {
            // form 초기화
            if (tagetForm != null) tagetForm.Dispose();
            // form 호출
            tagetForm = comm.getMdiForm(parentForm, new RuleForm(db), contents);
            tagetForm.Show();
        }

        private void btn3_click(object o, EventArgs a)
        {
            // form 초기화
            if (tagetForm != null) tagetForm.Dispose();
            // form 호출
            tagetForm = comm.getMdiForm(parentForm, new MappingForm(db), contents);
            tagetForm.Show();
        }
    }
}
