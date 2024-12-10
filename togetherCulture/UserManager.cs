using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace togetherCulture
{
    internal class UserManager
    {
        private readonly DBConnection _dbConnection;

        // Constructor
        public UserManager()
        {
            _dbConnection = DBConnection.getConnectionInstance();
        }

        // Method to Sign Up a New User
        public bool Signup(string username, string password, string email)
        {
            try
            {
                string hashedPassword = HashPassword(password);

                string query = "INSERT INTO users (Username, Email, Password, RoleID, JoinDate) VALUES (@Username, @Email, @Password, @RoleID, @JoinDate)";
                SqlParameter[] parameters = {
                    new SqlParameter("@Username", username),
                    new SqlParameter("@Email", email),
                    new SqlParameter("@Password", hashedPassword),
                    new SqlParameter("@RoleID", 1), 
                    new SqlParameter("@JoinDate", DateTime.Now)
                };

                int rowsInserted = _dbConnection.executeNonQuery(query, parameters);
                return rowsInserted > 0; // True if insertion is successful
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Signup Error: {ex.Message}");
                return false;
            }
        }

        // Method to Log In a User
        public bool Login(string username, string password)
        {
            try
            {
                string query = "SELECT Password FROM users WHERE Username = @Username";
                SqlParameter[] parameters = {
                    new SqlParameter("@Username", username)
                };

                object result = _dbConnection.executeScalar(query, parameters);

                if (result != null)
                {
                    string storedHash = result.ToString();
                    string enteredHash = HashPassword(password);

                    if (storedHash == enteredHash)
                    {
                        Globals.CurrentLoggedInUser = username;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false; // User not found or password mismatch
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login Error: {ex.Message}");
                return false;
            }
        }


        // Method to Log Out a User
        public void Logout()
        {
            Globals.CurrentLoggedInUser = "";
        }

        // Method to Delete a User
        public bool DeleteUser(string username)
        {
            try
            {
                string query = "DELETE FROM users WHERE Username = @Username";
                SqlParameter[] parameters = {
                    new SqlParameter("@Username", username)
                };

                int rowsDeleted = _dbConnection.executeNonQuery(query, parameters);
                return rowsDeleted > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DeleteUser Error: {ex.Message}");
                return false;
            }
        }

        // Method to Update last vist info of the loggedIn user 
        public void UpdateLastVisitInfo()
        {
            // update lastVisitDate field
            try
            {
                string query = "UPDATE users SET LastVisitDate = @LastVisitDate";
                SqlParameter[] parameters = {
                    new SqlParameter("@LastVisitDate", DateTime.Now),
                };

                _dbConnection.executeNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UpdateLastVisit Error: {ex.Message}");
            }

            // update TotalVisits field
            try
            {
                string query = "UPDATE users SET TotalVisits = TotalVisits + 1 WHERE Username = @Username";
                SqlParameter[] parameters = {
                    new SqlParameter("@Username", Globals.CurrentLoggedInUser)
                };

                object result = _dbConnection.executeScalar(query, parameters);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"UpdateLastVisit Error: {ex.Message}");
            }
        }

        // Method to Retrieve User Details
        public DataTable GetUserDetails(string username)
        {
            try
            {
                string query = "SELECT Username, Email FROM users WHERE Username = @Username";
                SqlParameter[] parameters = {
                    new SqlParameter("@Username", username)
                };

                return _dbConnection.executeQuery(query, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetUserDetails Error: {ex.Message}");
                return null;
            }
        }

        // Method to Get All Users
        public DataTable GetAllUsers()
        {
            try
            {
                string query = "SELECT Username, Email FROM users";
                return _dbConnection.executeQuery(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetAllUsers Error: {ex.Message}");
                return null;
            }
        }

        // Private Method to Hash Passwords
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
