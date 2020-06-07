using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using System.Windows;
using System.Linq;
using System.Net;
using System.IO;
using System;

public class Component
{
    public string Name { get; set; }
    public string Location { get; set; }
    public string Url { get; set; }
    public int Version { get; set; }

    public Component() { }

    public Component(string name, string location, string url, int version)
    {
        this.Name = name;
        this.Location = location;
        this.Url = url;
        this.Version = version;
    }

    public Component(string name)
    {
        this.Name = name;
        this.Location = new ComponentFile().directory + @"Kettle3D\" + name;
        this.Url = "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/" + name;
        this.Version = 1;
    }

    public Component(string name, int version)
    {
        this.Name = name;
        this.Location = new ComponentFile().directory + @"Kettle3D\" + name;
        this.Url = "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/" + name;
        this.Version = version;
    }
}

public class ComponentFile
{
    public String directory
    {
        get
        {
            return Environment.GetEnvironmentVariable("appdata") + @"\Kettle3D";
        }
    }

    public List<Component> Components { get; set; }
    public string FormatVersion { get; set; }

    public ComponentFile()
    {
        this.harry = new JsonSerializerOptions();
        this.Components = new List<Component>();
    }

    public ComponentFile(Component[] comps)
    {
        this.harry = new JsonSerializerOptions();
        this.Components = comps.ToList();
    }

    public ComponentFile(List<Component> comps)
    {
        this.harry = new JsonSerializerOptions();
        this.Components = comps;
    }

    public JsonSerializerOptions harry;

    public String dump()
    {
        this.harry.WriteIndented = true;

        return JsonSerializer.Serialize(this, this.GetType(), this.harry);
    }
}

class Program
{
    static void Main()
    {
        new Installer().Main();
    }
}

class Installer
{

    public String directory
    {
        get
        {
            return Environment.GetEnvironmentVariable("appdata");
        }
    }

    public void Install(Component comp)
    {
        Stream stream = client.OpenRead(comp.Url);
        StreamReader reader = new StreamReader(stream);
        String content = reader.ReadToEnd();
        File.WriteAllText(comp.Location, content);
    }

    public WebClient client;

