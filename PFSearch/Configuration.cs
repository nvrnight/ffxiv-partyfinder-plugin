using Dalamud.Configuration;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;

namespace PFSearch;

[Serializable]
public class Configuration : IPluginConfiguration
{
    public int Version { get; set; } = 0;

    [IgnoreDataMember]
    private Lock _encounterLock = new Lock();

    [IgnoreDataMember]
    public HashSet<string> EncounterIds { get; set; } = [];

    public string SearchExpression { get; set; } = "";
    public string ExcludeExpression { get; set; } = "";
    public bool M9S { get; set; } = true;
    public bool M10S { get; set; } = true;
    public bool M11S { get; set; } = true;
    public bool M12S { get; set; } = true;
    public bool TsukuyomiUnreal { get; set; } = true;
    public bool HellOnRailsExtreme { get; set; } = true;
    public bool Ucob { get; set; } = true;
    public bool Uwu { get; set; } = true;
    public bool Tea { get; set; } = true;
    public bool Dsr { get; set; } = true;
    public bool Top { get; set; } = true;
    public bool Fru { get; set; } = true;
    public bool Cod { get; set; } = true;
    public bool On { get; set; } = true;

    // the below exist just to make saving less cumbersome
    public void Save()
    {
        Plugin.PluginInterface.SavePluginConfig(this);
    }

    public void LoadEncounterIds()
    {
        lock(_encounterLock)
        {
            EncounterIds.Clear();
            if (M9S) EncounterIds.Add("1069");
            if (M10S) EncounterIds.Add("1071");
            if (M11S) EncounterIds.Add("1073");
            if (M12S) EncounterIds.Add("1075");
            if (TsukuyomiUnreal) EncounterIds.Add("1067");
            if (HellOnRailsExtreme) EncounterIds.Add("1077");
            if (Ucob) EncounterIds.Add("280");
            if (Uwu) EncounterIds.Add("539");
            if (Tea) EncounterIds.Add("694");
            if (Dsr) EncounterIds.Add("788");
            if (Top) EncounterIds.Add("908");
            if (Fru) EncounterIds.Add("1006");
            if (Cod) EncounterIds.Add("1010");
        }
    }
}
