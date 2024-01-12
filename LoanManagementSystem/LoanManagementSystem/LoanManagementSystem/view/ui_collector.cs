using LoanManagementSystem.module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoanManagementSystem.view
{
    internal class ui_collector : interface_ui_load
    {
        private Form1 form;

        public ui_collector() { }
        public ui_collector(Form1 form)
        {
            this.form = form;
        }
        public void load()
        {
            MessageBox.Show("COLLECT");

            this.form.panel_dashboard.Location = new System.Drawing.Point(0, 0);

            //disable

            //enable

        }
        public void remove()
        {
            //remove login panel upon entering
            this.form.panel_login.Location = new System.Drawing.Point(999, 999);
        }
    }
}
