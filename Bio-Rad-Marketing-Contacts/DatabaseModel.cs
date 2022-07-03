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
            string sqlQuery = @$"INSERT INTO dbo.Vendors (Name, Company, PhoneNumber, Address, CreatedOn) 
                              VALUES ('{name}', '{company}', '{phone_number}', '{address}', '{createdOn}')";
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public static List<Vendor> GetVendors()
        {
            List<Vendor> result = new List<Vendor>();

            string sqlString = @"
                SELECT Name, Company, PhoneNumber, Address, CreatedOn 
                FROM Vendors
                ORDER BY Id DESC";
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand(sqlString, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var vendorName = $"{reader[0]}";
                        var vendorCompany = $"{reader[1]}";
                        var vendorPhoneNumber = $"{reader[2]}";
                        var vendorAddress = $"{reader[3]}";
                        var vendorCreatedOn = Convert.ToDateTime($"{reader[4]}");

                        var vendor = new Vendor(vendorName, vendorCompany,
                            vendorPhoneNumber, vendorAddress, vendorCreatedOn);

                        result.Insert(0, vendor);
                    }
                }
            }

            return result;
        }
    }
}
