using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LoanManagementSystem.controllerandmodel;
using LoanManagementSystem.module;

namespace LoanManagementSystem.view
{
    internal class ui_admin : interface_ui_load
    {
        private Form1 form;

        //enable comboboxes
        ComboBox combobox_dashboard_loanlist_selectborrower = new ComboBox();
        ComboBox combobox_dashboard_loanlist_selectbranch = new ComboBox();
        ComboBox combobox_dashboard_loanlist_selectcoborrower = new ComboBox();
        ComboBox combobox_dashboard_loanlist_selectplan = new ComboBox();
        ComboBox combobox_dashboard_loanlist_selecttype = new ComboBox();

        //temp data
        private string[] tmploandata = new string[5];

        private pattern_singleton pattern_Singleton_instance = pattern_singleton.Instance;
        private function_user_manager function_user_manager_instance = null;
        private function_register_checker function_register_checker_instance = null;
        // Declare a list to store the original items for combobox_dashboard_loanlist_selectplan
        private List<LoanManagementSystem.module.KeyValuePair<int, string>> originalItemsSelectPlan = new List<LoanManagementSystem.module.KeyValuePair<int, string>>();
        private function_loan_calculation function_Loan_Calculation_instance = new function_loan_calculation();
        private List<LoanManagementSystem.module.KeyValuePair<int, string>> originalCoBorrowerItems = new List<LoanManagementSystem.module.KeyValuePair<int, string>>();

        public ui_admin() { }
        public ui_admin(Form1 form)
        {
            this.form = form;
        }
        public void load()
        {
            MessageBox.Show("ADMIN");

            this.form.panel_dashboard.Location = new System.Drawing.Point(0,0);

            //default call the events()
            this.events();

            //disable some buttons
            this.disable();

            //enable and show
            this.enable();
        }
        public void enable()
        {
            //for admin
            this.form.panel_dashboard_sidebar_adminstaff.Enabled = true;
            this.form.panel_dashboard_sidebar_adminstaff.Visible = true;

            //location
            this.form.panel_dashboard_sidebar_adminstaff.Location = new System.Drawing.Point(3,142);

            
        }
        public void disable()
        {
            //for borrower
            this.form.panel_dashboard_sidebar_borrower.Enabled = false;
            this.form.panel_dashboard_sidebar_borrower.Visible = false;
            
        }
        public void remove()
        {
            //remove login panel upon entering
            this.form.panel_login.Location = new System.Drawing.Point(999, 999);
        }

        private void events()
        {
            this.form.button_dashboard_profile.Click += event_dashboard_profile;
            this.form.button_dashboard_home.Click += event_dashboard_home;
            this.form.button_dashboard_loans.Click += event_dashboard_pendings;
            this.form.button_dashboard_payments.Click += event_dashboard_payments;
            this.form.button_dashboard_borrowers.Click += event_dashboard_borrowers;
            this.form.button_dashboard_loanplans.Click += event_dashboard_loanplans;
            this.form.button_dashboard_loantypes.Click += event_dashboard_loantypes;

            return;
        }

        // Event handlers for the buttons
        public void event_dashboard_profile(object sender, EventArgs e)
        {
            MessageBox.Show("Profile clicked!");
        }

        public void event_dashboard_home(object sender, EventArgs e)
        {
            MessageBox.Show("Home clicked!");
        }

        public void event_dashboard_pendings(object sender, EventArgs e)
        {
            MessageBox.Show("Pendings clicked!");
        }

        public void event_dashboard_payments(object sender, EventArgs e)
        {
            MessageBox.Show("Payments clicked!");
        }

        public void event_dashboard_borrowers(object sender, EventArgs e)
        {
            MessageBox.Show("Borrowers clicked!");
        }

        public void event_dashboard_loanplans(object sender, EventArgs e)
        {
            MessageBox.Show("Loan Plans clicked!");
        }

        public void event_dashboard_loantypes(object sender, EventArgs e)
        {
            MessageBox.Show("Loan Types clicked!");
        }

        public void event_dashboard_users(object sender, EventArgs e)
        {
            MessageBox.Show("Users clicked!");
        }

        

        

        //return tmpdata string[]
        public string[] gettmpdata()
        {
            return this.tmploandata;
        }
    }
}
