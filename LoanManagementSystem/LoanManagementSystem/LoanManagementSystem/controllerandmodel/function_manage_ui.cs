using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanManagementSystem.module;
using LoanManagementSystem.view;

namespace LoanManagementSystem.controller
{
    internal class function_manage_ui
    {
       public function_manage_ui()
        {
            //empty
        }

        public interface_ui_load manage_ui(interface_ui_load ui, Form1 form)
        {
            if (form is null)
            {
                return null;
            }
            else if (ui is ui_admin)
            {
                return new ui_admin(form);
            }
            else if (ui is ui_collector)
            {
                return new ui_collector(form);
            }
            else if (ui is ui_backoffice)
            {
                return new ui_backoffice(form);
            }
            else if (ui is ui_borrower)
            {
                return new ui_borrower(form);
            }

            return null;
        }
    }
}
