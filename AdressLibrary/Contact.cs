using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdressLibrary
{
    internal class Contact
    {
        private int id;
        private string name;
        private string number;
        private DateTime birthDate;

        public Contact(string name, string number, DateTime birthDate)
        {
            this.name = name;
            this.number = number;
            this.birthDate = birthDate;
        }
        public Contact() { }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Number { get => number; set => number = value; }
        public DateTime BirthDate { get => birthDate; set => birthDate = value; }
    }
}
