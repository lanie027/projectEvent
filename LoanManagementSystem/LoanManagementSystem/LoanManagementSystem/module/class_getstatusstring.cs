using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.module
{
    internal class class_getstatusstring
    {
        // Helper method to get the string representation of status
        public string GetStatusString(int statusValue)
        {
            switch (statusValue)
            {
                case 0:
                    return "For Approval";
                case 1:
                    return "Approved";
                case 2:
                    return "Released";
                case 4:
                    return "Collect";
                case 5:
                    return "Deny";
                // Add more cases as needed
                default:
                    return "Unknown";
            }
        }
    }
}
