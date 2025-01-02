using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace Login.MODEL.REPOSITORY
{
    public class LoginRepository
    {
        private string connectionString = @"D:\PEMROGRAMAN LANJUT\UTS\New folder\BIOSKOP\Database\DbBioskop.db"; // Database connection string.

        // Encrypt password with a basic method (replace with stronger encryption if needed)
        private string EncryptPassword(string password)
        {
            var salt = "*"; // A simple salt or encryption key
            var combined = password + salt;
            using (var sha256 = SHA256.Create())
            {
                byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(combined));
                return Convert.ToBase64String(hash);
            }
        }

        // Register a new user in the database
        public bool RegisterUser(string username, string password, string name, string phone, string dob)
        {
            try
            {
                string encryptedPassword = EncryptPassword(password);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Users (Username, Password, Name, Phone, DOB) VALUES (@Username, @Password, @Name, @Phone, @DOB)";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", encryptedPassword);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Phone", phone);
                    cmd.Parameters.AddWithValue("@DOB", dob);

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        // Validate login
        public bool ValidateLogin(string username, string password)
        {
            try
            {
                string encryptedPassword = EncryptPassword(password);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", encryptedPassword);

                    connection.Open();
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        // Get user details
        public User GetUserData(string username)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT Username, Password, Name, Phone, DOB FROM Users WHERE Username = @Username";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Username", username);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        return new User
                        {
                            Username = reader["Username"].ToString(),
                            Password = reader["Password"].ToString(),
                            Name = reader["Name"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            DOB = reader["DOB"].ToString()
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"Error: {ex.Message}");
            }

            return null;
        }

        // Update user data
        public bool UpdateUser(string username, string password, string name, string phone, string dob)
        {
            try
            {
                string encryptedPassword = EncryptPassword(password);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Users SET Password = @Password, Name = @Name, Phone = @Phone, DOB = @DOB WHERE Username = @Username";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", encryptedPassword);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Phone", phone);
                    cmd.Parameters.AddWithValue("@DOB", dob);

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        // Delete user from database
        public bool DeleteUser(string username)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Users WHERE Username = @Username";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Username", username);

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}
