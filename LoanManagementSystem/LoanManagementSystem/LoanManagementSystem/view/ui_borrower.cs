using LoanManagementSystem.module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoanManagementSystem.view
{
    internal class ui_borrower : interface_ui_load
    {
        private Form1 form;

        public ui_borrower() { }
        public ui_borrower(Form1 form)
        {
            this.form = form;
        }
        public void load()
        {
            MessageBox.Show("BORROWER");

            this.form.panel_dashboard.Location = new System.Drawing.Point(0, 0);

            this.disable();

            this.enable();
        }
        public void disable()
        {
            //for admin, staff, and collector
            this.form.panel_dashboard_sidebar_adminstaff.Enabled = false;
            this.form.panel_dashboard_sidebar_adminstaff.Visible = false;
        }
        public void enable()
        {
            this.form.panel_dashboard_sidebar_borrower.Enabled = true;
            this.form.panel_dashboard_sidebar_borrower.Visible = true;

            this.form.panel_dashboard_sidebar_borrower.Location = new System.Drawing.Point(3, 142);

        }
        public void remove()
        {
            //remove login panel upon entering
            this.form.panel_login.Location = new System.Drawing.Point(999, 999);


            //button that will be removed
            this.form.button_dashboard_loanplans.Visible = false;
            this.form.button_dashboard_payments.Visible = false;
            this.form.button_dashboard_borrowers.Visible = false;
            this.form.button_dashboard_loantypes.Visible = false;
            this.form.button_dashboard_loanschedule.Visible = false;
            this.form.button_dashboard_collectors.Visible = false;
            this.form.button_dashboard_loans.Visible = false;
        }
    }
}
