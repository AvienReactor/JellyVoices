using System.Reflection;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Models.Spt.Mod;
using Range = SemanticVersioning.Range;

namespace JellyVoicesWawa;

public record ModMetadata : AbstractModMetadata
{
    public override string ModGuid { get; init; } = "com.jejukon.jerma";
    public override string Name { get; init; } = "Jelly Voices";
    public override string Author { get; init; } = "Jejukon";
    public override SemanticVersioning.Version Version { get; init; } = new("1.0.0");
    public override Range SptVersion { get; init; } = new("~4.0.1");
    public override string License { get; init; } = "MIT";
    public override bool? IsBundleMod { get; init; } = true;
    public override Dictionary<string, Range>? ModDependencies { get; init; } = new()
    {
        { "com.wtt.commonlib", new Range("~2.0.0") }
    };
    public override string? Url { get; init; }
    public override List<string>? Contributors { get; init; }
    public override List<string>? Incompatibilities { get; init; }
}

[Injectable(TypePriority = OnLoadOrder.PostDBModLoader + 2)]
public class JellyVoices(
    WTTServerCommonLib.WTTServerCommonLib wttCommon
) : IOnLoad
{
    internal static readonly Dictionary<string, string> CharacterAudioMap = new()
    {
        {"Jerma", "jerma"}
    };

    //internal static readonly List<string> AudioBundleKeys = new()
    //    {
    //        "audio/jelly.bundle"
    //    };

    public async Task OnLoad()
    {

        // Get your current assembly
        var assembly = Assembly.GetExecutingAssembly();


        // Use WTT-CommonLib services
        //await wttCommon.CustomItemServiceExtended.CreateCustomItems(assembly);
        //await wttCommon.CustomLocaleService.CreateCustomLocales(assembly);

        await wttCommon.CustomVoiceService.CreateCustomVoices(assembly);
        //wttCommon.CustomAudioService.RegisterAudioBundles(AudioBundleKeys);
        foreach (var kvp in CharacterAudioMap)
        {
            wttCommon.CustomAudioService.CreateFaceCardAudio(kvp.Key, kvp.Value);
        }
        await Task.CompletedTask;
    }
}