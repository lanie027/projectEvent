using LoanManagementSystem.controllerandmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.module
{
    internal class class_user : class_person
    {
        private string username;
        private string password;

        public string Username
        {
            get { return username; } 
            set { username = value;}
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }


        private function_user_manager function_user_manager_instance = new function_user_manager();

        public void GetUserRowData(string username)
        {
            string[] userDataArray = function_user_manager_instance.GetUserRowData(username);

            if (userDataArray.Length >= 9)
            {
                this.Id = int.Parse(userDataArray[0]);
                this.FirstName = userDataArray[1];
                this.MiddleName = userDataArray[2];
                this.LastName = userDataArray[3];
                this.Age = userDataArray[4];
                this.Address = userDataArray[5];
                this.Email = userDataArray[6];
                this.Contact = userDataArray[7];
                this.Date = userDataArray[8];
            }
            else
            {
                throw new ArgumentException("Invalid array length for UserData");
            }

            return;
        }
    }
}
