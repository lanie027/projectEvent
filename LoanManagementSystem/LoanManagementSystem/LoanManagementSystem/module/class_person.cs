using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.module
{
    abstract class class_person
    {
        private int id;
        private string firstname;
        private string middlename;
        private string lastname;
        private string age;
        private string address;
        private string email;
        private string contact;
        private string date;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string FirstName
        {
            get { return firstname; }
            set { firstname = value; }
        }

        public string MiddleName
        {
            get { return middlename; }
            set { middlename = value; }
        }

        public string LastName
        {
            get { return lastname; }
            set { lastname = value; }
        }

        public string Age
        {
            get { return age; }
            set { age = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Contact
        {
            get { return contact; }
            set { contact = value; }
        }

        public string Date
        {
            get { return date; }
            set { date = value; }
        }
    }
}
