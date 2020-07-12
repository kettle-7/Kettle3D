using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO.Compression;
//using System.Windows.Forms;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.IO;
using System;

class Program
{
    static string username = "Kettle3D";
    static string repository = "Kettle3D";

    static void DeleteFolder(string name)
    {
        foreach (var folder in Directory.GetDirectories(name))
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) // Check if this is my computer
                if (!(folder == Environment.GetEnvironmentVariable("localappdata") + $@"\{username}\{repository}\~dev~"))
                    DeleteFolder(folder);
                else
                    Console.WriteLine("I'm just going to ignore that folder...");
            else
                DeleteFolder(folder);
        }
        foreach (var file in Directory.GetFiles(name))
        {
            File.Delete(file);
        }
        try
        {
            Directory.Delete(name);
        }
        catch
        {
            Console.WriteLine($"The folder at {name} couldn't be deleted.");
        }
    }

    static void Main(string[] args)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Console.WriteLine("Starting Process...");
            if (!File.Exists(Environment.GetEnvironmentVariable("localappdata") + $@"\{username}\{repository}\version.txt"))
            {
                // Install Completely
                Directory.CreateDirectory(Environment.GetEnvironmentVariable("localappdata") + $@"\{username}");
                WebClient client = new WebClient();
                client.DownloadFile($"https://github.com/{username}/{repository}/raw/master/Kettle3D-Windows.zip", Environment.GetEnvironmentVariable("localappdata") + $@"\{username}\pkgtemp");
                if (Directory.Exists(Environment.GetEnvironmentVariable("localappdata") + $@"\{username}\{repository}"))
                {
                    DeleteFolder(Environment.GetEnvironmentVariable("localappdata") + $@"\{username}\{repository}");
                }

                ZipFile.ExtractToDirectory(Environment.GetEnvironmentVariable("localappdata") + $@"\{username}\pkgtemp", Environment.GetEnvironmentVariable("localappdata") + $@"\{username}\{repository}");
                client.DownloadFile($"https://github.com/{username}/{repository}/raw/master/version.txt", Environment.GetEnvironmentVariable("localappdata") + $@"\{username}\{repository}\version.txt");
                // Launch with Python. You'll need to change this in order to use it with a different app.
                Process.Start($"{Environment.GetEnvironmentVariable("localappdata") + $@"\{username}\{repository}\Kettle3D.exe"}");
            }
            else
            {
                WebClient client = new WebClient();
                if (client.DownloadString($"https://github.com/{username}/{repository}/raw/master/version.txt") == File.ReadAllText(Environment.GetEnvironmentVariable("localappdata") + $@"\{username}\{repository}\version.txt"))
                {
                    // Launch with Python. You'll need to change this in order to use it with a different app.
                    Process.Start($"{Environment.GetEnvironmentVariable("localappdata") + $@"\{username}\{repository}\Kettle3D.exe"}");
                }
                else
                {
                    Directory.CreateDirectory(Environment.GetEnvironmentVariable("localappdata") + $@"\{username}");
                    client.DownloadFile($"https://github.com/{username}/{repository}/raw/master/game/Kettle3D.exe", Environment.GetEnvironmentVariable("localappdata") + $@"\{username}\{repository}\Kettle3D.exe");

                    client.DownloadFile($"https://github.com/{username}/{repository}/raw/master/version.txt", Environment.GetEnvironmentVariable("localappdata") + $@"\{username}\{repository}\version.txt");
                    client.DownloadFile($"https://github.com/{username}/{repository}/raw/master/game/Kettle3D_Data/Managed/Assembly-CSharp.dll", Environment.GetEnvironmentVariable("localappdata") + $@"\{username}\{repository}\Kettle3D_Data\Managed\Assembly-CSharp.dll");
                    // Launch with Python. You'll need to change this in order to use it with a different app.
                    Process.Start($"{Environment.GetEnvironmentVariable("localappdata") + $@"\{username}\{repository}\Kettle3D.exe"}");
                }
            }
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            Console.WriteLine("Starting Process...");
            var home = Environment.GetEnvironmentVariable("HOME");
            if (!File.Exists($"{home}/.{repository}/{repository}/version.txt"))
            {
                // Install Completely
                Directory.CreateDirectory($"{home}/.{repository}");
                WebClient client = new WebClient();
                client.DownloadFile($"https://github.com/{username}/{repository}/raw/master/Kettle3D-Linux.zip", $"{home}/.{repository}/pkgtemp");
                if (Directory.Exists($"{home}/.{repository}/{repository}"))
                {
                    DeleteFolder($"{home}/.{repository}/{repository}");
                }

                ZipFile.ExtractToDirectory($"{home}/.{repository}/pkgtemp", $"{home}/.{repository}/{repository}");
                client.DownloadFile($"https://github.com/{username}/{repository}/raw/master/version.txt", $"{home}/.{repository}/{repository}/version.txt");
                // Launch with Python. You'll need to change this in order to use it with a different app.
                Process.Start($"{home}/.{repository}/{repository}/Kettle3D-Linux.x86_64");
            }
            else
            {
                WebClient client = new WebClient();
                if (client.DownloadString($"https://github.com/{username}/{repository}/raw/master/version.txt") == File.ReadAllText($"{home}/.{repository}/{repository}/version.txt"))
                {
                    // Launch with Python. You'll need to change this in order to use it with a different app.
                    Process.Start($"{home}/.{repository}/{repository}/Kettle3D-Linux.x86_64");
                }
                else
                {
                    Directory.CreateDirectory($"{home}/.{repository}");
                    WebClient client = new WebClient();
                    client.DownloadFile($"https://github.com/{username}/{repository}/raw/master/Kettle3D-Linux.zip", $"{home}/.{repository}/pkgtemp");
                    if (Directory.Exists($"{home}/.{repository}/{repository}"))
                    {
                        DeleteFolder($"{home}/.{repository}/{repository}");
                    }

                    ZipFile.ExtractToDirectory($"{home}/.{repository}/pkgtemp", $"{home}/.{repository}/{repository}");
                    client.DownloadFile($"https://github.com/{username}/{repository}/raw/master/version.txt", $"{home}/.{repository}/{repository}/version.txt");
                    // Launch with Python. You'll need to change this in order to use it with a different app.
                    Process.Start($"{home}/.{repository}/{repository}/Kettle3D-Linux.x86_64");
                }
            }
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            Console.WriteLine("Starting Process...");
            if (!File.Exists($"/Library/Application Support/{username}/{repository}/version.txt"))
            {
                // Install Completely
                if (Directory.Exists($"/Library/Application Support/{username}/{repository}")) {
                    DeleteFolder($"/Library/Application Support/{username}/{repository}");
                }
                Directory.CreateDirectory($"/Library/Application Support/{username}/{repository}");
                WebClient client = new WebClient();
                client.DownloadFile($"https://github.com/{username}/{repository}/raw/master/Kettle3D-macOS.zip", $"/Library/Application Support/{username}/pkgtemp");

                ZipFile.ExtractToDirectory($"/Library/Application Support/{username}/pkgtemp", $"/Applications/{repository}.app");
                client.DownloadFile($"https://github.com/{username}/{repository}/raw/master/version.txt", $"/Library/Application Support/{username}/{repository}/version.txt");
                // Launch with Python. You'll need to change this in order to use it with a different app.
                var _ = Process.Start("chmod", $"+x /Applications/{repository}.app/Contents/macOS/{repository}");
                _.WaitForExit();
                Process.Start("open", $"-a /Applications/{repository}.app");
            }
            else
            {
                WebClient client = new WebClient();
                if (client.DownloadString($"https://github.com/{username}/{repository}/raw/master/version.txt") == File.ReadAllText($"/Library/Application Support/{username}/{repository}/version.txt"))
                {
                    // Launch with Python. You'll need to change this in order to use it with a different app.
                    Process.Start("open", $"-a /Applications/{repository}.app");
                }
                else // Update
                {
                    if (Directory.Exists($"/Library/Application Support/{username}/{repository}")) {
                        DeleteFolder($"/Library/Application Support/{username}/{repository}");
                    }
                    Directory.CreateDirectory($"/Library/Application Support/{username}/{repository}");
                    WebClient client = new WebClient();
                    client.DownloadFile($"https://github.com/{username}/{repository}/raw/master/Kettle3D-macOS.zip", $"/Library/Application Support/{username}/pkgtemp");

                    ZipFile.ExtractToDirectory($"/Library/Application Support/{username}/pkgtemp", $"/Applications/{repository}.app");
                    client.DownloadFile($"https://github.com/{username}/{repository}/raw/master/version.txt", $"/Library/Application Support/{username}/{repository}/version.txt");
                    // Launch with Python. You'll need to change this in order to use it with a different app.
                    var _ = Process.Start("chmod", $"+x /Applications/{repository}.app/Contents/macOS/{repository}");
                    _.WaitForExit();
                    Process.Start("open", $"-a /Applications/{repository}.app");
                }
            }
        }
        else
        {
            //MessageBox.Show("Sorry, Kettle3D is not supported on your device. It works on Windows, Linux and OS X.");
        }
    }
}
