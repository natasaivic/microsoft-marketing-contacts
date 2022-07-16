using System;
using System.Windows;
using System.Text.RegularExpressions;
using System.Windows.Threading;

namespace Microsoft_Marketing_Contacts
{
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            InitializeDataGrids();
            InitializeClock();
        }

        private void InitializeDataGrids()
        {
            DataGrid_Customers.ItemsSource = DatabaseModel.GetCustomers();
            DataGrid_Vendors.ItemsSource = DatabaseModel.GetVendors();
            DataGrid_MasterList.ItemsSource = DatabaseModel.GetMasterListVendors();
            DataGrid_All_Contacts.ItemsSource = DatabaseModel.GetAllContacts();
        }

        private void InitializeClock()
        {
            Clock_Tick(null, null);

            DispatcherTimer clock = new DispatcherTimer();
            clock.Interval = TimeSpan.FromSeconds(1);
            clock.Tick += Clock_Tick;
            clock.Start();
        }

        void Clock_Tick(object? sender, EventArgs? e)
        {
            Label_Time.Content = DateTime.Now.ToString("HH:mm:ss");
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

            if (String.IsNullOrEmpty(address))
            {
                MessageBox.Show("Address cannot be empty");
                return;
            }

            // Save data in DB
            DatabaseModel.SaveCustomer(name, company, phone, address, notes);

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
            var vendorCode = DatabaseModel.FindVendorCodeInMasterList(company);
            if (vendorCode is null)
            {
                var vendorCodeWindow = new VendorCodeWindow();
                vendorCodeWindow.ShowDialog();

                // if we click on cancel vendorCodeWindow.VendorCode will be null
                if (vendorCodeWindow.VendorCode is null) {
                    return;
                }

                DatabaseModel.AddVendorCompanyToMasterList(company, vendorCodeWindow.VendorCode);
                RefreshMasterListDataGrid();
            }

            // Save data in DB.
            DatabaseModel.SaveVendor(name, company, address, phone);

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
            var regex = @"^[0-9]{3}(-| )?[0-9]{3}(-| )?[0-9]{4}$";
            var result = Regex.Match(phone, regex);
            return result.Success;      
        }

        private void Button_Clear_Customer_Entry_Click(object sender, RoutedEventArgs e)
        {
            ClearCustomerForm();
        }

        private void Button_Clear_Vendor_Entry_Click(object sender, RoutedEventArgs e)
        {
            ClearVendorForm();
        }
    }
}
