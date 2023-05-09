using cyber_project.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using static System.Net.Mime.MediaTypeNames;
using cyber_project.Services.Business;
using cyber_project.Services.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;
using cyber_project;
using System.Web.Security;

namespace cyber_project.Services.Data
{
        public class SecurityDAO
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Test;Integrated Security=True;Connect Timeout=30;Encrypt=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        RsaEncryption rsa = new RsaEncryption();
        internal bool FindByUser(UserModel user)
        {
            string privateKey = rsa.GetPrivateKey();
            string publicKey = rsa.GetPublicKey();
            bool success = false;
            string queryString = "Select password from dbo.Users where username = @Username";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                
                command.Parameters.Add("@Username", System.Data.SqlDbType.VarChar, 50).Value = user.Username;
                
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string password = reader.GetString(0); 
                            string encryptedPassword = rsa.Encrypt(password);
                            string decryptedPassword = rsa.Decrypt(encryptedPassword);
                            if (decryptedPassword.Equals(user.Password))
                            {
                                success = true;
                            }
                        }
                    }
                    else
                    {
                        success = false;
                    }
                    reader.Close();
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return success;
        }
    }

    public class RegisterDAO
    {
        public void RegisterData(RegisterModel rm)
        {
            RsaEncryption rsa = new RsaEncryption();
            string publicKey = rsa.GetPublicKey();
            string privateKey = rsa.GetPrivateKey();
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Test;Integrated Security=True;Connect Timeout=30;Encrypt=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            string query = "insert into dbo.Users values (@Username,@Email,@Password,@Enpassword)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command1 = new SqlCommand(query, connection);
                string enpassword = rsa.Encrypt(rm.Password);
                command1.Parameters.AddWithValue("@Username", rm.Username);
                command1.Parameters.AddWithValue("@Email", rm.Email);
                command1.Parameters.AddWithValue("@Password", rm.Password);
                command1.Parameters.AddWithValue("@Enpassword", enpassword);
                command1.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}