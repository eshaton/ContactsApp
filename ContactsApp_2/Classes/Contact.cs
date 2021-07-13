using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ContactsApp_2.Classes
{
    public class Contact
    {
        // sqlite atributi
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
