using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hospital_Management_System
{
    public class Doctor : User
    {
        public Doctor(string name, string password, string email, string phone, string address) : base(name, password, email, phone, address)
        {
            Password = password;
            ID = GenerateID(2, @"G:\My Drive\UNI\2023-S2\.NET\Assignment 1\Hospital Management System\Doctors.txt");
        }

        public static void MainMenu(string role, int userId)
        {
            Console.WriteLine("DOTNET Hospital Management System");
            Console.WriteLine($"{role} Menu");
            Console.WriteLine("Welcome to DOTNET Hospital Management System " + FileManager.GetPropertiesById(userId, "name"));
            Console.WriteLine("\nPlease choose an option:");
            Console.WriteLine("1. List doctor details");
            Console.WriteLine("2. List patients");
            Console.WriteLine("3. List appointments");
            Console.WriteLine("4. Check particular patient");
            Console.WriteLine("5. Logout");
            Console.WriteLine("6. Close");

            char choice = Console.ReadKey().KeyChar;

            switch (choice)
            {
                case '1': //case 1
                    Console.Clear();
                    Console.WriteLine("DOTNET Hospital Management System");
                    Console.WriteLine("My Details");

                    string docDetails = FileManager.GetPropertiesById(userId, "name,email,phone,address");

                    if (docDetails != null)
                    {
                        string[] details = docDetails.Split(',');
                        Console.WriteLine($"\nName: {details[0]}");
                        Console.WriteLine($"Email: {details[1]}");
                        Console.WriteLine($"Phone: {details[2]}");
                        Console.WriteLine($"Address: {details[3]}");
                        Console.WriteLine("\nPress any key to return.");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Patient not found with the specified ID. Press any key to return.");
                        Console.ReadKey();
                    }

                    Console.Clear();
                    MainMenu(role, userId);
                    break;

                case '2': //case 2

                    Console.Clear();
                    Console.WriteLine("DOTNET Hospital Management System");
                    Console.WriteLine("My Patients");
                    
                    HashSet<int> patientIds = FileManager.GetPatientsForDoctor(userId);
                    string docName = FileManager.GetPropertiesById(userId, "name");
                    Console.WriteLine($"\nPatients assigned to {docName}");

                    if (patientIds.Count > 0)
                    {
                        foreach (int patientId in patientIds)
                        {
                            string patientDetails = FileManager.GetPropertiesById(patientId, "name,email,phone,address");

                            if (patientDetails != null)
                            {
                                string[] details = patientDetails.Split(',');
                                Console.WriteLine($"{details[0]} | {docName} | {details[1]} | {details[2]} | {details[3]}");
                            }
                        }

                        Console.WriteLine("\nPress any key to return.");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("No patients assigned to you. Press any key to return.");
                        Console.ReadKey();
                    }
                    Console.Clear();
                    MainMenu(role, userId);
                    break;

                case '3': //case 3

                    Console.Clear();
                    Console.WriteLine("DOTNET Hospital Management System");
                    Console.WriteLine("All Appointments\n");
                    string docAppName = FileManager.GetPropertiesById(userId, "name");
                    List<string> appointments = new List<string>();
                    try
                    {
                        // Read all lines from the appointments file
                        string[] lines = File.ReadAllLines(@"G:\My Drive\UNI\2023-S2\.NET\Assignment 1\Hospital Management System\Appointments.txt");

                        foreach (string line in lines)
                        {
                            string[] parts = line.Split(',');

                            if (parts.Length >= 3 && int.TryParse(parts[1], out int patientId) && int.TryParse(parts[2], out int docId) && docId == userId)
                            {
                                // Extract appointment details

                                string patName = FileManager.GetPropertiesById(patientId, "name");
                                string appointmentDetails = $"Doctor: {docAppName} | Patient: {patName} | Description: {parts[3]}";
                                appointments.Add(appointmentDetails);
                                Console.WriteLine(appointmentDetails);
                            }
                        }
                        if (appointments.Count == 0) 
                        { 
                            Console.WriteLine("No appointment bookings have been made. Press any key to return."); 
                        }
                        Console.ReadKey();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error reading appointments: {ex.Message}");
                        Console.ReadKey();
                    }
                    Console.Clear();
                    MainMenu(role, userId);
                    break;  
                    
                case '4': //case 4

                    Console.Clear();
                    Console.WriteLine("DOTNET Hospital Management System");
                    Console.WriteLine("Doctor Details");
                    Console.Write("\nPlease enter the ID of the patient to check: ");
                    string docPatCheckName = FileManager.GetPropertiesById(userId, "name");
                    int patientCheckId;
                    if (int.TryParse(Console.ReadLine(), out patientCheckId))
                    {
                        string patientCheckDetails = FileManager.GetPropertiesById(patientCheckId, "name,email,phone,address");

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

                case '5': //case 5

                    // Implement logout logic if needed
                    Console.Clear();
                    Utils.LoginMenu();
                    break; // Exit the Admin menu

                case '6': //case 6

                    Environment.Exit(0); // Exit the application
                    break;

                default: //default

                    Console.WriteLine("\nInvalid Option... Press any key to try again");
                    Console.ReadKey();
                    Console.Clear();
                    MainMenu(role, userId);
                    break;
            }  
        }

        public override string ToString()
        {
            // Compress the doctor details into a short line
            return $"{ID},{Name},{Password},{Email},{Phone},{Address}";
        }
    }

}