    public void Main()
    {
        this.client = new WebClient();
        if (File.Exists(this.directory + @"\installed.json"))
        {
            Component[] comps = new Component[] {
                new Component(@"Kettle3D_Data/Resources/unity default resources", this.directory + @"\Kettle3D_Data\Resources\unity default resources", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Resources/unity default resources", 1),
                new Component(@"Kettle3D_Data/Resources/unity_builtin_extra", this.directory + @"\Kettle3D_Data\Resources\unity_builtin_extra", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Resources/unity_builtin_extra", 1),
                new Component(@"Kettle3D_Data/Managed/Assembly-CSharp.dll", this.directory + @"\Kettle3D_Data\Managed\Assembly-CSharp.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/Assembly-CSharp.dll", 1),
                new Component(@"Kettle3D_Data/Managed/Mono.Security.dll", this.directory + @"\Kettle3D_Data\Managed\Mono.Security.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/Mono.Security.dll", 1),
                new Component(@"Kettle3D_Data/Managed/mscorlib.dll", this.directory + @"\Kettle3D_Data\Managed\mscorlib.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/mscorlib.dll", 1),
                new Component(@"Kettle3D_Data/Managed/netstandard.dll", this.directory + @"\Kettle3D_Data\Managed\netstandard.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/netstandard.dll", 1),
                new Component(@"Kettle3D_Data/Managed/System.ComponentModel.Composition.dll", this.directory + @"\Kettle3D_Data\Managed\System.ComponentModel.Composition.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/System.ComponentModel.Composition.dll", 1),
                new Component(@"Kettle3D_Data/Managed/System.Configuration.dll", this.directory + @"\Kettle3D_Data\Managed\System.Configuration.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/System.Configuration.dll", 1),
                new Component(@"Kettle3D_Data/Managed/System.Core.dll", this.directory + @"\Kettle3D_Data\Managed\System.Core.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/System.Core.dll", 1),
                new Component(@"Kettle3D_Data/Managed/System.Data.dll", this.directory + @"\Kettle3D_Data\Managed\System.Data.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/System.Data.dll", 1),
                new Component(@"Kettle3D_Data/Managed/System.Diagnostics.StackTrace.dll", this.directory + @"\Kettle3D_Data\Managed\System.Diagnostics.StackTrace.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/System.Diagnostics.StackTrace.dll", 1),
                new Component(@"Kettle3D_Data/Managed/System.dll", this.directory + @"\Kettle3D_Data\Managed\System.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/System.dll", 1),
                new Component(@"Kettle3D_Data/Managed/System.Drawing.dll", this.directory + @"\Kettle3D_Data\Managed\System.Drawing.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/System.Drawing.dll", 1),
                new Component(@"Kettle3D_Data/Managed/System.EnterpriseServices.dll", this.directory + @"\Kettle3D_Data\Managed\System.EnterpriseServices.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/System.EnterpriseServices.dll", 1),
                new Component(@"Kettle3D_Data/Managed/System.Globalization.Extensions.dll", this.directory + @"\Kettle3D_Data\Managed\System.Globalization.Extensions.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/System.Globalization.Extensions.dll", 1),
                new Component(@"Kettle3D_Data/Managed/System.IO.Compression.dll", this.directory + @"\Kettle3D_Data\Managed\System.IO.Compression.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/System.IO.Compression.dll", 1),
                new Component(@"Kettle3D_Data/Managed/System.IO.Compression.FileSystem.dll", this.directory + @"\Kettle3D_Data\Managed\System.IO.Compression.FileSystem.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/System.IO.Compression.FileSystem.dll", 1),
                new Component(@"Kettle3D_Data/Managed/System.Net.Http.dll", this.directory + @"\Kettle3D_Data\Managed\System.Net.Http.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/System.Net.Http.dll", 1),
                new Component(@"Kettle3D_Data/Managed/System.Numerics.dll", this.directory + @"\Kettle3D_Data\Managed\System.Numerics.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/System.Numerics.dll", 1),
                new Component(@"Kettle3D_Data/Managed/System.Runtime.Serialization.dll", this.directory + @"\Kettle3D_Data\Managed\System.Runtime.Serialization.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/System.Runtime.Serialization.dll", 1),
                new Component(@"Kettle3D_Data/Managed/System.Runtime.Serialization.Xml.dll", this.directory + @"\Kettle3D_Data\Managed\System.Runtime.Serialization.Xml.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/System.Runtime.Serialization.Xml.dll", 1),
                new Component(@"Kettle3D_Data/Managed/System.ServiceModel.Internals.dll", this.directory + @"\Kettle3D_Data\Managed\System.ServiceModel.Internals.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/System.ServiceModel.Internals.dll", 1),
                new Component(@"Kettle3D_Data/Managed/System.Transactions.dll", this.directory + @"\Kettle3D_Data\Managed\System.Transactions.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/System.Transactions.dll", 1),
                new Component(@"Kettle3D_Data/Managed/System.Xml.dll", this.directory + @"\Kettle3D_Data\Managed\System.Xml.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/System.Xml.dll", 1),
                new Component(@"Kettle3D_Data/Managed/System.Xml.Linq.dll", this.directory + @"\Kettle3D_Data\Managed\System.Xml.Linq.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/System.Xml.Linq.dll", 1),
                new Component(@"Kettle3D_Data/Managed/System.Xml.Xpath.XDocument.dll", this.directory + @"\Kettle3D_Data\Managed\System.Xml.Xpath.XDocument.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/System.Xml.Xpath.XDocument.dll", 1),
                new Component(@"Kettle3D_Data/Managed/Unity.TextMeshPro.dll", this.directory + @"\Kettle3D_Data\Managed\Unity.TextMeshPro.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/Unity.TextMeshPro.dll", 1),
                new Component(@"Kettle3D_Data/Managed/Unity.Timeline.dll", this.directory + @"\Kettle3D_Data\Managed\Unity.Timeline.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/Unity.Timeline.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.AccessibilityModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.AccessibilityModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.AccessibilityModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.AIModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.AIModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.AIModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.AndroidJNIModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.AndroidJNIModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.AndroidJNIModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.AnimationModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.AnimationModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.AnimationModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.ARModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.ARModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.ARModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.AssetBundleModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.AssetBundleModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.AssetBundleModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.AudioModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.AudioModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.AudioModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.ClothModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.ClothModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.ClothModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.ClusterInputModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.ClusterInputModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.ClusterInputModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.ClusterRendererModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.ClusterRendererModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.ClusterRendererModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.CoreModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.CoreModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.CoreModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.CrashReportingModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.CrashReportingModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.CrashReportingModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.DirectorModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.DirectorModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.DirectorModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.DSPGraphModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.DSPGraphModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.DSPGraphModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.GameCenterModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.GameCenterModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.GameCenterModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.GridModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.GridModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.GridModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.HotReloadModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.HotReloadModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.HotReloadModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.ImageConversionModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.ImageConversionModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.ImageConversionModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.IMGUIModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.IMGUIModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.IMGUIModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.InputLegacyModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.InputLegacyModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.InputLegacyModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.InputModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.InputModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.InputModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.JSONSerializeModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.JSONSerializeModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.JSONSerializeModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.LocalizationModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.LocalizationModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.LocalizationModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.ParticleSystemModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.ParticleSystemModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.ParticleSystemModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.PerformanceReportingModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.PerformanceReportingModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.PerformanceReportingModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.Physics2DModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.Physics2DModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.Physics2DModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.PhysicsModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.PhysicsModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.PhysicsModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.ProfilerModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.ProfilerModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.ProfilerModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.ScreenCaptureModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.ScreenCaptureModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.ScreenCaptureModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.SharedInternalsModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.SharedInternalsModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.SharedInternalsModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.SpriteMaskModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.SpriteMaskModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.SpriteMaskModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.SpriteShapeModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.SpriteShapeModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.SpriteShapeModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.StreamingModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.StreamingModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.StreamingModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.SubstanceModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.SubstanceModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.SubstanceModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.SubsystemsModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.SubsystemsModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.SubsystemsModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.TerrainModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.TerrainModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.TerrainModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.TerrainPhysicsModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.TerrainPhysicsModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.TerrainPhysicsModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.TextRenderingModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.TextRenderingModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.TextRenderingModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.TilemapModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.TilemapModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.TilemapModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.TLSModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.TLSModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.TLSModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.UI.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.UI.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.UI.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.UIElementsModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.UIElementsModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.UIElementsModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.UIModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.UIModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.UIModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.UmbraModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.UmbraModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.UmbraModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.UNETModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.UNETModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.UNETModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.UnityAnalyticsModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.UnityAnalyticsModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.UnityAnalyticsModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.UnitTestProtocolModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.UnitTestProtocolModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.UnitTestProtocolModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.UnityWebRequestAssetBundleModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.UnityWebRequestAssetBundleModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.UnityWebRequestAssetBundleModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.UnityWebRequestAudioModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.UnityWebRequestAudioModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.UnityWebRequestAudioModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.UnityWebRequestModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.UnityWebRequestModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.UnityWebRequestModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.UnityWebRequestTextureModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.UnityWebRequestTextureModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.UnityWebRequestTextureModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.UnityWebRequestWWWModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.UnityWebRequestWWWModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.UnityWebRequestWWWModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.VehiclesModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.VehiclesModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.VehiclesModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.VFXModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.VFXModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.VFXModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.VideoModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.VideoModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.VideoModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.VRModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.VRModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.VRModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.WindModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.WindModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.WindModule.dll", 1),
                new Component(@"Kettle3D_Data/Managed/UnityEngine.XRModule.dll", this.directory + @"\Kettle3D_Data\Managed\UnityEngine.XRModule.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.XRModule.dll", 1),
                new Component(@"Kettle3D_Data/app.info", this.directory + @"\Kettle3D_Data\app.info", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/app.info", 1),
                new Component(@"Kettle3D_Data/boot.config", this.directory + @"\Kettle3D_Data\boot.config", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/boot.config", 1),
                new Component(@"Kettle3D_Data/globalgamemanagers", this.directory + @"\Kettle3D_Data\globalgamemanagers", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/globalgamemanagers", 1),
                new Component(@"Kettle3D_Data/globalgamemanagers.assets", this.directory + @"\Kettle3D_Data\globalgamemanagers.assets", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/globalgamemanagers.assets", 1),
                new Component(@"Kettle3D_Data/level0", this.directory + @"\Kettle3D_Data\level0", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/level0", 1),
                new Component(@"Kettle3D_Data/sharedassets0.assets", this.directory + @"\Kettle3D_Data\sharedassets0.assets", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/sharedassets0.assets", 1),
                new Component(@"Kettle3D_Data/sharedassets0.assets.resS", this.directory + @"\Kettle3D_Data\sharedassets0.assets.resS", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/sharedassets0.assets.resS", 1),
                new Component(@"MonoBleedingEdge/EmbedRuntime/mono-2.0-bdwgc.dll", this.directory + @"\MonoBleedingEdge\EmbedRuntime\mono-2.0-bdwgc.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/MonoBleedingEdge/EmbedRuntime/mono-2.0-bdwgc.dll", 1),
                new Component(@"MonoBleedingEdge/EmbedRuntime/MonoPosixHelper.dll", this.directory + @"\MonoBleedingEdge\EmbedRuntime\MonoPosixHelper.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/MonoBleedingEdge/EmbedRuntime/MonoPosixHelper.dll", 1),
                new Component(@"MonoBleedingEdge/etc/mono/2.0/Browsers/Compat.browser", this.directory + @"\MonoBleedingEdge\etc\mono\2.0\Browsers\Compat.browser", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/MonoBleedingEdge/etc/mono/2.0/Browsers/Compat.browser", 1),
                new Component(@"MonoBleedingEdge/etc/mono/2.0/DefaultWsdlHelpGenerator.aspx", this.directory + @"\MonoBleedingEdge\etc\mono\2.0\DefaultWsdlHelpGenerator.aspx", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/MonoBleedingEdge/etc/mono/2.0/DefaultWsdlHelpGenerator.aspx", 1),
                new Component(@"MonoBleedingEdge/etc/mono/2.0/machine.config", this.directory + @"\MonoBleedingEdge\etc\mono\2.0\machine.config", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/MonoBleedingEdge/etc/mono/2.0/machine.config", 1),
                new Component(@"MonoBleedingEdge/etc/mono/2.0/settings.map", this.directory + @"\MonoBleedingEdge\etc\mono\2.0\settings.map", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/MonoBleedingEdge/etc/mono/2.0/settings.map", 1),
                new Component(@"MonoBleedingEdge/etc/mono/2.0/web.config", this.directory + @"\MonoBleedingEdge\etc\mono\2.0\web.config", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/MonoBleedingEdge/etc/mono/2.0/web.config", 1),
                new Component(@"MonoBleedingEdge/etc/mono/4.0/Browsers/Compat.browser", this.directory + @"\MonoBleedingEdge\etc\mono\4.0\Browsers\Compat.browser", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/MonoBleedingEdge/etc/mono/4.0/Browsers/Compat.browser", 1),
                new Component(@"MonoBleedingEdge/etc/mono/4.0/DefaultWsdlHelpGenerator.aspx", this.directory + @"\MonoBleedingEdge\etc\mono\4.0\DefaultWsdlHelpGenerator.aspx", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/MonoBleedingEdge/etc/mono/4.0/DefaultWsdlHelpGenerator.aspx", 1),
                new Component(@"MonoBleedingEdge/etc/mono/4.0/machine.config", this.directory + @"\MonoBleedingEdge\etc\mono\4.0\machine.config", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/MonoBleedingEdge/etc/mono/4.0/machine.config", 1),
                new Component(@"MonoBleedingEdge/etc/mono/4.0/settings.map", this.directory + @"\MonoBleedingEdge\etc\mono\4.0\settings.map", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/MonoBleedingEdge/etc/mono/4.0/settings.map", 1),
                new Component(@"MonoBleedingEdge/etc/mono/4.0/web.config", this.directory + @"\MonoBleedingEdge\etc\mono\4.0\web.config", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/MonoBleedingEdge/etc/mono/4.0/web.config", 1),
                new Component(@"MonoBleedingEdge/etc/mono/4.5/Browsers/Compat.browser", this.directory + @"\MonoBleedingEdge\etc\mono\4.5\Browsers\Compat.browser", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/MonoBleedingEdge/etc/mono/4.5/Browsers/Compat.browser", 1),
                new Component(@"MonoBleedingEdge/etc/mono/4.5/DefaultWsdlHelpGenerator.aspx", this.directory + @"\MonoBleedingEdge\etc\mono\4.5\DefaultWsdlHelpGenerator.aspx", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/MonoBleedingEdge/etc/mono/4.5/DefaultWsdlHelpGenerator.aspx", 1),
                new Component(@"MonoBleedingEdge/etc/mono/4.5/machine.config", this.directory + @"\MonoBleedingEdge\etc\mono\4.5\machine.config", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/MonoBleedingEdge/etc/mono/4.5/machine.config", 1),
                new Component(@"MonoBleedingEdge/etc/mono/4.5/settings.map", this.directory + @"\MonoBleedingEdge\etc\mono\4.5\settings.map", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/MonoBleedingEdge/etc/mono/4.5/settings.map", 1),
                new Component(@"MonoBleedingEdge/etc/mono/4.5/web.config", this.directory + @"\MonoBleedingEdge\etc\mono\4.5\web.config", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/MonoBleedingEdge/etc/mono/4.5/web.config", 1),
                new Component(@"MonoBleedingEdge/etc/mono/msconfig/config.xml", this.directory + @"\MonoBleedingEdge\etc\mono\msconfig\config.xml", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/MonoBleedingEdge/etc/mono/msconfig/config.xml", 1),
                new Component(@"MonoBleedingEdge/etc/mono/browscap.ini", this.directory + @"\MonoBleedingEdge\etc\mono\browscap.ini", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/MonoBleedingEdge/etc/mono/browscap.ini", 1),
                new Component(@"MonoBleedingEdge/etc/mono/config", this.directory + @"\MonoBleedingEdge\etc\mono\config", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/MonoBleedingEdge/etc/mono/config", 1),
                new Component(@"Kettle3D.exe", this.directory + @"\Kettle3D.exe", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D.exe", 1),
                new Component(@"UnityPlayer.dll", this.directory + @"\UnityPlayer.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/UnityPlayer.dll", 1)
            };
            File.WriteAllText(this.directory + @"\Kettle3D\installed.json", new ComponentFile(comps).dump());

            foreach (Component comp in comps)
            {
                this.Install(comp);
            }
        }
    }
}