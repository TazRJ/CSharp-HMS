using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management_System
{
    public abstract class User
    {
        //Each user has an id,usertype,name,password,email,phone and address to be stored in the .txt file
        public int ID { get; protected set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public User(string name, string password, string email, string phone, string address)
        {
            this.Name = name;
            this.Password = password;
            this.Email = email;
            this.Phone = phone;
            this.Address = address;
        }

        protected int GenerateID(int rolePrefix, string filePath)
        {
            int lastAssignedID = FileManager.ReadLastAssignedID(filePath);
            int newID = lastAssignedID + 1;

            File.AppendAllText(filePath, $"{newID},{Name},{Password}, {Email},{Phone},{Address}\n");

            return newID;
        }

    }
}

