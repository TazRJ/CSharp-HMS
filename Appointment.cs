using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management_System
{
    public class Appointment
    {
        public int ID { get; protected set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string Description { get; set; }

        public Appointment(int patId, int docId, string desc)
        {
            ID = GenerateAppID(@"G:\My Drive\UNI\2023-S2\.NET\Assignment 1\Hospital Management System\Appointments.txt");
            this.PatientId = patId;
            this.DoctorId = docId;
            this.Description = desc;
        }

        protected int GenerateAppID(string filePath)
        {
            int lastAssignedID = FileManager.ReadLastAssignedID(filePath);
            int newID = lastAssignedID + 1;

            return newID;
        }

        public override string ToString()
        {
            return $"{ID},{PatientId},{DoctorId},{Description}";
        }
    }

}
