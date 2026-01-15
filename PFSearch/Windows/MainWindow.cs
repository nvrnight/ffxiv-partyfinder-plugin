#pragma warning disable CA1416

using Dalamud.Interface.Utility.Raii;
using Dalamud.Interface.Windowing;

using System;
using System.Numerics;
using Dalamud.Bindings.ImGui;

namespace PFSearch.Windows;

public class MainWindow : Window, IDisposable
{
    private const string WindowId = "Party Finder Plugin##0a0281e6-3c3b-447d-b14f-f5c95f60463e";

    private readonly Configuration _configuration = null!;

    // We give this window a hidden ID using ##
    // So that the user will see "My Amazing Window" as window title,
    // but for ImGui the ID is "My Amazing Window##With a hidden ID"
    public MainWindow(Configuration configuration)
        : base(WindowId, ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse)
    {
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(330, 600),
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
        };

        _configuration = configuration;
    }

    public void Dispose() { }

    public override void Draw()
    {
        ImGui.Spacing();

        using var partyFinderPluginContentChild = ImRaii.Child("PartyFinderPlugin-Content", new Vector2(315, 0), true);
        if (partyFinderPluginContentChild.Success)
        {
            var on = _configuration.On;
            if (ImGui.Checkbox("On", ref on))
            {
                _configuration.On = on;
                _configuration.Save();
            }
                
            using (var searchChild = ImRaii.Child("PartyFinderPlugin-Search", new Vector2(300, 65), true))
            {
                if (searchChild.Success)
                {
                    if (ImGui.Button("X"))
                    {
                        _configuration.SearchExpression = "";
                        _configuration.Save();
                    }
                    ImGui.SameLine();
                    ImGui.SetNextItemWidth(125);
                    var searchExpression = _configuration.SearchExpression;
                    if (ImGui.InputText("Search Expression", ref searchExpression, 200))
                    {
                        _configuration.SearchExpression = searchExpression;
                        _configuration.Save();
                    }

                    if (ImGui.Button("Merc"))
                    {
                        _configuration.SearchExpression = @"\d *(m |mil)";
                        _configuration.Save();
                    }
                    ImGui.SameLine();
                    if (ImGui.Button("C41"))
                    {
                        _configuration.SearchExpression = @"c41";
                        _configuration.Save();
                    }
                    ImGui.SameLine();
                    if (ImGui.Button("C4X"))
                    {
                        _configuration.SearchExpression = @"c4\d";
                        _configuration.Save();
                    }
                }
            }
                
            using (var excludeChild = ImRaii.Child("PartyFinderPlugin-Exclude", new Vector2(300, 65), true))
            {
                if (excludeChild.Success)
                {
                    if (ImGui.Button("X"))
                    {
                        _configuration.ExcludeExpression = "";
                        _configuration.Save();
                    }
                    ImGui.SameLine();
                    ImGui.SetNextItemWidth(125);
                    var excludeExpression = _configuration.ExcludeExpression;
                    if (ImGui.InputText("Exclude Expression", ref excludeExpression, 200))
                    {
                        _configuration.ExcludeExpression = excludeExpression;
                        _configuration.Save();
                    }

                    if (ImGui.Button("Parse"))
                    {
                        _configuration.ExcludeExpression = "barse";
                        _configuration.Save();
                    }
                }
            }

            using (var raidsChild = ImRaii.Child("PartyFinderPlugin-Raids", new Vector2(300, 0), true))
            {
                if (raidsChild.Success)
                {
                    ImGui.Text("Raids");

                    
                    var m9s = _configuration.M9S;
                    if (ImGui.Checkbox("AAC Heavyweight M1 (Savage)", ref m9s))
                    {
                        _configuration.M9S = m9s;
                        _configuration.Save();
                        _configuration.LoadEncounterIds();
                    }

                    var m10s = _configuration.M10S;

                    if (ImGui.Checkbox("AAC Heavyweight M2 (Savage)", ref m10s))
                    {
                        _configuration.M10S = m10s;
                        _configuration.Save();
                        _configuration.LoadEncounterIds();
                    }

                    var m11s = _configuration.M11S;
                    if (ImGui.Checkbox("AAC Heavyweight M3 (Savage)", ref m11s))
                    {
                        _configuration.M11S = m11s;
                        _configuration.Save();
                        _configuration.LoadEncounterIds();
                    }

                    var m12s = _configuration.M12S;
                    if (ImGui.Checkbox("AAC Heavyweight M4 (Savage)", ref m12s))
                    {
                        _configuration.M12S = m12s;
                        _configuration.Save();
                        _configuration.LoadEncounterIds();
                    }

                    var tsukuyomiUnreal = _configuration.TsukuyomiUnreal;
                    if (ImGui.Checkbox("Tsukuyomi's Pain (Unreal)", ref tsukuyomiUnreal))
                    {
                        _configuration.TsukuyomiUnreal = tsukuyomiUnreal;
                        _configuration.Save();
                        _configuration.LoadEncounterIds();
                    }

                    var hellOnRailsExtreme = _configuration.HellOnRailsExtreme;
                    if (ImGui.Checkbox("Hell on Rails (Extreme)", ref hellOnRailsExtreme))
                    {
                        _configuration.HellOnRailsExtreme = hellOnRailsExtreme;
                        _configuration.Save();
                        _configuration.LoadEncounterIds();
                    }

                    var ucob = _configuration.Ucob;
                    if (ImGui.Checkbox("The Unending Coil of Bahamut (Ultimate)", ref ucob))
                    {
                        _configuration.Ucob = ucob;
                        _configuration.Save();
                        _configuration.LoadEncounterIds();
                    }

                    var uwu = _configuration.Uwu;
                    if (ImGui.Checkbox("The Weapon's Refrain (Ultimate)", ref uwu))
                    {
                        _configuration.Uwu = uwu;
                        _configuration.Save();
                        _configuration.LoadEncounterIds();
                    }

                    var tea = _configuration.Tea;
                    if (ImGui.Checkbox("The Epic of Alexander (Ultimate)", ref tea))
                    {
                        _configuration.Tea = tea;
                        _configuration.Save();
                        _configuration.LoadEncounterIds();
                    }

                    var dsr = _configuration.Dsr;
                    if (ImGui.Checkbox("Dragonsong's Reprise (Ultimate)", ref dsr))
                    {
                        _configuration.Dsr = dsr;
                        _configuration.Save();
                        _configuration.LoadEncounterIds();
                    }

                    var top = _configuration.Top;
                    if (ImGui.Checkbox("The Omega Protocol (Ultimate)", ref top))
                    {
                        _configuration.Top = top;
                        _configuration.Save();
                        _configuration.LoadEncounterIds();
                    }

                    var fru = _configuration.Fru;
                    if (ImGui.Checkbox("Futures Rewritten (Ultimate)", ref fru))
                    {
                        _configuration.Fru = fru;
                        _configuration.Save();
                        _configuration.LoadEncounterIds();
                    }

                    var cod = _configuration.Cod;
                    if (ImGui.Checkbox("The Cloud of Darkness (Chaotic)", ref cod))
                    {
                        _configuration.Cod = cod;
                        _configuration.Save();
                        _configuration.LoadEncounterIds();
                    }
                }
            }
        }
    }
}
