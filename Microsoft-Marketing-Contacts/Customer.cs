using System;

namespace Microsoft_Marketing_Contacts
{
    public class Customer
    {
        public string Name { get; set; }
        public string Company { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public DateTime CreatedOn { get; set; }

        public Customer(string name, string company, string phoneNumber, string address, string note, DateTime createdOn)
        {
            Name = name;
            Company = company;
            PhoneNumber = phoneNumber;
            Address = address;
            Note = note;
            CreatedOn = createdOn;
        }
    }
}
