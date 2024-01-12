using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoanManagementSystem.module
{
    internal class class_displayloandetails
    {
        private Form1 form;
        public class_displayloandetails(Form1 form)
        {
            this.form = form;
        }
        public void DisplayDetails(string lastName, string firstName, string middleName, decimal amount, DateTime dateCreated, int status, string statusString, string refNo)
        {

            // Create labels to display details
            this.form.label_loanlist_details_lastname.Text = $"Last Name: {lastName}";
            this.form.label_loanlist_details_firstname.Text = $"First Name: {firstName}";
            this.form.label_loanlist_details_middlename.Text = $"Middle Name: {middleName}";
            this.form.label_loanlist_details_amount.Text = $"Amount: {amount:C}";
            this.form.label_loanlist_details_date.Text = $"Date Created: {dateCreated.ToShortDateString()}";
            this.form.label_loanlist_details_status.Text = $"Status: {statusString}";
            this.form.textbox_loanlist_details_reference.Text = $"{refNo}";

            //get the reference number
            class_loan class_loan_instance = new class_loan();
            class_loan_instance.Reference = refNo;
        }
    }
}
