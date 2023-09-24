using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management_System
{
    public class Admin : User
    {
        public Admin(string name, string password, string email, string phone, string address) : base(name, password, email, phone, address)
        {
            Password = password;
            ID = GenerateID(3, @"G:\My Drive\UNI\2023-S2\.NET\Assignment 1\Hospital Management System\Admins.txt");
        }

        public static void MainMenu(string role, int userId)
        {
                Console.WriteLine("DOTNET Hospital Management System");
                Console.WriteLine($"{role} Menu");
                Console.WriteLine("Welcome to DOTNET Hospital Management System " + FileManager.GetPropertiesById(userId, "name"));
                Console.WriteLine("\nPlease choose an option:");
                Console.WriteLine("1. List all doctors");
                Console.WriteLine("2. Check doctor details");
                Console.WriteLine("3. List all patients");
                Console.WriteLine("4. Check patient details");
                Console.WriteLine("5. Add doctor");
                Console.WriteLine("6. Add patient");
                Console.WriteLine("5. Logout");
                Console.WriteLine("6. Close");

                char choice = Console.ReadKey().KeyChar;

                switch (choice)
                {
                    case '1':
                        Console.Clear();
                        Console.WriteLine("DOTNET Hospital Management System");
                        // Implement logic to list all doctors
                        Console.WriteLine("All doctors:\n");
                        // Display the list of doctors
                        FileManager.ListAllUsers("Doctor");
                        Console.WriteLine("Press any key to return...");
                        Console.ReadKey();
                        Console.Clear();
                        MainMenu(role, userId);
                        break;

                    case '2':

                        // Implement logic to check doctor details
                        Console.Clear();
                        Console.WriteLine("DOTNET Hospital Management System");
                        Console.WriteLine("Doctor Details");
                        Console.WriteLine("\nPlease enter the ID of the doctor who's details you are checking! Or press any key to return to menu. ");
                        int doctorId;
                        if (int.TryParse(Console.ReadLine(), out doctorId))
                        {
                            string doctorDetails = FileManager.GetPropertiesById(doctorId, "name,email,phone,address");

                            if (doctorDetails != null)
                            {
                                string[] details = doctorDetails.Split(',');
                                Console.WriteLine($"{details[0]} | {details[1]} | {details[2]} | {details[3]}");
                                Console.WriteLine("\nPress any key to return.");
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.WriteLine("Doctor not found with the specified ID. Press any key to return.");
                                Console.ReadKey();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Id. Press any key to return.");
                            
                        }
                        
                        Console.Clear();
                        MainMenu(role, userId);
                        break;

                    case '3':

                        Console.Clear();
                        Console.WriteLine("DOTNET Hospital Management System");
                        Console.WriteLine("All Details");
                        // Implement logic to list all patients
                        Console.WriteLine("\nAll patients registered to the DOTNET Hospital Management System");
                        // Display the list of patients
                        FileManager.ListAllUsers("Patient");
                        Console.ReadKey();
                        Console.Clear();
                        MainMenu(role, userId);
                        break;

                    case '4':

                        Console.Clear();
                        Console.WriteLine("DOTNET Hospital Management System");
                        Console.WriteLine("Patient Details");
                        Console.Write("\nPlease enter the ID of the patient to check: ");
                       
                        int patientCheckId;
                        if (int.TryParse(Console.ReadLine(), out patientCheckId))
                        {
                            string patientCheckDetails = FileManager.GetPropertiesById(patientCheckId, "name,email,phone,address");
                            int docPatCheckId = FileManager.GetRegisteredDoctorId(patientCheckId);
                            string docPatCheckName = FileManager.GetPropertiesById(docPatCheckId, "name");

                            if (patientCheckDetails != null)
                            {
                                string[] details = patientCheckDetails.Split(',');
                                Console.WriteLine($"{details[0]} | {docPatCheckName} | {details[1]} | {details[2]} | {details[3]}");
                                Console.WriteLine("\nPress any key to return.");
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.WriteLine("Patient not found with the specified ID. Press any key to return.");
                                Console.ReadKey();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Patient not found with the specified ID. Press any key to return.");
                            Console.ReadKey();
                        }
                        Console.Clear();
                        MainMenu(role, userId);
                        break;

                    case '5':

                        // Implement logic to add a doctor
                        Console.Clear();
                        Console.WriteLine("DOTNET Hospital Management System");
                        Console.WriteLine("Add Doctor");
                        Console.WriteLine("Registering a new doctor with the DOTNET Hospital Management System");
                        // Add doctor details
                        Console.Write("First Name: ");
                        string doctorFName = Console.ReadLine();
                        Console.Write("Last Name: ");
                        string doctorLName = Console.ReadLine();
                        string doctorName = doctorFName + doctorLName;
                        Console.Write("Password: ");
                        string doctorPassword = Utils.ReadPassword();
                        Console.Write("Email: ");
                        string doctorEmail = Console.ReadLine();
                        Console.Write("Phone: ");
                        string doctorPhone = Console.ReadLine();
                        Console.Write("Street Number: ");
                        string doctorAddress = Console.ReadLine();
                        Console.Write("Street: ");
                        doctorAddress += " " + Console.ReadLine();  
                        Console.Write("City: ");
                        doctorAddress += " " + Console.ReadLine();
                        Console.Write("State: ");
                        doctorAddress += " " + Console.ReadLine();

                        Doctor newDoc = new Doctor(doctorName, doctorPassword, doctorEmail, doctorPhone, doctorAddress);
                                                
                        Console.WriteLine($"{doctorFName} {doctorLName} added to the system!");
                        Console.ReadKey();
                        Console.Clear();
                        MainMenu(role, userId);
                        break;

                    case '6':

                        // Implement logic to add a patient
                        Console.Clear();
                        Console.WriteLine("DOTNET Hospital Management System");
                        Console.WriteLine("Add Patient");
                        Console.WriteLine("Registering a new patient with the DOTNET Hospital Management System");
                        // Add patient details
                        Console.Write("First Name: ");
                        string patientFName = Console.ReadLine();
                        Console.Write("Last Name: ");
                        string patientLName = Console.ReadLine();
                        string patientName = patientFName + patientLName;
                        Console.Write("Password: ");
                        string patientPassword = Utils.ReadPassword();
                        Console.Write("Email: ");
                        string patientEmail = Console.ReadLine();
                        Console.Write("Phone: ");
                        string patientPhone = Console.ReadLine();
                        Console.Write("Street Number: ");
                        string patientAddress = Console.ReadLine();
                        Console.Write("Street: ");
                        patientAddress += " " + Console.ReadLine();
                        Console.Write("City: ");
                        patientAddress += " " + Console.ReadLine();
                        Console.Write("State: ");
                        patientAddress += " " + Console.ReadLine();

                        Patient newPat = new Patient(patientName, patientPassword, patientEmail, patientPhone, patientAddress);

                        Console.WriteLine($"{patientFName} {patientLName} added to the system!");
                        Console.ReadKey();
                        Console.Clear();
                        MainMenu(role, userId);
                        break;

                    case '7':

                        // Implement logout logic if needed
                        Console.Clear();
                        Utils.LoginMenu();
                        break; // Exit the Admin menu

                    case '8':

                        Environment.Exit(0); // Exit the application
                        break;

                    default:

                        Console.WriteLine("\nInvalid Option... Press any key to try again");
                        Console.ReadKey();
                        Console.Clear();
                        MainMenu(role, userId);
                        break;

            }

        }
    }
}
