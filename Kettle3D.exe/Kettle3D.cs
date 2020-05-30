// This program updates and then runs the launcher.

using System.Net.NetworkInformation;
using System.Reflection;
using System.Net;
using System.IO;
using System;

public class Program {
    public static void Main() {
        var appdata = Environment.GetEnvironmentVariable("appdata");
        var internet = Internet();

        if (internet) {
        WebClient client = new WebClient();
        Stream stream = client.OpenRead("https://github.com/Kettle3D/Kettle3D/raw/C%23/launcher.dll/launcher.dll");
        StreamReader reader = new StreamReader(stream);
        String text = reader.ReadToEnd();
        System.IO.File.WriteAllText(appdata + "\\Kettle3D\\launcher.dll", text);
        Console.WriteLine(text);
        }

        var DLL = Assembly.LoadFile(appdata + "\\Kettle3D\\launcher.dll");

        var theType = DLL.GetType("DLL.Program");
        var c = Activator.CreateInstance(theType);
        var method = theType.GetMethod("__init__");
        method.Invoke(c, new String[0]);
    }

    public static bool Internet()  {
        var host = "http://github.com/";
        bool result = false;
        Ping p = new Ping();
        try
        {
            PingReply reply = p.Send(host, 3000);
            if (reply.Status == IPStatus.Success)
            return true;
        }
        catch { }
            return result;
    }
}