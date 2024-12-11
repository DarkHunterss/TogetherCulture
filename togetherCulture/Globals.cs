using System;
using System.IO;

namespace togetherCulture
{
    internal static class Globals
    {
        // global variables
        public static string AppName { get; set; } = "Together Culture";
        public static string Version { get; set; } = "1.0.0";

        public static int CurrentLoggedInUserID { get; set; } = 0;
        public static string CurrentLoggedInUsername { get; set; } = "";


        public static string ConnectionString { get; set; }



        // global constant
        public const string SupportEmail = "support@togetherculture.com";

        static Globals()
        {
            try
            {
                // Get the current base directory
                string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

                // Navigate to the project root directory
                DirectoryInfo directoryInfo = new DirectoryInfo(currentDirectory);
                while (directoryInfo != null && directoryInfo.Name != "togetherCulture")
                {
                    directoryInfo = directoryInfo.Parent;
                }

                if (directoryInfo != null)
                {
                    // Combine the directory with the .mdf file
                    string databaseFilePath = Path.Combine(directoryInfo.FullName, "together_culture.mdf");

                    // Validate the .mdf file existence
                    if (!File.Exists(databaseFilePath))
                    {
                        throw new FileNotFoundException("Database file not found: " + databaseFilePath);
                    }

                    // Define the connection string
                    ConnectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={databaseFilePath};Integrated Security=True;Connect Timeout=30";
                    Console.WriteLine("Connection String Initialized: " + ConnectionString);
                }
                else
                {
                    throw new DirectoryNotFoundException("Project root directory could not be found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing connection string: {ex.Message}");
            }
        }

        // global method
        public static void PrintAppInfo()
        {
            Console.WriteLine($"App Name: {AppName}");
            Console.WriteLine($"Version: {Version}");
            Console.WriteLine($"Support Email: {SupportEmail}");
        }
    }
}