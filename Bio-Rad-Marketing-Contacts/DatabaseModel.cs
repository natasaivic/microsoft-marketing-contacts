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
        private static string projectDir = Path.GetFullPath(Directory.GetCurrentDirectory() + @"\..\..\..");
        private static string connString = @$"
                Data Source=(LocalDB)\MSSQLLocalDB;
                AttachDbFilename={projectDir}\DatabaseFiles\Contacts.mdf;
                Integrated Security=True";

        public static void SaveCustomer(string name, string company, string phone, string address, string notes)
        {
            string queryString = @$"INSERT INTO dbo.Customers (Name, Company, PhoneNumber, Address, Note, CreatedOn) 
                              VALUES (@name, @company, @phone, @address, @notes, '{DateTime.Now}')";
            try
            {
                SqlConnection connection = new SqlConnection(connString);
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlParameter nameParam = new SqlParameter("@name", SqlDbType.NVarChar, 50);
                nameParam.Value = name;
                command.Parameters.Add(nameParam);

                SqlParameter companyParam = new SqlParameter("@company", SqlDbType.NVarChar, 50);
                companyParam.Value = company;
                command.Parameters.Add(companyParam);

                SqlParameter phoneParam = new SqlParameter("@phone", SqlDbType.NVarChar, 12);
                phoneParam.Value = phone;
                command.Parameters.Add(phoneParam);

                SqlParameter addressParam = new SqlParameter("@address", SqlDbType.NVarChar, 100);
                addressParam.Value = address;
                command.Parameters.Add(addressParam);

                SqlParameter noteParam = new SqlParameter("@notes", SqlDbType.NText);
                noteParam.Value = notes;
                command.Parameters.Add(noteParam);

                command.Prepare();
                command.ExecuteNonQuery();
            } catch (SqlException ex) {
                MessageBox.Show($"Error while saving Customer to database: {ex.Message}");
            }
        }

        public static List<Customer> GetCustomers()
        {
            List<Customer> result = new List<Customer>();

            string queryString = @"
                SELECT Name, Company, PhoneNumber, Address, Note, CreatedOn 
                FROM dbo.Customers 
                ORDER BY Id DESC";

            try
            {
                SqlConnection connection = new SqlConnection(connString);
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
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
            catch (SqlException ex) {
                MessageBox.Show($"Error while loading Customer list from database: {ex.Message}");
            }

            return result;
        }

        public static void SaveVendor(string name, string company, string address, string phone)
        {
            string sqlQuery = @$"INSERT INTO dbo.Vendors (Name, Company, PhoneNumber, Address, CreatedOn) 
                              VALUES (@name, @company, @phone, @address, '{DateTime.Now}')";
            try
            {
                SqlConnection connection = new SqlConnection(connString);
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Connection.Open();

                SqlParameter nameParam = new SqlParameter("@name", SqlDbType.NVarChar, 50);
                nameParam.Value = name;
                command.Parameters.Add(nameParam);

                SqlParameter companyParam = new SqlParameter("@company", SqlDbType.NVarChar, 50);
                companyParam.Value = company;
                command.Parameters.Add(companyParam);

                SqlParameter phoneParam = new SqlParameter("@phone", SqlDbType.NVarChar, 12);
                phoneParam.Value = phone;
                command.Parameters.Add(phoneParam);

                SqlParameter addressParam = new SqlParameter("@address", SqlDbType.NVarChar, 100);
                addressParam.Value = address;
                command.Parameters.Add(addressParam);

                command.Prepare();
                command.ExecuteNonQuery();
            } catch (SqlException ex) {
                MessageBox.Show($"Error while saving Vendor to database: {ex.Message}");
            }
        }

        public static List<Vendor> GetVendors()
        {
            List<Vendor> result = new List<Vendor>();

            string sqlString = @"
                SELECT Name, Company, PhoneNumber, Address, CreatedOn 
                FROM dbo.Vendors
                ORDER BY Id DESC";

            try
            {
                SqlConnection connection = new SqlConnection(connString);
                SqlCommand command = new SqlCommand(sqlString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var vendorName = reader.GetString(0);
                    var vendorCompany = reader.GetString(1);
                    var vendorPhoneNumber = reader.GetString(2);
                    var vendorAddress = reader.GetString(3);
                    var vendorCreatedOn = reader.GetDateTime(4);

                    var vendor = new Vendor(vendorName, vendorCompany,
                        vendorPhoneNumber, vendorAddress, vendorCreatedOn);

                    result.Insert(0, vendor);
                }
            }
            catch (SqlException ex) {
                MessageBox.Show($"SQL error while loading vendor list {ex.Message}");
            }
            catch (Exception)
            {
                MessageBox.Show("Error while loading vendor list from database, the list will be empty");
            }

            return result;
        }

        public static string? FindVendorCodeInMasterList(string company) {
            string sqlString = @"
                SELECT VendorCode
                FROM dbo.MasterList
                WHERE CompanyName = @company";

            try
            {
                SqlConnection connection = new SqlConnection(connString);
                SqlCommand command = new SqlCommand(sqlString, connection);
                connection.Open();

                SqlParameter companyParam = new SqlParameter("@company", SqlDbType.NVarChar, 50);
                companyParam.Value = company;
                command.Parameters.Add(companyParam);
                command.Prepare();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    return Convert.ToString(reader[0]);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"SQL error while loading vendor list {ex.Message}");
            }

            return null;    
        }

        public static void AddVendorCompanyToMasterList(string company, string vendorCode) {
            string sqlQuery = @"
                INSERT INTO dbo.MasterList (CompanyName, VendorCode) 
                VALUES (@company, @vendorCode)";
            try
            {
                SqlConnection connection = new SqlConnection(connString);           
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();

                SqlParameter companyParam = new SqlParameter("@company", SqlDbType.NVarChar, 50);
                companyParam.Value = company;
                command.Parameters.Add(companyParam);

                SqlParameter vendorCodeParam = new SqlParameter("@vendorCode", SqlDbType.NVarChar, 4);
                vendorCodeParam.Value = vendorCode;
                command.Parameters.Add(vendorCodeParam);

                command.Prepare();
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error while adding new Vendor. Invalid SQL. {ex.Message}");
                System.Windows.Application.Current.Shutdown();
            }
        }

        public static List<MasterListVendor> GetMasterListVendors()
        {
            List<MasterListVendor> result = new List<MasterListVendor>();

            string sqlString = @"
                SELECT CompanyName, VendorCode 
                FROM dbo.MasterList
                ORDER BY Id DESC";
            try
            {
                SqlConnection connection = new SqlConnection(connString);
                SqlCommand command = new SqlCommand(sqlString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var companyName = reader.GetString(0);
                    var vendorCode = reader.GetString(1);

                    var masterListVendor = new MasterListVendor(companyName, vendorCode);
                    result.Insert(0, masterListVendor);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error while adding new Vendor. Invalid SQL. {ex.Message}");
                System.Windows.Application.Current.Shutdown();
            }
            return result;
        }

        public static bool CheckIfVenorCodeExists(string code)
        {
            string sqlString = @"
                SELECT VendorCode 
                FROM dbo.MasterList
                WHERE VendorCode = @code";
            try
            {
                SqlConnection connection = new SqlConnection(connString);
                SqlCommand command = new SqlCommand(sqlString, connection);
                connection.Open();

                SqlParameter codeParam = new SqlParameter("@code", SqlDbType.NVarChar, 4);
                codeParam.Value = code;
                command.Parameters.Add(codeParam);

                SqlDataReader reader = command.ExecuteReader();
                return reader.HasRows;
            }
            catch (SqlException ex) {
                MessageBox.Show($"Database error while adding new Vendor. Invalid SQL. {ex.Message}");
                System.Windows.Application.Current.Shutdown();
            }
            return false;
        }
    }
}
