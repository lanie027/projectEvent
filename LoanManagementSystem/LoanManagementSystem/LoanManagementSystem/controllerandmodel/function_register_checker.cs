using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LoanManagementSystem.controller;
using LoanManagementSystem.controllerandmodel;

namespace LoanManagementSystem.controllerandmodel
{
    internal class function_register_checker
    {
        private function_add_borrower function_add_borrower_instance = new function_add_borrower();
        public bool isRegisterValid(string firstname, string middlename, string lastname, string age, string email, string address, string contact, string username, string password)
        {
            // Check if all inputs are empty
            if (string.IsNullOrWhiteSpace(firstname) &&
                string.IsNullOrWhiteSpace(middlename) &&
                string.IsNullOrWhiteSpace(lastname) &&
                string.IsNullOrWhiteSpace(email) &&
                string.IsNullOrWhiteSpace(address) &&
                string.IsNullOrWhiteSpace(contact) &&
                string.IsNullOrWhiteSpace(username) &&
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("All inputs are empty.");
                return false;
            }


            int nage = 0;

            if (int.TryParse(age, out nage))
            {
                // Successfully parsed as an integer
                if (nage == 0)
                {
                    MessageBox.Show("Age cannot be 0. Please enter a valid age.");
                    return false;
                }
                else
                {
                    // Use the 'age' variable for further processing
                    // For example: registerChecker.CheckRegistration(firstname, middlename, lastname, age, email, address);
                }
            }
            else
            {
                // Failed to parse as an integer
                MessageBox.Show("Invalid age format. Please enter a valid age.");
                return false;
            }

            //Successful process


            // Check specific formats for each input
            if (string.IsNullOrWhiteSpace(firstname) || !IsValidName(firstname))
            {
                MessageBox.Show("Invalid firstname format. Please enter a valid name.");
                return false;
            }

            if (!IsValidName(middlename))
            {
                MessageBox.Show("Invalid middlename format. Please enter a valid name.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(lastname) || !IsValidName(lastname))
            {
                MessageBox.Show("Invalid lastname format. Please enter a valid name.");
                return false;
            }

            if (nage <= 0 || nage > 150)
            {
                MessageBox.Show("Invalid age format. Please enter a valid age.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(email) || IsValidEmail(email))
            {
                MessageBox.Show("Invalid email format. Please enter a valid email address.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(address))
            {
                MessageBox.Show("Invalid address format. Please enter a valid address.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(contact))
            {
                MessageBox.Show("Invalid address format. Please enter a valid address.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Invalid address format. Please enter a valid address.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Invalid address format. Please enter a valid address.");
                return false;
            }

            // All checks passed
            Console.WriteLine("Registration checks passed.");

            this.function_add_borrower_instance.add_borrower(firstname, middlename, lastname, nage, address, email, contact, username, password);
            return true;
        }

        private bool IsValidName(string name)
        {
            // Add your custom logic for name validation
            // For example, check if it contains only letters
            return !string.IsNullOrWhiteSpace(name) && name.All(char.IsLetter);
        }

        private bool IsValidEmail(string email)
        {
            // Add your custom logic for email validation
            // For a simple example, checking for the presence of '@' and '.'
            return !string.IsNullOrWhiteSpace(email) && email.Contains("@") && email.Contains(".");
        }
    }
}
