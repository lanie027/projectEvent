using System.IO;
using LoanManagementSystem.controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using LoanManagementSystem.controllerandmodel;
using System.Data.SqlClient;
using LoanManagementSystem.module;
using System.Collections;
using LoanManagementSystem.view;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;
using System.Data.SqlTypes;
using System.Drawing.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Security.Cryptography.X509Certificates;

namespace LoanManagementSystem
{
    
    public partial class Form1 : Form
    {
        private pattern_singleton pattern_Singleton_instance = pattern_singleton.Instance;
        private function_user_manager function_user_manager_instance = null;
        private function_register_checker function_register_checker_instance = null;
        // Declare a list to store the original items for combobox_dashboard_loanlist_selectplan
        private List<LoanManagementSystem.module.KeyValuePair<int, string>> originalItemsSelectPlan = new List<LoanManagementSystem.module.KeyValuePair<int, string>>();
        private function_loan_calculation function_Loan_Calculation_instance = new function_loan_calculation();

        private List<LoanManagementSystem.module.KeyValuePair<int, string>> originalCoBorrowerItems = new List<LoanManagementSystem.module.KeyValuePair<int, string>>();

        private string[] tmpdata = new string[5];
        //ui admin
        private ui_admin ui_admin_instance = new ui_admin();

        public Form1()
        {
            InitializeComponent();

            //default essentials
            this.defaultessentials();

            

        }

        private void event_button_register(object sender, EventArgs e)
        {
            function_user_manager_instance = new function_user_manager();

            if(function_user_manager_instance.user_manager(
                this.textbox_register_firstname.Text.ToString(), this.textbox_register_middlename.Text.ToString(),
                this.textbox_register_lastname.Text.ToString(), this.textbox_register_age.Text.ToString(),
                this.textbox_register_address.Text.ToString(), this.textbox_register_email.Text.ToString(),
                this.textbox_register_contactnumber.Text.ToString(), this.textbox_register_username.Text.ToString(),
                this.textbox_register_password.Text.ToString()
                ) == true)
            {
                MessageBox.Show("Register Succesful!");

                //remove register form
                this.panel_registration.Location = new Point(999,999);

                //show login form
                this.panel_login.Location = new Point(0,0);
            }
        }
        private void defaultessentials()
        {

            

            //panel location
            this.panel_dashboard_sidebar_adminstaff.Location = new Point(999,999);
            this.panel_dashboard_sidebar_borrower.Location = new Point(999,999);
            this.panel_dashboard_sidebar_collector.Location = new Point(999,999);

            //deafult essential
            this.panel_dashboard_loanlist_createloanform.Visible = false;


            //--begin button event
            this.button_dashboard_loanlist_calculate.Click += event_dashboard_loanlist_calculate_Click;
            this.button_dashboard_loanlist_cancel.Click += event_dashboard_loanlist_cancel;
            this.button_dashboard_loanlist_save.Click += event_dashboard_loanlist_save;
            this.button_dashboard_loanlist_collateralupload.Click += event_dashboard_loanlist_collateralupload;

            this.button_dashboard_loans.Click += event_dashbaord_loans_collateralupload;

            

            //default event
            this.button_login.Click += event_button_login;

            //register
            this.button_register.Click += event_button_register;

            //sign up
            this.button_login_signup.Click += event_button_signup;

            //button hide
            this.button_dashboard_loanlist_save.Visible = false;

            this.button_dashboard_loanlist_back.Visible = false;

            //create application
            this.button_dashboard_loanlist_createloan.Click += this.event_dashboard_loanlist_createloan;

            //next application
            this.button_dashboard_loanlist_next.Click += event_dashboard_loalist_next;

            //back application
            this.button_dashboard_loanlist_back.Click += event_dashboard_loalist_back;
            
            //end button event--


            //--begin combobox event
            // Attach TextChanged event for auto-complete
            this.combobox_dashboard_loanlist_selectborrower.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.combobox_dashboard_loanlist_selectborrower.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.combobox_dashboard_loanlist_selectborrower.TextChanged += this.Combobox_dashboard_loanlist_selecttype_TextChanged;

            this.combobox_dashboard_loanlist_selecttype.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.combobox_dashboard_loanlist_selecttype.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.combobox_dashboard_loanlist_selecttype.TextChanged += this.Combobox_dashboard_loanlist_selectborrower_TextChanged;

            this.combobox_dashboard_loanlist_selectborrower.SelectedIndexChanged += this.event_dashboard_loanlist_selectborrower;

            this.combobox_dashboard_loanlist_selecttype.SelectedIndexChanged += this.event_dashboard_loanlist_selecttype;

            this.combobox_dashboard_loanlist_selectplan.SelectedIndexChanged += this.event_dashboard_loanlist_selectplan;

            this.combobox_dashboard_loanlist_selectcoborrower.SelectedIndexChanged += this.Combobox_dashboard_loanlist_selectcoborrower_SelectedIndexChanged;

            this.combobox_dashboard_loanlist_selectbranch.SelectedIndexChanged += this.event_dashboard_loanlist_selectbranch;

            this.combobox_dashboard_loanlist_selectcollector.SelectedIndexChanged += this.Combobox_dashboard_loanlist_selectcollector_SelectedIndexChanged;


            this.combobox_dashboard_loanlist_selectcompound.SelectedIndexChanged += this.combobox_dashboard_loanlist_selectcompound_SelectedIndexChanged;

            table_dashboard_loanlist_adminstaff.CellClick += event_dashboard_loanlist_adminstaff;

            //invisible
            panel_dashboard_loanlist_details.Location = new Point(999,999);


            //end combobox event--
        }

