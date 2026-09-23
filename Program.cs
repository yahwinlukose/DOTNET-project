// Define the namespace for the application
namespace MusicPlayer;

// Declare a static class for the application entry point
static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    // Attribute to indicate that the COM threading model for an application is single-threaded apartment (STA)
    [STAThread]
    // Define the main entry point method
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        // Initialize the application configuration
        ApplicationConfiguration.Initialize();
        // Run the main form of the application
        Application.Run(new Form1());
    }    
}