using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _20181123
{
    class Load
    {
        private Form parentForm;
        private object oDB;

        public Load(Form parentForm)
        {
            this.parentForm = parentForm;
        }

        public Load(Form parentForm, object oDB)
        {
            this.parentForm = parentForm;
            this.oDB = oDB;
        }

        public EventHandler GetHandler(string viewName)
        {
            switch (viewName)
            {
                case "main":
                    return GetMainLoad;
                case "member":
                    return GetMemberLoad;
                case "rule":
                    return GetRuleLoad;
                case "mapping":
                    return GetMappingLoad;
                default:
                    return null;
            }
        }

        private void GetMainLoad(object o, EventArgs a)
        {
            parentForm.IsMdiContainer = true;
            parentForm.Size = new Size(1000, 800);
            parentForm.FormBorderStyle = FormBorderStyle.FixedSingle;
            parentForm.MaximizeBox = false;
            parentForm.MinimizeBox = false;
            parentForm.Text = "회원정보";
            new MainView(parentForm);
        }

        private void GetMemberLoad(object o, EventArgs a)
        {
            parentForm.BackColor = Color.Blue;
            new UserView(parentForm, oDB);
        }

        private void GetRuleLoad(object o, EventArgs a)
        {
            parentForm.BackColor = Color.Green;
            new RuleView(parentForm, oDB);
        }

        private void GetMappingLoad(object o, EventArgs a)
        {
            parentForm.BackColor = Color.Green;
            new MappingView(parentForm, oDB);
        }
    }
}
