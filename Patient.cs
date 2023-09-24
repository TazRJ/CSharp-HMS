using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hospital_Management_System
{
    public class Patient : User
    {
        public Patient(string name, string password, string email, string phone, string address) : base(name, password, email, phone, address)
        {
            Password = password;
            ID = GenerateID(1, @"G:\My Drive\UNI\2023-S2\.NET\Assignment 1\Hospital Management System\Patients.txt");
        }

        public static void MainMenu(string role, int userId)
        {
            Console.WriteLine("DOTNET Hospital Management System");
            Console.WriteLine($"{role} Menu");
            Console.WriteLine("Welcome to DOTNET Hospital Management System " + FileManager.GetPropertiesById(userId, "name"));
            Console.WriteLine("\nPlease choose an option:");
            Console.WriteLine("1. List patient details");
            Console.WriteLine("2. List my doctor details");
            Console.WriteLine("3. List all appointments");
            Console.WriteLine("4. Book appointment");
            Console.WriteLine("5. Logout");
            Console.WriteLine("6. Close");

            char choice = Console.ReadKey().KeyChar;

            switch (choice)
            {
                case '1':

                    Console.Clear();
                    Console.WriteLine("DOTNET Hospital Management System");
                    Console.WriteLine("My Details");

                    string patientDetails = FileManager.GetPropertiesById(userId, "id,name,email,phone,address");

                    if (patientDetails != null)
                    {
                        string[] details = patientDetails.Split(',');                       
                        Console.WriteLine($"\n{details[1]}'s Details");
                        Console.WriteLine($"\nPatient ID: {details[0]}");
                        Console.WriteLine($"Full Name: {details[1]}");
                        Console.WriteLine($"Email: {details[2]}");
                        Console.WriteLine($"Phone: {details[3]}");
                        Console.WriteLine($"Address: {details[4]}");
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

                case '2':

                    Console.Clear();
                    Console.WriteLine("DOTNET Hospital Management System");
                    Console.WriteLine("My Doctor\n");
                    Console.WriteLine("Your doctor:\n");

                    int doctorId = FileManager.GetRegisteredDoctorId(userId);
                    
                    if (doctorId != -1)
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
                        Console.WriteLine("Doctor not found with the specified ID. Press any key to return.");
                    }
                    Console.Clear();
                    MainMenu(role, userId);
                    break;   
                    
                case '3':

                    Console.Clear();
                    Console.WriteLine("DOTNET Hospital Management System");
                    Console.WriteLine("My Appointments");
                    string patName = FileManager.GetPropertiesById(userId, "name");
                    Console.WriteLine($"\nAppointments for {patName}");
                    List<string> appointments = new List<string>();
                    try
                    {
                        // Read all lines from the appointments file
                        string[] lines = File.ReadAllLines(@"G:\My Drive\UNI\2023-S2\.NET\Assignment 1\Hospital Management System\Appointments.txt");

                        foreach (string line in lines)
                        {
                            string[] parts = line.Split(',');

                            if (parts.Length >= 3 && int.TryParse(parts[1], out int patientId) && int.TryParse(parts[2], out int docId) && patientId == userId)
                            {
                                // Extract appointment details

                                string doctorName = FileManager.GetPropertiesById(docId, "name");
                                string appointmentDetails = $"Doctor: {doctorName} | Patient: {patName} | Description: {parts[3]}";
                                appointments.Add(appointmentDetails);
                                Console.WriteLine(appointmentDetails);
                            }
 
                            
                        }
                        if (appointments.Count == 0) { Console.WriteLine("No bookings have been made. Press any key to return."); }
                        
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error reading appointments: {ex.Message}");
                    }

                    Console.ReadKey();
                    Console.Clear();
                    MainMenu(role, userId);
                    break;

                case '4':

                    Console.Clear();
                    Console.WriteLine("DOTNET Hospital Management System");
                    Console.WriteLine("Book Appointment");
                    int registeredDoctorId = FileManager.GetRegisteredDoctorId(userId);
                    if (registeredDoctorId != -1)
                    {
                        string doctorName = FileManager.GetPropertiesById(registeredDoctorId, "name");
                        if (doctorName != null)
                        {
                            Console.WriteLine($"Your registered doctor's name is {doctorName}");
                            Console.Write("Description of the appointment: ");
                            string desc = Console.ReadLine();
                            Appointment newAppointment = new Appointment(userId, registeredDoctorId, desc);
                            FileManager.AppendAppointment(newAppointment.ToString());
                            Console.WriteLine("\nThe appointment has been booked successfully!");
                            Console.ReadKey();
                        }
                    }
                    else          
                    {
                        Console.WriteLine("\nYou are not registered with any doctor! Please choose which doctor you would like to register with:");
                        FileManager.ListAllUsers("Doctor");
                        //Only do the following if the patient is not in the appointments.txt
                        Console.WriteLine("Please choose a doctor: ");

                        if (int.TryParse(Console.ReadLine(), out int docNum))
                        {
                            int docId = 20000 + (docNum - 1);
                            string doctorDetails = FileManager.GetPropertiesById(docId, "name");
                            if (doctorDetails != null)
                            {
                                string[] details = doctorDetails.Split(',');
                                Console.WriteLine($"You are booking a new appointment with {details[0]}");
                                Console.Write("Description of the appointment: ");
                                string desc = Console.ReadLine();
                                Appointment newAppointment = new Appointment(userId, docId, desc);
                                FileManager.AppendAppointment(newAppointment.ToString());
                                Console.WriteLine("\nThe appointment has been booked successfully!");
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.WriteLine("Doctor not found. Press any key to return.");
                                Console.ReadKey();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Id. Press any key to return.");

                        }
                    }                                 
                    Console.Clear();
                    MainMenu(role, userId);
                    break;

                case '5':

                    // Implement logout logic if needed
                    Console.Clear();
                    Utils.LoginMenu();
                    break; // Exit the Admin menu

                case '6':

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

        public override string ToString()
        {
            // Compress the doctor details into a short line
            return $"{ID},{Name},{Password},{Email},{Phone},{Address}";
        }
    }
}
