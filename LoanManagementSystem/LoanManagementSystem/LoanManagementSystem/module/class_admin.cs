using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanManagementSystem.controllerandmodel;
namespace LoanManagementSystem.module
{

    internal class class_admin : class_user, interface_getuserrowdata
    {
        class_user interface_getuserrowdata.GetUserRowData(string username)
        {
            throw new NotImplementedException();
        }
    }
}
