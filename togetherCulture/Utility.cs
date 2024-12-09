using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace togetherCulture
{
    public static class Utility
    {
        // Hash a string using SHA256
        public static string HashString(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return Convert.ToBase64String(bytes);
            }
        }

        // Validate if a string is an email
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email; 
            }
            catch
            {
                return false;
            }
        }

        // Validate if a password meets the minimum length requirement
        public static bool IsValidPassword(string password, int minLength = 8)
        {
            return password.Length >= minLength;
        }

        // Validate if a string is alphanumeric
        public static bool IsAlphanumeric(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        // Show a dialog message
        public static void ShowDialogMessage(string message, string title = "Notification")
        {
            var icon = title.Equals("Error", StringComparison.OrdinalIgnoreCase)
                       ? MessageBoxIcon.Error
                       : MessageBoxIcon.Information;

            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }

        // Read file content
        public static string ReadFileContent(string filePath)
        {
            try
            {
                return File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read file: {ex.Message}");
                return null;
            }
        }

        // Write content to a file
        public static void WriteFileContent(string filePath, string content)
        {
            try
            {
                File.WriteAllText(filePath, content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write file: {ex.Message}");
            }
        }

        // Check if a string contains only letters
        public static bool IsAlphabetic(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsLetter(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