        private void event_dashboard_loanlist_adminstaff(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                panel_dashboard_loanlist_details.Location = new Point(0,0);

                // Get the selected row
                DataGridViewRow selectedRow = table_dashboard_loanlist_adminstaff.Rows[e.RowIndex];

                // Extract data from the selected row
                string lastName = selectedRow.Cells["lname"].Value.ToString();
                string firstName = selectedRow.Cells["fname"].Value.ToString();
                string middleName = selectedRow.Cells["mname"].Value.ToString();
                decimal amount = Convert.ToDecimal(selectedRow.Cells["amount"].Value);
                DateTime dateCreated = Convert.ToDateTime(selectedRow.Cells["date_created"].Value);
                int status = Convert.ToInt32(selectedRow.Cells["status"].Value);
                string statusString = selectedRow.Cells["StatusString"].Value.ToString();
                string refNo = selectedRow.Cells["ref_no"].Value.ToString();

                class_displayloandetails class_displayloandetails_instance = new class_displayloandetails(this);

                // Display details in the panel or any other control
                class_displayloandetails_instance.DisplayDetails(lastName, firstName, middleName, amount, dateCreated, status, statusString, refNo);
            }
        }

        private void event_dashbaord_loans_collateralupload(object sender, EventArgs e)
        {
            panel_dashboard_loanlist_adminstaff.Location = new Point(3, 0);
            FetchAndDisplayData();
        }



        //click function (another button function))
        private string selectedImagePath = null; // This variable will store the full path of the selected image file

        private static int currentPage = 1;

        private void event_dashboard_loalist_back(object sender, EventArgs e)
        {
            // Decrement the page number
            currentPage--;

            // Ensure the page number is within valid bounds
            if (currentPage < 1)
            {
                currentPage = 1;
            }

            // Update UI based on the current page
            UpdateUI();
        }

        private void event_dashboard_loalist_next(object sender, EventArgs e)
        {
            // Increment the page number
            currentPage++;

            // Ensure the page number is within valid bounds
            if (currentPage > 4) // Adjust the upper bound based on the number of pages
            {
                currentPage = 4; // Adjust the upper bound based on the number of pages
            }

            // Update UI based on the current page
            UpdateUI();
        }

        private void UpdateUI()
        {
            // Reset the visibility of buttons
            this.button_dashboard_loanlist_cancel.Visible = true;
            this.button_dashboard_loanlist_back.Visible = true;
            this.button_dashboard_loanlist_next.Visible = true;
            this.button_dashboard_loanlist_save.Visible = false; // Hide "Save" button by default

            // Hide all panels
            this.panel_dashboard_loanlist_borrowerinfo.Location = new Point(999, 999);
            this.panel_dashboard_loanlist_loaninfo.Location = new Point(999, 999);
            this.panel_dashboard_loanlist_collateralinformation.Location = new Point(999, 999);
            this.panel_dashboard_loanlist_loancalculation.Location = new Point(999, 999);

            // Show the relevant panel based on the current page
            switch (currentPage)
            {
                case 1:
                    this.button_dashboard_loanlist_back.Visible = false;
                    this.panel_dashboard_loanlist_borrowerinfo.Location = new Point(50, 50);
                    break;
                case 2:
                    this.panel_dashboard_loanlist_loaninfo.Location = new Point(50, 50);
                    break;
                case 3:
                    this.panel_dashboard_loanlist_collateralinformation.Location = new Point(50, 50);
                    break;
                case 4:
                    this.panel_dashboard_loanlist_loancalculation.Location = new Point(50, 50);
                    this.button_dashboard_loanlist_next.Visible = false; // Hide "Next" on the last page
                    this.button_dashboard_loanlist_save.Visible = true; // Show "Save" button on the last page
                                                                        // Handle additional pages if needed
                    break;
                default:
                    break;
            }
        }



