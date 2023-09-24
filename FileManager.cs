using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Hospital_Management_System
{
    public class FileManager
    {

        private const string PatientsFilePath = @"G:\My Drive\UNI\2023-S2\.NET\Assignment 1\Hospital Management System\Patients.txt";
        private const string DoctorsFilePath = @"G:\My Drive\UNI\2023-S2\.NET\Assignment 1\Hospital Management System\Doctors.txt";
        private const string AdminsFilePath = @"G:\My Drive\UNI\2023-S2\.NET\Assignment 1\Hospital Management System\Admins.txt";
        private const string AppsFilePath = @"G:\My Drive\UNI\2023-S2\.NET\Assignment 1\Hospital Management System\Appointments.txt";

        private static Dictionary<int, List<int>> doctorPatientsMap = null;

        public static void InitializeDoctorPatientsMap()
        {
            if (doctorPatientsMap != null) // If already initialized, no need to reinitialize
            {
                return;
            }

            try
            {
                doctorPatientsMap = new Dictionary<int, List<int>>();
                // Check if the appointments file exists
                if (File.Exists(AppsFilePath))
                {
                    // Read all lines from the appointments file
                    string[] lines = File.ReadAllLines(AppsFilePath);

                    // Iterate through each line to extract patient and doctor IDs
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split(',');

                        if (parts.Length >= 3 &&
                            int.TryParse(parts[1], out int patientId) &&
                            int.TryParse(parts[2], out int doctorId))
                        {
                            // Initialize the doctor-patient relationship if it doesn't exist
                            if (!doctorPatientsMap.ContainsKey(doctorId))
                            {
                                doctorPatientsMap[doctorId] = new List<int>();
                            }

                            // Add the patient ID to the doctor's list of patients
                            doctorPatientsMap[doctorId].Add(patientId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading appointments: {ex.Message}");
            }
        }

        public static HashSet<int> GetPatientsForDoctor(int doctorId)
        {
            InitializeDoctorPatientsMap();
            // Check if the doctor exists in the doctor-patient map
            if (doctorPatientsMap.ContainsKey(doctorId))
            {
                // Return the list of patients associated with the doctor
                return new HashSet<int>(doctorPatientsMap[doctorId]);
            }

            // If the doctor doesn't exist or has no patients, return an empty list
            return new HashSet<int>();
        }

        //Login validation
        public static string ValidateCredentials(int userId, string password)
        {
            if (IsCredentialsValid(userId, password, PatientsFilePath))
            {
                Console.Write("Valid credentials");
                return "Patient";

            }

            else if (IsCredentialsValid(userId, password, DoctorsFilePath))
            {
                Console.Write("Valid credentials");
                return "Doctor";
            }

            else if (IsCredentialsValid(userId, password, AdminsFilePath))
            {
                Console.Write("Valid credentials");
                return "Admin";
            }

            else 
            {
                Console.Write("Invalid credentials"); // No matching credentials found
                return null;
            }
            
        }
        //Validation helper function
        private static bool IsCredentialsValid(int userId, string password, string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                return false; // File doesn't exist
            }

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] parts = line.Split(',');

                if (int.TryParse(parts[0], out int id) && id == userId && parts[2] == password)
                    //
                    //
                {
                    return true; // Credentials match
                }
            }

            return false; // No matching credentials found
        }

        public static string GetPropertiesById(int userId, string property)
        {
            string idString = userId.ToString();

            if (idString.Length != 5)
            {
                Console.WriteLine("Invalid user ID format.");
                return null;
            }

            char rolePrefix = idString[0];

            switch (rolePrefix)
            {
                case '1':
                    return GetPropertyFromFilePath(userId, property, PatientsFilePath);
                case '2':
                    return GetPropertyFromFilePath(userId, property, DoctorsFilePath);
                case '3':
                    return GetPropertyFromFilePath(userId, property, AdminsFilePath);
                case '4':
                    return GetPropertyFromFilePath(userId, property, AppsFilePath);
                default:
                    Console.WriteLine("Invalid user role.");
                    return null;
            }
        }

        private static string GetPropertyFromFilePath(int userId, string property, string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                return null;
            }

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] parts = line.Split(',');

                if (int.TryParse(parts[0], out int id) && id == userId)
                {
                    StringBuilder result = new StringBuilder();

                    foreach (string prop in property.ToLower().Split(','))
                    {
                        switch (prop)
                        {
                            case "id":
                                result.Append(parts[0]);
                                break;
                            case "name":
                                result.Append(parts[1]);
                                break;
                            case "email":
                                result.Append(parts[3]);
                                break;
                            case "phone":
                                result.Append(parts[4]);
                                break;
                            case "address":
                                result.Append(parts[5]);
                                break;
                            case "appointmentid":
                                result.Append(parts[0]);
                                break;
                            case "patientid":
                                result.Append(parts[1]);
                                break;
                            case "doctorid":
                                result.Append(parts[2]);
                                break;
                            case "description":
                                result.Append(parts[3]);
                                break;
                            default:
                                return "Invalid Property";

                        }
                        result.Append(",");
                    }

                    return result.ToString().TrimEnd(',');

                }
            }

            Console.WriteLine($"User not found with ID: {userId}");
            return null;
        }

        public static int ReadLastAssignedID(string filePath)
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                if (lines.Length > 0)
                {
                    string lastLine = lines[lines.Length - 1];
                    string[] parts = lastLine.Split(',');
                    if (parts.Length >= 1 && int.TryParse(parts[0], out int lastID))
                    {
                        return lastID;
                    }
                }
            }
            return 0; // Default to 0 if no file or data is available
        }

        public static void ListAllUsers(string role)
        {
            string filePath = GetFilePathForRole(role);

            Console.Clear();
            Console.WriteLine("DOTNET Hospital Management System");
            Console.WriteLine($"All {role}s:");

            // Read user data from the respective text file
            string[] userData = File.ReadAllLines(filePath);

            // Loop through the user records and display them
            foreach (string userRecord in userData)
            {
                string[] userFields = userRecord.Split(',');

                // Assuming the format of the line in the file is: ID,Name,Email,Phone,Address,Password
                if (userFields.Length >= 2)
                {
                    int userID;
                    if (int.TryParse(userFields[0], out userID))
                    {
                        // Extract fields as needed
                        string userName = userFields[1];                      
                        string userEmail = userFields[3];
                        string userPhone = userFields[4];
                        string userAddress = userFields[5];
                        
                        // Display the user information
                        Console.WriteLine($"{userName} | {userEmail} | {userPhone} | {userAddress}");
                        Console.WriteLine(); // Add a blank line between users
                    }
                }
            }
        }

        private static string GetFilePathForRole(string role)
        {
            switch (role)
            {
                case "Doctor":
                    return DoctorsFilePath;
                case "Patient":
                    return PatientsFilePath;
                case "Admin":
                    return AdminsFilePath;
                default:
                    throw new ArgumentException("Invalid role.");
            }
        }

        public static void AppendAppointment(string appointmentData)
        {
            string appointmentsFilePath = @"G:\My Drive\UNI\2023-S2\.NET\Assignment 1\Hospital Management System\Appointments.txt";

            try
            {
                // Append the appointment data to the appointments file
                using (StreamWriter writer = File.AppendText(appointmentsFilePath))
                {
                    writer.WriteLine(appointmentData);
                }

                Console.WriteLine("Appointment details added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error appending appointment: {ex.Message}");
            }
        }


        public static int GetRegisteredDoctorId(int userId)
        {
            // Define the file path for patient-doctor registrations
            string appointmentsFilePath = @"G:\My Drive\UNI\2023-S2\.NET\Assignment 1\Hospital Management System\Appointments.txt";

            try
            {
                // Check if the patient-doctor registration file exists
                if (File.Exists(appointmentsFilePath))
                {
                    // Read all lines from the registration file
                    string[] lines = File.ReadAllLines(appointmentsFilePath);

                    // Check if there is a registration entry for the given user ID
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length >= 2 && int.TryParse(parts[1], out int patientId) && int.TryParse(parts[2], out int doctorId))
                        {
                            if (patientId == userId)
                            {
                                // The user is registered with a doctor, return the doctor's ID
                                return doctorId;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking appointments: {ex.Message}");
            }
            // The user is not registered with a doctor
            return -1; 
        }
    }
}


