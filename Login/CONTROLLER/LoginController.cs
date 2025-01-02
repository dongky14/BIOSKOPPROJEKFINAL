using Login.MODEL;
using Login.MODEL.CONTEXT;
using Login.MODEL.REPOSITORY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
namespace Login.CONTROLLER
{
    public class LoginController
    {
        private LoginRepository _repository;

        // Constructor accepts ApplicationDbContext as parameter
        public LoginController(ApplicationDbContext context)
        {
            _repository = new LoginRepository(context);
        }

  
        public bool ValidateLogin(string username, string password)
        {
            try
            {
                // Check if username and password are not empty
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Username or Password cannot be empty!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                // Call repository to validate login
                bool isValid = _repository.ValidateLogin(username, password);

                if (isValid)
                {
                    MessageBox.Show("Login successful!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                return isValid;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool RegisterUser(string Username, string Password, string Name, string Phone, string DOB)
        {
            try
            {
                // Validate user input
                if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password) ||
                    string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(DOB))
                {
                    MessageBox.Show("All fields are required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                // Create User object
                User newUser = new User
                {
                    Username = Username,
                    Password = Password,
                    Name = Name,
                    Phone = Phone,
                    DOB = DOB
                };

                // Call repository to register user
                int result = _repository.RegisterUser(newUser);

                if (result > 0)
                {
                    MessageBox.Show("User successfully registered!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to register user!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                return result > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public List<User> GetUserData(string username = null)
        {
            return _repository.GetUserData(username);
        }

        public bool UpdateUser(string Username, string Password, string Name, string Phone, string DOB)
        {
            try
            {
                // Validate user input
                if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password) ||
                    string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(DOB))
                {
                    MessageBox.Show("All fields are required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                // Create User object
                User updatedUser = new User
                {
                    Username = Username,
                    Password = Password,
                    Name = Name,
                    Phone = Phone,
                    DOB = DOB
                };

                // Call repository to update user
                int result = _repository.UpdateUser(updatedUser);

                if (result > 0)
                {
                    MessageBox.Show("User data updated successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to update user data!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                return result > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

  
        public bool DeleteUser(string Username)
        {
            try
            {
                // Validate username
                if (string.IsNullOrEmpty(Username))
                {
                    MessageBox.Show("Username cannot be empty!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                // Call repository to delete user
                int result = _repository.DeleteUser(Username);

                if (result > 0)
                {
                    MessageBox.Show("User deleted successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to delete user!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                return result > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