        private void event_dashboard_loanlist_collateralupload(object sender, EventArgs e)
        {
            //Call display constructor
            LoanManagementSystem.controllerandmodel.function_display_collateralimage function_display_collateralimage_instance = new function_display_collateralimage();
            // Create an instance of OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set the file filter to only allow image files
            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp|All Files (*.*)|*.*";

            // Show the dialog and check if the user selected a file
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the selected file path
                selectedImagePath = openFileDialog.FileName;

                class_loan_instance.Collateral = selectedImagePath;

                try
                {
                    // Read the selected image file into a byte array
                    byte[] imageBytes = File.ReadAllBytes(selectedImagePath);

                    // Convert the byte array to a base64 string
                    selectedImageBase64 = System.Convert.ToBase64String(imageBytes);

                    // Display the image in the PictureBox
                    function_display_collateralimage_instance.DisplayImage(imageBytes, this);

                    // Display a message or perform other actions with the base64 string
                    MessageBox.Show("Image selected and converted to base64 string.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that may occur during file reading or conversion
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void event_dashboard_loanlist_save(object sender, EventArgs e)
        {
            function_add_loan function_add_loan_instance = new function_add_loan();

            MessageBox.Show(class_loan_instance.ToString());

            try
            {
                if (class_loan_instance.Borrower_id == class_loan_instance.Coborrower_id)
                {
                    MessageBox.Show("SHOULD NOT BE DUPLICATE!");
                }
                else
                {
                    // Declare an integer variable to store the result
                    int enteredAmount;

                    // Attempt to parse the text from the TextBox to an integer
                    if (int.TryParse(textbox_dashboard_loanlist_enteramount.Text, out enteredAmount))
                    {
                        // The conversion was successful, 'enteredAmount' now holds the integer value
                        // You can use 'enteredAmount' in your code
                        Console.WriteLine($"Entered Amount: {enteredAmount}");
                    }
                    else
                    {
                        // The conversion failed, handle the error or inform the user
                        MessageBox.Show("Invalid input. Please enter a valid integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    function_add_loan_instance.add_loan(class_loan_instance.Type_id, class_loan_instance.Borrower_id, class_loan_instance.Plan_id, class_loan_instance.Company_id, class_loan_instance.Collector_id, class_loan_instance.Coborrower_id, class_loan_instance.Compound_id, class_loan_instance.Collateral.ToString(), enteredAmount);

                    MessageBox.Show("Insert Successful!");

                    this.panel_dashboard_loanlist_createloanform.Location = new Point(999, 999);
                }
            }
            catch(Exception ex)
            {
                // Handle any exceptions that may occur during file reading or conversion
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void event_dashboard_loanlist_createloan(object sender, EventArgs e)
        {
            this.LoadDataIntoComboBox();

            this.panel_dashboard_loanlist_createloanform.Visible = true;
            this.panel_dashboard_loanlist_createloanform.Location = new Point(0,0);
            this.panel_dashboard_loanlist_borrowerinfo.Location = new Point(50,50);
        }

        //private static variables
        private static int cancollateral = 0;
        private static int cancoborrower = 0;
        private static int iseverydaycollect = 0;

        //priavte dictionary for branch
        // Declare a Dictionary to store additional data for each branch ID
        private Dictionary<int, Tuple<int, int, int>> branchDataDictionary = new Dictionary<int, Tuple<int, int, int>>();

        //Load Data 
        private void FetchAndDisplayData()
        {
            try
            {

                DataTable table_dashboard_loanlist_adminstaff_data = new DataTable();

                using (SqlConnection connection = new SqlConnection(pattern_Singleton_instance.GetStringConnection()))
                {
                    connection.Open();

                    string query = "select lname, fname, mname, ls.amount, ls.date_created, ls.status, ls.ref_no from loan_list ls inner join borrowers b on b.borrower_id = ls.borrower_id inner join users u on u.user_id = b.user_id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            // Clear existing data in the DataTable
                            table_dashboard_loanlist_adminstaff_data.Clear();

                            // Fill the DataTable with the retrieved data
                            adapter.Fill(table_dashboard_loanlist_adminstaff_data);

                            // Add a new DataColumn for string representation of status
                            DataColumn statusStringColumn = new DataColumn("StatusString", typeof(string));
                            table_dashboard_loanlist_adminstaff_data.Columns.Add(statusStringColumn);

                            class_getstatusstring class_getstatusstring_instance = new class_getstatusstring();

                            // Update the "status" column values based on the specified conditions
                            foreach (DataRow row in table_dashboard_loanlist_adminstaff_data.Rows)
                            {
                                int statusValue = Convert.ToInt32(row["status"]);
                                string statusString = class_getstatusstring_instance.GetStatusString(statusValue);

                                // Update the original "status" column with the number
                                row["status"] = statusValue;

                                // Update the new "StatusString" column with the string representation
                                row["StatusString"] = statusString;
                            }

                            // Bind the DataTable to the DataGridView or any other control
                            // Assuming you have a DataGridView named table_dashboard_loanlist_adminstaff
                            table_dashboard_loanlist_adminstaff.DataSource = table_dashboard_loanlist_adminstaff_data;

                            // Hide the original "status" column with numbers
                            table_dashboard_loanlist_adminstaff.Columns["status"].Visible = false;

                            // Hide the ref_no column in the DataGridView
                            table_dashboard_loanlist_adminstaff.Columns["ref_no"].Visible = false;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately (e.g., display an error message)
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Load Data Combobox
            public void LoadDataIntoComboBox()
        {
            SqlConnection connection = null;

            string connectionString = pattern_Singleton_instance.GetStringConnection();

            try
            {
                // Using a singleton pattern to get the connection instance
                using (connection = new SqlConnection(connectionString))
                {
                    if (connection != null && connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    

                    // Query for compounds
                    string compoundQuery = "SELECT compound_id, description FROM compound";

                    using (SqlCommand compoundCommand = new SqlCommand(compoundQuery, connection))
                    {
                        using (SqlDataReader compoundReader = compoundCommand.ExecuteReader())
                        {
                            while (compoundReader.Read())
                            {
                                string compoundDisplayString = $"{compoundReader["description"]}";

                                if (this.combobox_dashboard_loanlist_selectcompound != null)
                                {
                                    // Add compound items
                                    this.combobox_dashboard_loanlist_selectcompound.Items.Add(new LoanManagementSystem.module.KeyValuePair<int, string>((int)compoundReader["compound_id"], compoundDisplayString));
                                }
                            }
                        }
                    }

                    //query for collectors
                    string collectorQuery = "select c.collector_id, u.lname, u.fname, u.mname from collectors c inner join users u on c.user_id = u.user_id";

                    using (SqlCommand collectorCommand = new SqlCommand(collectorQuery, connection))
                    {
                        using (SqlDataReader collectorReader = collectorCommand.ExecuteReader())
                        {
                            while (collectorReader.Read())
                            {
                                string collectorDisplayString = $"{collectorReader["lname"]}, {collectorReader["fname"]}, {collectorReader["mname"]}";

                                if (this.combobox_dashboard_loanlist_selectcollector != null)
                                {
                                    // Add collector items
                                    this.combobox_dashboard_loanlist_selectcollector.Items.Add(new LoanManagementSystem.module.KeyValuePair<int, string>((int)collectorReader["collector_id"], collectorDisplayString));
                                }
                            }
                        }
                    }

                    // Query for borrowers
                    string borrowerQuery = "SELECT b.borrower_id, u.lname, u.fname, u.mname FROM borrowers b INNER JOIN users u ON u.user_id = b.user_id";

                    using (SqlCommand coBorrowerCommand = new SqlCommand(borrowerQuery, connection))
                    {
                        using (SqlDataReader coBorrowerReader = coBorrowerCommand.ExecuteReader())
                        {
                            while (coBorrowerReader.Read())
                            {
                                string coBorrowerDisplayString = $"{coBorrowerReader["lname"]}, {coBorrowerReader["fname"]}, {coBorrowerReader["mname"]}";

                                if (this.combobox_dashboard_loanlist_selectcoborrower != null)
                                {
                                    // Add co-borrower items
                                    this.combobox_dashboard_loanlist_selectcoborrower.Items.Add(new LoanManagementSystem.module.KeyValuePair<int, string>((int)coBorrowerReader["borrower_id"], coBorrowerDisplayString));
                                }

                            }
                        }
                    }


                    //query for company
                    string branchQuery = "select br.company_id, br.name, br.cancoborrower, br.cancollateral, br.iseverydaycollect from company br";

                    using (SqlCommand branchCommand = new SqlCommand(branchQuery, connection))
                    {
                        using (SqlDataReader branchReader = branchCommand.ExecuteReader())
                        {
                            while (branchReader.Read())
                            {
                                string branchDisplayString = $"{branchReader["name"]}";
                                int branchId = (int)branchReader["company_id"];

                                if (combobox_dashboard_loanlist_selectbranch != null)
                                {
                                    // Add branch items
                                    combobox_dashboard_loanlist_selectbranch.Items.Add(new LoanManagementSystem.module.KeyValuePair<int, string>((int)branchReader["company_id"], branchDisplayString));

                                    // Store additional data in the Dictionary
                                    int canCoBorrower = Convert.ToInt32(branchReader["cancoborrower"]);
                                    int canCollateral = Convert.ToInt32(branchReader["cancollateral"]);
                                    int isEverydayCollect = Convert.ToInt32(branchReader["iseverydaycollect"]);

                                    branchDataDictionary[branchId] = new Tuple<int, int, int>(canCoBorrower, canCollateral, isEverydayCollect);
                                }
                            }
                        }
                    }


                    using (SqlCommand borrowerCommand = new SqlCommand(borrowerQuery, connection))
                    {
                        using (SqlDataReader borrowerReader = borrowerCommand.ExecuteReader())
                        {
                            while (borrowerReader.Read())
                            {
                                string borrowerDisplayString = $"{borrowerReader["lname"]}, {borrowerReader["fname"]}, {borrowerReader["mname"]}";
                                this.combobox_dashboard_loanlist_selectborrower.Items.Add(new LoanManagementSystem.module.KeyValuePair<int, string>((int)borrowerReader["borrower_id"], borrowerDisplayString));
                            }
                        }
                    }

                    // Query for plans
                   string planQuery = "SELECT pl.plan_id, pl.months, ip.value as interest, pr.value as penalty, r.value as rebate " +
                   "FROM loan_plan pl " +
                   "INNER JOIN interest_percentage ip ON ip.interest_percentage_id = pl.interest_percentage_id " +
                   "INNER JOIN penalty_rate pr ON pr.penalty_rate_id = pl.penalty_rate_id " +
                   "INNER JOIN rebate r ON r.rebate_id = pl.rebate_id;";

                    using (SqlCommand planCommand = new SqlCommand(planQuery, connection))
                    {
                        using (SqlDataReader planReader = planCommand.ExecuteReader())
                        {
                            while (planReader.Read())
                            {
                                // Create a formatted string with distinct values
                                string planFormattedString = $"{planReader["months"]} month/s " +
                                                             $"[Interest: {planReader["interest"]}%," +
                                                             $" Penalty: {planReader["penalty"]}%," +
                                                             $" Rebate: {planReader["rebate"]}%]";

                                // Create KeyValuePair and add to ComboBox
                                var item = new LoanManagementSystem.module.KeyValuePair<int, string>((int)planReader["plan_id"], planFormattedString);
                                this.combobox_dashboard_loanlist_selectplan.Items.Add(item);

                                //store the rates and months
                                class_loan_instance.Interestrate = Convert.ToInt32(planReader["interest"]);
                                class_loan_instance.Penaltyrate = Convert.ToInt32(planReader["penalty"]);
                                class_loan_instance.Rebaterate = Convert.ToInt32(planReader["rebate"]);
                                class_loan_instance.Month = Convert.ToInt32(planReader["months"]);

                                // Store the original item
                                originalItemsSelectPlan.Add(item);
                            }
                        }
                    }

                    // SQL query to select all rows from loan_types
                    string query = "SELECT type_id, type_name, description FROM loan_types";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Format the data into the desired string
                                string formattedString = $"{reader["type_name"]}";

                                // Add the formatted string to the ComboBox
                                this.combobox_dashboard_loanlist_selecttype.Items.Add(new LoanManagementSystem.module.KeyValuePair<int, string>((int)reader["type_id"], formattedString));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Close the connection in the finally block to ensure it is always closed
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void event_button_signup(object sender, EventArgs e)
        {
            //temp remove the login form
            this.panel_login.Location = new Point(999,999);

            //temp show register form
            this.panel_registration.Location = new Point(0,0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.panel_login.Location = new Point(0, 0);

            CenterToScreen();
        }

        private void event_button_login(object sender, EventArgs e)
        {
            if(this.textbox_username.Text.ToString().Length > 0 && this.textbox_password.Text.ToString().Length > 0)
            {
                function_user_manager_instance = new function_user_manager(this.textbox_username.Text.ToString(), this.textbox_password.Text.ToString(), this);
                
                //load function
                this.FetchAndDisplayData();
                return;
            }
            else
            {
                MessageBox.Show("Wrong username or password!");
            }
            return;
        }

        private void clear()
        {
            this.textbox_password.Text = "";
            this.textbox_username.Text = "";

            this.textbox_register_firstname.Text = "";
            this.textbox_register_middlename.Text = "";
            this.textbox_register_lastname.Text = "";
            this.textbox_register_age.Text = "";
            this.textbox_register_address.Text = "";
            this.textbox_register_email.Text = "";
            this.textbox_register_contactnumber.Text = "";
            this.label_register_username.Text = "";
            this.textbox_register_password.Text = "";
        }

        // Helper method to update the selected ID in the array
        private void UpdateSelectedId(int index, System.Windows.Forms.ComboBox comboBox)
        {
            if (comboBox.SelectedItem != null)
            {
                selectedIds[index] = ((LoanManagementSystem.module.KeyValuePair<int, string>)comboBox.SelectedItem).Key;
            }
            else
            {
                // Handle the case when no item is selected
                selectedIds[index] = 0; // or any default value
            }
        }

        //Select function

        // Define an array to store the selected IDs
        static int[] selectedIds = new int[7]; // Assuming you have 6 ComboBoxes

        //Loan details class
        class_loan class_loan_instance = new class_loan();


        public void combobox_dashboard_loanlist_selectcompound_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if an item is selected
            if (this.combobox_dashboard_loanlist_selectcompound.SelectedItem != null)
            {
                // Access the selected value using SelectedValue property
                int selectedCompoundId = ((LoanManagementSystem.module.KeyValuePair<int, string>)this.combobox_dashboard_loanlist_selectcompound.SelectedItem).Key;
                string selectedCompoundDescription = ((LoanManagementSystem.module.KeyValuePair<int, string>)this.combobox_dashboard_loanlist_selectcompound.SelectedItem).Value;

                class_loan_instance.Compound_id = selectedCompoundId; 

                // Your logic for using the selected values goes here
                // Example: MessageBox.Show($"Selected Compound: {selectedCompoundId} - {selectedCompoundDescription}");

                // Update your temporary data array as needed
                UpdateSelectedId(6, combobox_dashboard_loanlist_selectcompound);
            }
            else
            {
                // Handle the case when no item is selected
                MessageBox.Show("No item selected.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void Combobox_dashboard_loanlist_selectcollector_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if an item is selected
            if (this.combobox_dashboard_loanlist_selectcollector.SelectedItem != null)
            {
                // Access the selected value using SelectedValue property
                int selectedCollectorId = ((LoanManagementSystem.module.KeyValuePair<int, string>)this.combobox_dashboard_loanlist_selectcollector.SelectedItem).Key;
                string selectedCollectorName = ((LoanManagementSystem.module.KeyValuePair<int, string>)this.combobox_dashboard_loanlist_selectcollector.SelectedItem).Value;

                class_loan_instance.Collector_id = selectedCollectorId;

                // Your logic for using the selected values goes here
                // Example: MessageBox.Show($"Selected Collector: {selectedCollectorId} - {selectedCollectorName}");

                // Update your temporary data array as needed
                UpdateSelectedId(4, combobox_dashboard_loanlist_selectcollector);
            }
            else
            {
                // Handle the case when no item is selected
                MessageBox.Show("No item selected.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        public void event_dashboard_loanlist_selectbranch(object sender, EventArgs e)
        {
            // Check if an item is selected
            if (this.combobox_dashboard_loanlist_selectbranch.SelectedItem != null)
            {
                // Access the selected value using SelectedValue property
                int selectedBranchId = ((LoanManagementSystem.module.KeyValuePair<int, string>)this.combobox_dashboard_loanlist_selectbranch.SelectedItem).Key;
                string selectedBranchName = ((LoanManagementSystem.module.KeyValuePair<int, string>)this.combobox_dashboard_loanlist_selectbranch.SelectedItem).Value;

                class_loan_instance.Company_id = selectedBranchId;

                UpdateSelectedId(3, combobox_dashboard_loanlist_selectbranch);

                // Use the selected values as needed
                // Example: MessageBox.Show($"Selected Branch: {selectedBranchId} - {selectedBranchName}");


                // Access additional data from the Dictionary
                if (branchDataDictionary.TryGetValue(selectedBranchId, out var branchData))
                {
                    int canCoBorrower = branchData.Item1;
                    int canCollateral = branchData.Item2;
                    int isEverydayCollect = branchData.Item3;

                    // Use the selected values as needed
                    Console.WriteLine($"Selected Branch: {selectedBranchId} - {selectedBranchName}\nCan Co-Borrower: {canCoBorrower}\nCan Collateral: {canCollateral}\nIs Everyday Collect: {isEverydayCollect}");

                    if (canCoBorrower == 0)
                    {
                        combobox_dashboard_loanlist_selectcoborrower.Enabled = false;
                    }
                    else
                    {
                        combobox_dashboard_loanlist_selectcoborrower.Enabled = true;
                    }

                    if (canCollateral == 0)
                    {
                        button_dashboard_loanlist_collateralupload.Enabled = false;
                    }
                    else
                    {
                        button_dashboard_loanlist_collateralupload.Enabled = true;
                    }

                    //Default

                }
            }
            else
            {
                // Handle the case when no item is selected
                MessageBox.Show("No item selected.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void Combobox_dashboard_loanlist_selectcoborrower_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               

                // Handle the selected co-borrower
                var selectedCoBorrower = (LoanManagementSystem.module.KeyValuePair<int, string>)this.combobox_dashboard_loanlist_selectcoborrower.SelectedItem;

                if (selectedCoBorrower != null)
                {
                    // Access the selected value using SelectedValue property
                    int selectedCoBorrowerId = ((LoanManagementSystem.module.KeyValuePair<int, string>)this.combobox_dashboard_loanlist_selectcoborrower.SelectedItem).Key;
                    string selectedCoBorrowerName = ((LoanManagementSystem.module.KeyValuePair<int, string>)this.combobox_dashboard_loanlist_selectcoborrower.SelectedItem).Value;

                    class_loan_instance.Coborrower_id = selectedCoBorrowerId;
                }
                else
                {
                    // Handle the case when no item is selected
                    MessageBox.Show("No item selected for co-borrower.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception, e.g., log it or display an error message
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void event_dashboard_loanlist_selectborrower(object sender, EventArgs e)
        {
            // Check if an item is selected
            if (this.combobox_dashboard_loanlist_selectborrower.SelectedItem != null)
            {
                // Access the selected value using SelectedValue property
                int selectedUserId = ((LoanManagementSystem.module.KeyValuePair<int, string>)this.combobox_dashboard_loanlist_selectborrower.SelectedItem).Key;
                string selectedUserName = ((LoanManagementSystem.module.KeyValuePair<int, string>)this.combobox_dashboard_loanlist_selectborrower.SelectedItem).Value;

                // Get the selected borrower item
                LoanManagementSystem.module.KeyValuePair<int, string> selectedBorrower = (LoanManagementSystem.module.KeyValuePair<int, string>)combobox_dashboard_loanlist_selectborrower.SelectedItem;

                if (selectedBorrower != null)
                {
                    // Log the selected borrower ID (for debugging)
                    Console.WriteLine($"Selected Borrower ID: {selectedBorrower.Key}");
                }

                // Access the selected value using SelectedValue property
                int selectedBorrowerId = ((LoanManagementSystem.module.KeyValuePair<int, string>)combobox_dashboard_loanlist_selectborrower.SelectedItem).Key;

                class_loan_instance.Borrower_id = selectedBorrowerId;

                // Additional logic, if needed
                // For example: MessageBox.Show($"Selected Borrower: {selectedBorrowerId}");

                UpdateSelectedId(1, combobox_dashboard_loanlist_selectborrower);

            }
            else
            {
                // Handle the case when no item is selected
                MessageBox.Show("No item selected.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void event_dashboard_loanlist_selectplan(object sender, EventArgs e)
        {
            // Check if an item is selected
            if (this.combobox_dashboard_loanlist_selectplan.SelectedItem != null)
            {
                // Access the selected value using SelectedValue property
                int selectedUserId = ((LoanManagementSystem.module.KeyValuePair<int, string>)this.combobox_dashboard_loanlist_selectplan.SelectedItem).Key;
                string selectedUserName = ((LoanManagementSystem.module.KeyValuePair<int, string>)this.combobox_dashboard_loanlist_selectplan.SelectedItem).Value;


                class_loan_instance.Plan_id = selectedUserId;


                // Use the selected values as needed
                // Example: MessageBox.Show($"Selected User: {selectedUserId} - {selectedUserName}");

                UpdateSelectedId(2, combobox_dashboard_loanlist_selectplan);

            }
            else
            {
                // Handle the case when no item is selected
                MessageBox.Show("No item selected.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void event_dashboard_loanlist_selecttype(object sender, EventArgs e)
        {
            // Check if an item is selected
            if (this.combobox_dashboard_loanlist_selecttype.SelectedItem != null)
            {
                // Access the selected value using SelectedValue property
                int selectedUserId = ((LoanManagementSystem.module.KeyValuePair<int, string>)this.combobox_dashboard_loanlist_selecttype.SelectedItem).Key;
                string selectedUserName = ((LoanManagementSystem.module.KeyValuePair<int, string>)this.combobox_dashboard_loanlist_selecttype.SelectedItem).Value;

                class_loan_instance.Type_id = selectedUserId;

                // Use the selected values as needed
                // Example: MessageBox.Show($"Selected User: {selectedUserId} - {selectedUserName}");

                UpdateSelectedId(0, combobox_dashboard_loanlist_selecttype);

            }
            else
            {
                // Handle the case when no item is selected
                MessageBox.Show("No item selected.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



        //Button Calculate Function
        public void event_dashboard_loanlist_calculate_Click(object sender, EventArgs e)
        {

            int value;
            try
            {
                function_loan_calculation function_loan_calculation_instance = new function_loan_calculation();

                if (int.TryParse(textbox_dashboard_loanlist_enteramount.Text, out value))
                {
                    function_loan_calculation_instance.CalculateTotalPaymentAndDisplay(class_loan_instance.Month, value, class_loan_instance.Penaltyrate, class_loan_instance.Compound_id,this);
                }
                else
                {
                    MessageBox.Show("ERROR");
                    return;
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error calculating loan details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //textchange function
        public List<int> selectedCoBorrowers = new List<int>();
        private string selectedImageBase64;

        public void Combobox_dashboard_loanlist_selectborrower_TextChanged(object sender, EventArgs e)
        {
            // Filter the items based on the user input
            string userInput = this.combobox_dashboard_loanlist_selectborrower.Text.ToLower();

            // Temporary list to store filtered items
            List<LoanManagementSystem.module.KeyValuePair<int, string>> filteredItems = new List<LoanManagementSystem.module.KeyValuePair<int, string>>();

            // Repopulate the temporary list with items matching the user input
            foreach (LoanManagementSystem.module.KeyValuePair<int, string> item in this.combobox_dashboard_loanlist_selectborrower.Items)
            {
                if (item.Value.ToLower().Contains(userInput))
                {
                    filteredItems.Add(item);
                }
            }

            // Clear and repopulate the ComboBox with filtered items
            this.combobox_dashboard_loanlist_selectborrower.Items.Clear();
            this.combobox_dashboard_loanlist_selectborrower.Items.AddRange(filteredItems.ToArray());
        }

        public void Combobox_dashboard_loanlist_selectcoborrower_TextChanged(object sender, EventArgs e)
        {
            // Filter the items based on the user input
            string userInput = this.combobox_dashboard_loanlist_selectcoborrower.Text.ToLower();

            // Temporary list to store filtered items
            List<LoanManagementSystem.module.KeyValuePair<int, string>> filteredItems = new List<LoanManagementSystem.module.KeyValuePair<int, string>>();

            // Repopulate the temporary list with items matching the user input and not selected already
            foreach (LoanManagementSystem.module.KeyValuePair<int, string> item in this.combobox_dashboard_loanlist_selectcoborrower.Items)
            {
                if (item.Value.ToLower().Contains(userInput) && !selectedCoBorrowers.Contains(item.Key))
                {
                    filteredItems.Add(item);
                }
            }

            // Clear and repopulate the ComboBox with filtered items
            this.combobox_dashboard_loanlist_selectcoborrower.Items.Clear();
            this.combobox_dashboard_loanlist_selectcoborrower.Items.AddRange(filteredItems.ToArray());
        }

        public void Combobox_dashboard_loanlist_selecttype_TextChanged(object sender, EventArgs e)
        {
            // Filter the items based on the user input
            string userInput = this.combobox_dashboard_loanlist_selecttype.Text.ToLower();

            // Temporary list to store filtered items
            List<LoanManagementSystem.module.KeyValuePair<int, string>> filteredItems = new List<LoanManagementSystem.module.KeyValuePair<int, string>>();

            // Repopulate the temporary list with items matching the user input
            foreach (LoanManagementSystem.module.KeyValuePair<int, string> item in this.combobox_dashboard_loanlist_selecttype.Items)
            {
                if (item.Value.ToLower().Contains(userInput))
                {
                    filteredItems.Add(item);
                }
            }

            // Clear and repopulate the ComboBox with filtered items
            this.combobox_dashboard_loanlist_selecttype.Items.Clear();
            this.combobox_dashboard_loanlist_selecttype.Items.AddRange(filteredItems.ToArray());
        }



        public void event_dashboard_loanlist_cancel(object sender, EventArgs e)
        {
            this.panel_dashboard_loanlist_createloanform.Visible = false;

            this.combobox_dashboard_loanlist_selectborrower.Items.Clear();
            this.combobox_dashboard_loanlist_selectplan.Items.Clear();
            this.combobox_dashboard_loanlist_selecttype.Items.Clear();
            this.combobox_dashboard_loanlist_selectcoborrower.Items.Clear();
            this.combobox_dashboard_loanlist_selectbranch.Items.Clear();
            this.combobox_dashboard_loanlist_selectcollector.Items.Clear();
            this.combobox_dashboard_loanlist_selectcompound.Items.Clear();

            this.combobox_dashboard_loanlist_selectcoborrower.SelectedIndex = -1;
            this.combobox_dashboard_loanlist_selectplan.SelectedIndex = -1;
            this.combobox_dashboard_loanlist_selecttype.SelectedIndex = -1;
            this.combobox_dashboard_loanlist_selectcoborrower.SelectedIndex = -1;
            this.combobox_dashboard_loanlist_selectbranch.SelectedIndex = -1;
            this.combobox_dashboard_loanlist_selectcollector.SelectedIndex = -1;
            this.combobox_dashboard_loanlist_selectcompound.SelectedIndex = -1;

            this.combobox_dashboard_loanlist_selectborrower.Text = string.Empty;
            this.combobox_dashboard_loanlist_selectplan.Text = string.Empty;
            this.combobox_dashboard_loanlist_selecttype.Text = string.Empty;
            this.combobox_dashboard_loanlist_selectcoborrower.Text = string.Empty;
            this.combobox_dashboard_loanlist_selectbranch.Text = string.Empty;
            this.combobox_dashboard_loanlist_selectcollector.Text = string.Empty;
            this.combobox_dashboard_loanlist_selectcompound.Text = string.Empty;

            combobox_dashboard_loanlist_selectcoborrower.Enabled = true;
            button_dashboard_loanlist_collateralupload.Enabled = true;
            combobox_dashboard_loanlist_selectcompound.Enabled = true;

            textbox_dashboard_loanlist_enteramount.Text = string.Empty;

            

            //reset their location value
            this.panel_dashboard_loanlist_createloanform.Location = new Point(999,999);
            this.panel_dashboard_loanlist_borrowerinfo.Location = new Point(999, 999);
            this.panel_dashboard_loanlist_collateralinformation.Location = new Point(999, 999);
            this.panel_dashboard_loanlist_loancalculation.Location = new Point(999, 999);
            this.panel_dashboard_loanlist_loaninfo.Location = new Point(999,999);

            //default click value
            currentPage = 1;

            //reset value
            class_loan_instance.reset();
        }


        //automatic events
        private void button_dashboard_loanlist_detailscancel_Click(object sender, EventArgs e)
        {
            this.panel_dashboard_loanlist_details.Location = new Point(999,999);
        }

        private void button_dashboard_loanlist_detailsupdate_Click(object sender, EventArgs e)
        {
            // Assuming you have a ComboBox named combobox_loanlist_details_status
            string selectedStatus = combobox_loanlist_details_status.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(selectedStatus))
            {
                Console.WriteLine($"Selected Status ID: {selectedStatus}");
            }
            else
            {
                Console.WriteLine("No status selected.");
            }

            switch (selectedStatus)
            {
                case "For Approve":
                    class_loan_instance.Status = 0;
                    break;
                case "Approve":
                    class_loan_instance.Status = 1;
                    break;
                case "Released":
                    class_loan_instance.Status = 2;
                    break;
                case "Collect":
                    class_loan_instance.Status = 3;
                    break;
                case "Deny":
                    class_loan_instance.Status = 4;
                    break;
            }

            //data
            int list_id = 0;
            int months = 0;
            int principal = 0;
            int penaltyrate = 0;
            int compoundId = 0;

            using (SqlConnection connection = new SqlConnection(pattern_Singleton_instance.GetStringConnection()))
            {
                connection.Open();

                

                string ref_no = textbox_loanlist_details_reference.Text.ToString();

                string query = $"select * from loan_list ls inner join loan_plan pl on pl.plan_id = ls.plan_id inner join penalty_rate pr on pr.penalty_rate_id = pl.penalty_rate_id inner join compound cp on cp.compound_id = ls.compound_id where ref_no = '{@ref_no}'";

                using (SqlCommand command = new SqlCommand(query, connection))  // Pass the query to SqlCommand constructor
                {
                    

                    Console.WriteLine(textbox_loanlist_details_reference.Text.ToString());

                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            // Access columns using reader["ColumnName"]
                            list_id = int.Parse(reader["list_id"].ToString());
                            months = int.Parse(reader["months"].ToString());
                            principal = int.Parse(reader["amount"].ToString());
                            penaltyrate = int.Parse(reader["value"].ToString());
                            principal = int.Parse(reader["amount"].ToString());
                            compoundId = int.Parse(reader["compound_id"].ToString());

                        }

                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error executing SQL query: {ex.Message}");
                        return;
                    }
                }

                using (SqlCommand command = new SqlCommand("dbo.GenerateLoanSchedules", connection))
                {

                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // Add parameters to the command
                    command.Parameters.AddWithValue("@list_id", list_id);
                    command.Parameters.AddWithValue("@months", months);
                    command.Parameters.AddWithValue("@principalAmount", principal);
                    command.Parameters.AddWithValue("@penaltyRate", penaltyrate);
                    command.Parameters.AddWithValue("@compoundId", compoundId);

                    try
                    {
                        // Execute the stored procedure
                        command.ExecuteNonQuery();

                        Console.WriteLine("Schedule added successfully!");
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"Error executing stored procedure: {ex.Message}");

                        return;
                    }
                }

                query = $"update loan_list set status = 1 where ref_no = '{ref_no}'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        // Execute the stored procedure
                        command.ExecuteNonQuery();

                        Console.WriteLine("Status added successfully!");
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"Error executing stored procedure: {ex.Message}");

                        return;
                    }
                }
            }

        }

        private void button_dashboard_loanschedule_Click(object sender, EventArgs e)
        {
            class_readloanschedules class_readloanschedules_instance = new class_readloanschedules(this);
            this.table_loanschedule.DataSource = class_readloanschedules_instance.getschedulelist();

            this.table_loanschedule.Columns["schedule_id"].Visible = false;
            this.table_loanschedule.Columns["isPaid"].HeaderText = "Paid";
        }

        private void button_dashboard_loanplans_Click(object sender, EventArgs e)
        {
            
        }
    }
}