﻿using System;

namespace Bio_Rad_Marketing_Contacts
{
    public class Vendor
    {
        public string Name { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedOn { get; set; }

        public Vendor(string name, string company, string address, string phoneNumber, DateTime createdOn)
        {
            Name = name;
            Company = company;
            Address = address;
            PhoneNumber = phoneNumber;
            CreatedOn = createdOn;
        }
    }
}