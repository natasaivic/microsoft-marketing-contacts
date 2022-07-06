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
        public string? Code { get; set; }

        public VendorCodeWindow()
        {
            InitializeComponent();
        }

        private void Btn_Vendor_Code_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Btn_Vendor_Code_Save_Click(object sender, RoutedEventArgs e)
        {
            Code = TextBox_Vendor_Code.Text;
            Close();
        }
    }
}
