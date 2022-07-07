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
using System.Windows.Shapes;

namespace Bio_Rad_Marketing_Contacts
{
    public partial class VendorCodeWindow : Window
    {
        public string? VendorCode { get; set; }

        public VendorCodeWindow()
        {
            InitializeComponent();
        }

        private void Btn_Vendor_Code_Cancel_Click(object sender, RoutedEventArgs e)
        {
            VendorCode = null;
            Close();
        }

        private void Btn_Vendor_Code_Save_Click(object sender, RoutedEventArgs e)
        {
            VendorCode = TextBox_Vendor_Code.Text;
            if (VendorCode.Length != 4) {
                MessageBox.Show("Vendor code must be 4 characters long (e.g. A001)");
                return;
            }

            if (!Char.IsLetter(VendorCode[0])) {
                MessageBox.Show("Vendor code must start with a letter (e.g. A001)");
                return;
            }

            if (!Char.IsNumber(VendorCode[1]) || !Char.IsNumber(VendorCode[2]) || !Char.IsNumber(VendorCode[3]))
            {
                MessageBox.Show("Vendor code must have three last characters as numbers (e.g. A001)");
                return;
            }

            if (DatabaseModel.CheckIfVenorCodeExists(VendorCode)) {
                MessageBox.Show("Vendor code already exists in database, use unique vendor code.");
                return;
            }

            Close();
        }
    }
}
