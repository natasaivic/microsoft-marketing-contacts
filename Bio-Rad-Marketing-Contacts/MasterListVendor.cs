using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bio_Rad_Marketing_Contacts
{
    public class MasterListVendor
    {
        public string CompanyName { get; set; }
        public string VendorCode { get; set; }


        public MasterListVendor(string name, string code)
        {
            CompanyName = name;
            VendorCode = code;
        }
    }
}
