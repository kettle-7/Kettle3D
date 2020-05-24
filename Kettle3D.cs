// Compile this with "csc.exe /target:winexe Kettle3D.cs /win32icon:Kettle3D.ico"

using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

class BatCaller {
    static void Main() {
        var directory = Directory.GetCurrentDirectory();
		var batFile = directory + "\\scripts\\main.bat";
        if (!File.Exists(batFile)) {
            MessageBox.Show("Kettle3D is missing important files. It probably didn't install properly :-(", "Critical error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            System.Environment.Exit(42);
        }
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