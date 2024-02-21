using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // Specify the folder path where the .exe file is located (replace "YourFolderPath" with the actual folder path)
        string folderPath = Path.Combine("C:\\Prince\\TestProcedures\\SelfTest-API2");

        // Print out the list of .exe files in the specified folder and its subfolders for debugging
        Console.WriteLine("Files in the specified folder and its subfolders:");
        var exeFiles = Directory.GetFiles(folderPath, "*.exe", SearchOption.AllDirectories);
        foreach (string file in exeFiles)
        {
            Console.WriteLine(file);
        }

        // Specify the name of the .exe file (replace "SelfTest-API.exe" with the actual filename)
        string exeFileName = "SelfTest-API.exe";

        // Find the .exe file in the list
        string exeFilePath = exeFiles.FirstOrDefault(filePath => Path.GetFileName(filePath).Equals(exeFileName, StringComparison.OrdinalIgnoreCase));

        // Check if the .exe file exists
        if (exeFilePath != null)
        {
            while (true)
            {
                // Get the command line input from the user
                Console.Write("Enter the command to run against the .exe (type 'exit' to end): ");
                string? command = Console.ReadLine();
                // Check if the user wants to exit
                if (command.Equals("exit", StringComparison.CurrentCultureIgnoreCase))
                {
                    break;
                }
                // Set up the process start info
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = exeFilePath,
                    Arguments = command,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                // Create and start the process
                using Process process = new() { StartInfo = startInfo };
                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                // Display the output
                Console.WriteLine("Output of the command:\n" + output);
            }
        }
        else
        {
            Console.WriteLine($"Error: The specified .exe file '{exeFileName}' does not exist in the folder '{folderPath}' or its subfolders.");
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
