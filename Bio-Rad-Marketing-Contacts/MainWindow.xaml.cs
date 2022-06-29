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
    }

    public partial class MainWindow : Window
    {
        private List<Customer> customers;

        public MainWindow()
        {
            InitializeComponent();
            
            customers = DatabaseModel.GetCustomers();
            DataGrid_Customers.ItemsSource = customers;
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
    }
}
