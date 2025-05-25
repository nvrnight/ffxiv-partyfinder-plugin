
using Dalamud.Interface.Utility.Raii;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using System;
using System.Numerics;

namespace PartyFinderPlugin.Windows;

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

        using (var partyFinderPluginContentChild = ImRaii.Child("PartyFinderPlugin-Content", new Vector2(315, 0), true))
        {
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

                        var m5s = _configuration.M5S;
                        if (ImGui.Checkbox("AAC Cruiserweight M1 (Savage)", ref m5s))
                        {
                            _configuration.M5S = m5s;
                            _configuration.Save();
                            _configuration.LoadEncounterIds();
                        }

                        var m6s = _configuration.M6S;
                        if (ImGui.Checkbox("AAC Cruiserweight M2 (Savage)", ref m6s))
                        {
                            _configuration.M6S = m6s;
                            _configuration.Save();
                            _configuration.LoadEncounterIds();
                        }

                        var m7s = _configuration.M7S;
                        if (ImGui.Checkbox("AAC Cruiserweight M3 (Savage)", ref m7s))
                        {
                            _configuration.M7S = m7s;
                            _configuration.Save();
                            _configuration.LoadEncounterIds();
                        }

                        var m8s = _configuration.M8S;
                        if (ImGui.Checkbox("AAC Cruiserweight M4 (Savage)", ref m8s))
                        {
                            _configuration.M8S = m8s;
                            _configuration.Save();
                            _configuration.LoadEncounterIds();
                        }

                        var hellsKierUnreal = _configuration.HellsKierUnreal;
                        if (ImGui.Checkbox("Hells' Kier (Unreal)", ref hellsKierUnreal))
                        {
                            _configuration.HellsKierUnreal = hellsKierUnreal;
                            _configuration.Save();
                            _configuration.LoadEncounterIds();
                        }

                        var recollectionExtreme = _configuration.RecollectionExtreme;
                        if (ImGui.Checkbox("Recollection (Extreme)", ref recollectionExtreme))
                        {
                            _configuration.RecollectionExtreme = recollectionExtreme;
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
}
