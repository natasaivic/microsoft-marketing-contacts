using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft_Marketing_Contacts
{
    public class Contact
    {
        public string Name { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public Contact(string name, string company, string phone, string address)
        {
            Name = name;
            Company = company;
            Phone = phone;
            Address = address;
        }
    }
}
