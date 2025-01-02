using System;
using System.Collections.Generic;
using System.Data.SQLite; // Ganti SqlClient dengan SQLite karena menggunakan SQLite
using System.Security.Cryptography;
using System.Text;
using Login.MODEL.CONTEXT;
using Login.MODEL.ENTITY; // Ensure this namespace contains User entity model

namespace Login.MODEL.REPOSITORY
{
    public class LoginRepository
    {
        // Declare the connection object
        private SQLiteConnection _conn;

        // Constructor that accepts DbContext (ApplicationDbContext)
        public LoginRepository(ApplicationDbContext context)
        {
            // Initialize the connection object
            _conn = context.Conn;
        }

        // Encrypt password with a basic method (replace with stronger encryption if needed)
        private string EncryptPassword(string password)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                // Generate a random salt
                byte[] salt = new byte[16];
                rng.GetBytes(salt);

                // Use PBKDF2 to hash the password with the salt
                using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
                {
                    byte[] hash = pbkdf2.GetBytes(20);
                    byte[] hashBytes = new byte[36];
                    Array.Copy(salt, 0, hashBytes, 0, 16);
                    Array.Copy(hash, 0, hashBytes, 16, 20);
                    return Convert.ToBase64String(hashBytes);
                }
            }
        }


        // Register a new user in the database
        public int RegisterUser(User user)
        {
            int result = 0;

            try
            {
                string encryptedPassword = EncryptPassword(user.Password);

                // Declare SQL query for inserting a new user record
                string sql = @"INSERT INTO Login (Username, Password, Name, Phone, DOB)
                               VALUES (@Username, @Password, @Name, @Phone, @DOB)";

                // Create the command object inside a using block
                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    // Register parameters and set their values
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Password", encryptedPassword);
                    cmd.Parameters.AddWithValue("@Name", user.Name);
                    cmd.Parameters.AddWithValue("@Phone", user.Phone);
                    cmd.Parameters.AddWithValue("@DOB", user.DOB);

                    // Execute the INSERT command and store the result
                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("RegisterUser error: {0}", ex.Message);
            }

            return result;
        }

        // Validate login
        public bool ValidateLogin(string username, string password)
        {
            try
            {
                string encryptedPassword = EncryptPassword(password);

                // Declare SQL query to validate the user
                string sql = @"SELECT COUNT(*) FROM Login WHERE Username = @Username AND Password = @Password";

                // Create the command object inside a using block
                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    // Register parameters and set their values
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", encryptedPassword);

                    // Execute the SELECT command and check the result
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ValidateLogin error: {0}", ex.Message);
                return false;
            }
        }

        // Get user details
        // Get all users or a specific user
        public List<User> GetUserData(string username = null)
        {
            var users = new List<User>();

            try
            {
                string sql = string.IsNullOrEmpty(username)
                    ? "SELECT * FROM Login"
                    : "SELECT * FROM Login WHERE Username = @Username";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    if (!string.IsNullOrEmpty(username))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                    }

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new User
                            {
                                Username = reader["Username"].ToString(),
                                Password = reader["Password"].ToString(),
                                Name = reader["Name"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                DOB = reader["DOB"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("GetUserData error: {0}", ex.Message);
            }

            return users;
        }


        // Update user data
        public int UpdateUser(User user)
        {
            int result = 0;

            try
            {
                string encryptedPassword = EncryptPassword(user.Password);

                // Declare SQL query for updating user data
                string sql = @"UPDATE Login SET  Username = @Username, Password = @Password, Name = @Name, Phone = @Phone, DOB = @DOB WHERE Username = @Username";

                // Create the command object inside a using block
                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    // Register parameters and set their values
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Password", encryptedPassword);
                    cmd.Parameters.AddWithValue("@Name", user.Name);
                    cmd.Parameters.AddWithValue("@Phone", user.Phone);
                    cmd.Parameters.AddWithValue("@DOB", user.DOB);

                    // Execute the UPDATE command and store the result
                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("UpdateUser error: {0}", ex.Message);
            }

            return result;
        }

        // Delete user from database
        public int DeleteUser(string username)
        {
            int result = 0;

            try
            {
                // Declare SQL query for deleting a user record
                string sql = @"DELETE FROM Login WHERE Username = @Username";

                // Create the command object inside a using block
                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    // Register parameter and set its value
                    cmd.Parameters.AddWithValue("@Username", username);

                    // Execute the DELETE command and store the result
                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("DeleteUser error: {0}", ex.Message);
            }

            return result;
        }
    }
}
