using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management_System
{
    public class Utils
    {
        public static void LoginMenu()
        {
            Console.WriteLine("DOTNET Hospital Management System");
            Console.WriteLine("Login");
            Console.Write("\nID: ");

            if (!int.TryParse(Console.ReadLine(), out int userId))
            {
                Console.WriteLine("Invalid input. Please enter a valid ID.");
            }

            Console.Write("Password: ");
            string password = Utils.ReadPassword();
            string role = FileManager.ValidateCredentials(userId, password);
            Thread.Sleep(2000); 

            Console.Clear();

            if (role != null)
            {
                switch (role)
                {
                    case "Patient":
                        Patient.MainMenu(role, userId);
                        break;
                    case "Doctor":
                        Doctor.MainMenu(role, userId);
                        break;
                    case "Admin":
                        Admin.MainMenu(role, userId);
                        break;
                }
                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                LoginMenu();    
            }
            
        }

        public static string ReadPassword()
        {
            StringBuilder password = new StringBuilder();
            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey(true); // Read a key without displaying it

                // Check if the key is not Enter (13) or Backspace (8)
                if (keyInfo.Key != ConsoleKey.Enter && keyInfo.Key != ConsoleKey.Backspace)
                {
                    password.Append(keyInfo.KeyChar);
                    Console.Write("*"); // Display an asterisk for each character
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    // If Backspace is pressed, remove the last character from the password
                    password.Remove(password.Length - 1, 1);
                    Console.Write("\b \b"); // Erase the last asterisk
                }
            } while (keyInfo.Key != ConsoleKey.Enter);

            Console.WriteLine(); // Move to the next line after Enter
            return password.ToString();
        }
    }
}
