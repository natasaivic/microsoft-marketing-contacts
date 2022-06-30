using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;


namespace Bio_Rad_Marketing_Contacts
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

    public class DatabaseModel
    {
        public static void SaveCustomer(string name, string company, string phone_number, string address, string notes, DateTime createdOn)
        {
            // string sqlQuery = @$"INSERT INTO customers (name, company, phone_number, address, notes, created_on) 
            //                    VALUES ('{name}', '{company}', '{phone_number}', '{address}', '{notes}', '{DateTime.Now}')";
            // MessageBox.Show(sqlQuery);
        }

        public static List<Customer> GetCustomers()
        {
            //string sqlQuery = "SELECT name, company, phone_number, address, notes, created_on FROM customers";
            List<Customer> result = new List<Customer>();
            result.Insert(0, new Customer("Natasa Ivic", "Bio-Rad", "123-456-7890", "12345 Bio-Rad Benicia", "No notes", DateTime.Now));

            return result;
        }

        public static void SaveVendor(string name, string company, string address, string phone_number, DateTime createdOn)
        {
            string sqlQuery = $@"INSERT INTO vendors (name, company, address, phone_number, created_on)
                                VALUES ('{name}', '{company}', '{address}', '{phone_number}', '{DateTime.Now}')";
            MessageBox.Show(sqlQuery);
        }
        public static List<Vendor> GetVendors()
        {
            // string sqlQuery = "SELECT name, company, address, phone_number, created_on";
            List<Vendor> result = new List<Vendor>();
            result.Insert(0, new Vendor("John Smith", "Microsoft", "94301 Palo Alto", "111-222-3334", DateTime.Now));

            return result;
        }
    }


    public partial class MainWindow : Window
    {
        private List<Customer> customers;
        private List<Vendor> vendors;

        public MainWindow()
        {
            InitializeComponent();
            
            customers = DatabaseModel.GetCustomers();
            DataGrid_Customers.ItemsSource = customers;
            vendors = DatabaseModel.GetVendors();
            DataGrid_Vendors.ItemsSource = vendors;
        }

        private void Add_Customer_Click(object sender, RoutedEventArgs e)
        {
            var name = TextBox_Customer_Name.Text;
            var company = TextBox_Customer_Company.Text;
            var phone = TextBox_Customer_PhoneNumber.Text;
            var address = TextBox_Customer_Address.Text;
            var notes = TextBox_Customer_Notes.Text;

            // Input validations
            if (String.IsNullOrEmpty(name)) {
                MessageBox.Show("Name cannot be empty");
                return;
            }

            if (String.IsNullOrEmpty(company))
            {
                MessageBox.Show("Company cannot be empty");
                return;
            }

            if (!isValidPhoneNumber(phone)) // validate if it's a number or not?
            {
                MessageBox.Show("Phone number is not valid");
                return;
            }

            if (String.IsNullOrEmpty(address))
            {
                MessageBox.Show("Address cannot be empty");
                return;
            }

            // Save data in DB
            DatabaseModel.SaveCustomer(name, company, phone, address, notes, DateTime.Now);

            // Since there is no DB save yet, let's add to this.customers so we can see it in the grid
            var currenCustomer = new Customer(name, company, phone, address, notes, DateTime.Now);
            customers.Insert(0, currenCustomer);

            // reload data grid
            RefreshDataGrid();
            ClearForm();
        }

        private void RefreshDataGrid() {
            // load from database
            // customers = DatabaseModel.GetCustomers();
            DataGrid_Customers.ItemsSource = null;
            DataGrid_Customers.ItemsSource = customers;
        }

        private void ClearForm() {
            TextBox_Customer_Name.Clear();
            TextBox_Customer_Company.Clear();
            TextBox_Customer_PhoneNumber.Clear();
            TextBox_Customer_Address.Clear();
            TextBox_Customer_Notes.Clear();
        }

        private bool isValidPhoneNumber(string phone)
        {
            if (String.IsNullOrEmpty(phone))
            {
                return false;
            }

            return true;
        }

        private void Add_Vendor_Click(object sender, RoutedEventArgs e)
        {
            var name = TextBox_Vendor_Name.Text;
            var company = TextBox_Vendor_Company.Text;
            var address = TextBox_Vendor_Address.Text;
            var phone = TextBox_Vendor_Phone_Number.Text;

            // input validation
            if (String.IsNullOrEmpty(name)) {
                MessageBox.Show("Name cannot be empty.");
                return;
            }
            if (String.IsNullOrEmpty(company))
            {
                MessageBox.Show("Company cannot be empty.");
                return;
            }
            if (String.IsNullOrEmpty(address)) {
                MessageBox.Show("Address cannot be empty.");
                return;
            }
            if (String.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Phone number cannot be empty.");
                return;
            }


            // Save data in DB.
            DatabaseModel.SaveVendor(name, company, address, phone, DateTime.Now);

            var currentVendor = new Vendor(name, company, address, phone, DateTime.Now);
            vendors.Insert(0, currentVendor);

            // reload data grid
            RefreshDataGrid_v();
            ClearForm_v();

            
            }
        private void RefreshDataGrid_v()
        {
            DataGrid_Vendors.ItemsSource = null;
            DataGrid_Vendors.ItemsSource = vendors;
        }
        private void ClearForm_v() {
            TextBox_Vendor_Name.Clear();
            TextBox_Vendor_Company.Clear();
            TextBox_Vendor_Address.Clear();
            TextBox_Vendor_Phone_Number.Clear();
        }
    }
}
