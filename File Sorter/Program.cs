using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace File_Sorter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Get the file path to sort from the user
            string filePath = UserInput();

            // Get every file path in the users path
            List<string> filePaths = OSWalk(filePath);

            // Create Directory Names and create them
            List<string> newDirectories = CreateDirectoriesNames(filePath);

            // Create the new Directories
            CreateDirectories(newDirectories);

            // Sort Gifs
            FileMove(filePaths, newDirectories[3], ".gif");
            // Sort MP4
            FileMove(filePaths, newDirectories[0], ".mp4");
            // Sort MKV
            FileMove(filePaths, newDirectories[0], ".mkv");
            // Sort M4V
            FileMove(filePaths, newDirectories[0], ".m4v");
            // Sort WEBM
            FileMove(filePaths, newDirectories[0], ".webm");
            // Sort AVI
            FileMove(filePaths, newDirectories[0], ".avi");
            // Sort JPG
            FileMove(filePaths, newDirectories[1], ".jpg");
            // Sort JPEG
            FileMove(filePaths, newDirectories[1], ".jpeg");
            // Sort TXT
            FileMove(filePaths, newDirectories[5], ".txt");
            // Sort PNG
            FileMove(filePaths, newDirectories[1], ".png");
            // Sort SWF
            FileMove(filePaths, newDirectories[2], ".swf");
            // Sort MOV
            FileMove(filePaths, newDirectories[0], ".mov");
            // Sort Zips
            FileMove(filePaths, newDirectories[4], ".zip");

            // Delete the empty directories
            DeleteEmptyDirectory(filePath);
        }
        static bool HasFileEnding(string filePath, string inputType)
        {
            // Get file extension
            string fileExtension = Path.GetExtension(filePath);

            // return if file has ending
            return string.Equals(fileExtension, inputType, StringComparison.OrdinalIgnoreCase);
        }

        static List<string> OSWalk(string filePath)
        {
            // Create list of strings for each file path
            List<string> filePaths = new List<string>();

            // Add file names to List of file paths
            string[] files = Directory.GetFiles(filePath);
            filePaths.AddRange(files);

            // Get file names from sub folders
            string[] subDirectories = Directory.GetDirectories(filePath);
            foreach (string subDirectory in subDirectories)
            {
                filePaths.AddRange(OSWalk(subDirectory));
            }

            // Return all file paths
            return filePaths;
        }

        static void FileMove(List<string> filePaths, string finalPath, string inputType)
        {
            foreach (string filePath in filePaths)
            {
                if (HasFileEnding(filePath, inputType))
                {
                    string fileName = Path.GetFileName(filePath);
                    string destinationPath = Path.Combine(finalPath, fileName);

                    // Check if the destination file already exists
                    if (File.Exists(destinationPath))
                    {
                        
                        File.Delete(destinationPath);
                    }

                    
                    File.Move(filePath, destinationPath);
                }
            }
        }
        static string UserInput()
        {
            // Prompt question and recieve answer
            Console.WriteLine("Which file path would you like to sort?");
            return (Console.ReadLine());
        }

        static void DeleteEmptyDirectory(string filePaths)
        {
            foreach (var directory in Directory.GetDirectories(filePaths))
            {
                DeleteEmptyDirectory(directory);
                if (Directory.GetFiles(directory).Length == 0 &&
                    Directory.GetDirectories(directory).Length == 0)
                {
                    Directory.Delete(directory, false);
                }
            }
        }

        static List<string> CreateDirectoriesNames(string targetDir)
        {
            // Create a list of directory names with the original file path
            List<string> newDirectories = new List<string>();
            newDirectories.Add(Path.Combine(targetDir, "Videos"));
            newDirectories.Add(Path.Combine(targetDir, "Images"));
            newDirectories.Add(Path.Combine(targetDir, "SWF"));
            newDirectories.Add(Path.Combine(targetDir, "GIF"));
            newDirectories.Add(Path.Combine(targetDir, "ZIP"));
            newDirectories.Add(Path.Combine(targetDir, "TXT"));

            return newDirectories;

        }

        static void CreateDirectories(List<string> newDirectories)
        {
            // Create a list to see which directories need to be created
            List<string> directoryRecurrsionList = new List<string>();

            // Loop to create directories
            foreach(string dir in newDirectories)
            {
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                //else
                //{
                //    Directory.Delete(dir);
                //    directoryRecurrsionList.Add(dir);
                //}
            }

            // Run recurrsion if there is file path needed to be created in the loop
            //if (directoryRecurrsionList.Count > 0)
            //{
            //    CreateDirectories(directoryRecurrsionList);
            //}
        }

    }
}
