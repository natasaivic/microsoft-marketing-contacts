using System;
using System.Collections.Generic;
using System.Windows;
using System.Text.RegularExpressions;
using System.Windows.Threading;

namespace Bio_Rad_Marketing_Contacts
{
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();

            DataGrid_Customers.ItemsSource = DatabaseModel.GetCustomers();
            DataGrid_Vendors.ItemsSource = DatabaseModel.GetVendors();
            DataGrid_MasterList.ItemsSource = DatabaseModel.GetMasterListVendors();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object? sender, EventArgs e)
        {
            lblTime.Content = DateTime.Now.ToString("HH:mm:ss");
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

            if (!isValidPhoneNumber(phone))
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
            //var currenCustomer = new Customer(name, company, phone, address, notes, DateTime.Now);
            // customers.Insert(0, currenCustomer);

            // reload data grid
            RefreshCustomerDataGrid();
            ClearCustomerForm();
        }

        private void RefreshCustomerDataGrid() {
            // reset and load from database
            DataGrid_Customers.ItemsSource = null;
            DataGrid_Customers.ItemsSource = DatabaseModel.GetCustomers();
        }

        private void ClearCustomerForm() {
            TextBox_Customer_Name.Clear();
            TextBox_Customer_Company.Clear();
            TextBox_Customer_PhoneNumber.Clear();
            TextBox_Customer_Address.Clear();
            TextBox_Customer_Notes.Clear();
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
            if (!isValidPhoneNumber(phone))
            {
                MessageBox.Show("Phone number is not valid");
                return;
            }

            // check master list
            var vendorCode = DatabaseModel.findVendorCodeInMasterList(company);
            if (vendorCode is null)
            {
                while (true)
                {
                    var vendorCodeWindow = new VendorCodeWindow();
                    vendorCodeWindow.ShowDialog();

                    if (vendorCodeWindow.Code is null) {
                        return;
                    }
                    var success = DatabaseModel.addVendorCompanyToMasterList(company, vendorCodeWindow.Code);
                    if (success)
                    {
                        RefreshMasterListDataGrid();
                        break;
                    }
                    else
                    {
                        MessageBox.Show("Invalid vendor code, please use another one");
                    }
                }
            }

            // Save data in DB.
            DatabaseModel.SaveVendor(name, company, address, phone, DateTime.Now);

            // reload data grid
            RefreshVendorDataGrid();
            ClearVendorForm();
        }

        private void RefreshVendorDataGrid()
        {
            DataGrid_Vendors.ItemsSource = null;
            DataGrid_Vendors.ItemsSource = DatabaseModel.GetVendors();
        }

        private void ClearVendorForm() {
            TextBox_Vendor_Name.Clear();
            TextBox_Vendor_Company.Clear();
            TextBox_Vendor_Address.Clear();
            TextBox_Vendor_Phone_Number.Clear();
        }

        private void RefreshMasterListDataGrid()
        {
            DataGrid_MasterList.ItemsSource = null;
            DataGrid_MasterList.ItemsSource = DatabaseModel.GetMasterListVendors();
        }

        private bool isValidPhoneNumber(string phone)
        {
            if (String.IsNullOrEmpty(phone))
            {
                return false;
            }

            var regex = @"^[0-9]{3}(-| )?[0-9]{3}(-| )?[0-9]{4}$";
            var result = Regex.Match(phone, regex);
            if (!result.Success)
            {
                return false;
            }

            return true;
        }
    }
}
