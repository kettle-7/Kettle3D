// Kettle3D Windows 10 installer

using System.Windows.Forms;
using System.Diagnostics;
using System.Net;
using System.IO;
using System;

class Installer {
static void Main() {
    WebClient client = new WebClient();
    Stream stream = client.OpenRead("https://raw.githubusercontent.com/Kettle3D/Kettle3D/master/Kettle3D_Windows_Installer.bat");
    StreamReader reader = new StreamReader(stream);
    String content = reader.ReadToEnd();

    var appdata = Environment.GetEnvironmentVariable ("appdata");
    var batFile = appdata + "\\Kettle3D\\install.bat";
    System.IO.File.WriteAllText(@"" + batFile, content.ToString());

    var processInfo = new ProcessStartInfo("cmd.exe", "/c \"" + batFile + "\"");
    processInfo.CreateNoWindow = true;
    processInfo.UseShellExecute = false;
    processInfo.RedirectStandardError = true;
    processInfo.RedirectStandardOutput = true;

    var process = Process.Start(processInfo);

    process.OutputDataReceived += (object sender, DataReceivedEventArgs e) => Console.WriteLine("output>>" + e.Data);
    process.BeginOutputReadLine();

    process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) => Console.WriteLine("error>>" + e.Data);
    process.BeginErrorReadLine();

    process.WaitForExit();

    Console.WriteLine("ExitCode: {0}", process.ExitCode);
    process.Close();
}
}