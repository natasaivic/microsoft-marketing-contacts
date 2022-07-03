using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;

namespace Bio_Rad_Marketing_Contacts
{
    public class DatabaseModel
    {
        private static string connString = @$"
                Data Source=(LocalDB)\MSSQLLocalDB;
                AttachDbFilename=C:\Users\popov\source\repos\bio-rad-marketing-contacts\Bio-Rad-Marketing-Contacts\DB\Contacts.mdf;
                Integrated Security=True";

        public static void SaveCustomer(string name, string company, string phone, string address, string notes, DateTime createdOn)
        {
            string queryString = @$"INSERT INTO dbo.Customers (Name, Company, PhoneNumber, Address, Note, CreatedOn) 
                              VALUES ('{name}', '{company}', '{phone}', '{address}', '{notes}', '{createdOn}')";
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public static List<Customer> GetCustomers()
        {
            List<Customer> result = new List<Customer>();

            string queryString = @"
                SELECT Name, Company, PhoneNumber, Address, Note, CreatedOn 
                FROM dbo.Customers 
                ORDER BY Id DESC";
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var customerName = $"{reader[0]}";
                        var customerCompany = $"{reader[1]}";
                        var customerPhoneNumber = $"{reader[2]}";
                        var customerAddress = $"{reader[3]}";
                        var customerNote = $"{reader[4]}";
                        var customerCreatedOn = Convert.ToDateTime($"{reader[5]}");

                        var customer = new Customer(customerName, customerCompany,
                            customerPhoneNumber, customerAddress, customerNote, customerCreatedOn);

                        result.Insert(0, customer);
                    }
                }
            }

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
}
