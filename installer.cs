// Kettle3D Windows 10 installer

using System.Text.Json.Serialization;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.Json;
using System.Net;
using System.IO;
using System;

public class Component {
    public string Name { get; set; }
    public string Location { get; set; }
    public string Url { get; set; }
    public string Version { get; set; }

    public Component() {}

    public Component(string name, string location, string url, string version) {
        this.Name = name;
        this.Location = location;
        this.Url = url;
        this.Version = version;
    }
}

public class ComponentFile {
    public String appdata {
        get {
            return Environment.GetEnvironmentVariable ("appdata");
        }
    }

    public List<Component> Components { get; set; }
    public List<String> serialised_components { get; set; }
    public string FormatVersion { get; set; }

    public void ComponentFile() {
        this.Components = new List<Component>();
    }

    public void ComponentFile(Component[] comps) {
        this.Components = comps.ToList();
    }

    public void ComponentFile(List<Component> comps) {
        this.components = comps;
    }

    public dump() {
        this.serialised_components = new List<String>();

        for (item in this.Components) {
            this.serialised_components.Add(JsonSerializer.Serialize(item));
        }

        return JsonSerializer.Serialize(this);
    }
}

class Installer {

public String appdata {
    get {
        return Environment.GetEnvironmentVariable ("appdata");
    }
}

static void install_app() {
    var comps = List<Component>() {// name, location, url, version
    
    new Component(@"Kettle3D_Data/Resources/unity default resources", this.appdata + @"\Kettle3D\Kettle3D_Data\Resources\unity default resources", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Resources/unity default resources", 1),
    new Component(@"Kettle3D_Data/Resources/unity_builtin_extra", this.appdata + @"\Kettle3D\Kettle3D_Data\Resources\unity_builtin_extra", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Resources/unity_builtin_extra", 1),
    new Component(@"Kettle3D_Data/Managed/Assembly-CSharp", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\Assembly-CSharp", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/Assembly-CSharp", 1),
    new Component(@"Kettle3D_Data/Managed/Mono.Security", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\Mono.Security", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/Mono.Security", 1),
    new Component(@"Kettle3D_Data/Managed/mscorlib", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\mscorlib", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/mscorlib", 1),
    new Component(@"Kettle3D_Data/Managed/netstandard", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\netstandard", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/netstandard", 1),
    new Component(@"Kettle3D_Data/Managed/System.ComponentModel.Composition", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\System.ComponentModel.Composition", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/System.ComponentModel.Composition", 1),
    new Component(@"Kettle3D_Data/Managed/System.Configuration", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\System.Configuration", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/System.Configuration", 1),
    new Component(@"Kettle3D_Data/Managed/System.Core", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\System.Core", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/System.Core", 1),
    new Component(@"Kettle3D_Data/Managed/System.Data", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\System.Data", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/System.Data", 1),
    new Component(@"Kettle3D_Data/Managed/System.Diagnostics.StackTrace", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\System.Diagnostics.StackTrace", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/System.Diagnostics.StackTrace", 1),
    new Component(@"Kettle3D_Data/Managed/System", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\System", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/System", 1),
    new Component(@"Kettle3D_Data/Managed/System.Drawing", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\System.Drawing", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/System.Drawing", 1),
    new Component(@"Kettle3D_Data/Managed/System.EnterpriseServices", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\System.EnterpriseServices", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/System.EnterpriseServices", 1),
    new Component(@"Kettle3D_Data/Managed/System.Globalization.Extensions", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\System.Globalization.Extensions", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/System.Globalization.Extensions", 1),
    new Component(@"Kettle3D_Data/Managed/System.IO.Compression", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\System.IO.Compression", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/System.IO.Compression", 1),
    new Component(@"Kettle3D_Data/Managed/System.IO.Compression.FileSystem", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\System.IO.Compression.FileSystem", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/System.IO.Compression.FileSystem", 1),
    new Component(@"Kettle3D_Data/Managed/System.Net.Http", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\System.Net.Http", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/System.Net.Http", 1),
    new Component(@"Kettle3D_Data/Managed/System.Numerics", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\System.Numerics", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/System.Numerics", 1),
    new Component(@"Kettle3D_Data/Managed/System.Runtime.Serialization", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\System.Runtime.Serialization", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/System.Runtime.Serialization", 1),
    new Component(@"Kettle3D_Data/Managed/System.Runtime.Serialization.Xml", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\System.Runtime.Serialization.Xml", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/System.Runtime.Serialization.Xml", 1),
    new Component(@"Kettle3D_Data/Managed/System.ServiceModel.Internals", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\System.ServiceModel.Internals", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/System.ServiceModel.Internals", 1),
    new Component(@"Kettle3D_Data/Managed/System.Transactions", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\System.Transactions", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/System.Transactions", 1),
    new Component(@"Kettle3D_Data/Managed/System.Xml", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\System.Xml", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/System.Xml", 1),
    new Component(@"Kettle3D_Data/Managed/System.Xml.Linq", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\System.Xml.Linq", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/System.Xml.Linq", 1),
    new Component(@"Kettle3D_Data/Managed/System.Xml.Xpath.XDocument", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\System.Xml.Xpath.XDocument", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/System.Xml.Xpath.XDocument", 1),
    new Component(@"Kettle3D_Data/Managed/Unity.TextMeshPro", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\Unity.TextMeshPro", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/Unity.TextMeshPro", 1),
    new Component(@"Kettle3D_Data/Managed/Unity.Timeline", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\Unity.Timeline", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/Unity.Timeline", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.AccessibilityModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.AccessibilityModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.AccessibilityModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.AIModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.AIModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.AIModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.AndroidJNIModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.AndroidJNIModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.AndroidJNIModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.AnimationModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.AnimationModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.AnimationModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.ARModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.ARModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.ARModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.AssetBundleModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.AssetBundleModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.AssetBundleModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.AudioModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.AudioModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.AudioModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.ClothModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.ClothModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.ClothModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.ClusterInputModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.ClusterInputModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.ClusterInputModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.ClusterRendererModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.ClusterRendererModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.ClusterRendererModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.CoreModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.CoreModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.CoreModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.CrashReportingModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.CrashReportingModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.CrashReportingModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.DirectorModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.DirectorModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.DirectorModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.DSPGraphModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.DSPGraphModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.DSPGraphModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.GameCenterModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.GameCenterModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.GameCenterModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.GridModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.GridModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.GridModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.HotReloadModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.HotReloadModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.HotReloadModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.ImageConversionModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.ImageConversionModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.ImageConversionModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.IMGUIModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.IMGUIModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.IMGUIModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.InputLegacyModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.InputLegacyModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.InputLegacyModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.InputModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.InputModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.InputModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.JSONSerializeModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.JSONSerializeModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.JSONSerializeModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.LocalizationModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.LocalizationModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.LocalizationModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.ParticleSystemModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.ParticleSystemModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.ParticleSystemModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.PerformanceReportingModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.PerformanceReportingModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.PerformanceReportingModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.Physics2DModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.Physics2DModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.Physics2DModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.PhysicsModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.PhysicsModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.PhysicsModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.ProfilerModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.ProfilerModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.ProfilerModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.ScreenCaptureModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.ScreenCaptureModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.ScreenCaptureModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.SharedInternalsModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.SharedInternalsModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.SharedInternalsModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.SpriteMaskModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.SpriteMaskModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.SpriteMaskModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.SpriteShapeModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.SpriteShapeModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.SpriteShapeModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.StreamingModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.StreamingModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.StreamingModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.SubstanceModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.SubstanceModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.SubstanceModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.SubsystemsModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.SubsystemsModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.SubsystemsModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.TerrainModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.TerrainModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.TerrainModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.TerrainPhysicsModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.TerrainPhysicsModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.TerrainPhysicsModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.TextRenderingModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.TextRenderingModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.TextRenderingModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.TilemapModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.TilemapModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.TilemapModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.TLSModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.TLSModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.TLSModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.UI", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.UI", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.UI", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.UIElementsModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.UIElementsModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.UIElementsModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.UIModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.UIModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.UIModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.UmbraModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.UmbraModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.UmbraModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.UNETModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.UNETModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.UNETModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.UnityAnalyticsModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.UnityAnalyticsModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.UnityAnalyticsModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.UnitTestProtocolModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.UnitTestProtocolModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.UnitTestProtocolModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.UnityWebRequestAssetBundleModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.UnityWebRequestAssetBundleModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.UnityWebRequestAssetBundleModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.UnityWebRequestAudioModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.UnityWebRequestAudioModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.UnityWebRequestAudioModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.UnityWebRequestModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.UnityWebRequestModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.UnityWebRequestModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.UnityWebRequestTextureModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.UnityWebRequestTextureModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.UnityWebRequestTextureModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.UnityWebRequestWWWModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.UnityWebRequestWWWModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.UnityWebRequestWWWModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.VehiclesModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.VehiclesModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.VehiclesModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.VFXModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.VFXModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.VFXModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.VideoModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.VideoModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.VideoModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.VRModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.VRModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.VRModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.WindModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.WindModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.WindModule", 1),
    new Component(@"Kettle3D_Data/Managed/UnityEngine.XRModule", this.appdata + @"\Kettle3D\Kettle3D_Data\Managed\UnityEngine.XRModule", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/Managed/UnityEngine.XRModule", 1),
    new Component(@"Kettle3D_Data/app.info", this.appdata + @"\Kettle3D\Kettle3D_Data\app.info", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/app.info", 1),
    new Component(@"Kettle3D_Data/boot.config", this.appdata + @"\Kettle3D\Kettle3D_Data\boot.config", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/boot.config", 1),
    new Component(@"Kettle3D_Data/globalgamemanagers", this.appdata + @"\Kettle3D\Kettle3D_Data\globalgamemanagers", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/globalgamemanagers", 1),
    new Component(@"Kettle3D_Data/globalgamemanagers.assets", this.appdata + @"\Kettle3D\Kettle3D_Data\globalgamemanagers.assets", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/globalgamemanagers.assets", 1),
    new Component(@"Kettle3D_Data/level0", this.appdata + @"\Kettle3D\Kettle3D_Data\level0", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/level0", 1),
    new Component(@"Kettle3D_Data/sharedassets0.assets", this.appdata + @"\Kettle3D\Kettle3D_Data\sharedassets0.assets", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/sharedassets0.assets", 1),
    new Component(@"Kettle3D_Data/sharedassets0.assets.resS", this.appdata + @"\Kettle3D\Kettle3D_Data\sharedassets0.assets.resS", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D_Data/sharedassets0.assets.resS", 1),
    new Component(@"MonoBleedingEdge/EmbedRuntime/mono-2.0-bdwgc.dll", this.appdata + @"\Kettle3D\MonoBleedingEdge\EmbedRuntime\mono-2.0-bdwgc.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/MonoBleedingEdge/EmbedRuntime/mono-2.0-bdwgc.dll", 1),
    new Component(@"MonoBleedingEdge/EmbedRuntime/MonoPosixHelper.dll", this.appdata + @"\Kettle3D\MonoBleedingEdge\EmbedRuntime\MonoPosixHelper.dll", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/MonoBleedingEdge/EmbedRuntime/MonoPosixHelper.dll", 1),
    new Component(@"MonoBleedingEdge/etc/mono/2.0/Browsers/Compat.browser", this.appdata + @"\Kettle3D\MonoBleedingEdge\etc\mono\2.0\Browsers\Compat.browser", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/MonoBleedingEdge/etc/mono/2.0/Browsers/Compat.browser", 1),
    new Component(@"MonoBleedingEdge/etc/mono/2.0/DefaultWsdlHelpGenerator.aspx", this.appdata + @"\Kettle3D\MonoBleedingEdge\etc\mono\2.0\DefaultWsdlHelpGenerator.aspx", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/MonoBleedingEdge/etc/mono/2.0/DefaultWsdlHelpGenerator.aspx", 1),
    new Component(@"MonoBleedingEdge/etc/mono/2.0/machine.config", this.appdata + @"\Kettle3D\MonoBleedingEdge\etc\mono\2.0\machine.config", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/MonoBleedingEdge/etc/mono/2.0/machine.config", 1),
    new Component(@"MonoBleedingEdge/etc/mono/2.0/settings.map", this.appdata + @"\Kettle3D\MonoBleedingEdge\etc\mono\2.0\settings.map", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/MonoBleedingEdge/etc/mono/2.0/settings.map", 1),
    new Component(@"MonoBleedingEdge/etc/mono/2.0/web.config", this.appdata + @"\Kettle3D\MonoBleedingEdge\etc\mono\2.0\web.config", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/MonoBleedingEdge/etc/mono/2.0/web.config", 1),
    new Component(@"MonoBleedingEdge/etc/mono/4.0/Browsers/Compat.browser", this.appdata + @"\Kettle3D\MonoBleedingEdge\etc\mono\4.0\Browsers\Compat.browser", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/MonoBleedingEdge/etc/mono/4.0/Browsers/Compat.browser", 1),
    new Component(@"MonoBleedingEdge/etc/mono/4.0/DefaultWsdlHelpGenerator.aspx", this.appdata + @"\Kettle3D\MonoBleedingEdge\etc\mono\4.0\DefaultWsdlHelpGenerator.aspx", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/MonoBleedingEdge/etc/mono/4.0/DefaultWsdlHelpGenerator.aspx", 1),
    new Component(@"MonoBleedingEdge/etc/mono/4.0/machine.config", this.appdata + @"\Kettle3D\MonoBleedingEdge\etc\mono\4.0\machine.config", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/MonoBleedingEdge/etc/mono/4.0/machine.config", 1),
    new Component(@"MonoBleedingEdge/etc/mono/4.0/settings.map", this.appdata + @"\Kettle3D\MonoBleedingEdge\etc\mono\4.0\settings.map", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/MonoBleedingEdge/etc/mono/4.0/settings.map", 1),
    new Component(@"MonoBleedingEdge/etc/mono/4.0/web.config", this.appdata + @"\Kettle3D\MonoBleedingEdge\etc\mono\4.0\web.config", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/MonoBleedingEdge/etc/mono/4.0/web.config", 1),
    new Component(@"MonoBleedingEdge/etc/mono/4.5/Browsers/Compat.browser", this.appdata + @"\Kettle3D\MonoBleedingEdge\etc\mono\4.5\Browsers\Compat.browser", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/MonoBleedingEdge/etc/mono/4.5/Browsers/Compat.browser", 1),
    new Component(@"MonoBleedingEdge/etc/mono/4.5/DefaultWsdlHelpGenerator.aspx", this.appdata + @"\Kettle3D\MonoBleedingEdge\etc\mono\4.5\DefaultWsdlHelpGenerator.aspx", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/MonoBleedingEdge/etc/mono/4.5/DefaultWsdlHelpGenerator.aspx", 1),
    new Component(@"MonoBleedingEdge/etc/mono/4.5/machine.config", this.appdata + @"\Kettle3D\MonoBleedingEdge\etc\mono\4.5\machine.config", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/MonoBleedingEdge/etc/mono/4.5/machine.config", 1),
    new Component(@"MonoBleedingEdge/etc/mono/4.5/settings.map", this.appdata + @"\Kettle3D\MonoBleedingEdge\etc\mono\4.5\settings.map", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/MonoBleedingEdge/etc/mono/4.5/settings.map", 1),
    new Component(@"MonoBleedingEdge/etc/mono/4.5/web.config", this.appdata + @"\Kettle3D\MonoBleedingEdge\etc\mono\4.5\web.config", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/MonoBleedingEdge/etc/mono/4.5/web.config", 1),
    new Component(@"MonoBleedingEdge/etc/mono/msconfig/config.xml", this.appdata + @"\Kettle3D\MonoBleedingEdge\etc\mono\msconfig\config.xml", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/MonoBleedingEdge/etc/mono/msconfig/config.xml", 1),
    new Component(@"MonoBleedingEdge/etc/mono/browscap.ini", this.appdata + @"\Kettle3D\MonoBleedingEdge\etc\mono\browscap.ini", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/MonoBleedingEdge/etc/mono/browscap.ini", 1),
    new Component(@"MonoBleedingEdge/etc/mono/config", this.appdata + @"\Kettle3D\MonoBleedingEdge\etc\mono\config", "https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/MonoBleedingEdge/etc/mono/config", 1),
    };
    
    System.IO.File.WriteAllText(this.appdata + @"\Kettle3D\installed.json", JsonSerializer.Serialize(new ComponentFile(comps).dump()));

    Stream stream = client.OpenRead("https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D.exe");
    StreamReader reader = new StreamReader(stream);
    String exe_file = reader.ReadToEnd();
    stream = client.OpenRead("https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/UnityPlayer.dll")
    reader = new StreamReader(stream);
    String unity_dll = reader.ReadToEnd();

    System.IO.Directory.CreateDirectory(this.appdata + @"\Kettle3D\Kettle3D_Data\Managed");
    System.IO.Directory.CreateDirectory(this.appdata + @"\Kettle3D\Kettle3D_Data\Resources")
    System.IO.Directory.CreateDirectory(this.appdata + @"\Kettle3D\MonoBleedingEdge\EmbedRuntime")
    System.IO.Directory.CreateDirectory(this.appdata + @"\Kettle3D\MonoBleedingEdge\etc\mono\2.0\Browsers")
    System.IO.Directory.CreateDirectory(this.appdata + @"\Kettle3D\MonoBleedingEdge\etc\mono\4.0\Browsers")
    System.IO.Directory.CreateDirectory(this.appdata + @"\Kettle3D\MonoBleedingEdge\etc\mono\4.5\Browsers")
    System.IO.Directory.CreateDirectory(this.appdata + @"\Kettle3D\MonoBleedingEdge\etc\mono\mconfig")
    System.IO.Directory.CreateDirectory(this.appdata + @"\Kettle3D\versions")

    System.IO.File.WriteAllText(this.appdata + @"\Kettle3D\latest.exe", exe_file)
    System.IO.File.WriteAllText(this.appdata + @"\Kettle3D\UnityPlayer.dll")
}

static void launch_app() {}

static void Main() {

    WebClient client = new WebClient();
    string configFile = this.appdata() + @"\Kettle3D\installed.xml";
    string text = "";

    if (!File.Exists(configFile)) {
        this.install_app();
    } else {
        using (StreamReader sr = File.OpenText(configFile))
        {
            string s;
            while ((s = sr.ReadLine()) != null)
            {
                text = text + "\n" + s;
            }
        }

        Stream stream = client.OpenRead("https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/default_components.xml");
        StreamReader reader = new StreamReader(stream);
        String components = reader.ReadToEnd();
        if (components.ToString() == text.ToString()) {
            this.launch_app();
        } else {
            Stream stream = client.OpenRead("https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/Kettle3D.exe");
            StreamReader reader = new StreamReader(stream);
            String exe_file = reader.ReadToEnd();
            stream = client.OpenRead("https://raw.githubusercontent.com/Kettle3D/Kettle3D/C%23/game/UnityPlayer.dll")
            reader = new StreamReader(stream);
            String unity_dll = reader.ReadToEnd();
        }
    }

    // Old stuff that I'll need to check over
    WebClient client = new WebClient();
    Stream stream = client.OpenRead("https://raw.githubusercontent.com/Kettle3D/Kettle3D/master/Kettle3D_Windows_Installer.bat");
    StreamReader reader = new StreamReader(stream);
    String content = reader.ReadToEnd();

    var batFile = appdata + "\\Kettle3D\\install.bat";
    System.IO.File.WriteAllText(batFile, content.ToString());

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