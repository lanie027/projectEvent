using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanManagementSystem.module;
using LoanManagementSystem.view;

namespace LoanManagementSystem.controller
{
    internal class function_user_manager
    {
        private function_login_checker function_login_checker_instance = new function_login_checker();
        private function_manage_ui function_manage_ui_instance = new function_manage_ui();
        private interface_ui_load interface_ui_load_instance;

        public function_user_manager(string username, string password, Form1 form)
        {
            try
            {
                bool valid = function_login_checker_instance.isLoginValid(username, password);

                if (valid)
                {
                    int type = function_login_checker_instance.loginType(username, password);

                    switch (type)
                    {
                        case 1:
                            interface_ui_load_instance = function_manage_ui_instance.manage_ui(new ui_admin(), form);
                            break;
                        case 2:
                            interface_ui_load_instance = function_manage_ui_instance.manage_ui(new ui_backoffice(), form);
                            break;
                        case 3:
                            interface_ui_load_instance = function_manage_ui_instance.manage_ui(new ui_collector(), form);
                            break;
                        case 4:
                            interface_ui_load_instance = function_manage_ui_instance.manage_ui(new ui_borrower(), form);
                            break;
                        default:
                            // Handle unexpected user types
                            Console.WriteLine("Unexpected user type");
                            break;
                    }

                    // Load the interface
                    interface_ui_load_instance.load();
                }
                else
                {
                    // Handle invalid login
                    Console.WriteLine("Invalid login credentials");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                // Cleanup or additional logic to be executed whether an exception occurs or not
            }
        }
    }
}
