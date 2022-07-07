using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows;

namespace Bio_Rad_Marketing_Contacts
{
    public class DatabaseModel
    {
        private static string directory = Path.GetFullPath(Directory.GetCurrentDirectory() + @"\..\..\..");
        private static string connString = @$"
                Data Source=(LocalDB)\MSSQLLocalDB;
                AttachDbFilename={directory}\DB\Contacts.mdf;
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
                        var customerName = reader.GetString(0);
                        var customerCompany = reader.GetString(1);
                        var customerPhoneNumber = reader.GetString(2);
                        var customerAddress = reader.GetString(3);
                        var customerNote = "";
                        if (!reader.IsDBNull(4))
                        {
                            customerNote = reader.GetString(4);
                        }
                        var customerCreatedOn = reader.GetDateTime(5);

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
                FROM dbo.Vendors
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

        public static string? findVendorCodeInMasterList(string company) {
            string sqlString = @"
                SELECT VendorCode
                FROM dbo.MasterList
                WHERE CompanyName = @company";

            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand(sqlString, connection);
                connection.Open();

                SqlParameter companyParam = new SqlParameter("@company", SqlDbType.NVarChar, 50);
                companyParam.Value = company;
                command.Parameters.Add(companyParam);
                command.Prepare();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) {
                        reader.Read();
                        return Convert.ToString(reader[0]);
                    }
                    return null;
                }
            }
        }

        public static bool addVendorCompanyToMasterList(string company, string vendorCode) {
            string sqlQuery = @"
                INSERT INTO dbo.MasterList (CompanyName, VendorCode) 
                VALUES (@company, @vendorCode)";
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Connection.Open();
                
                SqlParameter companyParam = new SqlParameter("@company", SqlDbType.NVarChar, 50);
                companyParam.Value = company;
                command.Parameters.Add(companyParam);
                SqlParameter vendorCodeParam = new SqlParameter("@vendorCode", SqlDbType.NVarChar, 4);
                vendorCodeParam.Value = vendorCode;
                command.Parameters.Add(vendorCodeParam);

                command.Prepare();

                command.ExecuteNonQuery(); // error handling
                return true;
            }
        }

        public static List<MasterListVendor> GetMasterListVendors()
        {
            List<MasterListVendor> result = new List<MasterListVendor>();

            string sqlString = @"
                SELECT CompanyName, VendorCode 
                FROM dbo.MasterList
                ORDER BY Id DESC";
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand(sqlString, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var companyName = $"{reader[0]}";
                        var vendorCode = $"{reader[1]}";

                        var master_list_vendor = new MasterListVendor(companyName, vendorCode);

                        result.Insert(0, master_list_vendor);
                    }
                }
            }

            return result;
        }
    }
}
