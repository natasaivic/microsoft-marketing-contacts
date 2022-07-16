using System;

namespace Microsoft_Marketing_Contacts
{
    public class Vendor
    {
        public string Name { get; set; }

        public string Company { get; set; }
        
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public DateTime CreatedOn { get; set; }

        public Vendor(string name, string company, string phoneNumber, string address, DateTime createdOn)
        {
            Name = name;
            Company = company;
            PhoneNumber = phoneNumber;
            Address = address;
            CreatedOn = createdOn;
        }
    }
}
